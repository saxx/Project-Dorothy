using Dorothy.Models;
using Dorothy.ViewModels.Guests;
using Haufwerk.Client;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Data.Entity;
using Microsoft.Dnx.Runtime;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Dorothy
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            // Setup configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .AddEnvironmentVariables("Dorothy:");
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry(Configuration);
            services.AddHaufwerk("Dorothy", "https://hauwerk.adliance-labs.net");
            services.AddMvc();

            var config = new Configuration(Configuration);
            services.AddSingleton(x => config);

            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<Db>(options =>
                {
                    options.UseSqlServer(config.ConnectionString);
                });
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.MinimumLevel = LogLevel.Information;
            loggerFactory.AddConsole();

            app.UseApplicationInsightsRequestTelemetry();
            app.UseHaufwerk();

            app.UseCookieAuthentication(options =>
            {
                options.AutomaticAuthenticate = true;
                options.AutomaticChallenge = true;
                options.LoginPath = "/Login";
            });

            app.UseIISPlatformHandler();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            CreateAutoMapperMappings();
        }

        private void CreateAutoMapperMappings()
        {
            AutoMapper.Mapper.CreateMap(typeof(CreateViewModel), typeof(Guest));
            AutoMapper.Mapper.CreateMap(typeof(EditViewModel), typeof(Guest));
            AutoMapper.Mapper.CreateMap(typeof(Guest), typeof(EditViewModel));
            AutoMapper.Mapper.CreateMap(typeof(Guest), typeof(IndexViewModel.Guest));
            AutoMapper.Mapper.CreateMap(typeof(ViewModels.Rsvp.IndexViewModel), typeof(Rsvp));
        }
    }
}
