using System.IO;
using AutoMapper;
using Dorothy.Models;
using Dorothy.ViewModels.Guests;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;


namespace Dorothy
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
            host.Run();
        }


        // ReSharper disable once UnusedParameter.Local
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder().AddEnvironmentVariables("Dorothy:");
            Configuration = builder.Build();
        }


        public IConfigurationRoot Configuration { get; set; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry("4bdaf152-c203-425b-a9a4-2ad8f4dfa9d4");
            services.AddMvc();

            var config = new Configuration(Configuration);
            services.AddSingleton(x => config);
            services.AddSingleton(x => CreateAutoMapperMappings());

            services.AddDbContext<Db>(options =>
                {
                    options.UseSqlServer(config.ConnectionString);
                });
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(LogLevel.Information).AddDebug(LogLevel.Information);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                LoginPath = "/Login"
            });

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private IMapper CreateAutoMapperMappings()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<CreateViewModel, Guest>();
                cfg.CreateMap<EditViewModel, Guest>();
                cfg.CreateMap<Guest, EditViewModel>();
                cfg.CreateMap<Guest, IndexViewModel.Guest>();
                cfg.CreateMap<ViewModels.Rsvp.IndexViewModel, Rsvp>();
            });
            return config.CreateMapper();
        }
    }
}
