using Microsoft.EntityFrameworkCore;
using WebAPI.Context;
using WebAPI.Migrations;
using WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpecialtyCourseController : ControllerBase
{
    private readonly UniversityContext _context;
    private readonly IMapper _mapper;

    public SpecialtyCourseController(UniversityContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var specialtyCourses = await _context.specialty_courses.ToListAsync();
        var specialtyCourseViewModels = _mapper.Map<List<SpecialtyCourseViewModel>>(specialtyCourses);
        return Ok(specialtyCourseViewModels);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var specialtyCourse = await _context.specialty_courses.FindAsync(id);
        if (specialtyCourse == null) return NotFound();

        var specialtyCourseViewModel = _mapper.Map<SpecialtyCourseViewModel>(specialtyCourse);
        return Ok(specialtyCourseViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(SpecialtyCourseViewModel model)
    {
        var specialtyCourse = _mapper.Map<specialty_course>(model);
        _context.specialty_courses.Add(specialtyCourse);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = specialtyCourse.specialty_course_id }, model);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, SpecialtyCourseViewModel model)
    {
        var specialtyCourse = await _context.specialty_courses.FindAsync(id);
        if (specialtyCourse == null) return NotFound();

        _mapper.Map(model, specialtyCourse);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var specialtyCourse = await _context.specialty_courses.FindAsync(id);
        if (specialtyCourse == null) return NotFound();

        _context.specialty_courses.Remove(specialtyCourse);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
