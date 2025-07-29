﻿using System.Linq.Expressions;

namespace Shared.Domain.Specifications
{
    public class AndSpecification<T> : Specification<T>
    {
        private readonly Specification<T> _left;
        private readonly Specification<T> _right;

        public AndSpecification(Specification<T> left, Specification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpr = _left.ToExpression();
            var rightExpr = _right.ToExpression();

            var parameter = Expression.Parameter(typeof(T));
            var body = Expression.AndAlso(
                Expression.Invoke(leftExpr, parameter),
                Expression.Invoke(rightExpr, parameter));

            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}
