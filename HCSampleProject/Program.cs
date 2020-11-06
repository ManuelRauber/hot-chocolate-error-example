using System.Threading.Tasks;
using HCSampleProject.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HCSampleProject
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      var host = CreateHostBuilder(args).Build();

      using (var serviceScope = host.Services.CreateScope())
      {
        var dataInit = serviceScope.ServiceProvider.GetRequiredService<SampleDataInitializer>();
        await dataInit.InitAsync();
      }

      await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
  }
}
