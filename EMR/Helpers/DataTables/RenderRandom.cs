using System;
using System.Collections.Generic;
using System.Text;

namespace EMR.DataTables
{
    /// <summary>
    /// Represents row render with some condition for DataTables column
    /// </summary>
    public partial class RenderRandom : IRender
    {

        /// <summary>
        /// Initializes a new instance of the RenderRandom class 
        /// </summary>
        /// <param name="value">random HTML rendering</param>
        public RenderRandom(string value)
        {
            Value = value;
        }

        /// <summary>
        /// HTML string
        /// </summary>
        public string Value { get; set; }
    }
}
