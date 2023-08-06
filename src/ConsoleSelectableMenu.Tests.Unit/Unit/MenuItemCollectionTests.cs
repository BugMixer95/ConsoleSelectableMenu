﻿using ConsoleSelectableMenu.Infrastructure;

namespace ConsoleSelectableMenu.Tests.Unit;

public class MenuItemCollectionTests
{
    [Fact]
    internal void Constructor_ThrowsArgumentNullException_WhenPassingNullCollection()
    {
        // arrange

        // act
        Action act = () => new MenuItemCollection(null);
        
        // assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    internal void IsEmpty_ReturnsTrue_OnDefaultConstructorCall()
    {
        // arrange
        var collection = new MenuItemCollection();

        // act

        // assert
        collection.IsEmpty.Should().BeTrue();
        collection.Count.Should().Be(0);
    }

    [Fact]
    internal void IsEmpty_ReturnsTrue_OnConstructorCall_WithEmptyEnumerable()
    {
        // arrange
        var items = Enumerable.Empty<MenuItem>();
        var collection = new MenuItemCollection(items);

        // act

        // assert
        collection.IsEmpty.Should().BeTrue();
        collection.Count.Should().Be(0);
    }

    [Fact]
    internal void IsEmpty_ReturnsFalse_OnConstructorCall_WithNonEmptyEnumerable()
    {
        // arrange
        var items = new List<MenuItem>
        {
            new MenuItem { InnerText = "first" },
            new MenuItem { InnerText = "second" }
        };
        var collection = new MenuItemCollection(items);

        // act

        // assert
        collection.IsEmpty.Should().BeFalse();
        collection.Count.Should().Be(2);
    }

    [Fact]
    internal void Add_AddsMenuItem_OnCall()
    {
        // arrange
        var item = new MenuItem { InnerText = "random" };
        var collection = new MenuItemCollection();

        // act
        collection.Add(item);

        // assert
        collection.IsEmpty.Should().BeFalse();
        collection.Count.Should().Be(1);
        collection.Selected.Should().Be(item);
    }

    [Fact]
    internal void Add_SetsFirstItemAsSelected_OnAddingTwoItems()
    {
        // arrange
        var firstItem = new MenuItem { InnerText = "first" };
        var secondItem = new MenuItem { InnerText = "second" };

        var collection = new MenuItemCollection();

        // act
        collection.Add(firstItem);
        collection.Add(secondItem);

        // assert
        collection.IsEmpty.Should().BeFalse();
        collection.Count.Should().Be(2);
        collection.Selected.Should().Be(firstItem);
        collection.Selected.Should().NotBe(secondItem);
    }

    [Fact]
    internal void Add_ThrowsArgumentNullException_OnAddingNull()
    {
        // arrange
        var collection = new MenuItemCollection();

        // act
        Action act = () => collection.Add(null);

        // assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    internal void Remove_RemovesItemAndReturnsTrue_OnCall()
    {
        // arrange
        var item = new MenuItem { InnerText = "random" };
        var collection = new MenuItemCollection { item };

        // act
        var result = collection.Remove(item);

        // assert
        collection.IsEmpty.Should().BeTrue();
        collection.Count.Should().Be(0);
        collection.Selected.Should().BeNull();

        result.Should().BeTrue();
    }

    [Fact]
    internal void Remove_ReturnsFalse_WhenCollectionIsEmpty()
    {
        // arrange
        var item = new MenuItem { InnerText = "random" };
        var collection = new MenuItemCollection();

        // act
        var result = collection.Remove(item);

        // assert
        result.Should().BeFalse();
    }

    [Fact]
    internal void Remove_ReturnsFalse_OnRemovingNonExistingItem()
    {
        // arrange
        var firstItem = new MenuItem { InnerText = "first" };
        var secondItem = new MenuItem { InnerText = "second" };
        var itemToRemove = new MenuItem { InnerText = "remove me" };

        var collection = new MenuItemCollection { firstItem, secondItem };

        // act
        var result = collection.Remove(itemToRemove);

        // assert
        collection.IsEmpty.Should().BeFalse();
        collection.Count.Should().Be(2);

        result.Should().BeFalse();
    }

    [Fact]
    internal void Remove_ReturnsFalse_OnPassingNull()
    {
        // arrange
        var item = new MenuItem();
        var collection = new MenuItemCollection { item };

        // act
        var result = collection.Remove(null);

        // assert
        result.Should().BeFalse();
    }

    [Fact]
    internal void Clear_ClearsCollection_OnCall()
    {
        // arrange
        var firstItem = new MenuItem { InnerText = "first" };
        var secondItem = new MenuItem { InnerText = "second" };

        var collection = new MenuItemCollection { firstItem, secondItem };

        // act
        collection.Clear();

        // assert
        collection.IsEmpty.Should().BeTrue();
        collection.Count.Should().Be(0);
        collection.Selected.Should().BeNull();
    }

    [Fact]
    internal void IndexOf_ReturnsOne_WhenSearchingForSecondItem()
    {
        // arrange
        var firstItem = new MenuItem { InnerText = "first" };
        var secondItem = new MenuItem { InnerText = "second" };
        var thirdItem = new MenuItem { InnerText = "third" };

        var collection = new MenuItemCollection { firstItem, secondItem, thirdItem };

        // act
        var result = collection.IndexOf(secondItem);

        // assert
        result.Should().Be(1);
    }

    [Fact]
    internal void IndexOf_ReturnsMinusOne_WhenSearchingForNonExistingItem()
    {
        // arrange
        var firstItem = new MenuItem { InnerText = "first" };
        var secondItem = new MenuItem { InnerText = "second" };
        var thirdItem = new MenuItem { InnerText = "third" };

        var collection = new MenuItemCollection { firstItem, secondItem };

        // act
        var result = collection.IndexOf(thirdItem);

        // assert
        result.Should().Be(-1);
    }

    [Fact]
    internal void IndexOf_ReturnsMinusOne_WhenCollectionIsEmpty()
    {
        // arrange
        var collection = new MenuItemCollection();

        // act
        var result = collection.IndexOf(new MenuItem { InnerText = "first" });

        // assert
        result.Should().Be(-1);
    }

    [Fact]
    internal void IndexOf_ReturnsMinusOne_WhenSearchingForNull()
    {
        // arrange
        var collection = new MenuItemCollection();

        // act
        var result = collection.IndexOf(null);

        // assert
        result.Should().Be(-1);
    }

    [Theory]
    [InlineData(MoveDirection.Previous)]
    [InlineData(MoveDirection.Next)]
    internal void MoveSelection_DoesNothing_WhenCollectionIsEmpty(MoveDirection direction)
    {
        // arrange
        var collection = new MenuItemCollection();

        // act
        collection.MoveSelection(direction);

        // assert
        collection.Selected.Should().BeNull();
    }

    [Theory]
    [InlineData(MoveDirection.Previous)]
    [InlineData(MoveDirection.Next)]
    internal void MoveSelection_SelectsSameItem_WhenCollectionHasOnlyOneItem(MoveDirection direction)
    {
        // arrange
        var item = new MenuItem { InnerText = "first" };
        var collection = new MenuItemCollection { item };

        // act
        collection.MoveSelection(direction);

        // assert
        collection.Selected.Should().NotBeNull();
        collection.Selected.Should().Be(item);
    }

    [Fact]
    internal void MoveSelection_SelectsPreviousItem_OnUsingPreviousDirection()
    {
        // arrange
        var firstItem = new MenuItem { InnerText = "first" };
        var secondItem = new MenuItem { InnerText = "second" };
        var thirdItem = new MenuItem { InnerText = "third" };

        var collection = new MenuItemCollection { firstItem, secondItem, thirdItem };
    
        // act
        collection.MoveSelection(MoveDirection.Previous);

        // assert
        collection.Selected.Should().NotBeNull();
        collection.Selected.Should().Be(thirdItem);
    }

    internal void MoveSelection_SelectsNextItem_OnUsingNextDirection()
    {
        // arrange
        var firstItem = new MenuItem { InnerText = "first" };
        var secondItem = new MenuItem { InnerText = "second" };
        var thirdItem = new MenuItem { InnerText = "third" };

        var collection = new MenuItemCollection { firstItem, secondItem, thirdItem };

        // act
        collection.MoveSelection(MoveDirection.Next);

        // assert
        collection.Selected.Should().NotBeNull();
        collection.Selected.Should().Be(secondItem);
    }

    [Fact]
    internal void MoveSelection_ThrowsArgumentException_WhenPassingIncorrectDirection()
    {
        // arrange
        var firstItem = new MenuItem { InnerText = "first" };
        var secondItem = new MenuItem { InnerText = "second" };
        var thirdItem = new MenuItem { InnerText = "third" };

        var collection = new MenuItemCollection { firstItem, secondItem, thirdItem };

        // act
        Action act = () => collection.MoveSelection((MoveDirection)int.MaxValue);

        // assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    internal void GetEnumerator_IteratesCorrectly_WithEmptyCollection()
    {
        // arrange
        var collection = new MenuItemCollection();
        var count = 0;

        // act
        foreach (var item in collection)
            count++;

        // assert
        count.Should().Be(0);
    }

    [Fact]
    internal void GetEnumerator_IteratesCorrectly_WithNonEmptyCollection()
    {
        // arrange
        var collection = new MenuItemCollection
        {
            new MenuItem { InnerText = "first" },
            new MenuItem { InnerText = "second" }
        };
        var count = 0;

        // act
        foreach (var item in collection)
            count++;

        // assert
        collection.Count.Should().Be(2);
        count.Should().Be(collection.Count);
    }
}
