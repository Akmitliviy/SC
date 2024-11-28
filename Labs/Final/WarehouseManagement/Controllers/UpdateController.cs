using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WarehouseManagement.Context;
using WarehouseManagement.Models;

namespace WarehouseManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UpdateController : ControllerBase
    {
        private readonly MyDbContext _context;

        public UpdateController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Update/Edit?type=Client&id=1
        [HttpGet("Edit")]
        public async Task<IActionResult> Edit(string type, int id)
        {
            var entityType = Type.GetType($"WarehouseManagement.Models.{type}");
            if (entityType == null)
                return NotFound("Сутність не знайдена");

            var entity = await _context.FindAsync(entityType, id);
            if (entity == null)
                return NotFound("Запис не знайдено");

            var viewModel = new UpdateViewModel
            {
                EntityType = entityType.Name,
                Id = id,
                Properties = new Dictionary<string, object>()
            };

            foreach (var property in entityType.GetProperties())
            {
                viewModel.Properties[property.Name] = property.GetValue(entity);
            }

            return Ok(viewModel); // Повертаємо модель у форматі JSON
        }
         
        [HttpPost("UpdateEntity")]
        public async Task<IActionResult> UpdateEntity(string type, int id, [FromBody] Dictionary<string, string> updatedValues)
        {
            System.Console.WriteLine("UpdateEntity");
            var entityType = Type.GetType($"WarehouseManagement.Models.{type}");
            if (entityType == null)
                return NotFound("Сутність не знайдена");

            var entity = await _context.FindAsync(entityType, id);
            if (entity == null)
                return NotFound("Запис не знайдено");

            foreach (var property in entityType.GetProperties())
            {
                if (updatedValues.ContainsKey(property.Name))
                {
                    // Пропуск колекційних властивостей
                    if (typeof(System.Collections.IEnumerable).IsAssignableFrom(property.PropertyType) &&
                        property.PropertyType != typeof(string))
                    {
                        continue; // Пропускаємо, щоб уникнути помилки конвертації
                    }

                    var propertyType = property.PropertyType;
                    var propertyValue = updatedValues[property.Name];

                    // Перевірка, чи властивість є nullable типу
                    if (Nullable.GetUnderlyingType(propertyType) != null)
                    {
                        propertyType = Nullable.GetUnderlyingType(propertyType);
                    }

                    try
                    {
                        var convertedValue = string.IsNullOrEmpty(propertyValue)
                            ? null
                            : Convert.ChangeType(propertyValue, propertyType);

                        property.SetValue(entity, convertedValue);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Exception: {ex.Message}");
                        return BadRequest($"Помилка при перетворенні значення для властивості {property.Name}: {ex.Message}");
                    }
                }
            }

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Запис успішно оновлено." });
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "Помилка збереження змін");
            }

        }
    }
}