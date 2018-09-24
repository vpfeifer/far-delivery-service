using DeliveryService.Core.Entities;
using DeliveryService.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointsController : EntityController<Point>
    {
        private readonly IPointRepository _repository;
        public PointsController(IPointRepository repository)
            : base(repository)
        {
            _repository = repository;
        }

        internal override void UpdateValues(Point currentObject, Point updatedObject)
        {
            currentObject.Name = updatedObject.Name;
        }

        public override async Task<ActionResult<Point>> GetAsync([FromRoute]long id)
        {
            var item = await _repository.GetWithRoutesAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }
    }
}