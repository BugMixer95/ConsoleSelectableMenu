namespace ConsoleSelectableMenu.Infrastructure
{
    /// <summary>
    /// Represents a node in a <see cref="MenuItemCollection"/>. This class cannot be inherited.
    /// </summary>
    internal sealed class MenuNode
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuNode"/> class, containing the specified value.
        /// </summary>
        /// <param name="item"></param>
        public MenuNode(MenuItem item)
        {
            Value = item;
        }
        #endregion

        #region Class Members
        /// <summary>
        /// Gets the value contained in the node.
        /// </summary>
        public MenuItem Value { get; private set; }

        /// <summary>
        /// Gets the next node in the <see cref="MenuItemCollection"/>.
        /// </summary>
        public MenuNode? Next { get; internal set; }

        /// <summary>
        /// Gets the previous node in the <see cref="MenuItemCollection"/>.
        /// </summary>
        public MenuNode? Previous { get; internal set; }
        #endregion
    }
}
