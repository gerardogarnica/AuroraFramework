using Aurora.Common.Domain.Catalogs.Models;
using Aurora.Common.Domain.Catalogs.Repositories;
using Aurora.Common.Domain.Exceptions;
using Aurora.Common.Services.Catalogs.Commands;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Aurora.Common.Services.Catalogs.Handlers
{
    public class CatalogUpdateHandler : IRequestHandler<CatalogUpdateCommand, CatalogResponse>
    {
        #region Miembros privados de la clase

        private readonly ICatalogRepository _catalogRepository;

        #endregion

        #region Constructores de la clase

        public CatalogUpdateHandler(
            ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }

        #endregion

        #region Implementación de la interface IRequestHandler

        async Task<CatalogResponse> IRequestHandler<CatalogUpdateCommand, CatalogResponse>.Handle(
            CatalogUpdateCommand request, CancellationToken cancellationToken)
        {
            // Se obtiene el catálogo existente
            var entry = await GetExistentCatalogData(request.Code.Trim());

            // Se actualiza el registro de catálogo
            UpdateCatalogData(entry, request);
            entry = await _catalogRepository.UpdateAsync(entry);

            return new CatalogResponse(entry);
        }

        #endregion

        #region Métodos privados de la clase

        private void UpdateCatalogData(CatalogData catalog, CatalogUpdateCommand request)
        {
            catalog.Name = request.Name.Trim();
            catalog.Description = request.Description.Trim();
            catalog.IsVisible = request.IsVisible;
            catalog.IsEditable = request.IsEditable;
        }

        private async Task<CatalogData> GetExistentCatalogData(string code)
        {
            var catalogData = await _catalogRepository.GetByCodeAsync(code);

            if (catalogData == null)
            {
                throw new InvalidCatalogCodeException(code);
            }

            if (!catalogData.IsEditable)
            {
                throw new CatalogNotEditableException(code);
            }

            return catalogData;
        }

        #endregion
    }
}