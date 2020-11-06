using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HCSampleProject.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace HCSampleProject.Database
{
  public class SampleDataInitializer
  {
    private readonly IDbContextFactory<MyContext> _contextFactory;

    public SampleDataInitializer(IDbContextFactory<MyContext> contextFactory)
    {
      _contextFactory = contextFactory;
    }

    public async Task InitAsync()
    {
      await using var context = _contextFactory.CreateDbContext();

      await context.Cfps.AddAsync(new Cfp()
      {
        Id = Guid.NewGuid(),
        Title = "CFP 1",
        Materials = new List<Material>()
        {
          new Material()
          {
            Id = Guid.NewGuid(),
            Name = "CFP 1 Material 1"
          },
          new Material()
          {
            Id = Guid.NewGuid(),
            Name = "CFP 1 Material 2"
          }
        }
      });

      await context.Cfps.AddAsync(new Cfp()
      {
        Id = Guid.NewGuid(),
        Title = "CFP 2",
        Materials = new List<Material>()
        {
          new Material()
          {
            Id = Guid.NewGuid(),
            Name = "CFP 2 Material 1"
          },
          new Material()
          {
            Id = Guid.NewGuid(),
            Name = "CFP 2 Material 2"
          }
        }
      });
    }
  }
}
