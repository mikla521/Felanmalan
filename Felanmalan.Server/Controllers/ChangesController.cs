using Felanmalan.Server.Contracts;
using Felanmalan.Server.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Felanmalan.Server.Controllers;

[Authorize(Roles = "Support,Manager")]
[ApiController]
[Route("api/felanmalan")]
public class ChangesController : ControllerBase
{
    private readonly FelanmalanDbContext _context;

    public ChangesController(FelanmalanDbContext context)
    {
        _context = context;
    }

    [HttpGet("changes")]
    [ProducesResponseType(typeof(List<ChangeDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ChangeDto>>> GetAll()
    {
        var changes = await _context.Changes
            .AsNoTracking()
            .OrderByDescending(change => change.TimeStamp)
            .ThenByDescending(change => change.Id)
            .Select(change => new ChangeDto
            {
                Id = change.Id,
                UserId = change.UserId,
                TimeStamp = change.TimeStamp,
                Change = change.Change,
                TicketId = change.TicketId
            })
            .ToListAsync();

        return Ok(changes);
    }

    [HttpGet("{ticketId:int}/changes")]
    [ProducesResponseType(typeof(List<ChangeDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ChangeDto>>> GetByTicketId(int ticketId)
    {
        var changes = await _context.Changes
            .AsNoTracking()
            .Where(change => change.TicketId == ticketId)
            .OrderByDescending(change => change.TimeStamp)
            .ThenByDescending(change => change.Id)
            .Select(change => new ChangeDto
            {
                Id = change.Id,
                UserId = change.UserId,
                TimeStamp = change.TimeStamp,
                Change = change.Change,
                TicketId = change.TicketId
            })
            .ToListAsync();

        return Ok(changes);
    }
}
