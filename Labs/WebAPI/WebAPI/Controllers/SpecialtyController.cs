using Microsoft.EntityFrameworkCore;
using WebAPI.Context;
using WebAPI.Migrations;
using WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpecialtyController : ControllerBase
{
    private readonly UniversityContext _context;
    private readonly IMapper _mapper;

    public SpecialtyController(UniversityContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var specialties = await _context.specialties.ToListAsync();
        var specialtyViewModels = _mapper.Map<List<SpecialtyViewModel>>(specialties);
        return Ok(specialtyViewModels);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var specialty = await _context.specialties.FindAsync(id);
        if (specialty == null) return NotFound();

        var specialtyViewModel = _mapper.Map<SpecialtyViewModel>(specialty);
        return Ok(specialtyViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(SpecialtyViewModel model)
    {
        var specialty = _mapper.Map<specialty>(model);
        _context.specialties.Add(specialty);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = specialty.specialty_id }, model);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, SpecialtyViewModel model)
    {
        var specialty = await _context.specialties.FindAsync(id);
        if (specialty == null) return NotFound();

        _mapper.Map(model, specialty);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var specialty = await _context.specialties.FindAsync(id);
        if (specialty == null) return NotFound();

        _context.specialties.Remove(specialty);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
