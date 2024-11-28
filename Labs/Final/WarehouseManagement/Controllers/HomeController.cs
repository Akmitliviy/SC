using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WarehouseManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using WarehouseManagement.Context;

namespace WarehouseManagement.Controllers
{
    [ApiController]
    [Route("api")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyDbContext _context;

        public HomeController(ILogger<HomeController> logger, MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Метод для отримання списку сутностей
        [HttpGet("entities")]
        public async Task<ActionResult<List<string>>> GetEntityTypes()
        {
            var entityTypes = typeof(MyDbContext).GetProperties()
                .Where(p => p.PropertyType.IsGenericType &&
                            p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                .Select(p => p.PropertyType.GenericTypeArguments[0].Name)
                .ToList();

            // Повертаємо список сутностей
            return Ok(entityTypes);
        }

        // Метод для отримання властивостей сутності за її типом
        [HttpGet("entities/{entityType}")]
        public ActionResult<List<string>> GetEntityProperties(string entityType)
        {
            // Знаходимо тип сутності за її ім'ям
            var type = typeof(MyDbContext).GetProperties()
                .Where(p => p.PropertyType.IsGenericType &&
                            p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                .Select(p => p.PropertyType.GenericTypeArguments[0])
                .FirstOrDefault(t => t.Name.Equals(entityType, StringComparison.OrdinalIgnoreCase));

            if (type == null)
            {
                return NotFound($"Entity type '{entityType}' not found.");
            }

            // Отримуємо властивості сутності
            var properties = type.GetProperties()
                .Select(p => p.Name)
                .ToList();

            // Повертаємо список властивостей
            return Ok(properties);
        }



        /*
        public IActionResult Index()
        {
            _logger.LogInformation("Home page accessed.");

            // Отримання типів сутностей з контексту бази даних
            var entityTypes = typeof(MyDbContext).GetProperties()
                .Where(p => p.PropertyType.IsGenericType &&
                            p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>))
                .Select(p => p.PropertyType.GenericTypeArguments[0].Name)
                .ToList();

            ViewBag.EntityTypes = entityTypes; // Передача типів сутностей у представлення

            return View();
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("Privacy page accessed.");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            _logger.LogError("An error occurred.");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }*/
    }
}