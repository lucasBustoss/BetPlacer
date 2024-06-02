using BetPlacer.Backtest.API.Models.Enums;
using BetPlacer.Core.Models.Response.MicroserviceAPI.Fixtures;
using System.Linq.Expressions;

namespace BetPlacer.Backtest.API.Utils
{
    public static class FilterPredicateBuilding
    {
        public static Func<FixturesApiResponseModel, bool> BuildPredicate(string propertyPath, double initialValue, double finalValue, FilterCompareType compareType)
        {
            var parameter = Expression.Parameter(typeof(FixturesApiResponseModel), "n");

            Expression propertyExpression = parameter;
            foreach (var propertyName in propertyPath.Split('.'))
            {
                propertyExpression = Expression.Property(propertyExpression, propertyName);
            }

            var initialValueExpression = Expression.Constant(initialValue);
            var finalValueExpression = Expression.Constant(finalValue);

            Expression predicateBody;
            if (compareType == FilterCompareType.Greater)
            {
                var greaterThanInitial = Expression.GreaterThan(propertyExpression, initialValueExpression);
                var lessThanFinal = Expression.LessThan(propertyExpression, finalValueExpression);
                predicateBody = Expression.AndAlso(greaterThanInitial, lessThanFinal);
            }
            else
            {
                var greaterThanOrEqualInitial = Expression.GreaterThanOrEqual(propertyExpression, initialValueExpression);
                var lessThanOrEqualFinal = Expression.LessThanOrEqual(propertyExpression, finalValueExpression);
                predicateBody = Expression.AndAlso(greaterThanOrEqualInitial, lessThanOrEqualFinal);
            }

            return Expression.Lambda<Func<FixturesApiResponseModel, bool>>(predicateBody, parameter).Compile();
        }
    }
}
