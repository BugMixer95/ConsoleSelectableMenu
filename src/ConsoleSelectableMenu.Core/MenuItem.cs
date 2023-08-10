using System;

namespace ConsoleSelectableMenu
{
    /// <summary>
    /// Represents a menu item.
    /// </summary>
    public class MenuItem
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItem"/> class.
        /// </summary>
        public MenuItem()
        {
            InnerText = string.Empty;
            Action = null;
            Enabled = true;
        }
        #endregion

        /// <summary>
        /// Gets the parent <see cref="SelectableMenu"/> instance, which the menu item is assigned to.
        /// </summary>
        public SelectableMenu? ParentMenu { get; internal set; }

        /// <summary>
        /// The text to display in the console.
        /// </summary>
        public string InnerText { get; set; }

        /// <summary>
        /// The description of menu item action.
        /// </summary>
        public string? ActionDescription { get; set; }

        /// <summary>
        /// Action that would be executed when menu item is selected and activated with Enter key.
        /// </summary>
        public Action? Action { get; set; }

        /// <summary>
        /// The value indicating whether the menu item is enabled for selection or not.
        /// </summary>
        public bool Enabled { get; set; }
    }
}
