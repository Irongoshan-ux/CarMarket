using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using System.Net;
using Toolbelt.Blazor;

namespace CarMarket.UI.Services.HttpInterceptor
{
    public class HttpInterceptorService
    {
        private readonly HttpClientInterceptor _interceptor;
        private readonly IToastService _toastService;

        public HttpInterceptorService(HttpClientInterceptor interceptor, IToastService toastService)
        {
            _interceptor = interceptor;
            _toastService = toastService;
        }

        public void RegisterEvent() => _interceptor.AfterSend += InterceptResponse;

        private void InterceptResponse(object sender, HttpClientInterceptorEventArgs e)
        {
            if (!e.Response.IsSuccessStatusCode)
            {
                var statusCode = e.Response.StatusCode;
                
                switch (statusCode)
                {
                    case HttpStatusCode.NotFound:
                        _toastService.ShowError("The requested resource was not found");
                        break;
                    case HttpStatusCode.Unauthorized:
                        _toastService.ShowError("User is not authorized");
                        break;
                    default:
                        _toastService.ShowError("Something went wrong");
                        break;
                }
            }
        }
        public void DisposeEvent() => _interceptor.AfterSend -= InterceptResponse;
    }
}
