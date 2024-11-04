using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Akycha.Model;

public class FactoryContext(DbContextOptions<FactoryContext> options) : DbContext(options)
{
    public DbSet<Facility> Facilities { get; set; }
    public DbSet<Input> Inputs { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Machine> Machines { get; set; }
    public DbSet<Part> Parts { get; set; }
    public DbSet<Process> Processes { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Setting> Settings { get; set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Conventions.Remove(typeof(TableNameFromDbSetConvention));
    }
}
