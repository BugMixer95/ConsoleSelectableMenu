namespace ConsoleSelectableMenu
{
    /// <summary>
    /// Indicates the way with which the menu item would be selected.
    /// </summary>
    public enum SelectionType
    {
        /// <summary>
        /// Menu item would be marked with filled background when selected.
        /// </summary>
        BackgroundFilled,
        
        /// <summary>
        /// 'More than' sign ('>') would preceed the menu item name when selected.
        /// </summary>
        Arrowed
    }

    /// <summary>
    /// Represents the options which could be applied to the menu.
    /// </summary>
    public class SelectableMenuOptions
    {
        #region Global Options
        /// <summary>
        /// Options which are applied to all instances of the <see cref="SelectableMenu"/> class, 
        /// if specific options have not been provided on initialization.
        /// </summary>
        public static SelectableMenuOptions GlobalOptions { get => _globalOptions; }
        
        static SelectableMenuOptions()
        {
            _globalOptions = new SelectableMenuOptions
            {
                SelectionType = SelectionType.BackgroundFilled
            };
        }

        private static readonly SelectableMenuOptions _globalOptions;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectableMenuOptions"/> class.
        /// </summary>
        public SelectableMenuOptions()
        {
            SelectionType = default;
        }
        #endregion

        /// <summary>
        /// Determines how the selected menu item would be marked.
        /// </summary>
        public SelectionType SelectionType { get; set; }
    }
}
