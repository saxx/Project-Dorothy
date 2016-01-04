using Dorothy.Models;
using Dorothy.ViewModels.Guests;
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
            services.AddMvc();

            var config = new Configuration(Configuration);
            services.AddSingleton(x=>config);

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

            app.UseCookieAuthentication(options =>
            {
                options.AutomaticAuthenticate = true;
                options.AutomaticChallenge = true;
                options.LoginPath = "/Login";
            });

            // Configure the HTTP request pipeline.

            // Add the following to the request pipeline only in development environment.
            /*if (env.IsDevelopment())
            {*/
                app.UseDeveloperExceptionPage();
            /*}
            else
            {
                // Add Error handling middleware which catches all application specific errors and
                // send the request to the following path or controller action.
                app.UseExceptionHandler("/Home/Error");
            }*/

            // Add the platform handler to the request pipeline.
            app.UseIISPlatformHandler();

            // Add static files to the request pipeline.
            app.UseStaticFiles();

            // Add MVC to the request pipeline.
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                // Uncomment the following line to add a route for porting Web API 2 controllers.
                // routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
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
