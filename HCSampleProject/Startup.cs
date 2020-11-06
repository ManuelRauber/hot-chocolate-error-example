using HCSampleProject.Database;
using HCSampleProject.GraphQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HCSampleProject
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services
        .AddCors()
        .AddTransient<SampleDataInitializer>()
        .AddPooledDbContextFactory<MyContext>(builder => builder.UseInMemoryDatabase("my-db"))
        .AddGraphQLServer()
        .AddQueryType(t => t.Name("Query"))
        .AddSorting()
        .AddFiltering()
        .AddType<CfpGraphQL>()
        .AddType<MaterialQueries>()
        .AddType<MaterialGraphQL>()
        .EnableRelaySupport();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();

      app.UseEndpoints(endpoints => endpoints.MapGraphQL());
    }
  }
}
