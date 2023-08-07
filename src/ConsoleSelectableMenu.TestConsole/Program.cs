using ConsoleSelectableMenu;
using System;

namespace ConsoleSelectableMenu.TestConsole;

internal class Program
{
    static async Task Main(string[] args)
    {
        Console.CursorVisible = false;

        var homeItem = new MenuItem { InnerText = "Home" };
        var helpItem = new MenuItem { InnerText = "Help" };
        var exitItem = new MenuItem
        {
            InnerText = "Exit",
            Action = () =>
            {
                Environment.Exit(0);
            }
        };

        var menu = new SelectableMenu();
        menu.Items.Add(homeItem);
        menu.Items.Add(helpItem);
        menu.Items.Add(exitItem);

        await menu.StartAsync();
    }
}