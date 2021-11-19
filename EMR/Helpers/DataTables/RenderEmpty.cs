namespace ERM.Helpers.DataTables
{
    /// <summary>
    /// Represents manage method to render empty data for DataTables column
    /// </summary>
    public partial class RenderEmpty : IRender
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the RenderEmpty class 
        /// </summary>
        /// <param name="text">Text for column if row data is empty</param>
        public RenderEmpty(string text)
        {
            Text = text;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Text for empty row in column
        /// </summary>
        public string Text { get; set; }

        #endregion
    }
}
