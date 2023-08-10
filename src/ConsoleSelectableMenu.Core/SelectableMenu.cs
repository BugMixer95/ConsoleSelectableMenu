using ConsoleSelectableMenu.Infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleSelectableMenu
{
    /// <summary>
    /// Represents a menu with selectable items.
    /// </summary>
    public class SelectableMenu
    {
        #region Private Members
        private readonly SelectableMenuOptions _options;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectableMenu"/> class.
        /// </summary>
        public SelectableMenu()
        {
            Items = new MenuItemCollection();
            _options = SelectableMenuOptions.GlobalOptions;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectableMenu"/> class with specified options.
        /// </summary>
        /// <param name="options"></param>
        public SelectableMenu(SelectableMenuOptions options)
            : this()
        {
            _options = options;
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
        /// Occurs before rendering menu items. <br />
        /// <i>Hint: use it for adding headers for the menu.</i>
        /// </summary>
        public event Action? BeforeRendering = null;

        /// <summary>
        /// Occurs after rendering menu items.
        /// </summary>
        public event Action? AfterRendering = null;

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
            if (!Console.IsOutputRedirected)
            {
                if (clear)
                    Console.Clear();

                Console.CursorTop = 0;
            }

            BeforeRendering?.Invoke();

            RenderMenuItems();

            AfterRendering?.Invoke();
        }

        #region Assistants
        /// <summary>
        /// Renders menu items according to the menu options.
        /// </summary>
        private void RenderMenuItems()
        {
            int startCursorPositionTop = 0;
            if (!Console.IsOutputRedirected) 
                startCursorPositionTop = Console.CursorTop;

            foreach (MenuItem item in Items)
            {
                // marking menu item as selected according to the menu options
                if (item.Equals(Items.Selected))
                {
                    switch (_options.SelectionType)
                    {
                        case SelectionType.BackgroundFilled:
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            break;

                        case SelectionType.Arrowed:
                            Console.Write(Constants.ArrowedSelection);
                            break;
                    }
                }

                if (!item.Enabled)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }

                // rendering menu item inner text
                Console.Write(item.InnerText);
                Console.WriteLine(Constants.MenuItemTail);

                // returning console style to its initial state
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }

            // renders selected menu item description if such exists
            if (Items.Selected?.ActionDescription is { })
            {
                Console.WriteLine();

                // clearing previous description
                Console.Write(new string(
                    c: ' ',
                    count: Console.IsOutputRedirected ? Constants.EmptyStringLength : Console.WindowWidth
                    ));

                if (!Console.IsOutputRedirected)
                    Console.CursorLeft = 0;

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(Items.Selected!.ActionDescription);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.WriteLine();

                if (!Console.IsOutputRedirected)
                    Console.CursorLeft = 0;

                Console.WriteLine(new string(
                    c: ' ', 
                    count: Console.IsOutputRedirected ? Constants.EmptyStringLength : Console.WindowWidth
                    ));
            }

            if (!Console.IsOutputRedirected) 
                Console.CursorTop = startCursorPositionTop;
        }

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
