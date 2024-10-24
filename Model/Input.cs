using System.ComponentModel.DataAnnotations;

namespace Akycha.Model;

public class Input
{
    [Key] public int Id { get; set; }
    public int? FromId { get; set; }
    public int? ToId { get; set; }
    public int? PartId { get; set; }

    public float QuantityPerMinute { get; set; }
    public string? TransportMethod { get; set; }

    public Facility? From { get; set; }
    public Facility? To { get; set; }
    public Part? Part { get; set; }
}