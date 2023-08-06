using ConsoleSelectableMenu.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleSelectableMenu
{
    /// <summary>
    /// Represents a menu, which is a collection of objects of <see cref="SelectableMenuItem"/> class.
    /// </summary>
    public class SelectableMenu : IEnumerable
    {
        #region Private members
        private MenuNode? _head = null;
        private MenuNode? _selected = null;
        private int _count = 0;

        private enum MoveDirection
        {
            Previous,
            Next
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectableMenu"/> class that is empty.
        /// </summary>
        public SelectableMenu()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectableMenu"/> class that contains elements copied from the specified collection.
        /// </summary>
        /// <param name="items"></param>
        public SelectableMenu(IEnumerable<SelectableMenuItem> items)
        {
            foreach (var item in items)
                Add(item);
        }
        #endregion

        public event EventHandler<SelectableMenuEventArgs>? KeyPressed = null;

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
        public SelectableMenuItem? Selected
        {
            get => _selected?.Value;
        }

        public void OnKeyPressed(object? sender, SelectableMenuEventArgs args)
        {
            KeyPressed?.Invoke(sender, args);

            switch (args.KeyInfo.Key)
            {
                case ConsoleKey.DownArrow:
                    MoveSelection(MoveDirection.Next);
                    break;

                case ConsoleKey.UpArrow:
                    MoveSelection(MoveDirection.Previous);
                    break;

                default:
                    break;
            }

            // Render();
        }

        public void Render(bool clear = true)
        {
            if (clear)
                Console.Clear();

            foreach (SelectableMenuItem item in this)
            {
                if (item.Equals(Selected))
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(item.InnerText);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.CursorTop -= _count;
        }

        /// <summary>
        /// Adds an item to the menu.
        /// </summary>
        /// <param name="item">The menu item to add to the menu.</param>
        public void Add(SelectableMenuItem item)
        {
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
        public bool Remove(SelectableMenuItem item)
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
            }
            else
            {
                if (removed == _head)
                {
                    _head = _head.Next;
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
        }

        /// <summary>
        /// Returns the zero-based index of the first occurence of a menu item in the menu.
        /// </summary>
        /// <param name="item">The menu item to locate in the menu.</param>
        /// <returns>The zero-based index of the first occurrence of menu item within the entire menum if found;
        /// otherwise, -1.</returns>
        public int IndexOf(SelectableMenuItem item)
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
        private void MoveSelection(MoveDirection direction)
        {
            if (_count == 0)
                return;

            _ = direction switch
            {
                MoveDirection.Previous => _selected = _selected!.Previous,
                MoveDirection.Next => _selected = _selected!.Next,
                _ => throw new Exception("Wait, what? How such could even happen?")
            };

            Render(false);
        }
        #endregion
    }
}
