using Aurora.Common.Domain.Exceptions;
using Aurora.Common.Domain.Settings;
using Aurora.Common.Domain.Settings.Models;
using Aurora.Common.Domain.Settings.Repositories;
using Aurora.Common.Services.Settings.Commands;
using Aurora.Framework.Logic.Data;
using Aurora.Framework.Sessions;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Aurora.Common.Services.Settings.Handlers
{
    public class ValueSaveHandler : IRequestHandler<ValueSaveCommand, ValueResponse>
    {
        #region Miembros privados de la clase

        private readonly IAttributeSettingRepository _settingRepository;
        private readonly IAttributeValueRepository _valueRepository;
        private readonly IMapper _mapper;
        private readonly IAuroraSession _auroraSession;
        private readonly string _userName;

        #endregion

        #region Constructores de la clase

        public ValueSaveHandler(
            IAttributeSettingRepository settingRepository,
            IAttributeValueRepository valueRepository,
            IMapper mapper,
            IAuroraSession auroraSession)
        {
            _settingRepository = settingRepository;
            _valueRepository = valueRepository;
            _mapper = mapper;
            _auroraSession = auroraSession;

            _userName = _auroraSession.GetSessionInfo().UserLoginName;
        }

        #endregion

        #region Implementación de la interface IRequestHandler

        async Task<ValueResponse> IRequestHandler<ValueSaveCommand, ValueResponse>.Handle(
            ValueSaveCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Se obtiene la configuración de atributo existente
                var setting = await GetExistentAttributeSetting(request.Code);

                var entry = await GetExistentAttributeValueData(setting.Code, request.RelationshipId);

                if (entry == null)
                {
                    entry = CreateValueData(setting, request);
                    entry = await _valueRepository.InsertAsync(entry);
                }
                else
                {
                    UpdateValueData(entry, setting, request);
                    entry = await _valueRepository.UpdateAsync(entry);
                }

                return new ValueResponse(entry);
            }
            catch (Framework.Exceptions.BusinessException e)
            {
                return new ValueResponse(e.ErrorKeyName, e.Message);
            }
        }

        #endregion

        #region Métodos privados de la clase

        private AttributeValueData CreateValueData(AttributeSetting setting, ValueSaveCommand request)
        {
            var valueData = new AttributeValueData()
            {
                AttributeId = setting.AttributeId,
                RelationshipId = request.RelationshipId,
                Value = request.GetConfigurationSetting(setting)
            };

            valueData.AddCreated(_userName);
            valueData.AddLastUpdated(_userName);

            return valueData;
        }

        private void UpdateValueData(AttributeValueData entry, AttributeSetting setting, ValueSaveCommand request)
        {
            entry.Value = request.GetConfigurationSetting(setting);
            entry.AddLastUpdated(_userName);
        }

        private async Task<AttributeSetting> GetExistentAttributeSetting(string code)
        {
            var settingData = await _settingRepository.GetAsync(x => x.Code.Equals(code));

            if (settingData == null) throw new InvalidSettingCodeException(code);

            return _mapper.Map<AttributeSetting>(settingData);
        }

        private async Task<AttributeValueData> GetExistentAttributeValueData(string code, int relationshipId)
        {
            return await _valueRepository.GetAsync(code, relationshipId);
        }

        #endregion
    }
}