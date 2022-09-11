using System.Threading;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;

namespace CarMarket.Server.Services
{
    public class CancellationTokenService : IDisposable
    {
        private static readonly object locker = new();
        private readonly CancellationTokenSource cancellationTokenSource;
        public readonly CancellationToken token;
        private readonly string _name;

        private static IDictionary<string, CancellationTokenService> services;

        public ILogger Logger
        {
            private get;
            set;
        }

        public static CancellationTokenService GetInstance(string name)
        {
            lock (locker)
            {
                CancellationTokenService service = null;

                if (services != null)
                {
                    if (services.ContainsKey(name))
                    {
                        services[name].Logger.LogInformation("Services: " + string.Join(',', services.Keys));
                        service = services[name];
                    }
                }

                return service;
            }
        }

        public static CancellationTokenService CreateInstance(string name)
        {
            lock (locker)
            {
                if (services != null)
                {
                    services.Add(name, new CancellationTokenService(name));
                }
                else services = new Dictionary<string, CancellationTokenService>
                    {
                    { name, new CancellationTokenService(name) }
                    };

                return services[name];
            }
        }

        private CancellationTokenService(string name)
        {
            cancellationTokenSource = new CancellationTokenSource();
            token = cancellationTokenSource.Token;
            _name = name;
        }

        ~CancellationTokenService()
        {
            services = null;
        }

        public void CancelToken()
        {
            cancellationTokenSource.Cancel();
            Dispose();
        }

        public void Dispose()
        {
            cancellationTokenSource?.Dispose();
            Logger.LogInformation($"CancellationTokenService \"{_name}\" has been disposed.");

            services.Remove(_name);

            Logger.LogInformation("Services: " + string.Join(',', services.Keys));

            GC.SuppressFinalize(this);
        }
    }
}