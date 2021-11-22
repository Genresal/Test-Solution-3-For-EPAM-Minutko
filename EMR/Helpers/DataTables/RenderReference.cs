using System.Collections.Generic;

namespace EMR.DataTables
{
    /// <summary>
    /// Represents reference render for DataTables column
    /// </summary>
    public partial class RenderReference : IRender
    {
        #region Ctor

        /// <summary>
        /// Initializes a new instance of the RenderReference class 
        /// </summary>
        /// <param name="url">Reference URL</param>
        /// <param name="text">Button text</param>
        public RenderReference(string url, string text)
        {
            Columns = new List<string>();
            Url = url;
            Text = text;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Text for reference
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// URL, example - TestRegisters/Details
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// List of column names for sending to controller
        /// </summary>
        public List<string> Columns { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Add to Column list with changing first char to lowercase
        /// </summary>
        /// <param name="item"></param>
        public void AddWithFormatting(string item)
        {
            Columns.Add(item.Substring(0, 1).ToLower() + item.Substring(1));
        }

        #endregion
    }
}
