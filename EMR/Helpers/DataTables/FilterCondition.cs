using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.DataTables
{
    public class FilterCondition
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the FilterParameter class
        /// </summary>
        /// <param name="id">Filter parameter id</param>
        /// <param name="name">Filter parameter name</param>
        public FilterCondition(int id, string name)
        {
            Id = id;
            Name = name;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Filter field id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Filter field name
        /// </summary>
        public string Name { get; set; }

        #endregion
    }
}
