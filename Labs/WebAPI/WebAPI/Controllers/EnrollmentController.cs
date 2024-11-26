using Microsoft.EntityFrameworkCore;
using WebAPI.Context;
using WebAPI.Migrations;
using WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnrollmentController : ControllerBase
{
    private readonly UniversityContext _context;
    private readonly IMapper _mapper;

    public EnrollmentController(UniversityContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var enrollments = await _context.enrollments.ToListAsync();
        var enrollmentViewModels = _mapper.Map<List<EnrollmentViewModel>>(enrollments);
        return Ok(enrollmentViewModels);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var enrollment = await _context.enrollments.FindAsync(id);
        if (enrollment == null) return NotFound();

        var enrollmentViewModel = _mapper.Map<EnrollmentViewModel>(enrollment);
        return Ok(enrollmentViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(EnrollmentViewModel model)
    {
        var enrollment = _mapper.Map<enrollment>(model);
        _context.enrollments.Add(enrollment);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = enrollment.enrollment_id }, model);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, EnrollmentViewModel model)
    {
        var enrollment = await _context.enrollments.FindAsync(id);
        if (enrollment == null) return NotFound();

        _mapper.Map(model, enrollment);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var enrollment = await _context.enrollments.FindAsync(id);
        if (enrollment == null) return NotFound();

        _context.enrollments.Remove(enrollment);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
