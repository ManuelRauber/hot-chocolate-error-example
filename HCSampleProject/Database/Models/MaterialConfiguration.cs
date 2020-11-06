using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCSampleProject.Database.Models
{
  public class MaterialConfiguration : IEntityTypeConfiguration<Material>
  {
    public void Configure(EntityTypeBuilder<Material> builder)
    {
      builder.HasKey(p => p.Id);
    }
  }
}
