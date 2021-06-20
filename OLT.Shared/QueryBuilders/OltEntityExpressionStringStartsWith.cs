using System;
using System.Linq.Expressions;

namespace OLT.Core
{
    public class OltEntityExpressionStringStartsWith<TEntity> : OltEntityExpression<TEntity, string>
        where TEntity : class, IOltEntity
    {

        public OltEntityExpressionStringStartsWith(Expression<Func<TEntity, string>> fieldExpression)
        {
            FieldExpression = fieldExpression;
        }


        public override Expression<Func<TEntity, string>> FieldExpression { get; }

        public override Expression<Func<string, bool>> WhereExpression
        {
            get
            {
                return (value) => value.StartsWith(Value);
            }
        }

    }
}