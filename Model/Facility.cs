using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akycha.Model;

public class Facility : IOrdered<Facility>
{
    [Key] public int Id { get; set; }
    public int? SiteId { get; set; }

    public string Name { get; set; } = "New Facility";
    public byte[]? Icon { get; set; }

    [InverseProperty(nameof(Lots))] public Facility? Site { get; set; }
    [InverseProperty(nameof(Site))] public ICollection<Facility> Lots { get; set; } = [];
    [InverseProperty(nameof(Input.To))] public ICollection<Input> Inputs { get; } = [];
    [InverseProperty(nameof(Input.From))] public ICollection<Input> Outputs { get; } = [];
    [InverseProperty(nameof(Process.Facility))] public ICollection<Process> Processes { get; set; } = [];

    public static string GetKey(Facility t) => t.Name;
}