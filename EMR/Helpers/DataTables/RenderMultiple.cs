using System;
using System.Collections.Generic;
using System.Text;

namespace EMR.DataTables
{
    /// <summary>
    /// Represents row render with some condition for DataTables column
    /// </summary>
    public partial class RenderMultiple : IRender
    {

        /// <summary>
        /// Initializes a new instance of the RenderMultiple class 
        /// </summary>
        /// <param name="value">If data more than that valuer? row will be recolored to red</param>
        public RenderMultiple(string separator, string rowName)
        {
            Separator = separator;
            RowName = rowName;
        }
        /// <summary>
        /// String for dividing two values
        /// </summary>
        public string Separator { get; set; }

        /// <summary>
        /// Column for render
        /// </summary>
        public string RowName { get; set; }
    }
}
