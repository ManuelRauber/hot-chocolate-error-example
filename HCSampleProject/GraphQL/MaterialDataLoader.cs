using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GreenDonut;
using HCSampleProject.Database;
using HCSampleProject.Database.Models;
using HotChocolate.DataLoader;
using Microsoft.EntityFrameworkCore;

namespace HCSampleProject.GraphQL
{
  public class MaterialDataLoader : BatchDataLoader<Guid, Material>
  {
    private readonly IDbContextFactory<MyContext> _contextFactory;

    public MaterialDataLoader(
      IBatchScheduler batchScheduler,
      IDbContextFactory<MyContext> contextFactory
    ) : base(batchScheduler)
    {
      _contextFactory = contextFactory;
    }

    protected override async Task<IReadOnlyDictionary<Guid, Material>> LoadBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
    {
      await using var context = _contextFactory.CreateDbContext();
      return await context
        .Materials.Where(model => keys.Contains(model.Id))
        .ToDictionaryAsync(model => model.Id, cancellationToken);
    }
  }
}
