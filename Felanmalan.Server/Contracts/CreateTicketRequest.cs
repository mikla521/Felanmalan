using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Felanmalan.Server.Entities.Enums;

namespace Felanmalan.Server.Contracts;

public class CreateTicketRequest
{
    [Required(ErrorMessage = "Beskrivning måste anges.")]
    public string Description { get; set; } = string.Empty;

    // ? gör att vi kan upptäcka om användaren inte har valt någon kategori.
    [Required(ErrorMessage = "Kategori måste anges.")]
    [EnumDataType(typeof(TicketCategory), ErrorMessage = "Kategorin är ogiltig.")]
    // Gör att API:t kan ta emot kategorins namn, till exempel "Hardware".
    [JsonConverter(typeof(JsonStringEnumConverter<TicketCategory>))]
    public TicketCategory? Category { get; set; }
}
