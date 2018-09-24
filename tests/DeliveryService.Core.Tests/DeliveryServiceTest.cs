using System;
using Xunit;
using FluentAssertions;
using DeliveryService.Core.Entities;
using CoreServices = DeliveryService.Core.Services;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;
using DeliveryService.Core.Interfaces.Repositories;

namespace DeliveryService.Core.Tests
{
    public class DeliveryServiceTest
    {
        [Fact(DisplayName = "Should return null when start point has no routes.")]
        public async Task Should_return_null_when_start_point_has_no_routesAsync()
        {
            // Arrange
            var delivery = new CoreServices.DeliveryService(null);
            var from = new Point();

            // Act
            var result = await delivery.FindMinTimeRoutesAsync(from, It.IsAny<Point>());

            // Assert
            result.Should().BeNull();
        }

        [Fact(DisplayName = "Should return min time delivery in simple scenarios.")]
        public async Task Should_return_min_time_delivery_in_simple_scenariosAsync()
        {
            // Arrange
            var a = new Point
            {
                Name = "A",
                Routes = new List<Route>
                {
                    new Route { FromId = 1, ToId = 3, Time = 1, Cost = 20 },
                    new Route { FromId = 1, ToId = 5, Time = 30, Cost = 5 },
                    new Route { FromId = 1, ToId = 8, Time = 10, Cost = 1 },
                }
            };

            var b = new Point
            {
                Name = "B",
                Routes = new List<Route>()
            };

            var c = new Point
            {
                Name = "C",
                Routes = new List<Route> { new Route { FromId = 3, ToId = 2, Time = 1, Cost = 12 } }
            };

            var pointsMock = new Mock<IPointRepository>();
            pointsMock.Setup(p => p.GetWithRoutesAsync(It.Is<long>(id => id == 2))).ReturnsAsync(b);
            pointsMock.Setup(p => p.GetWithRoutesAsync(It.Is<long>(id => id == 3))).ReturnsAsync(c);

            var delivery = new CoreServices.DeliveryService(pointsMock.Object);

            var expected = new Delivery
            {
                Routes = new List<Route>
                {
                    new Route { FromId = 1, ToId = 3, Time = 1, Cost = 20 },
                    new Route { FromId = 3, ToId = 2, Time = 1, Cost = 12 }
                },
                TotalTime = 2,
                TotalCost = 32
            };

            // Act
            var result = await delivery.FindMinTimeRoutesAsync(a, b);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact(DisplayName = "Should return min time delivery even in more complex scenarios.")]
        public async Task Should_return_min_time_delivery_even_in_more_complex_scenariosAsync()
        {
            // Arrange
            var a = new Point
            {
                Name = "A",
                Routes = new List<Route>
                {
                    new Route { FromId = 1, ToId = 3, Time = 1, Cost = 20 },
                    new Route { FromId = 1, ToId = 5, Time = 30, Cost = 5 },
                    new Route { FromId = 1, ToId = 8, Time = 10, Cost = 1 },
                }
            };

            var b = new Point
            {
                Name = "B",
                Routes = new List<Route>()
            };

            var c = new Point
            {
                Name = "C",
                Routes = new List<Route> { new Route { FromId = 3, ToId = 2, Time = 1, Cost = 12 } }
            };

            var d = new Point
            {
                Name = "D",
                Routes = new List<Route> { new Route { FromId = 4, ToId = 6, Time = 4, Cost = 50 } }
            };

            var e = new Point
            {
                Name = "E",
                Routes = new List<Route> { new Route { FromId = 5, ToId = 4, Time = 3, Cost = 5 } }
            };

            var f = new Point
            {
                Name = "F",
                Routes = new List<Route> 
                { 
                    new Route { FromId = 6, ToId = 7, Time = 40, Cost = 50 },
                    new Route { FromId = 6, ToId = 9, Time = 45, Cost = 50 }
                }
            };

            var g = new Point
            {
                Name = "G",
                Routes = new List<Route> { new Route { FromId = 7, ToId = 2, Time = 64, Cost = 73 } }
            };

            var h = new Point
            {
                Name = "H",
                Routes = new List<Route> { new Route { FromId = 8, ToId = 5, Time = 30, Cost = 1 } }
            };

            var i = new Point
            {
                Name = "I",
                Routes = new List<Route> { new Route { FromId = 9, ToId = 2, Time = 65, Cost = 5 } }
            };
            

            var pointsMock = new Mock<IPointRepository>();
            pointsMock.Setup(p => p.GetWithRoutesAsync(It.Is<long>(id => id == 1))).ReturnsAsync(a);
            pointsMock.Setup(p => p.GetWithRoutesAsync(It.Is<long>(id => id == 2))).ReturnsAsync(b);
            pointsMock.Setup(p => p.GetWithRoutesAsync(It.Is<long>(id => id == 3))).ReturnsAsync(c);
            pointsMock.Setup(p => p.GetWithRoutesAsync(It.Is<long>(id => id == 4))).ReturnsAsync(d);
            pointsMock.Setup(p => p.GetWithRoutesAsync(It.Is<long>(id => id == 5))).ReturnsAsync(e);
            pointsMock.Setup(p => p.GetWithRoutesAsync(It.Is<long>(id => id == 6))).ReturnsAsync(f);
            pointsMock.Setup(p => p.GetWithRoutesAsync(It.Is<long>(id => id == 7))).ReturnsAsync(g);
            pointsMock.Setup(p => p.GetWithRoutesAsync(It.Is<long>(id => id == 8))).ReturnsAsync(h);
            pointsMock.Setup(p => p.GetWithRoutesAsync(It.Is<long>(id => id == 9))).ReturnsAsync(i);

            var delivery = new CoreServices.DeliveryService(pointsMock.Object);

            var expected = new Delivery
            {
                Routes = new List<Route>
                {
                    new Route { FromId = 1, ToId = 5, Time = 30, Cost = 5 },
                    new Route { FromId = 5, ToId = 4, Time = 3, Cost = 5 },
                    new Route { FromId = 4, ToId = 6, Time = 4, Cost = 50 },
                },
                TotalTime = 37,
                TotalCost = 60
            };

            // Act
            var result = await delivery.FindMinTimeRoutesAsync(a, f);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }
    }
}
