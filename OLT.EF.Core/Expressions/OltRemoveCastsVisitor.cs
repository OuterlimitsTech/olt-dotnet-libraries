using System.Linq.Expressions;

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