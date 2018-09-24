using DeliveryService.Core.Entities;
using DeliveryService.Core.Extensions;
using DeliveryService.Core.Interfaces.Repositories;
using DeliveryService.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoutesController : EntityController<Route>
    {
        private readonly IPointRepository _points;
        private readonly IDeliveryService _delivery;

        public RoutesController(
            IRepository<Route> repository,
            IPointRepository points,
            IDeliveryService delivery)
            : base(repository)
        {
            _points = points;
            _delivery = delivery;
        }

        internal override void UpdateValues(Route currentObject, Route updatedObject)
        {
            currentObject.FromId = updatedObject.FromId;
            currentObject.ToId = updatedObject.ToId;
            currentObject.Time = updatedObject.Time;
            currentObject.Cost = updatedObject.Cost;
        }

        [HttpGet("search")]
        public async Task<ActionResult<Delivery>> SearchAsync([FromQuery]long fromId, [FromQuery]long toId)
        {
            var currentPosition = await _points.GetWithRoutesAsync(fromId);
            if (currentPosition == null)
            {
                return NotFound($"The start point {fromId} was not found.");
            }

            var destination = await _points.GetWithRoutesAsync(toId);
            if (destination == null)
            {
                return NotFound($"The destination point {toId} was not found.");
            }

            var delivery = await _delivery.FindMinTimeRoutesAsync(currentPosition, destination);
            if (delivery == null)
            {
                return NotFound($"No routes found from {fromId} to {toId}.");
            }

            return Ok(delivery);
        }
    }
}