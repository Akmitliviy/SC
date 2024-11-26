using Microsoft.EntityFrameworkCore;
using WebAPI.Context;
using WebAPI.Migrations;
using WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QRSessionController : ControllerBase
{
    private readonly UniversityContext _context;
    private readonly IMapper _mapper;

    public QRSessionController(UniversityContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var qrSessions = await _context.qr_sessions.ToListAsync();
        var qrSessionViewModels = _mapper.Map<List<QRSessionViewModel>>(qrSessions);
        return Ok(qrSessionViewModels);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var qrSessions = await _context.qr_sessions.FindAsync(id);
        if (qrSessions == null) return NotFound();

        var qrSessionViewModel = _mapper.Map<QRSessionViewModel>(qrSessions);
        return Ok(qrSessionViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Create(QRSessionViewModel model)
    {
        var qrSession = _mapper.Map<qr_session>(model);
        _context.qr_sessions.Add(qrSession);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = qrSession.qr_session_id }, model);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, QRSessionViewModel model)
    {
        var qrSession = await _context.qr_sessions.FindAsync(id);
        if (qrSession == null) return NotFound();

        _mapper.Map(model, qrSession);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var qrSession = await _context.qr_sessions.FindAsync(id);
        if (qrSession == null) return NotFound();

        _context.qr_sessions.Remove(qrSession);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
