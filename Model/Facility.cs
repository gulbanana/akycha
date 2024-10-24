using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akycha.Model;

public class Facility : IOrdered<Facility>
{
    [Key] public int Id { get; set; }

    public string Name { get; set; } = "New Facility";
    public string? Site { get; set; }
    public byte[]? Icon { get; set; }

    [InverseProperty(nameof(Input.To))] public ICollection<Input> Inputs { get; } = [];
    [InverseProperty(nameof(Input.From))] public ICollection<Input> Outputs { get; } = [];
    [InverseProperty(nameof(Process.Facility))] public ICollection<Process> Processes { get; set; } = [];

    public static string GetKey(Facility t) => t.Name;

    public IEnumerable<byte[]?> GetIcons()
    {
        var any = false;
        foreach (var p in Processes)
        {
            if (p.Recipe?.GetIcon() is byte[] processIcon)
            {
                any = true;
                yield return processIcon;
            }
        }
        
        if (!any)
        {
            yield return null;
        }
    }

    public IEnumerable<Part> CalculateLocalParts()
    {
        foreach (var input in Inputs)
        {
            if (input.Part is not null)
            {
                yield return input.Part;
            }
        }

        foreach (var process in Processes)
        {
            if (process.Recipe is not null)
            {
                foreach (var item in process.Recipe.Items)
                {
                    if (item.Part is not null && item.Role != ItemRole.Ingredient)
                    {
                        yield return item.Part;
                    }
                }
            }
        }
    }

    public IEnumerable<Input> CalculateMissingInputs()
    {
        foreach (var kvp in BalanceQuantities())
        {
            if (kvp.Value < 0)
            {
                yield return new Input
                {
                    From = this,
                    Part = kvp.Key,
                    QuantityPerMinute = kvp.Value * -1
                };
            }
        }
    }

    public IEnumerable<Input> CalculateExcessOutputs()
    {
        foreach (var kvp in BalanceQuantities())
        {
            if (kvp.Value > 0)
            {
                yield return new Input
                {
                    From = this,
                    Part = kvp.Key,
                    QuantityPerMinute = kvp.Value
                };
            }
        }
    }

    public string CalculatePowerText()
    {
        var mw = Processes.Select(p => p.CalculatePowerUsage()).Sum();

        if (mw < 1000)
        {
            return $"{mw}MW";
        }
        else
        {
            return $"{Math.Round((float)mw / 1000.0, 1)}GW";
        }
    }

    private IReadOnlyDictionary<Part, float> BalanceQuantities()
    {
        var result = new Dictionary<Part, float>();

        foreach (var input in Inputs)
        {
            if (input.Part is not null)
            {
                if (!result.ContainsKey(input.Part))
                {
                    result[input.Part] = 0;
                }
                
                result[input.Part] += input.QuantityPerMinute;
            }
        }

        foreach (var output in Outputs)
        {
            if (output.Part is not null)
            {
                if (!result.ContainsKey(output.Part))
                {
                    result[output.Part] = 0;
                }

                result[output.Part] -= output.QuantityPerMinute;
            }
        }

        foreach (var process in Processes)
        {
            if (process.Recipe is not null)
            {
                var quantity = 1.0f * process.QuantityMachines + 0.5f * process.PowerShards;
                foreach (var item in process.Recipe.Items)
                {
                    if (item.Part is not null)
                    {
                        if (!result.ContainsKey(item.Part))
                        {
                            result[item.Part] = 0f;
                        }

                        if (item.Role == ItemRole.Ingredient)
                        {
                            result[item.Part] -= quantity * item.QuantityPerMinute;
                        }
                        else
                        {
                            result[item.Part] += quantity * item.QuantityPerMinute;
                        }
                    }
                }
            }
        }
        
        return result;
    }
}