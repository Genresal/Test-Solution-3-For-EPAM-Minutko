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
        public IEnumerable<M> Order<M, S>(S searchParameters, IEnumerable<M> data) where S : DataTablesParameters
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
