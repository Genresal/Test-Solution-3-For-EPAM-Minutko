using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMR.DataTables
{
    public class DataTablesModel
    {
        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public DataTablesModel()
        {
            //set default values
            ServerSide = true;
            Processing = true;
            Paging = true;
            Filters = new List<FilterParameter>();
            ColumnCollection = new List<ColumnProperty>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets table name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Save actual table state
        /// </summary>
        public bool StateSave { get; set; }

        /// <summary>
        /// Automatic table widgh
        /// </summary>
        public bool AutoWidth { get; set; }

        /// <summary>
        /// Enable or disable the display of a 'processing' indicator when the table is being processed 
        /// </summary>
        public bool Processing { get; set; }

        /// <summary>
        /// Feature control DataTables' server-side processing mode
        /// </summary>
        public bool ServerSide { get; set; }

        /// <summary>
        /// Enable or disable table pagination.
        /// </summary>
        public bool Paging { get; set; }

        /// <summary>
        /// Enable or disable Searching field
        /// </summary>
        public bool Searching { get; set; }

        /// <summary>
        /// This parameter allows you to readily specify the entries in the length drop down select list that DataTables shows when pagination is enabled
        /// </summary>
        public string LengthMenu { get; set; }

        /// <summary>
        /// Number of rows to display on a single page when using pagination
        /// </summary>
        public int PageLength { get; set; }

        /// <summary>
        /// ///https://datatables.net/reference/option/order
        /// [column id, "asc"/"desc"]
        /// </summary>
        public string Order { get; set; }

        /// <summary>
        /// !!!!!!!!!!!!!!!!!! URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Selection mode, multi or single
        /// </summary>
        public string Select { get; set; }

        /// <summary>
        /// Enable scrolling, value is heigh in px
        /// </summary>
        public int ScrollY { get; set; }

        /// <summary>
        /// Enable scrolling
        /// </summary>
        public bool ScrollCollapse { get; set; }

        /// <summary>
        /// Gets or set filters controls
        /// </summary>
        public IList<FilterParameter> Filters { get; set; }

        /// <summary>
        /// Gets or set column collection 
        /// </summary>
        public IList<ColumnProperty> ColumnCollection { get; set; }

        #endregion
    }
}
