using System.Linq.Expressions;
using System.Reflection;
using Castle.DynamicProxy.Internal;

namespace OLT.Core
{
    public class OltRemoveCastsVisitor : ExpressionVisitor
    {
        private static readonly ExpressionVisitor Default = new OltRemoveCastsVisitor();

        private OltRemoveCastsVisitor()
        {
        }

        public new static Expression Visit(Expression node)
        {
            return Default.Visit(node);
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            if (node.NodeType == ExpressionType.Convert && node.Type.IsAssignableFrom(node.Operand.Type))
            {
                return base.Visit(node.Operand);
            }
            return base.VisitUnary(node);
        }
    }
}