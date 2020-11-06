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
  public class CfpDataLoader : BatchDataLoader<Guid, Cfp>
  {
    private readonly IDbContextFactory<MyContext> _contextFactory;

    public CfpDataLoader(
      IBatchScheduler batchScheduler,
      IDbContextFactory<MyContext> contextFactory
    ) : base(batchScheduler)
    {
      _contextFactory = contextFactory;
    }

    protected override async Task<IReadOnlyDictionary<Guid, Cfp>> LoadBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
    {
      await using var context = _contextFactory.CreateDbContext();
      return await context
        .Cfps.Where(model => keys.Contains(model.Id))
        .ToDictionaryAsync(model => model.Id, cancellationToken);
    }
  }
}
