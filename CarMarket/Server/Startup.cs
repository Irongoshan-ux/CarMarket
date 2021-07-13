using CarMarket.BusinessLogic.User.Service;
using CarMarket.Core.User.Repository;
using CarMarket.Core.User.Service;
using CarMarket.Data;
using CarMarket.Data.User.Converter;
using CarMarket.Data.User.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using CarMarket.BusinessLogic.User.Authentication.Models;
using CarMarket.BusinessLogic.User.Authentication.Service;
using CarMarket.Data.Car.Repository;
using CarMarket.Core.Car.Repository;
using CarMarket.Core.Car.Service;

namespace CarMarket.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CarMarket", Version = "v1" });
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<UserConverter>();

            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ICarService, CarService>();
            services.AddSingleton<UserConverter>();

            //services.AddScoped<IAuthService, JWTService>();
            //services.AddScoped<IAuthContainerModel, JWTContainerModel>();
            //services.AddScoped<System.Net.Http.HttpClient>();

            services.AddDbContext<ApplicationDbContext>(builder =>
            {
                builder.UseSqlServer(Configuration.GetConnectionString("CarMarketDb"));
            });
           

            services.Configure<JWTContainerModel>(Configuration.GetSection("JWTSecretKey"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarMarket v1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
