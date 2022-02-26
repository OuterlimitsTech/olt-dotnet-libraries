using System;
using System.Linq;
using System.Linq.Expressions;

namespace OLT.Core
{
    public abstract class OltEntityExpression<TEntity, TValueType> : OltEntityExpressionBuilder<TEntity, TValueType>, IOltEntityExpression<TEntity, TValueType>
        where TEntity : class, IOltEntity
    {
        public abstract Expression<Func<TValueType, bool>> WhereExpression { get; }
        public abstract Expression<Func<TEntity, TValueType>> FieldExpression { get; }

        /// <summary>
        /// queryable.Where(this.BuildExpression());
        /// </summary>
        /// <param name="queryable"></param>
        /// <returns></returns>
        public override IQueryable<TEntity> BuildQueryable(IQueryable<TEntity> queryable)
        {
            return queryable.Where(this.BuildExpression());
        }
    }
}
