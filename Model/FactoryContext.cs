using Microsoft.EntityFrameworkCore;

namespace Akycha.Model;

public class FactoryContext(DbContextOptions<FactoryContext> options) : DbContext(options)
{
    public DbSet<Part> Parts { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
}
