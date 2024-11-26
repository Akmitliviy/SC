using Microsoft.EntityFrameworkCore;
using WebAPI.Context;
using WebAPI.Migrations;
using WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AcademicYearPlanController : ControllerBase
{
    private readonly UniversityContext _context;
    private readonly IMapper _mapper;

    public AcademicYearPlanController(UniversityContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var academicYearPlans = await _context.academic_year_plans.ToListAsync();
        var academicYearPlansViewModels = _mapper.Map<List<AcademicYearPlanViewModel>>(academicYearPlans);
        return Ok(academicYearPlansViewModels);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var academicYearPlan = await _context.academic_year_plans.FindAsync(id);
        if (academicYearPlan == null) return NotFound();

        var academicYearPlanViewModel = _mapper.Map<AcademicYearPlanViewModel>(academicYearPlan);
        return Ok(academicYearPlanViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(AcademicYearPlanViewModel model)
    {
        var academicYearPlan = _mapper.Map<academic_year_plan>(model);
        _context.academic_year_plans.Add(academicYearPlan);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = academicYearPlan.year_plan_id }, model);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, AcademicYearPlanViewModel model)
    {
        var academicYearPlan = await _context.academic_year_plans.FindAsync(id);
        if (academicYearPlan == null) return NotFound();

        _mapper.Map(model, academicYearPlan);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var academicYearPlan = await _context.academic_year_plans.FindAsync(id);
        if (academicYearPlan == null) return NotFound();

        _context.academic_year_plans.Remove(academicYearPlan);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
