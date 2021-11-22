using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aurora.Framework.Logic.Behaviors
{
    /// <summary>
    /// Clase que implementa el comportamiento de validaciones de modelos de datos de entradas de requerimientos.
    /// </summary>
    /// <typeparam name="TRequest">Tipo de objeto de requerimiento.</typeparam>
    /// <typeparam name="TResponse">Tipo de objeto de respuesta.</typeparam>
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        #region Miembros privados de la clase

        private readonly IEnumerable<IValidator<TRequest>> _validators;

        #endregion

        #region Constructores de la clase

        /// <summary>
        /// Inicializa una nueva instancia de la clase ValidationBehavior.
        /// </summary>
        /// <param name="validators">Define un validador para un objeto específico.</param>
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        #endregion

        #region Implementación de la interface IPipelineBehavior

        async Task<TResponse> IPipelineBehavior<TRequest, TResponse>.Handle(
            TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var failures = _validators
                    .Select(v => v.Validate(request))
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .Select(f => f.ErrorMessage)
                    .ToList();

                if (failures.Any())
                {
                    throw new Exceptions.ValidationException(failures);
                }
            }

            return await next();
        }

        #endregion
    }
}