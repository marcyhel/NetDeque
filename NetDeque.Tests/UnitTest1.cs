using NetDeque;
using Xunit;
using FluentAssertions;

namespace NetDeque.Tests
{
    public class DequeTests
    {
        [Fact]
        public void Constructor_ShouldCreateEmptyDeque()
        {
            // Arrange & Act
            var deque = new Deque<int>();

            // Assert
            deque.Count.Should().Be(0);
            deque.IsEmpty.Should().BeTrue();
        }

        [Fact]
        public void AddBeg_ShouldAddItemToBeginning()
        {
            // Arrange
            var deque = new Deque<int>();

            // Act
            deque.AddBeg(1);
            deque.AddBeg(2);

            // Assert
            deque.Count.Should().Be(2);
            deque.IsEmpty.Should().BeFalse();
            deque.PeekBeg().Should().Be(2);
            deque.PeekEnd().Should().Be(1);
        }

        [Fact]
        public void AddEnd_ShouldAddItemToEnd()
        {
            // Arrange
            var deque = new Deque<int>();

            // Act
            deque.AddEnd(1);
            deque.AddEnd(2);

            // Assert
            deque.Count.Should().Be(2);
            deque.IsEmpty.Should().BeFalse();
            deque.PeekBeg().Should().Be(1);
            deque.PeekEnd().Should().Be(2);
        }

        [Fact]
        public void AddBeg_AndAddEnd_ShouldWorkTogether()
        {
            // Arrange
            var deque = new Deque<string>();

            // Act
            deque.AddBeg("middle");
            deque.AddBeg("first");
            deque.AddEnd("last");

            // Assert
            deque.Count.Should().Be(3);
            deque.PeekBeg().Should().Be("first");
            deque.PeekEnd().Should().Be("last");
        }

        [Fact]
        public void RemBeg_ShouldRemoveAndReturnFirstItem()
        {
            // Arrange
            var deque = new Deque<int>();
            deque.AddBeg(1);
            deque.AddBeg(2);
            deque.AddBeg(3);

            // Act
            var result = deque.RemBeg();

            // Assert
            result.Should().Be(3);
            deque.Count.Should().Be(2);
            deque.PeekBeg().Should().Be(2);
        }

        [Fact]
        public void RemEnd_ShouldRemoveAndReturnLastItem()
        {
            // Arrange
            var deque = new Deque<int>();
            deque.AddEnd(1);
            deque.AddEnd(2);
            deque.AddEnd(3);

            // Act
            var result = deque.RemEnd();

            // Assert
            result.Should().Be(3);
            deque.Count.Should().Be(2);
            deque.PeekEnd().Should().Be(2);
        }

        [Fact]
        public void RemBeg_OnEmptyDeque_ShouldThrowException()
        {
            // Arrange
            var deque = new Deque<int>();

            // Act & Assert
            deque.Invoking(d => d.RemBeg())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("Deque is empty.");
        }

        [Fact]
        public void RemEnd_OnEmptyDeque_ShouldThrowException()
        {
            // Arrange
            var deque = new Deque<int>();

            // Act & Assert
            deque.Invoking(d => d.RemEnd())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("Deque is empty.");
        }

        [Fact]
        public void PeekBeg_ShouldReturnFirstItemWithoutRemoving()
        {
            // Arrange
            var deque = new Deque<string>();
            deque.AddBeg("first");
            deque.AddEnd("last");

            // Act
            var result = deque.PeekBeg();

            // Assert
            result.Should().Be("first");
            deque.Count.Should().Be(2); // Count should remain unchanged
            deque.PeekBeg().Should().Be("first"); // Should still be there
        }

        [Fact]
        public void PeekEnd_ShouldReturnLastItemWithoutRemoving()
        {
            // Arrange
            var deque = new Deque<string>();
            deque.AddBeg("first");
            deque.AddEnd("last");

            // Act
            var result = deque.PeekEnd();

            // Assert
            result.Should().Be("last");
            deque.Count.Should().Be(2); // Count should remain unchanged
            deque.PeekEnd().Should().Be("last"); // Should still be there
        }

        [Fact]
        public void PeekBeg_OnEmptyDeque_ShouldThrowException()
        {
            // Arrange
            var deque = new Deque<int>();

            // Act & Assert
            deque.Invoking(d => d.PeekBeg())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("Deque is empty.");
        }

        [Fact]
        public void PeekEnd_OnEmptyDeque_ShouldThrowException()
        {
            // Arrange
            var deque = new Deque<int>();

            // Act & Assert
            deque.Invoking(d => d.PeekEnd())
                .Should().Throw<InvalidOperationException>()
                .WithMessage("Deque is empty.");
        }

        [Fact]
        public void Count_ShouldReflectNumberOfItems()
        {
            // Arrange
            var deque = new Deque<int>();

            // Act & Assert
            deque.Count.Should().Be(0);

            deque.AddBeg(1);
            deque.Count.Should().Be(1);

            deque.AddEnd(2);
            deque.Count.Should().Be(2);

            deque.RemBeg();
            deque.Count.Should().Be(1);

            deque.RemEnd();
            deque.Count.Should().Be(0);
        }

        [Fact]
        public void IsEmpty_ShouldReturnCorrectValue()
        {
            // Arrange
            var deque = new Deque<int>();

            // Act & Assert
            deque.IsEmpty.Should().BeTrue();

            deque.AddBeg(1);
            deque.IsEmpty.Should().BeFalse();

            deque.RemBeg();
            deque.IsEmpty.Should().BeTrue();
        }

        [Fact]
        public void SingleItem_AllOperationsShouldWork()
        {
            // Arrange
            var deque = new Deque<char>();

            // Act & Assert
            deque.AddBeg('A');
            deque.Count.Should().Be(1);
            deque.PeekBeg().Should().Be('A');
            deque.PeekEnd().Should().Be('A');

            var removed = deque.RemEnd();
            removed.Should().Be('A');
            deque.IsEmpty.Should().BeTrue();
        }

        [Fact]
        public void ComplexOperations_ShouldMaintainCorrectOrder()
        {
            // Arrange
            var deque = new Deque<int>();

            // Act
            deque.AddEnd(1);    // [1]
            deque.AddBeg(2);    // [2, 1]
            deque.AddEnd(3);    // [2, 1, 3]
            deque.AddBeg(4);    // [4, 2, 1, 3]

            // Assert
            deque.Count.Should().Be(4);
            deque.PeekBeg().Should().Be(4);
            deque.PeekEnd().Should().Be(3);

            // Remove from both ends
            deque.RemBeg().Should().Be(4); // [2, 1, 3]
            deque.RemEnd().Should().Be(3);  // [2, 1]
            deque.RemBeg().Should().Be(2); // [1]
            deque.RemEnd().Should().Be(1);  // []

            deque.IsEmpty.Should().BeTrue();
        }

        [Fact]
        public void NullValues_ShouldBeHandledCorrectly()
        {
            // Arrange
            var deque = new Deque<string?>();

            // Act
            deque.AddBeg(null);
            deque.AddEnd("test");
            deque.AddBeg(null);

            // Assert
            deque.Count.Should().Be(3);
            deque.PeekBeg().Should().BeNull();
            deque.PeekEnd().Should().Be("test");

            deque.RemBeg().Should().BeNull();
            deque.RemBeg().Should().BeNull();
            deque.RemEnd().Should().Be("test");
        }
    }
}