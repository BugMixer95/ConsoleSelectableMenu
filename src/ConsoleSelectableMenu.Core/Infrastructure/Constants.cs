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
    }
}
