using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akycha.Model;

public class Item
{
    [Key] public int Id { get; set; }
    public int RecipeId { get; set; }
    public int? PartId { get; set; }

    public ItemRole Role { get; set; }
    public float QuantityPerMinute { get; set; }

    public required Recipe Recipe { get; set; }
    public Part? Part { get; set; }

    [NotMapped]
    public bool IsByproduct
    {
        get => Role == ItemRole.Byproduct;
        set => Role = value ? ItemRole.Byproduct : ItemRole.Product;
    }

    public static IOrderedEnumerable<Item> Sort(IEnumerable<Item> ps)
    {
        return ps.OrderBy(p => p.Part?.Category).ThenBy(p => p.Part?.Name);
    }
}