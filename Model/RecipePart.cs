using System.ComponentModel.DataAnnotations;
using Akycha.Model;

public enum RecipePartRole
{
    Ingredient,
    Product,
}

public class RecipePart
{
    [Key] public int Id { get; set; }
    public int RecipeId { get; set; }
    public int? PartId { get; set; }
    public RecipePartRole Role { get; set; }
    public float Quantity { get; set; }

    public required Recipe Recipe { get; set; }
    public Part? Part { get; set; }
}