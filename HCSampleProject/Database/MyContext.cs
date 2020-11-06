using HCSampleProject.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace HCSampleProject.Database
{
  public class MyContext : DbContext
  {
    public DbSet<Material> Materials { get; set; } = default!;
    public DbSet<Cfp> Cfps { get; set; } = default!;

    public MyContext(DbContextOptions<MyContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.ApplyConfiguration(new CfpConfiguration());
      modelBuilder.ApplyConfiguration(new MaterialConfiguration());
    }
  }
}
