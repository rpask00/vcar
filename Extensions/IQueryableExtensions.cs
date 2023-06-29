using System.Collections.Generic;
using System.Linq;
using System;
using System.Linq.Expressions;
using vcar.Core.Models;

namespace vcar.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryObj QueryObj, Dictionary<string, Expression<Func<T, object>>> SortingDict)
        {
            if (!SortingDict.ContainsKey(QueryObj.sortBy))
                return query;

            if (QueryObj.sortAsc)
                return query.OrderBy(SortingDict[QueryObj.sortBy]);

            return query.OrderByDescending(SortingDict[QueryObj.sortBy]);
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, IQueryObj QueryObj)
        {
            if (QueryObj.Page <= 0)
                QueryObj.Page = 1;

            if (QueryObj.PageSize <= 0)
                return query;

            return query.Skip((QueryObj.Page - 1) * QueryObj.PageSize).Take(QueryObj.PageSize);
        }

        public static IQueryable<Car> ApplyFiltering(this IQueryable<Car> query, CarQuery carQuery)
        {
            if (carQuery.MakeId.HasValue)
                query = query.Where(c => c.Model.MakeId == carQuery.MakeId);

            if (carQuery.ModelId.HasValue)
                query = query.Where(c => c.Model.Id == carQuery.ModelId);

            if (carQuery.yearmax.HasValue)
                query = query.Where(c => c.year <= carQuery.yearmax);

            if (carQuery.yearmin.HasValue)
                query = query.Where(c => c.year >= carQuery.yearmin);

            if (carQuery.PriceMax.HasValue)
                query = query.Where(c => c.Price <= carQuery.PriceMax);

            if (carQuery.PriceMin.HasValue)
                query = query.Where(c => c.Price >= carQuery.PriceMin);

            if (!string.IsNullOrEmpty(carQuery.Owner))
                query = query.Where(c => c.Email == carQuery.Owner);

            return query;
        }

    }
}
