using DeliveryService.Core.Entities;
using DeliveryService.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryService.Api.Controllers
{
    public abstract class EntityController<T> : ControllerBase
        where T : Entity
    {
        private readonly IRepository<T> _repository;

        public EntityController(IRepository<T> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<List<T>>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        [HttpGet("{id:long}")]
        public virtual async Task<ActionResult<T>> GetAsync([FromRoute]long id)
        {
            var item = await _repository.GetAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(T item)
        {
            var createdItem = await _repository.CreateAsync(item);
            return CreatedAtAction(nameof(GetAsync), new { id = createdItem.Id }, createdItem);
        }

        [HttpPut("{id:long}")]
        public async Task<IActionResult> UpdateAsync(long id, T item)
        {
            var currentObject = await _repository.GetAsync(id);
            if (currentObject == null)
            {
                return NotFound();
            }

            UpdateValues(currentObject, item);

            await _repository.UpdateAsync(currentObject);
            return NoContent();
        }

        internal abstract void UpdateValues(T currentObject, T updatedObject);

        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var item = await _repository.GetAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(item);
            return NoContent();
        }
    }
}