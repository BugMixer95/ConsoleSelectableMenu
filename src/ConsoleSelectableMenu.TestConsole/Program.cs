using ConsoleSelectableMenu;
using System;

namespace ConsoleSelectableMenu.TestConsole;

internal class Program
{
    static void Main(string[] args)
    {
        Console.CursorVisible = false;

        var homeItem = new MenuItem { InnerText = "Home" };
        var helpItem = new MenuItem { InnerText = "Help" };
        var exitItem = new MenuItem { InnerText = "Exit" };

        var menu = new SelectableMenu();
        menu.Items.Add(homeItem);
        menu.Items.Add(helpItem);
        menu.Items.Add(exitItem);
        menu.Render();

        while (true)
        {
            var key = Console.ReadKey();
            menu.OnKeyPressed(null, new SelectableMenuEventArgs(key));
        }
    }
}