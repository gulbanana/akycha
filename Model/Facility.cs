using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Akycha.Model;

public class Facility : IListable<Facility>
{
    [Key] public int Id { get; set; }

    public string Name { get; set; } = "New Facility";
    public string Category { get; set; } = "Facilities";
    public byte[]? Icon { get; set; }
    public string Notes { get; set; } = "";

    [InverseProperty(nameof(Input.To))] public ICollection<Input> Inputs { get; } = [];
    [InverseProperty(nameof(Input.From))] public ICollection<Input> Outputs { get; } = [];
    [InverseProperty(nameof(Process.Facility))] public ICollection<Process> Processes { get; set; } = [];

    public static IOrderedEnumerable<Facility> Sort(IEnumerable<Facility> fs)
    {
        return fs.OrderBy(f => f.Category).ThenBy(f => f.Name);
    }

    // registered + missing inputs
    public IEnumerable<Quantity> GetInputQuantities()
    {
        var amountsByPart = new Dictionary<Part, float>();

        foreach (var p in Inputs)
        {
            if (p.From is not null && p.Part is not null)
            {
                if (!amountsByPart.ContainsKey(p.Part))
                {
                    amountsByPart[p.Part] = 0f;
                }
                amountsByPart[p.Part] += p.QuantityPerMinute;
            }
        }

        foreach (var p in BalanceQuantities())
        {
            if (p.Value < 0)
            {
                if (!amountsByPart.ContainsKey(p.Key))
                {
                    amountsByPart[p.Key] = 0f;
                }
                amountsByPart[p.Key] -= p.Value;
            }
        }

        if (amountsByPart.Any())
        {
            return amountsByPart.Select(kvp => new Quantity(kvp.Key.Id, kvp.Value));
        }
        else
        {
            return [];
        }
    }

    // registered outputs + random excess
    public IEnumerable<Quantity> GetOutputQuantities()
    {
        var amountsByPart = new Dictionary<Part, float>();

        foreach (var p in Outputs)
        {
            if (p.To is not null && p.Part is not null)
            {
                if (!amountsByPart.ContainsKey(p.Part))
                {
                    amountsByPart[p.Part] = 0f;
                }
                amountsByPart[p.Part] += p.QuantityPerMinute;
            }
        }

        foreach (var p in BalanceQuantities())
        {
            if (p.Value > 0)
            {
                if (!amountsByPart.ContainsKey(p.Key))
                {
                    amountsByPart[p.Key] = 0f;
                }
                amountsByPart[p.Key] += p.Value;
            }
        }
        
        if (amountsByPart.Any())
        {
            return amountsByPart.Select(kvp => new Quantity(kvp.Key.Id, kvp.Value));
        }
        else
        {
            return [new(null, null)];
        }
    }

    public IEnumerable<Part> CalculateLocalParts()
    {
        var result = new HashSet<Part>(); 

        foreach (var input in Inputs)
        {
            if (input.Part is not null)
            {
                result.Add(input.Part);
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
                        result.Add(item.Part);
                    }
                }
            }
        }

        return result;
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

    public Dictionary<string, int> CalculateMachines()
    {
        var result = new Dictionary<string, int>();

        foreach (var process in Processes)
        {
            if (process.Recipe?.Machine is null)
            {
                continue;
            }

            var key = string.IsNullOrEmpty(process.Recipe.Machine.Plural) 
                ? process.Recipe.Machine.Name
                : process.Recipe.Machine.Plural;

            if (!result.ContainsKey(key))
            {
                result[key] = 0;
            }

            result[key] += (int)Math.Ceiling(process.QuantityMachines);
        }

        return result;
    }

    public string CalculatePowerText()
    {
        var mw = Processes.Select(p => p.CalculatePowerUsage()).Sum();

        if (mw == 0)
        {
            return "";
        }
        else if (mw < 1000)
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