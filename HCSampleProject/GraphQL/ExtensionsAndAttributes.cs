using System.Reflection;
using HCSampleProject.Database;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HCSampleProject.GraphQL
{
  public class UseMyContextAttribute : ObjectFieldDescriptorAttribute
  {
    public override void OnConfigure(
      IDescriptorContext context,
      IObjectFieldDescriptor descriptor,
      MemberInfo member
    )
    {
      descriptor.UseDbContext<MyContext>();
    }
  }

  public static class ObjectFieldDescriptorExtensions
  {
    public static IObjectFieldDescriptor UseDbContext<T>(this IObjectFieldDescriptor descriptor)
      where T : DbContext
    {
      return descriptor.UseScopedService(
        serviceProvider => serviceProvider.GetRequiredService<IDbContextFactory<T>>().CreateDbContext(),
        disposeAsync: (_, context) => context.DisposeAsync()
      );
    }
  }
}
