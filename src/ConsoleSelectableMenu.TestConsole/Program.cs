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
            },
            ActionDescription = "Exit the program."
        };

        var menu = new SelectableMenu();
        
        menu.Items.Add(homeItem);
        menu.Items.Add(helpItem);
        menu.Items.Add(exitItem);

        menu.BeforeRendering += () =>
        {
            Console.Write("This is the demonstration of ");
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("ConsoleSelectableMenu");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(" library.");
            Console.WriteLine("(c) 2023.");

            Console.WriteLine();
        };

        await menu.StartAsync();
    }
}