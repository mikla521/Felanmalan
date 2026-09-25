using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Felanmalan.Server.Entities.Enums;

namespace Felanmalan.Server.Contracts;

public class UpdateTicketCategoryRequest
{
    [Required(ErrorMessage = "Kategori måste anges.")]
    [EnumDataType(typeof(TicketCategory), ErrorMessage = "Kategorin är ogiltig.")]
    [JsonConverter(typeof(JsonStringEnumConverter<TicketCategory>))]
    public TicketCategory? Category { get; set; }
}
