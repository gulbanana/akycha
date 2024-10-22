using System.ComponentModel.DataAnnotations;

namespace Akycha.Model;

public class Machine : IOrdered<Machine>
{
    [Key] public int Id { get; set; }
    public string Name { get; set; } = "New Machine";
    public byte[]? Icon { get; set; }
    public int PowerUsageMegawatts { get; set; }

    public static string GetKey(Machine t) => t.Name;
}