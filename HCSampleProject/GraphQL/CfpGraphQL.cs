using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HCSampleProject.Database;
using HCSampleProject.Database.Models;
using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;

namespace HCSampleProject.GraphQL
{
  public class CfpGraphQL : ObjectType<Cfp>
  {
    protected override void Configure(IObjectTypeDescriptor<Cfp> descriptor)
    {
      base.Configure(descriptor);

      descriptor
        .Field(model => model.Materials)
        .ResolveWith<MaterialsResolver>(resolver => resolver.GetMaterialsAsync(default!, default!, default!, default!))
        .UseDbContext<MyContext>()
        .UsePaging<ObjectType<MaterialGraphQL>>()
        .Name("materials");

      descriptor
        .ImplementsNode()
        .IdField(model => model.Id)
        .ResolveNode((context, id) => context.DataLoader<CfpDataLoader>().LoadAsync(id, context.RequestAborted));
    }

    private class MaterialsResolver
    {
      public async Task<IEnumerable<Material>> GetMaterialsAsync(
        Cfp model,
        [ScopedService] MyContext context,
        MaterialDataLoader dataLoader,
        CancellationToken cancellationToken
      )
      {
        Guid[] ids = await context.Cfps
          .Where(item => item.Id == model.Id)
          .SelectMany(item => item.Materials)
          .Select(item => item.Id)
          .ToArrayAsync(cancellationToken);

        return await dataLoader.LoadAsync(ids, cancellationToken);
      }
    }
  }
}
