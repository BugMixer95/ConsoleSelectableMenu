using System.Threading;
using System.Threading.Tasks;

namespace ConsoleSelectableMenu.Infrastructure
{
    /// <summary>
    /// Represents the singleton class, which purpose is to process rendering and catching input keys.
    /// </summary>
    internal sealed class MenuDirector
    {
        #region Private Members
        private static MenuDirector? _instance = null;
        private volatile SelectableMenu? _currentMenu = null;
        private bool _isProcessing = false;
        #endregion

        #region Constructors
        private MenuDirector()
        { }
        #endregion

        /// <summary>
        /// Gets the instance of the <see cref="MenuDirector"/> class.
        /// </summary>
        internal static MenuDirector Instance { get => _instance ??= new MenuDirector(); }

        /// <summary>
        /// Starts processing the menu.
        /// </summary>
        internal async Task ProcessMenu(CancellationToken token = default)
        {
            if (_currentMenu is null || _isProcessing)
            {
                await Task.CompletedTask;
                return;
            }

            _isProcessing = true;

            while (!token.IsCancellationRequested)
            {
                await _currentMenu!.StartAsync(token);
            }

            _isProcessing = false;
        }

        /// <summary>
        /// Switches the current menu to the specified one.
        /// </summary>
        /// <param name="menu">The menu to switch to.</param>
        internal void SwitchMenu(SelectableMenu menu)
        {
            if (_currentMenu is { })
                _currentMenu.IsSwitchRequested = true;

            menu.IsSwitchRequested = false;

            _currentMenu = menu;
        }
    }
}
