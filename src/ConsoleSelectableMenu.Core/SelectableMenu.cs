using ConsoleSelectableMenu.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleSelectableMenu
{
    public class SelectableMenu
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectableMenu"/> class.
        /// </summary>
        public SelectableMenu()
        {
            Items = new MenuItemCollection();
        }
        #endregion

        /// <summary>
        /// Gets the items of the menu.
        /// </summary>
        public MenuItemCollection Items { get; }

        public event EventHandler<SelectableMenuEventArgs>? KeyPressed = null;

        public void OnKeyPressed(object? sender, SelectableMenuEventArgs args)
        {
            KeyPressed?.Invoke(sender, args);

            switch (args.KeyInfo.Key)
            {
                case ConsoleKey.DownArrow:
                    Items.MoveSelection(MoveDirection.Next);
                    Render(false);
                    break;

                case ConsoleKey.UpArrow:
                    Items.MoveSelection(MoveDirection.Previous);
                    Render(false);
                    break;

                default:
                    break;
            }
        }

        public void Render(bool clear = true)
        {
            if (clear)
                Console.Clear();

            foreach (MenuItem item in Items)
            {
                if (item.Equals(Items.Selected))
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }

                Console.WriteLine(item.InnerText);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.CursorTop -= Items.Count;
        }
    }
}
