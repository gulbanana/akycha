using System.ComponentModel.DataAnnotations;

namespace Akycha.Model;

public class Recipe : IOrdered<Recipe>
{
    [Key] public int Id { get; set; }
    public int? MachineId { get; set; }

    public string Name { get; set; } = "New Recipe";

    public Machine? Machine { get; set; }
    public ICollection<Item> Items { get; } = [];

    public static string GetKey(Recipe t) => t.Name;
    public byte[]? GetIcon() => Items.Where(i => i.Role == ItemRole.Product).FirstOrDefault()?.Part?.Icon;
}