using System.ComponentModel.DataAnnotations;

namespace Akycha.Model;

public class Recipe : IListable<Recipe>
{
    [Key] public int Id { get; set; }
    public int? MachineId { get; set; }

    public string Name { get; set; } = "New Recipe";
    public string Category => GetProduct()?.Category ?? "Recipes";

    public Machine? Machine { get; set; }
    public ICollection<Item> Items { get; } = [];

    public static IOrderedEnumerable<Recipe> Sort(IEnumerable<Recipe> rs)
    {
        return rs.OrderBy(r => r.Category).ThenBy(r => r.Name);
    }

    public byte[]? GetIcon()
    {
        return GetProduct()?.Icon;
    }

    private Part? GetProduct()
    {
        return Items.Where(i => i.Role == ItemRole.Product).FirstOrDefault()?.Part;
    }
}