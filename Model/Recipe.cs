using System.ComponentModel.DataAnnotations;

namespace Akycha.Model;

public class Recipe : IOrdered<Recipe>
{
    [Key] public int Id { get; set; }
    public string Name { get; set; } = "New Recipe";
    public byte[]? Icon { get; set; }

    public static string GetKey(Recipe t) => t.Name;
}