using ConsoleSelectableMenu;
using System;

namespace ConsoleSelectableMenu.TestConsole;

internal class Program
{
    static void Main(string[] args)
    {
        Console.CursorVisible = false;

        var homeItem = new SelectableMenuItem { InnerText = "Home" };
        var helpItem = new SelectableMenuItem { InnerText = "Help" };
        var exitItem = new SelectableMenuItem { InnerText = "Exit" };

        var menu = new SelectableMenu { homeItem, helpItem, exitItem };
        menu.Render();

        while (true)
        {
            var key = Console.ReadKey();
            menu.OnKeyPressed(null, new SelectableMenuEventArgs(key));
        }
    }
}