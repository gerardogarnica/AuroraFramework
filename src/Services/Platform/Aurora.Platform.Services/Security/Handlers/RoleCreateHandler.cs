using Aurora.Platform.Domain.Exceptions;
using Aurora.Platform.Domain.Security.Models;
using Aurora.Platform.Domain.Security.Repositories;
using Aurora.Platform.Services.Security.Commands;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Aurora.Platform.Services.Security.Handlers
{
    public class RoleCreateHandler : IRequestHandler<RoleCreateCommand, RoleResponse>
    {
        #region Miembros privados de la clase

        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructores de la clase

        public RoleCreateHandler(
            IRoleRepository roleRepository,
            IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        #endregion

        #region Implementación de la interface IRequestHandler

        async Task<RoleResponse> IRequestHandler<RoleCreateCommand, RoleResponse>.Handle(
            RoleCreateCommand request, CancellationToken cancellationToken)
        {
            // Se verifica si el nombre del rol de usuario ya se encuentra registrado en el repositorio indicado
            await VerifyIfRoleDataExists(request.RepositoryId, request.Name);

            // Se crea el registro de rol de usuario
            var entry = CreateRoleData(request);
            entry = await _roleRepository.InsertAsync(entry);

            return new RoleResponse(entry);
        }

        #endregion

        #region Métodos privados de la clase

        private RoleData CreateRoleData(RoleCreateCommand request)
        {
            return _mapper.Map<RoleData>(request);
        }

        private async Task VerifyIfRoleDataExists(int repositoryId, string name)
        {
            var roleData = await _roleRepository.GetAsync(
                x => x.RepositoryId.Equals(repositoryId) && x.Name.Equals(name));

            if (roleData != null)
            {
                throw new ExistsRoleNameException(name);
            }
        }

        #endregion
    }
}