using Aurora.Common.Domain.Catalogs.Models;
using Aurora.Common.Domain.Catalogs.Repositories;
using Aurora.Common.Domain.Exceptions;
using Aurora.Common.Services.Catalogs.Commands;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Aurora.Common.Services.Catalogs.Handlers
{
    public class CatalogCreateHandler : IRequestHandler<CatalogCreateCommand, CatalogResponse>
    {
        #region Miembros privados de la clase

        private readonly ICatalogRepository _catalogRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructores de la clase

        public CatalogCreateHandler(
            ICatalogRepository catalogRepository,
            IMapper mapper)
        {
            _catalogRepository = catalogRepository;
            _mapper = mapper;
        }

        #endregion

        #region Implementación de la interface IRequestHandler

        async Task<CatalogResponse> IRequestHandler<CatalogCreateCommand, CatalogResponse>.Handle(
            CatalogCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Se verifica si el catálogo ya se encuentra registrado
                await VerifyIfCatalogDataExists(request.Code.Trim());

                var entry = CreateCatalogData(request);
                entry = await _catalogRepository.InsertAsync(entry);

                return new CatalogResponse(entry);
            }
            catch (Framework.Exceptions.BusinessException e)
            {
                return new CatalogResponse(e.ErrorKeyName, e.Message);
            }
        }

        #endregion

        #region Métodos privados de la clase

        private CatalogData CreateCatalogData(CatalogCreateCommand request)
        {
            return _mapper.Map<CatalogData>(request);
        }

        private async Task VerifyIfCatalogDataExists(string code)
        {
            var catalogData = await _catalogRepository.GetByCodeAsync(code);

            if (catalogData != null)
            {
                throw new ExistsCatalogCodeException(code);
            }
        }

        #endregion
    }
}