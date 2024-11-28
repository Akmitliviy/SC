using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using WarehouseManagement.Context;
using WarehouseManagement.Models;

namespace WarehouseManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreateController : ControllerBase
    {
        private readonly MyDbContext _context;

        public CreateController(MyDbContext context)
        {
            _context = context;
        }

        // POST: api/Create
        [HttpPost]
        public async Task<IActionResult> CreateEntity(string type, [FromBody] Dictionary<string, object> values)
        {
            var entityType = Type.GetType($"WarehouseManagement.Models.{type}");
            if (entityType == null)
                return NotFound("Сутність не знайдена");

            var entity = Activator.CreateInstance(entityType);

            foreach (var property in entityType.GetProperties())
            {
                if (values.ContainsKey(property.Name))
                {
                    // Пропуск Id та колекційних властивостей
                    if (property.Name == "Id" ||
                        (typeof(System.Collections.IEnumerable).IsAssignableFrom(property.PropertyType) &&
                        property.PropertyType != typeof(string)))
                    {
                        continue;
                    }

                    var propertyType = property.PropertyType;
                    var propertyValue = values[property.Name];

                    // Перевірка, чи властивість є nullable типу
                    if (Nullable.GetUnderlyingType(propertyType) != null)
                    {
                        propertyType = Nullable.GetUnderlyingType(propertyType);
                    }

                    try
                    {
                        object convertedValue = null;

                        if (propertyValue != null)
                        {
                            // Спробуйте привести значення до типу властивості
                            if (propertyValue is JsonElement jsonElement)
                            {
                                // Обробка JsonElement, якщо ви отримали його
                                convertedValue = jsonElement.Deserialize(propertyType);
                            }
                            else
                            {
                                convertedValue = Convert.ChangeType(propertyValue, propertyType);
                            }
                        }

                        property.SetValue(entity, convertedValue);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest($"Помилка при перетворенні значення для властивості {property.Name}: {ex.Message}");
                    }
                }
            }

            try
            {
                _context.Add(entity);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(CreateEntity), new { type = entityType.Name, id = entity.GetType().GetProperty("Id")?.GetValue(entity) }, entity);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Помилка збереження: {ex.Message}");
            }
        }
    }
}