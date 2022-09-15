using System.Threading.Tasks;
using FluentAssertions;
using Grape.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using FakeItEasy;
using Grape.API.Services;
using Grape.API.Models;

namespace Grape.UnitTests.Systems.Controllers;

public class TestCatsController {
    [Fact]
    public async Task Get_OnSuccess_ReturnsCode200() {
        // Arrange
        var mockService = A.Fake<ICatsService>();
        var sut = new CatsController(mockService);

        // Act
        var result = (OkObjectResult) await sut.Get();

        // Assert
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Get_OnSuccess_InvokesCatsServiceOnceExactly() {
        // Arrange
        var mockService = A.Fake<ICatsService>();
        A.CallTo(() => mockService.FetchFact()).Returns(Task.FromResult<CatFact>(null));

        var sut = new CatsController(mockService);

        // Act
        var result = await sut.Get();

        // Assert
        A.CallTo(() => mockService.FetchFact()).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task Get_OnSuccess_ReturnsACatFact() {
        // Arrange
        var mockService = A.Fake<ICatsService>();
        A.CallTo(() => mockService.FetchFact()).Returns(Task.FromResult<CatFact>(new CatFact()));

        var sut = new CatsController(mockService);

        // Act
        var result = await sut.Get();

        // Assert
        result.Should().BeOfType<OkObjectResult>();

        var objectResult = (OkObjectResult) result;
        objectResult.Value.Should().BeOfType<CatFact>();
    }

    [Fact]
    public async Task Get_OnCatFactNull_Returns404() {
        // Arrange
        var mockService = A.Fake<ICatsService>();
        A.CallTo(() => mockService.FetchFact()).Returns(Task.FromResult<CatFact>(null));

        var sut = new CatsController(mockService);

        // Act
        var result = await sut.Get();

        // Assert
        result.Should().BeOfType<NotFoundResult>();
    }
}