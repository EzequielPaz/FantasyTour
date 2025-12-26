using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FT.Infrastructure.Helpers
{
    public static class OrderByExtensions
    {
        public static IQueryable<T> OrderByDynamic<T>(
        this IQueryable<T> source,
        string orderByProperty,
        bool ascending)
        {
            var entityType = typeof(T);
            var property = entityType.GetProperty(orderByProperty);

            if (property == null)
                throw new Exception($"La propiedad '{orderByProperty}' no existe en {entityType.Name}");

            var parameter = Expression.Parameter(entityType, "x");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);

            string methodName = ascending ? "OrderBy" : "OrderByDescending";

            var resultExpression = Expression.Call(
                typeof(Queryable),
                methodName,
                new Type[] { entityType, property.PropertyType },
                source.Expression,
                Expression.Quote(orderByExpression)
            );

            return source.Provider.CreateQuery<T>(resultExpression);
        }
    }
}
