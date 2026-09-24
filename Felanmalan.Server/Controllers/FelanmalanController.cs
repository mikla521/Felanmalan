using Felanmalan.Server.Contracts;
using Felanmalan.Server.Data;
using Felanmalan.Server.Entities;
using Felanmalan.Server.Entities.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Felanmalan.Server.Controllers;

[ApiController]
[Route("api/felanmalan")]
public class FelanmalanController : ControllerBase
{
    private readonly FelanmalanDbContext _context;

    public FelanmalanController(FelanmalanDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Ticket), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
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
}
