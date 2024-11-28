using Microsoft.AspNetCore.Mvc;
using WarehouseManagement.Services;

namespace WarehouseManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController<TEntity> : ControllerBase where TEntity : class
    {
        protected readonly IEntityService<TEntity> _service;

        public BaseApiController(IEntityService<TEntity> service)
        {
            _service = service;
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TEntity>>> GetAll()
        {
            var entities = await _service.GetAllAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TEntity>> GetById(int id)
        {
            var entity = await _service.GetByIdAsync(id);
            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        [HttpPost]
        public virtual async Task<ActionResult<TEntity>> Create([FromBody] TEntity entity)
        {
            var createdEntity = await _service.CreateAsync(entity);
            return CreatedAtAction(
                nameof(GetById),
                new { id = GetEntityId(createdEntity) },
                createdEntity);
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult<TEntity>> Update(int id, [FromBody] TEntity entity)
        {
            var updatedEntity = await _service.UpdateAsync(id, entity);
            if (updatedEntity == null)
                return NotFound();

            return Ok(updatedEntity);
        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        protected virtual int GetEntityId(TEntity entity)
        {
            // Отримуємо значення Id через рефлексію
            var idProperty = typeof(TEntity).GetProperty("Id");
            return (int)idProperty?.GetValue(entity);
        }
    }
}
