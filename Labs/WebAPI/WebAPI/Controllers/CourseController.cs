using Microsoft.EntityFrameworkCore;
using WebAPI.Context;
using WebAPI.Migrations;
using WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
    private readonly UniversityContext _context;
    private readonly IMapper _mapper;

    public CourseController(UniversityContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var courses = await _context.courses.ToListAsync();
        var courseViewModels = _mapper.Map<List<CourseViewModel>>(courses);
        return Ok(courseViewModels);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var course = await _context.courses.FindAsync(id);
        if (course == null) return NotFound();

        var courseViewModel = _mapper.Map<CourseViewModel>(course);
        return Ok(courseViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CourseViewModel model)
    {
        var course = _mapper.Map<course>(model);
        _context.courses.Add(course);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = course.course_id }, model);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, CourseViewModel model)
    {
        var course = await _context.courses.FindAsync(id);
        if (course == null) return NotFound();

        _mapper.Map(model, course);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var course = await _context.courses.FindAsync(id);
        if (course == null) return NotFound();

        _context.courses.Remove(course);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}