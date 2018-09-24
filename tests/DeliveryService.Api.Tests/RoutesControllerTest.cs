using System;
using System.Threading.Tasks;
using DeliveryService.Api.Controllers;
using DeliveryService.Core.Entities;
using DeliveryService.Core.Interfaces.Repositories;
using DeliveryService.Core.Interfaces.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DeliveryService.Api.Tests
{
    public class RoutesControllerTest
    {
        [Fact(DisplayName = "Should return 200 OK when find a min time delivery.")]
        public async Task Should_return_200_OK_when_find_a_min_time_deliveryAsync()
        {
            // Arrange
            var pointsMock = new Mock<IPointRepository>();
            pointsMock.Setup(p => p.GetWithRoutesAsync(It.IsAny<long>())).ReturnsAsync(new Point());

            var deliveryMock = new Mock<IDeliveryService>();
            deliveryMock.Setup(d => d.FindMinTimeRoutesAsync(It.IsAny<Point>(), It.IsAny<Point>()))
                                            .ReturnsAsync(new Delivery());

            var controller = new RoutesController(null, pointsMock.Object, deliveryMock.Object);

            // Act
            var actionResult = await controller.SearchAsync(1, 2);

            // Assert
            actionResult.Result.Should().BeOfType<OkObjectResult>();
            var okResult = actionResult.Result as OkObjectResult;
            okResult.Value.Should().BeOfType<Delivery>();
        }

        [Fact(DisplayName = "Should return NotFound when there is no routes from start point to end point.")]
        public async Task Should_return_NotFound_when_there_is_no_routes_from_start_point_to_end_pointAsync()
        {
            // Arrange
            var pointsMock = new Mock<IPointRepository>();
            pointsMock.Setup(p => p.GetWithRoutesAsync(It.IsAny<long>())).ReturnsAsync(new Point());

            var deliveryMock = new Mock<IDeliveryService>();
            deliveryMock.Setup(d => d.FindMinTimeRoutesAsync(It.IsAny<Point>(), It.IsAny<Point>()))
                                            .ReturnsAsync((Delivery)null);

            var controller = new RoutesController(null, pointsMock.Object, deliveryMock.Object);

            // Act
            var actionResult = await controller.SearchAsync(1, 2);

            // Assert
            actionResult.Value.Should().BeNull();
            actionResult.Result.Should().BeOfType<NotFoundObjectResult>();
            
            var notFoundResult = actionResult.Result as NotFoundObjectResult;
            notFoundResult.Value.Should().Be("No routes found from 1 to 2.");
        }

        [Fact(DisplayName = "Should return NotFound when start point not exists.")]
        public async Task Should_return_NotFound_when_start_point_not_existsAsync()
        {
            // Arrange
            var pointsMock = new Mock<IPointRepository>();
            pointsMock.Setup(p => p.GetWithRoutesAsync(It.Is<long>(id => id == 0))).ReturnsAsync((Point)null);
            pointsMock.Setup(p => p.GetWithRoutesAsync(It.Is<long>(id => id == 1))).ReturnsAsync(new Point());

            var controller = new RoutesController(null, pointsMock.Object, null);

            // Act
            var actionResult = await controller.SearchAsync(0, 1);

            // Assert
            actionResult.Value.Should().BeNull();
            actionResult.Result.Should().BeOfType<NotFoundObjectResult>();

            var notFoundResult = actionResult.Result as NotFoundObjectResult;
            notFoundResult.Value.Should().Be("The start point 0 was not found.");
        }

        [Fact(DisplayName = "Should return NotFound when destination point not exists.")]
        public async Task Should_return_NotFound_when_destination_point_not_existsAsync()
        {
            // Arrange
            var pointsMock = new Mock<IPointRepository>();
            pointsMock.Setup(p => p.GetWithRoutesAsync(It.Is<long>(id => id == 0))).ReturnsAsync((Point)null);
            pointsMock.Setup(p => p.GetWithRoutesAsync(It.Is<long>(id => id == 1))).ReturnsAsync(new Point());

            var controller = new RoutesController(null, pointsMock.Object, null);

            // Act
            var actionResult = await controller.SearchAsync(1, 0);

            // Assert
            actionResult.Value.Should().BeNull();
            actionResult.Result.Should().BeOfType<NotFoundObjectResult>();
            
            var notFoundResult = actionResult.Result as NotFoundObjectResult;
            notFoundResult.Value.Should().Be("The destination point 0 was not found.");
        }
    }
}