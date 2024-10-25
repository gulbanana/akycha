using System.ComponentModel.DataAnnotations;

namespace Akycha.Model;

public class Part : IListable<Part>
{
    [Key] public int Id { get; set; }
    
    public string Name { get; set; } = "New Part";
    public string Category { get; set; } = "Standard Parts";
    public byte[]? Icon { get; set; }

    public static IOrderedEnumerable<Part> Sort(IEnumerable<Part> ps)
    {
        return ps.OrderBy(p => p.Category).ThenBy(p => p.Name);
    }
}