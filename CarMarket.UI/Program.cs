using CarMarket.UI.Services;
using CarMarket.UI.Services.Car;
using CarMarket.UI.Services.User;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using CarMarket.UI.Polly;
using Blazored.Toast;
using CarMarket.UI.Services.CarValuer;
using CarMarket.UI.Services.CarBrand;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using CarMarket.UI.Services.HttpInterceptor;
using MudBlazor.Services;

namespace CarMarket.UI
{
    public class Program
    {
        private static readonly string API_BASE_ADDRESS = "https://localhost:10001";
        private static readonly string API_CAR_VALUER_BASE_ADDRESS = "https://localhost:11001";

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped<HttpAccessTokenSetter>();

            builder.Services.AddHttpClient("API", client =>
            {
                client.BaseAddress = new Uri(API_BASE_ADDRESS);
            });

            builder.Services.AddHttpClient<IHttpCarService, HttpCarService>((provider, client) =>
            {
                client.BaseAddress = new Uri(API_BASE_ADDRESS);
                client.EnableIntercept(provider);
            })
                .SetHandlerLifetime(TimeSpan.FromSeconds(10))
                .AddPolicyHandler(PollyConfigurator.GetRetryPolicy());

            builder.Services.AddHttpClient<IHttpCarBrandService, HttpCarBrandService>((provider, client) =>
            {
                client.BaseAddress = new Uri(API_BASE_ADDRESS);
                client.EnableIntercept(provider);
            })
                .SetHandlerLifetime(TimeSpan.FromSeconds(10))
                .AddPolicyHandler(PollyConfigurator.GetRetryPolicy());

            builder.Services.AddHttpClient<IHttpCarValuerService, HttpCarValuerService>((provider, client) =>
            {
                client.BaseAddress = new Uri(API_CAR_VALUER_BASE_ADDRESS);
                client.EnableIntercept(provider);
            })
                .SetHandlerLifetime(TimeSpan.FromSeconds(10))
                .AddPolicyHandler(PollyConfigurator.GetRetryPolicy());

            builder.Services.AddHttpClient<IHttpUserService, HttpUserService>((provider, client) =>
            {
                client.BaseAddress = new Uri(API_BASE_ADDRESS);
                client.EnableIntercept(provider);
            })
                .SetHandlerLifetime(TimeSpan.FromSeconds(10))
                .AddPolicyHandler(PollyConfigurator.GetRetryPolicy());

            builder.Services.AddHttpClient<IHttpUserService, HttpUserService>()
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(PollyConfigurator.GetRetryPolicy());

            builder.Services.AddHttpClientInterceptor();

            builder.Services.AddScoped<HttpInterceptorService>();

            builder.Services.AddOidcAuthentication(options =>
            {
                // Configure your authentication provider options here.
                // For more information, see https://aka.ms/blazor-standalone-auth
                builder.Configuration.Bind("Local", options.ProviderOptions);
            });

            builder.Services.AddOidcAuthentication(options =>
            {
                options.ProviderOptions.Authority = API_BASE_ADDRESS;
                options.ProviderOptions.ClientId = "client_blazor";
                options.ProviderOptions.DefaultScopes.Add("profile");
                options.ProviderOptions.DefaultScopes.Add("email");
                options.ProviderOptions.DefaultScopes.Add("openid");
                options.ProviderOptions.ResponseType = "code";

                options.UserOptions.NameClaim = "sub";
            });

            builder.Services.AddBlazoredToast();

            builder.Services.AddMudServices();

            await builder.Build().RunAsync();
        }
    }
}
