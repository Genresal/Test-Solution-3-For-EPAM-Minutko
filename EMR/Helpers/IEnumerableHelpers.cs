using EMR.DataTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace EMR.Helpers
{
    public static class IEnumerableHelpers
    {

        public static IEnumerable<M> Order<M, S>(this IEnumerable<M> data, S searchParameters) where S : DataTablesParameters
        {
            var orderCriteria = "Id";
            var orderAscendingDirection = true;

            if (searchParameters.Order != null)
            {
                orderCriteria = searchParameters.Columns[searchParameters.Order[0].Column].Data;
                orderAscendingDirection = searchParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }

            var result = data;

            result = orderAscendingDirection ? result.OrderByDynamic(orderCriteria, TableOrder.Asc) : result.OrderByDynamic(orderCriteria, TableOrder.Desc);

            return result;
        }
    }
}
