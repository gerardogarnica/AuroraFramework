using Aurora.Common.Domain.Catalogs.Repositories;
using Aurora.Common.Domain.Exceptions;
using Aurora.Common.Domain.Settings.Models;
using Aurora.Common.Domain.Settings.Repositories;
using Aurora.Common.Services.Settings.Commands;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aurora.Common.Services.Settings.Handlers
{
    public class SettingCreateHandler : IRequestHandler<SettingCreateCommand, SettingResponse>
    {
        #region Miembros privados de la clase

        private readonly IAttributeSettingRepository _settingRepository;
        private readonly ICatalogRepository _catalogRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructores de la clase

        public SettingCreateHandler(
            IAttributeSettingRepository settingRepository,
            ICatalogRepository catalogRepository,
            IMapper mapper)
        {
            _settingRepository = settingRepository;
            _catalogRepository = catalogRepository;
            _mapper = mapper;
        }

        #endregion

        #region Implementación de la interface IRequestHandler

        async Task<SettingResponse> IRequestHandler<SettingCreateCommand, SettingResponse>.Handle(
            SettingCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Se verifica si la configuración de atributo ya se encuentra registrado
                await VerifyIfSettingDataExists(request.Code.Trim());

                // Se verifica si el tipo de ámbito es válido
                await VerifyIfScopeExists(request.ScopeType.Trim());

                var entry = CreateSettingData(request);
                entry = await _settingRepository.InsertAsync(entry);

                return new SettingResponse(entry);
            }
            catch (Framework.Exceptions.BusinessException e)
            {
                return new SettingResponse(e.ErrorKeyName, e.Message);
            }
        }

        #endregion

        #region Métodos privados de la clase

        private AttributeSettingData CreateSettingData(SettingCreateCommand request)
        {
            return _mapper.Map<AttributeSettingData>(request);
        }

        private async Task VerifyIfSettingDataExists(string code)
        {
            var settingData = await _settingRepository.GetAsync(x => x.Code.Equals(code));

            if (settingData != null)
            {
                throw new ExistsSettingCodeException(code);
            }
        }

        private async Task VerifyIfScopeExists(string scopeType)
        {
            var code = "TipoAtributo";

            var scope = await _catalogRepository.GetByCodeAsync(code);

            if (scope == null)
            {
                throw new InvalidCatalogCodeException(code);
            }

            if (!scope.Items.ToList().Any(x => x.Code.Equals(scopeType)))
            {
                throw new InvalidCatalogItemCodeException(code, scopeType);
            }
        }

        private async void VerifyIfCatalogExists(string code, IList<string> items)
        {
            var catalog = await _catalogRepository.GetByCodeAsync(code);

            if (catalog == null) throw new InvalidCatalogCodeException(code);

            if (!catalog.IsVisible) throw new InvalidCatalogCodeException(code);

            if (!catalog.Items.Any(x => x.Code.Equals(items))) throw new InvalidSettingCatalogValue(code);
        }

        #endregion
    }
}