using Microsoft.EntityFrameworkCore;

namespace Akycha.Model;

public class FactoryContext(DbContextOptions<FactoryContext> options) : DbContext(options)
{
    public DbSet<Part> Parts { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<RecipePart> RecipeParts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Recipe>().Navigation(e => e.Parts).AutoInclude();
        modelBuilder.Entity<RecipePart>().Navigation(e => e.Part).AutoInclude();
    }
}
