using System;
using System.Linq;
using System.Linq.Expressions;

namespace OLT.Core
{

    /// <summary>
    /// Used with the BuildExpression() extenstion
    /// example: searcherExpressionObject.BuildExpression(); or queryable.Where(searcherExpressionObject.BuildExpression());
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TValueType"></typeparam>
    public interface IOltEntityExpression<TEntity, TValueType> : IOltEntityQueryBuilder<TEntity, TValueType>
        where TEntity : class, IOltEntity
    {
        Expression<Func<TValueType, bool>> WhereExpression { get; }
        Expression<Func<TEntity, TValueType>> FieldExpression { get; }
    }

}