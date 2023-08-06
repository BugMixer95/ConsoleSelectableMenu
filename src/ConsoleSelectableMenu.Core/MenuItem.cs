﻿namespace ConsoleSelectableMenu
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
        }
        #endregion

        /// <summary>
        /// The text to display in the console.
        /// </summary>
        public string InnerText { get; set; }
    }
}