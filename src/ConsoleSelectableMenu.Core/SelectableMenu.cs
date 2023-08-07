using ConsoleSelectableMenu.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

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

        /// <summary>
        /// Occurs when any key is pressed.
        /// </summary>
        public event EventHandler<SelectableMenuEventArgs>? KeyPressed = null;

        /// <summary>
        /// Asynchronously renders initialized menu and starts listening for pressed keys.
        /// </summary>
        public async Task StartAsync(CancellationToken token = default)
        {
            Render(true);
            await ListenForKey(token);
        }

        /// <summary>
        /// Prints all the menu content to the console.
        /// </summary>
        /// <param name="clear">Indicates whether the console should be cleared before rendering.
        /// <see langword="true"/> will clear the console.</param>
        public void Render(bool clear = true)
        {
            if (clear)
                Console.Clear();

            if (Console.CursorTop != 0)
                Console.CursorTop -= Items.Count;

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
        }

        #region Assistants
        /// <summary>
        /// Asynchronously listens for a pressed key.
        /// </summary>
        private Task ListenForKey(CancellationToken token = default)
        {
            return Task.Factory.StartNew(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(false);
                        ProcessPressedKey(this, new SelectableMenuEventArgs(key));
                    }
                }
            }, token);
        }

        /// <summary>
        /// Performs an action based on a pressed key.
        /// </summary>
        private void ProcessPressedKey(object? sender, SelectableMenuEventArgs args)
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

                case ConsoleKey.Enter:
                    Items.Selected!.Action?.Invoke();
                    break;

                default:
                    break;
            }
        }
        #endregion
    }
}
