using System;
using System.Linq;
using System.Linq.Expressions;

namespace TLabs.DotnetHelpers
{
    public static class PredicateBuilder
    {
        /// <summary>
        /// Start new expression
        /// </summary>
        /// <param name="initialValue">false - for OR-chains, true - for AND-chains</param>
        public static Expression<Func<T, bool>> Init<T>(bool initialValue)
        {
            return f => initialValue;
        }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                          Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
              (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                           Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
              (Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }
    }
}
