using App.Data.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace App.Data.Models
{
    public static class BaseExtention
    {
        public static Func<T, object> GetExpression<T>(this string orderByColumn)
        {
            Func<T, object> orderByExpr = null;
            if (!String.IsNullOrEmpty(orderByColumn))
            {
                Type type = typeof(T);

                if (type.GetProperties().Any(prop => prop.Name.Equals(orderByColumn)))
                {
                    PropertyInfo pinfo = type.GetProperty(orderByColumn);
                    orderByExpr = (x => pinfo.GetValue(x, null));
                }
            }

            return orderByExpr;
        }

        public static IQueryable<T> SortBy<T>(this IQueryable<T> query, Func<T, object> sortExpression, OrderBy sortOrder)
        {
            return sortOrder == OrderBy.ASC ? query.OrderBy(sortExpression) as IQueryable<T>
                                      : query.OrderByDescending(sortExpression) as IQueryable<T>;
        }

        public static void ThowValidationException(string message)
        {
            throw new ValidationException(message);
        }
    }
}
