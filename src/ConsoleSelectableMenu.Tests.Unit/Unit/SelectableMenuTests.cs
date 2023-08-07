using ConsoleSelectableMenu.Infrastructure;

namespace ConsoleSelectableMenu.Tests.Unit
{
    public class SelectableMenuTests
    {
        [Fact]
        internal void Render_WritesArrowSign_WithAppropriateOptions()
        {
            // arrange
            var item = new MenuItem { InnerText = "Home" };
            
            var menu = new SelectableMenu(new SelectableMenuOptions { SelectionType = SelectionType.Arrowed });
            menu.Items.Add(item);

            var sw = new StringWriter();
            Console.SetOut(sw);

            // act
            menu.Render();
            var output = sw.ToString().Trim();

            // assert
            output.Should().Be(Constants.ArrowedSelection + item.InnerText);
        }

        [Fact]
        internal void Render_WritesInnerTextAndActionDescription_OnCall()
        {
            // arrange
            var item = new MenuItem { InnerText = "Exit", ActionDescription = "Exit the program." };

            var menu = new SelectableMenu(new SelectableMenuOptions { SelectionType = SelectionType.BackgroundFilled});
            menu.Items.Add(item);

            var sw = new StringWriter();
            Console.SetOut(sw);

            // act
            menu.Render();
            var output = sw.ToString();

            // assert
            output.Should().ContainAll(item.InnerText, item.ActionDescription);
        }
    }
}
