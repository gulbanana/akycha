using System.ComponentModel.DataAnnotations;

namespace Akycha.Model;

public class Part : INamed
{
    [Key] public int Id { get; set; }
    public required string Name { get; set; }
    public byte[]? Icon { get; set; }
}