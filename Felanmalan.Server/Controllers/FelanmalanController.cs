using Felanmalan.Server.Contracts;
using Felanmalan.Server.Data;
using Felanmalan.Server.Entities;
using Felanmalan.Server.Entities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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

    [Authorize(Roles = "Manager")]
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

    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(Ticket), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create([FromBody] CreateTicketRequest request)
    {
        var ticket = new Ticket
        {
            Description = request.Description.Trim(),
            Category = TicketCategory.NotSet,
            Status = TicketStatus.New,
            TimeCreated = DateTime.UtcNow
        };

        _context.Ticket.Add(ticket);

        AddChange(ticket, $"Skapad: {ticket.Description}");

        await _context.SaveChangesAsync();

        return StatusCode(StatusCodes.Status201Created, ticket);
    }

    [Authorize(Roles = "Support,Manager")]
    [HttpPut("{ticketId}/Category")]
    [ProducesResponseType(typeof(Ticket), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCategory(
        int ticketId,
        [FromBody] UpdateTicketCategoryRequest request)
    {
        var ticket = await _context.Ticket
            .FirstOrDefaultAsync(t => t.Id == ticketId);

        if (ticket == null)
            return NotFound();

        var oldCategory = ticket.Category;

        ticket.Category = request.Category!.Value;

        AddChange(
            ticket,
            $"Kategori: {oldCategory} → {ticket.Category}");

        await _context.SaveChangesAsync();

        return Ok(ticket);
    }

    [Authorize(Roles = "Support,Manager")]
    [HttpPut("{ticketId}/StatusInProgress")]
    [ProducesResponseType(typeof(Ticket), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> StatusInProgress(int ticketId)
    {
        var ticket = await _context.Ticket
            .FirstOrDefaultAsync(t => t.Id == ticketId);

        if (ticket == null)
            return NotFound();

        var oldStatus = ticket.Status;

        if (ticket.TimeStarted == null)
        {
            ticket.TimeStarted = DateTime.UtcNow;
        }

        ticket.Status = TicketStatus.InProgress;

        AddChange(
            ticket,
            $"Status: {oldStatus} → {ticket.Status}");

        await _context.SaveChangesAsync();

        return StatusCode(StatusCodes.Status200OK, ticket);
    }

    [Authorize(Roles = "Support,Manager")]
    [HttpPut("{ticketId}/StatusResolved")]
    [ProducesResponseType(typeof(Ticket), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> StatusResolved(int ticketId)
    {
        var ticket = await _context.Ticket
            .FirstOrDefaultAsync(t => t.Id == ticketId);

        if (ticket == null)
            return NotFound();

        var oldStatus = ticket.Status;

        ticket.Status = TicketStatus.Resolved;

        AddChange(
            ticket,
            $"Status: {oldStatus} → {ticket.Status}");

        await _context.SaveChangesAsync();

        return StatusCode(StatusCodes.Status200OK, ticket);
    }

    [Authorize(Roles = "Support,Manager")]
    [HttpPut("{ticketId}/StatusClosed")]
    [ProducesResponseType(typeof(Ticket), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> StatusClosed(int ticketId)
    {
        var ticket = await _context.Ticket
            .FirstOrDefaultAsync(t => t.Id == ticketId);

        if (ticket == null)
            return NotFound();

        var oldStatus = ticket.Status;

        ticket.Status = TicketStatus.Closed;

        AddChange(
            ticket,
            $"Status: {oldStatus} → {ticket.Status}");

        await _context.SaveChangesAsync();

        return StatusCode(StatusCodes.Status200OK, ticket);
    }

    private void AddChange(Ticket ticket, string change)
    {
        var userIdText = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(userIdText, out var userId))
            throw new InvalidOperationException("Inloggad användare saknar id.");

        _context.Changes.Add(new Changes
        {
            UserId = userId,
            TimeStamp = DateTime.UtcNow,
            Change = change,
            Ticket = ticket
        });
    }
}