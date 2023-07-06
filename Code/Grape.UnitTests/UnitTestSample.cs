using Xunit;

namespace Grape.UnitTests;

public class UnitTestSample
{
    // Facts cannot be parameterized
    [Fact]
    public void TestFact() {
        // Arrange

        // Act

        // Assert
    }

    // Theories can be parameterized
    // This is done by using InlineData attributes
    [Theory]
    [InlineData("My Value")]
    public void TestTheory(string input) {}
}