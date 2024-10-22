using System.ComponentModel.DataAnnotations;

namespace Akycha.Model;

public class Part : IOrdered<Part>
{
    [Key] public int Id { get; set; }
    
    public string Name { get; set; } = "New Part";
    public string Category { get; set; } = "Standard Parts";
    public byte[]? Icon { get; set; }

    public static string GetKey(Part t) => t.Name;
}