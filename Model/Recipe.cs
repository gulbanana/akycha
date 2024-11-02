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

    public IEnumerable<Quantity> GetIngredientQuantities()
    {
        var products = Items.Where(i => i.Role == ItemRole.Ingredient)
            .Where(i => i.Part != null);

        if (products.Any())
        {
            return Item.Sort(products!)
                .Select(i => new Quantity(i.Part!.Icon, i.QuantityPerMinute));
        }
        else
        {
            return [new(null, null)];
        }    
    }

    public IEnumerable<Quantity> GetProductQuantities()
    {
        var products = Items.Where(i => i.Role == ItemRole.Product)
            .Where(i => i.Part != null);

        var byproducts = Items.Where(i => i.Role == ItemRole.Byproduct)
            .Where(i => i.Part != null);

        if (products.Any())
        {
            return Item.Sort(products!)
                .Concat(Item.Sort(byproducts!))
                .Select(i => new Quantity(i.Part!.Icon, i.QuantityPerMinute));
        }
        else
        {
            return [new(null, null)];
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