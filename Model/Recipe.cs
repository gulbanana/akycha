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
        return rs.OrderBy(r => r.Category)
            .ThenBy(r => r.Items.Where(i => i.Role == ItemRole.Product).Select(i => i.Part?.Name).FirstOrDefault() ?? "")
            .ThenBy(r => r.Name);
    }

    public IEnumerable<byte[]?> GetIngredientIcons()
    {
        var products = Items.Where(i => i.Role == ItemRole.Ingredient)
            .Select(i => i.Part)
            .Where(p => p != null);
        
        if (products.Any())
        {
            return Part.Sort(products!).Select(p => p.Icon);
        }
        else
        {
            return [null];
        }    
    }

    public IEnumerable<byte[]?> GetProductIcons()
    {
        var products = Items.Where(i => i.Role == ItemRole.Product)
            .Select(i => i.Part)
            .Where(p => p != null);

        var byproducts = Items.Where(i => i.Role == ItemRole.Byproduct)
            .Select(i => i.Part)
            .Where(p => p != null);

        if (products.Any())
        {
            return Part.Sort(products!).Concat(Part.Sort(byproducts!)).Select(p => p.Icon);
        }
        else
        {
            return [null];
        }
    }

    private Part? GetProduct()
    {
        return Items.Where(i => i.Role == ItemRole.Product).FirstOrDefault()?.Part;
    }

    public string CalculatePowerText()
    {
        if (Machine is null)
        {
            return "";
        }
        else
        {
            return $"{Machine.PowerUsageMegawatts}MW";
        }
    }
}