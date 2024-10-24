using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akycha.Model;

public class Facility : IOrdered<Facility>
{
    [Key] public int Id { get; set; }

    public string Name { get; set; } = "New Facility";
    public string? Site { get; set; }
    public byte[]? Icon { get; set; }

    [InverseProperty(nameof(Input.To))] public ICollection<Input> Inputs { get; } = [];
    [InverseProperty(nameof(Input.From))] public ICollection<Input> Outputs { get; } = [];
    [InverseProperty(nameof(Process.Facility))] public ICollection<Process> Processes { get; set; } = [];

    public static string GetKey(Facility t) => t.Name;
}