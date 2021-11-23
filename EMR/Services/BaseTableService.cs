using EMR.Business.Models;
using EMR.Helpers;
using EMR.DataTables;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.Services
{
    public abstract class BaseTableService
    {
        public IQueryable<M> Order<M, S>(S searchParameters, IQueryable<M> baseQuery) where S : DataTablesParameters
        {
            // if we have an empty search then just order the results by Id ascending
            var orderCriteria = "Id";
            var orderAscendingDirection = true;

            if (searchParameters.Order != null)
            {
                // in this example we just default sort on the 1st column
                orderCriteria = searchParameters.Columns[searchParameters.Order[0].Column].Data;
                orderAscendingDirection = searchParameters.Order[0].Dir.ToString().ToLower() == "asc";
            }

            var result = baseQuery;

            result = orderAscendingDirection ? result.OrderByDynamic(orderCriteria, TableOrder.Asc) : result.OrderByDynamic(orderCriteria, TableOrder.Desc);

            return result;
        }
    }
}
