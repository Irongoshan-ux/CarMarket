using CarMarket.BusinessLogic.User.Service;
using CarMarket.Core.User.Repository;
using CarMarket.Core.User.Service;
using CarMarket.Data;
using CarMarket.Data.User.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using CarMarket.Data.Car.Repository;
using CarMarket.Core.Car.Repository;
using CarMarket.Core.Car.Service;
using CarMarket.Server.Infrastructure;
using CarMarket.BusinessLogic.Car.Service;
using CarMarket.Data.Configuration.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using CarMarket.Server.Infrastructure.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;

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
            services.AddCors();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(options =>
                 {
                     options.Authority = "https://localhost:5001";
                     options.Audience = "API";
                 });


            services.AddAuthorization();
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Admin", policy =>
            //        policy.RequireClaim("IsAdmin", "true"));

            //    options.AddPolicy("User", policy =>
            //        policy.RequireClaim("IsUser", "true"));

            //    options.AddPolicy("Guest", policy =>
            //        policy.RequireClaim("IsGuest", "true"));
            //});

            services.AddIdentityServer(options =>
                {
                    options.UserInteraction.LoginUrl = "/identification/login";
                    options.UserInteraction.LogoutUrl = "/identification/logout";
                })
                .AddInMemoryApiResources(IdentityServerConfiguration.GetApiResources())
                .AddInMemoryClients(IdentityServerConfiguration.GetClients())
                .AddInMemoryIdentityResources(IdentityServerConfiguration.GetIdentityResources())
                .AddInMemoryApiScopes(IdentityServerConfiguration.GetScopes())
                .AddDeveloperSigningCredential();


            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CarMarket", Version = "v1" });
            });

            services.AddBlazoredLocalStorage();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ICarService, CarService>();

            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            services.AddScoped<IUserAuthService, UserAuthService>();

            //services.AddScoped<IAuthService, JWTService>();
            //services.AddScoped<IAuthContainerModel, JWTContainerModel>();
            //services.AddScoped<System.Net.Http.HttpClient>();

            services.AddDbContext<ApplicationDbContext>(builder =>
            {
                builder.UseSqlServer(Configuration.GetConnectionString("CarMarketDb"));
            });

            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<EntityToModelMappingProfile>();
            });

            //services.Configure<JWTContainerModel>(Configuration.GetSection("JWTSecretKey"));
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

            app.UseCors(builder =>
            {
                builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
            });

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
