using System;
using System.Linq.Expressions;

namespace OLT.Core
{
    public class OltEntityExpressionInt<TEntity> : OltEntityExpression<TEntity, int>
        where TEntity : class, IOltEntity
    {

        public OltEntityExpressionInt(Expression<Func<TEntity, int>> fieldExpression)
        {
            FieldExpression = fieldExpression;
        }


        public override Expression<Func<TEntity, int>> FieldExpression { get; }

        public override Expression<Func<int, bool>> WhereExpression
        {
            get
            {
                return (value) => Value == value;
            }
        }


    }
}