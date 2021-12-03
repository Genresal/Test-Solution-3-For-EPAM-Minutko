using EMR.DataTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace EMR.Helpers
{
    public static class LinqHelpers
    {
        /// <summary>
        /// Sort input data list.
        /// </summary>
        /// <typeparam name="T">Data model.</typeparam>
        /// <param name="data">Input data.</param>
        /// <param name="orderByMember">Data member for sorting.</param>
        /// <param name="order">Ascending or descending order.</param>
        /// <returns>Ordered data.</returns>
        public static IEnumerable<T> OrderByDynamic<T>(this IEnumerable<T> data, string orderByMember, TableOrder order)
        {
            Type objType = typeof(T);
            PropertyInfo prop;
            if (orderByMember == null)
            {
                prop = new List<PropertyInfo>(objType.GetProperties())[0];
            }
            else
            {
                string normalizedMember = char.ToUpper(orderByMember[0]) + orderByMember.Substring(1);
                prop = objType.GetProperty(normalizedMember);
            }

            if (order == TableOrder.Asc)
            {
                return data.OrderBy(x => prop.GetValue(x, null));
            }
            else
            {
                return data.OrderByDescending(x => prop.GetValue(x, null));
            }
        }
    }
}
