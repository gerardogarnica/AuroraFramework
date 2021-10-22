using System;
using System.Linq.Expressions;

namespace Aurora.Framework.Logic
{
    /// <summary>
    /// Clase para el manejo de expresiones binarias de consultas en repositorios de datos.
    /// </summary>
    public static class BinaryExpressions
    {
        /// <summary>
        /// Crea una expresión binaria para agregar un filtro de tipo AND en una expresión existente.
        /// </summary>
        /// <typeparam name="T">Tipo de parámetro del método que encapsula el delegado.</typeparam>
        /// <param name="filter">Filtro de expresión al cual se va a agregar la nueva expresión.</param>
        /// <param name="expressionToAdd">Filtro de expresión a añadir de tipo AND.</param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> filter, Expression<Func<T, bool>> expressionToAdd)
        {
            var newFilter = new ReplaceVisitor(filter.Parameters[0], expressionToAdd.Parameters[0])
                .Visit(filter.Body);

            return Expression.Lambda<Func<T, bool>>(Expression.And(newFilter, expressionToAdd.Body), expressionToAdd.Parameters);
        }
    }
}