using ConsoleSelectableMenu.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleSelectableMenu
{
    /// <summary>
    /// Represents a menu, which is a collection of objects of <see cref="MenuItem"/> class.
    /// </summary>
    public class MenuItemCollection : IEnumerable
    {
        #region Private members
        private MenuNode? _head = null;
        private MenuNode? _selected = null;
        private int _count = 0;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemCollection"/> class that is empty.
        /// </summary>
        public MenuItemCollection()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemCollection"/> class that contains elements copied from the specified collection.
        /// </summary>
        /// <param name="items"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public MenuItemCollection(IEnumerable<MenuItem> items)
        {
            if (items is null)
                throw new ArgumentNullException(nameof(items), "Input collection cannot be null.");

            foreach (var item in items)
                Add(item);
        }
        #endregion

        /// <summary>
        /// Number of menu items contained in the menu.
        /// </summary>
        public int Count { get => _count; }

        /// <summary>
        /// Indicates whether the menu has any menu items.
        /// </summary>
        public bool IsEmpty { get => _count == 0; }

        /// <summary>
        /// Gets the currently selected menu item.
        /// </summary>
        public MenuItem? Selected
        {
            get => _selected?.Value;
        }

        /// <summary>
        /// Adds an item to the menu.
        /// </summary>
        /// <param name="item">The menu item to add to the menu.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Add(MenuItem item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item), "Input menu item cannot be null.");

            MenuNode node = new MenuNode(item);

            // set specified menu item as head if menu is empty
            if (_head is null)
            {
                _head = node;
                _head.Next = node;
                _head.Previous = node;

                // because this is the first added item in the menu, set it as selected by default
                _selected = _head;
            }
            else
            {
                node.Previous = _head.Previous;
                node.Next = _head;
                _head.Previous!.Next = node;
                _head.Previous = node;
            }

            _count++;
        }

        /// <summary>
        /// Removes an item from the menu.
        /// </summary>
        /// <param name="item">The menu item to remove from the menu.</param>
        /// <returns><see langword="true"/>, if the item has been successfully removed; otherwise, <see langword="false"/>.</returns>
        public bool Remove(MenuItem item)
        {
            // return false when menu is empty
            if (_count == 0)
                return false;

            MenuNode? current = _head;
            MenuNode? removed = null;

            // searching for requested menu item
            do
            {
                if (current!.Value.Equals(item))
                {
                    removed = current;
                    break;
                }

                current = current.Next;
            }
            while (current != _head);

            // return false if specified menu item has not been found
            if (removed is null)
                return false;

            // remove head element if menu contains only one menu item
            if (_count == 1)
            {
                _head = null;
                _selected = null;
            }
            else
            {
                if (removed == _head)
                {
                    _head = _head.Next;
                }

                if (removed == _selected)
                {
                    _selected = _selected.Next;
                }

                removed.Previous!.Next = removed.Next;
                removed.Next!.Previous = removed.Previous;
            }

            _count--;
            return true;
        }

        /// <summary>
        /// Removes all items from the menu.
        /// </summary>
        public void Clear()
        {
            _head = null;
            _count = 0;
            _selected = null;
        }

        /// <summary>
        /// Returns the zero-based index of the first occurence of a menu item in the menu.
        /// </summary>
        /// <param name="item">The menu item to locate in the menu.</param>
        /// <returns>The zero-based index of the first occurrence of menu item within the entire menum if found;
        /// otherwise, -1.</returns>
        public int IndexOf(MenuItem item)
        {
            if (_count == 0 || item is null)
                return -1;

            MenuNode? current = _head;
            int index = 0;

            do
            {
                if (current!.Value.Equals(item))
                    return index;

                current = current.Next;
                index++;
            }
            while (current != _head);

            return -1;
        }

        /// <inheritdoc />
        public IEnumerator GetEnumerator()
        {
            MenuNode? current = _head;

            do
            {
                if (current is { })
                {
                    yield return current.Value;
                    current = current.Next;
                }
            }
            while (current != _head);
        }

        #region Assistants
        /// <summary>
        /// Moves selection of menu item to the specified direction.
        /// </summary>
        /// <param name="direction">Indicates which menu item should be selected.</param>
        /// <exception cref="ArgumentException"></exception>
        internal void MoveSelection(MoveDirection direction)
        {
            if (_count == 0)
                return;

            switch (direction)
            {
                case MoveDirection.Previous:
                    do
                    {
                        _selected = _selected!.Previous;
                    }
                    while (_selected!.Value.Enabled == false);
                    break;

                case MoveDirection.Next:
                    do
                    {
                        _selected = _selected!.Next;
                    }
                    while (_selected!.Value.Enabled == false);
                    break;

                default:
                    throw new ArgumentException("Wait, what? How such could even happen?");
            }
        }
        #endregion
    }
}
