using System.ComponentModel.DataAnnotations;
using Akycha.Model;

public enum IngredientType
{
    Input,
    Output,
    Byproduct
}

public class Ingredient
{
    [Key] public int Id { get; set; }
    public int RecipeId { get; set; }
    public int PartId { get; set; }
    public IngredientType Type { get; set; }
    public short Quantity { get; set; }

    public required Recipe Recipe { get; set; }
    public required Part Part { get; set; }
}