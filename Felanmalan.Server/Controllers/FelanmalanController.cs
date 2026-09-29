using Felanmalan.Server.Contracts;
using Felanmalan.Server.Data;
using Felanmalan.Server.DTOs;
using Felanmalan.Server.Entities;
using Felanmalan.Server.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Felanmalan.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/felanmalan")]
public class FelanmalanController : ControllerBase
{
    private readonly FelanmalanDbContext _context;

    public FelanmalanController(FelanmalanDbContext context)
    {
        _context = context;
    }

    [Authorize(Roles = "Support,Manager")]
    [HttpGet]
    [ProducesResponseType(typeof(List<TicketListItemDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<TicketListItemDto>>> GetAll()
    {
        var tickets = await _context.Ticket
            .AsNoTracking()
            .OrderByDescending(ticket => ticket.TimeCreated)
            .ThenByDescending(ticket => ticket.Id)
            .Select(ticket => new TicketListItemDto
            {
                Id = ticket.Id,
                Description = ticket.Description,
                Category = ticket.Category,
                Status = ticket.Status,
                TimeStarted = ticket.TimeStarted,
                TimeCreated = ticket.TimeCreated
            })
            .ToListAsync();

        return Ok(tickets);
    }

    [Authorize(Roles = "Manager")] //Tänker att enbart manager kan se all denna info
    [HttpGet("manager/{ticketId}")]
    public async Task<ActionResult<TicketManagerDto>> GetTicket(int ticketId)
    {
        var ticket = await _context.Ticket
            .FirstOrDefaultAsync(t => t.Id == ticketId);
        if (ticket == null)
            return NotFound();

        var ticketDto = new TicketManagerDto
        {
            Id = ticket.Id,
            TimeCreated = ticket.TimeCreated,
            TimeStarted = ticket.TimeStarted
        };


        return Ok(ticketDto);
    }

    [HttpGet("statistics/response-time")]
    public async Task<ActionResult<List<AverageResponseTimeDTO>>> GetAverageResponseTime()
    {
        var statistics = await _context.Ticket
            .AsNoTracking()
            .Where(ticket => ticket.TimeStarted != null)
            .GroupBy(ticket => ticket.Category)
            .Select(group => new AverageResponseTimeDTO
            {
                Category = group.Key,
                AverageResponseTimeInMinutes = group
                    .Average(ticket => EF.Functions.DateDiffMinute(
                        ticket.TimeCreated,
                        ticket.TimeStarted!.Value))
            })
            .ToListAsync();

        return Ok(statistics);
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(Ticket), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create([FromBody] CreateTicketRequest request)
    {
        // [ApiController] returnerar 400 automatiskt om indata är ogiltig.
        var ticket = new Ticket
        {
            Description = request.Description.Trim(),
            Category = TicketCategory.NotSet,
            Status = TicketStatus.New,
            TimeCreated = DateTime.UtcNow
        };

        _context.Ticket.Add(ticket);
        await _context.SaveChangesAsync();

        return StatusCode(StatusCodes.Status201Created, ticket);
    }

    [Authorize(Roles = "Support,Manager")]
    [HttpPut("{ticketId}/Category")]
    [ProducesResponseType(typeof(Ticket), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCategory(int ticketId, [FromBody] UpdateTicketCategoryRequest request)
    {
        var ticket = await _context.Ticket.FirstOrDefaultAsync(t => t.Id == ticketId);

        if (ticket == null)
            return NotFound();

        ticket.Category = request.Category!.Value;
        await _context.SaveChangesAsync();

        return Ok(ticket);
    }

    [Authorize(Roles = "Support,Manager")]
    [HttpPut("{ticketId}/StatusInProgress")]
    [ProducesResponseType(typeof(Ticket), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> StatusInProgress(int ticketId)
    {
        var ticket = await _context.Ticket.FirstOrDefaultAsync(t => t.Id == ticketId);

        if (ticket == null)
            return NotFound();

        if (ticket.TimeStarted == null)
        {
            ticket.TimeStarted = DateTime.UtcNow;
        }

        ticket.Status = TicketStatus.InProgress;

        await _context.SaveChangesAsync();

        return StatusCode(StatusCodes.Status200OK);
    }

    [Authorize(Roles = "Support,Manager")]
    [HttpPut("{ticketId}/StatusResolved")]
    [ProducesResponseType(typeof(Ticket), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> StatusResolved(int ticketId)
    {
        var ticket = await _context.Ticket.FirstOrDefaultAsync(t => t.Id == ticketId);

        if (ticket == null)
            return NotFound();

        ticket.Status = TicketStatus.Resolved;

        await _context.SaveChangesAsync();

        return StatusCode(StatusCodes.Status200OK);
    }

    [Authorize(Roles = "Support,Manager")]
    // Ändrar status till Closed.
    [HttpPut("{ticketId}/StatusClosed")]
    [ProducesResponseType(typeof(Ticket), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> StatusClosed(int ticketId)
    {
        var ticket = await _context.Ticket.FirstOrDefaultAsync(t => t.Id == ticketId);

        if (ticket == null)
            return NotFound();

        ticket.Status = TicketStatus.Closed;

        await _context.SaveChangesAsync();

        return StatusCode(StatusCodes.Status200OK);
    }
}
