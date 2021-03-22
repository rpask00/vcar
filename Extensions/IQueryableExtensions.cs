using vcar.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Linq.Expressions;


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


    }
}