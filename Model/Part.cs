using System.ComponentModel.DataAnnotations;

public class Part
{
    [Key] public int Id { get; set; }
    public required string Name { get; set; }
    public byte[]? Icon { get; set; }
}