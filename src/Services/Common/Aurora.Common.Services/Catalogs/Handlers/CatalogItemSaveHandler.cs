using Aurora.Common.Domain.Catalogs.Models;
using Aurora.Common.Domain.Catalogs.Repositories;
using Aurora.Common.Domain.Exceptions;
using Aurora.Common.Services.Catalogs.Commands;
using Aurora.Framework.Logic.Data;
using Aurora.Framework.Sessions;
using AutoMapper;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aurora.Common.Services.Catalogs.Handlers
{
    public class CatalogItemSaveHandler : IRequestHandler<CatalogItemSaveCommand, CatalogResponse>
    {
        #region Miembros privados de la clase

        private readonly ICatalogRepository _catalogRepository;
        private readonly IMapper _mapper;
        private readonly IAuroraSession _auroraSession;
        private readonly string _userName;

        #endregion

        #region Constructores de la clase

        public CatalogItemSaveHandler(
            ICatalogRepository catalogRepository,
            IMapper mapper,
            IAuroraSession auroraSession)
        {
            _catalogRepository = catalogRepository;
            _mapper = mapper;
            _auroraSession = auroraSession;

            _userName = _auroraSession.GetSessionInfo().UserLoginName;
        }

        #endregion

        #region Implementación de la interface IRequestHandler

        async Task<CatalogResponse> IRequestHandler<CatalogItemSaveCommand, CatalogResponse>.Handle(
            CatalogItemSaveCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Se obtiene el catálogo existente
                var entry = await GetExistentCatalogData(request.CatalogCode.Trim());

                // Se almacena el registro de item de catálogo
                SaveCatalogItemData(entry, request);
                entry = await _catalogRepository.UpdateAsync(entry);

                return new CatalogResponse(entry);
            }
            catch (Framework.Exceptions.BusinessException e)
            {
                return new CatalogResponse(e.ErrorKeyName, e.Message);
            }
        }

        #endregion

        #region Métodos privados de la clase

        private void SaveCatalogItemData(CatalogData catalogData, CatalogItemSaveCommand request)
        {
            if (ExistsCatalogItemData(catalogData, request.ItemCode))
            {
                UpdateCatalogItemData(catalogData, request);
            }
            else
            {
                CreateCatalogItemData(catalogData, request);
            }
        }

        private void CreateCatalogItemData(CatalogData catalogData, CatalogItemSaveCommand request)
        {
            catalogData.Items.Add(_mapper.Map<CatalogItemData>(request));
        }

        private void UpdateCatalogItemData(CatalogData catalogData, CatalogItemSaveCommand request)
        {
            var itemData = catalogData
                .Items
                .FirstOrDefault(x => x.CatalogId.Equals(catalogData.CatalogId) && x.Code.Equals(request.ItemCode));

            if (!itemData.IsEditable)
            {
                throw new CatalogItemNotEditableException(catalogData.Code, request.ItemCode);
            }

            itemData.Description = request.Description.Trim();
            itemData.IsEditable = request.IsEditable;
            itemData.IsActive = request.IsActive;
            itemData.AddLastUpdated(_userName);
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

        private bool ExistsCatalogItemData(CatalogData catalogData, string itemCode)
        {
            return catalogData
                .Items
                .ToList()
                .Exists(x => x.CatalogId.Equals(catalogData.CatalogId) && x.Code.Equals(itemCode));
        }

        #endregion
    }
}