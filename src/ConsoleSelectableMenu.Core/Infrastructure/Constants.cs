namespace ConsoleSelectableMenu.Infrastructure
{
    internal static class Constants
    {
        /// <summary>
        /// Used to mark menu item as selected.
        /// </summary>
        public const string ArrowedSelection = "> ";

        /// <summary>
        /// Used for removing anomalies when re-rendering a menu with <see cref="SelectionType.Arrowed"/> selection.
        /// </summary>
        public const string MenuItemTail = "  ";

        /// <summary>
        /// Indicates length of the empty string which is written when menu item has no ActionDescription. 
        /// Used for unit tests purposes only (when console output is redirected).
        /// </summary>
        public const int EmptyStringLength = 100;
    }
}
