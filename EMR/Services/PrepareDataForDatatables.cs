using ERM.DataTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERM.Services
{
    public class PrepareDataForDatatables<T, S> where S : DataTablesParameters
    {
        /*
        public IQueryable<T>LoadTable(S searchParameters, IQueryable baseQuery)
        {
            var searchBy = searchParameters.Search?.Value;

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

            // Fitering

            var searchAttributes = typeof(S).GetProperties(System.Reflection.BindingFlags.Public
    | System.Reflection.BindingFlags.Instance
    | System.Reflection.BindingFlags.DeclaredOnly);

            foreach (var prop in searchAttributes)

            return result;
        }*/
    }
}
