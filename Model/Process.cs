using System.ComponentModel.DataAnnotations;

namespace Akycha.Model;

public class Process
{
    [Key] public int Id { get; set; }
    public int FacilityId { get; set; }
    public int? RecipeId { get; set; }

    public int QuantityMachines { get; set; }
    public int PowerShards { get; set; }

    public required Facility Facility { get; set; }
    public Recipe? Recipe { get; set; }

    public double CalculatePowerUsage()
    {
        if (Recipe?.Machine is null)
        {
            return 0.0;
        }
             
        var machineShards = new int[QuantityMachines];
        var machinePower = new double[QuantityMachines];

        var totalShards = PowerShards;
        var currentMachine = 0;        
        while (totalShards > 0)
        {
            machineShards[currentMachine]++;
            currentMachine %= QuantityMachines;
            totalShards--;
        }

        machinePower = machineShards.Select(s => Recipe.Machine.CalculateUsage(100 + s * 50)).ToArray();

        return Math.Round(machinePower.Sum(), 1);
    }
}