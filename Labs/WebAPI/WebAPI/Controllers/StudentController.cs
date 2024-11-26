using Microsoft.EntityFrameworkCore;
using WebAPI.Context;
using WebAPI.Migrations;
using WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly UniversityContext _context;
    private readonly IMapper _mapper;

    public StudentController(UniversityContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var students = await _context.students.ToListAsync();
        var studentViewModels = _mapper.Map<List<StudentViewModel>>(students);
        return Ok(studentViewModels);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var student = await _context.students.FindAsync(id);
        if (student == null) return NotFound();

        var studentViewModel = _mapper.Map<StudentViewModel>(student);
        return Ok(studentViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(StudentViewModel model)
    {
        var student = _mapper.Map<student>(model);
        _context.students.Add(student);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = student.student_id }, model);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, StudentViewModel model)
    {
        var student = await _context.students.FindAsync(id);
        if (student == null) return NotFound();

        _mapper.Map(model, student);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var student = await _context.students.FindAsync(id);
        if (student == null) return NotFound();

        _context.students.Remove(student);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}