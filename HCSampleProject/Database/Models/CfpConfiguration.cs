using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCSampleProject.Database.Models
{
  public class CfpConfiguration : IEntityTypeConfiguration<Cfp>
  {
    public void Configure(EntityTypeBuilder<Cfp> builder)
    {
      builder.HasKey(p => p.Id);

      builder.HasMany(p => p.Materials);
    }
  }
}
