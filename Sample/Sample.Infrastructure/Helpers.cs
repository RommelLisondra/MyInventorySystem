using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

namespace Sample.Infrastructure
{
    public class Helpers
    {
        // ConvertPredicate returns Expression<Func<TTarget,bool>> now
        public static Expression<Func<TTarget, bool>> ConvertPredicate<TSource, TTarget>(
            Expression<Func<TSource, bool>> root)
        {
            var visitor = new ParameterTypeVisitor<TSource, TTarget>();
            var expression = (Expression<Func<TTarget, bool>>)visitor.Visit(root);
            return expression;
        }
    }

    public class ParameterTypeVisitor<TSource, TTarget> : ExpressionVisitor
    {
        private ReadOnlyCollection<ParameterExpression>? _parameters; // nullable

        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (_parameters != null)
            {
                // return first matching parameter or node itself if not found
                return _parameters.FirstOrDefault(p => p.Name == node.Name) ?? node;
            }
            else
            {
                return node.Type == typeof(TSource)
                    ? Expression.Parameter(typeof(TTarget), node.Name)
                    : node;
            }
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            _parameters = VisitAndConvert<ParameterExpression>(node.Parameters, "VisitLambda");
            var body = Visit(node.Body)!; // null-forgiving because Visit can return null
            return Expression.Lambda(body, _parameters);
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Member.DeclaringType == typeof(TSource))
            {
                var expr = Visit(node.Expression)!; // null-forgiving
                return Expression.Property(expr, node.Member.Name);
            }
            return base.VisitMember(node);
        }
    }
}
