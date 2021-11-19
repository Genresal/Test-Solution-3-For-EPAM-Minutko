using System;
using System.Collections.Generic;
using System.Text;

namespace Electronic_Medical_Record.Helpers.DataTables
{
    /// <summary>
    /// Represents row render with some condition for DataTables column
    /// </summary>
    public partial class RenderWithCondition : IRender
    {

        /// <summary>
        /// Initializes a new instance of the RenderWithCondition class 
        /// </summary>
        /// <param name="value">If data more than that valuer? row will be recolored to red</param>
        public RenderWithCondition(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Condition value
        /// </summary>
        public string Value { get; set; }
    }
}
