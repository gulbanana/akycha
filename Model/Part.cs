using System.ComponentModel.DataAnnotations;

namespace Akycha.Model;

public class Part : IOrdered<Part, string>
{
    [Key] public int Id { get; set; }
    public required string Name { get; set; }
    public byte[]? Icon { get; set; }

    public static string GetKey(Part t) => t.Name;
}