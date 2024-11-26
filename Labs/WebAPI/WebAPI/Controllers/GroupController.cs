using Microsoft.EntityFrameworkCore;
using WebAPI.Context;
using WebAPI.Migrations;
using WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupController : ControllerBase
{
    private readonly UniversityContext _context;
    private readonly IMapper _mapper;

    public GroupController(UniversityContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var groups = await _context.groups.ToListAsync();
        var groupViewModels = _mapper.Map<List<GroupViewModel>>(groups);
        return Ok(groupViewModels);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var group = await _context.groups.FindAsync(id);
        if (group == null) return NotFound();

        var groupViewModel = _mapper.Map<GroupViewModel>(group);
        return Ok(groupViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(GroupViewModel model)
    {
        var group = _mapper.Map<group>(model);
        _context.groups.Add(group);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = group.group_id }, model);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, GroupViewModel model)
    {
        var group = await _context.groups.FindAsync(id);
        if (group == null) return NotFound();

        _mapper.Map(model, group);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var group = await _context.groups.FindAsync(id);
        if (group == null) return NotFound();

        _context.groups.Remove(group);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}