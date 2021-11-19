namespace Electronic_Medical_Record.Helpers.DataTables
{
    /// <summary>
    /// Represent DataTables column property
    /// </summary>
    public partial class ColumnProperty
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the ColumnProperty class
        /// </summary>
        /// <param name="data">The data source for the column from the rows data object</param>
        public ColumnProperty(string data)
        {
            Data = data;
            //set default values
            Visible = true;
            Orderable = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Set the data source for the column from the rows data object / array.
        /// See also "https://datatables.net/reference/option/columns.data"
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Render differents types of columns
        /// </summary>
        public IRender Render { get; set; }

        /// <summary>
        /// Enable or disable the display of this column.
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// !!!!!!!!!! Can be orderable
        /// </summary>
        public bool Orderable { get; set; }
        //***28.04.21
        /// <summary>
        /// !!!!!!!!!! For CSS
        /// </summary>
        public string ClassName { get; set; }
        /// <summary>
        /// !!!!!!!!!! 
        /// </summary>
        public string DefaultConsent { get; set; }

        #endregion
    }
}



