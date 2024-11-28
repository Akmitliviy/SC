using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WarehouseManagement.Context;
using WarehouseManagement.Models;

namespace WarehouseManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeleteController : ControllerBase
    {
        private readonly MyDbContext _context;

        public DeleteController(MyDbContext context)
        {
            _context = context;
        }

        // POST: api/Delete
        [HttpDelete]
        public async Task<IActionResult> DeleteEntity(string type, int id)
        {
            try
            {
                var entityType = Type.GetType($"WarehouseManagement.Models.{type}");
                if (entityType == null)
                    return NotFound(new { success = false, message = "Сутність не знайдена" });

                var dbSetProperty = typeof(MyDbContext).GetProperty(type + "s");
                if (dbSetProperty == null)
                    return NotFound(new { success = false, message = "Таблиця не знайдена" });

                dynamic dbSet = dbSetProperty.GetValue(_context);
                var entity = await dbSet.FindAsync(id);

                if (entity == null)
                    return NotFound(new { success = false, message = "Запис не знайдено" });

                dbSet.Remove(entity);
                await _context.SaveChangesAsync();

                return Ok(new { success = true, message = "Запис успішно видалено" });
            }
            catch (DbUpdateException)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Неможливо видалити запис через зв'язки з іншими таблицями"
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Сталася помилка при видаленні запису"
                });
            }
        }
    }
}