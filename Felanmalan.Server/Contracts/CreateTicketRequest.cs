using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Felanmalan.Server.Entities.Enums;

namespace Felanmalan.Server.Contracts;

public class CreateTicketRequest
{
    [Required(ErrorMessage = "Beskrivning måste anges.")]
    public string Description { get; set; } = string.Empty;
}
