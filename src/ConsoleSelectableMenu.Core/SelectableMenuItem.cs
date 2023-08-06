namespace ConsoleSelectableMenu
{
    /// <summary>
    /// Represents a menu item.
    /// </summary>
    public class SelectableMenuItem
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectableMenu"/> class.
        /// </summary>
        public SelectableMenuItem()
        {
            InnerText = string.Empty;
        }
        #endregion

        /// <summary>
        /// The text to display in the console.
        /// </summary>
        public string InnerText { get; set; }
    }
}
