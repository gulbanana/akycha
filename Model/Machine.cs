using System.ComponentModel.DataAnnotations;
using Akycha.Components.Pages;

namespace Akycha.Model;

public class Machine : IListable<Machine>
{
    [Key] public int Id { get; set; }
    public string Name { get; set; } = "New Machine";
    public string Category => "Machines";
    public byte[]? Icon { get; set; }
    public int PowerUsageMegawatts { get; set; }

    public static IOrderedEnumerable<Machine> Sort(IEnumerable<Machine> ms)
    {
        return ms.OrderBy(m => m.PowerUsageMegawatts).ThenBy(m => m.Name);
    }

    public double CalculateUsage(double clockSpeed)
    {
        return PowerUsageMegawatts * Math.Pow(clockSpeed/100.0, 1.321928);
    }
}