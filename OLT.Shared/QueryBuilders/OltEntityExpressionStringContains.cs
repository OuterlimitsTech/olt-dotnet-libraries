using System;
using System.Linq.Expressions;

namespace OLT.Core
{
    public class OltEntityExpressionStringContains<TEntity> : OltEntityExpression<TEntity, string>
        where TEntity : class, IOltEntity
    {

        public OltEntityExpressionStringContains(Expression<Func<TEntity, string>> fieldExpression)
        {
            FieldExpression = fieldExpression;
        }


        public override Expression<Func<TEntity, string>> FieldExpression { get; }

        public override Expression<Func<string, bool>> WhereExpression
        {
            get
            {
                return (value) => value.Contains(Value);
            }
        }

    }
}