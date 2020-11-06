using HCSampleProject.Database.Models;
using HotChocolate.Resolvers;
using HotChocolate.Types;

namespace HCSampleProject.GraphQL
{
  public class MaterialGraphQL : ObjectType<Material>
  {
    protected override void Configure(IObjectTypeDescriptor<Material> descriptor)
    {
      base.Configure(descriptor);

      descriptor
        .ImplementsNode()
        .IdField(model => model.Id)
        .ResolveNode((context, id) => context.DataLoader<MaterialDataLoader>().LoadAsync(id, context.RequestAborted));
    }
  }
}
