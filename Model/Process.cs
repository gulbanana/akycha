using System.ComponentModel.DataAnnotations;

namespace Akycha.Model;

public class Process
{
    [Key] public int Id { get; set; }
    public int FacilityId { get; set; }
    public int? RecipeId { get; set; }

    public int QuantityMachines { get; set; }

    public required Facility Facility { get; set; }
    public Recipe? Recipe { get; set; }
}