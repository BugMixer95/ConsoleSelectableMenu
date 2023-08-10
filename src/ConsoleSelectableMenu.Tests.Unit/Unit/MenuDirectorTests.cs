using ConsoleSelectableMenu.Infrastructure;

namespace ConsoleSelectableMenu.Tests.Unit;

public class MenuDirectorTests
{
    [Fact]
    internal void ProcessMenu_ReturnsCompletedTask_WhenCurrentMenuIsNull()
    {
        // arrange

        // act
        Task act = MenuDirector.Instance.ProcessMenu();

        // assert
        act.Should().Be(Task.CompletedTask);
    }

    [Fact]
    internal void ProcessMenu_ReturnsCompletedTask_WhenAlreadyProcessing()
    {
        // arrange
        var menu = new SelectableMenu();

        // act
        MenuDirector.Instance.SwitchMenu(menu);
        Task start = MenuDirector.Instance.ProcessMenu();

        Task act = MenuDirector.Instance.ProcessMenu();

        // assert
        act.Should().Be(Task.CompletedTask);
    }

    [Fact]
    internal void SwitchMenu_ChangesIsSwitchRequested_OnCall()
    {
        // arrange
        var firstMenu = new SelectableMenu();
        var secondMenu = new SelectableMenu();

        // act
        MenuDirector.Instance.SwitchMenu(firstMenu);
        MenuDirector.Instance.SwitchMenu(secondMenu);

        // assert
        firstMenu.IsSwitchRequested.Should().BeTrue();
        secondMenu.IsSwitchRequested.Should().BeFalse();
    }
}
