using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using d3lfg.Models;
using Newtonsoft.Json;
using d3lfg.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AutoMapper;
using d3lfg.ViewModels;
using Microsoft.AspNetCore.Http;

namespace d3lfg
{
  public class Startup
  {
    private IHostingEnvironment _env;
    private IConfigurationRoot _config;

    public Startup(IHostingEnvironment env)
    {
      _env = env;

      var builder = new ConfigurationBuilder()
        .SetBasePath(_env.ContentRootPath)
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables();

      builder.AddUserSecrets();

      _config = builder.Build();
    }
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddSingleton(_config);

      services.AddDbContext<Context>();

      services.AddScoped<IRepository, Repository>();

      services.AddSingleton<IMessageRepository, MessageRepository>();

      services.AddScoped<BlizzardServices, BlizzardServices>();

      services.AddSignalR();

      services.AddMvc();

      services.AddIdentity<d3User, IdentityRole>(options =>
      {
        options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789#-._@+";
      })
        .AddEntityFrameworkStores<Context>();


      //this is all to patch signalr to work properly w/ core. Might not even be necessary, need to double check.
      var settings = new JsonSerializerSettings();
      settings.ContractResolver = new SignalRContractResolver();

      var serializer = JsonSerializer.Create(settings);
      services.Add(new ServiceDescriptor(typeof(JsonSerializer),
                   provider => serializer,
                   ServiceLifetime.Transient));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, Context context)
    {
      loggerFactory.AddDebug(LogLevel.Critical);
      loggerFactory.AddAzureWebAppDiagnostics();

      Mapper.Initialize(config =>
      {
        config.CreateMap<Group, GroupViewModel>();
      });

      //context.Database.Migrate();

      //app.Use(async (webContext, next) =>
      //{
      //  if (webContext.Request.IsHttps)
      //  {
      //    await next();
      //  }
      //  else
      //  {
      //    //same as what's used for blizz redirect so let's be lazy :P
      //    var httpsUrl = _config.GetSection("BlizzRedirect").Value;
      //    webContext.Response.Redirect(httpsUrl);
      //  }
      //});

      if(_env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      
      app.UseBrowserLink();

      app.UseWebSockets();

      app.UseSignalR();

      app.UseStaticFiles();

      app.UseIdentity();

      app.UseMvc(config =>
      {
        config.MapRoute(
            name: "default",
            template: "{controller=home}/{action=index}/{id?}"
          );
      });

    }
  }
}
