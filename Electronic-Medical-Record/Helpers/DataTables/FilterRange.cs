using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Electronic_Medical_Record.Helpers.DataTables
{
    public class FilterRange
    {
        #region Ctor
        /// <summary>
        /// Initializes a new instance of the FilterRange class
        /// </summary>
        /// <param name="type">Type of range values</param>
        public FilterRange(Type type)
        {
            Type = type;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Sstar range value
        /// </summary>
        public string Start { get; set; }

        /// <summary>
        /// End range value
        /// </summary>
        public string End { get; set; }

        /// <summary>
        /// Type of range values
        /// </summary>
        public Type Type { get; set; }

        #endregion
    }
}
