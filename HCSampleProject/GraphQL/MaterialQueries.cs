using System.Linq;
using HCSampleProject.Database;
using HCSampleProject.Database.Models;
using HotChocolate;
using HotChocolate.Types;

namespace HCSampleProject.GraphQL
{
  [ExtendObjectType(Name = "Query")]
  public class MaterialQueries
  {
    [UseMyContext]
    [UsePaging(typeof(ObjectType<MaterialGraphQL>))]
    public IQueryable<Material> GetMaterials([ScopedService] MyContext context) => context.Materials;
  }
}
