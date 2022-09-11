using CarMarket.Core.Car.Domain;
using CarMarket.Core.User.Domain;
using CarMarket.Core.User.Service;
using CarMarket.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarMarket.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeController : ControllerBase
    {
        private readonly ILogger<TimeController> _logger;
        
        public TimeController(ILogger<TimeController> logger)
        {
            _logger = logger;
            _logger.LogInformation("TimeController was created in thread: " + Thread.CurrentThread.ManagedThreadId);
        }

        ~TimeController()
        {
            _logger.LogInformation("TimeController was disposed in thread: " + Thread.CurrentThread.ManagedThreadId);
        }

        [HttpPost]
        [Route("Test method")]
        public IActionResult TestMethod(string car, string user)
        {
            return Ok("Info: [Car]\n" + car + "\n[User]\n" + user);
        }

        [HttpGet]
        public void StartTimer(long time)
        {
            CancellationTokenService timerService;

            try
            {
                timerService = CancellationTokenService.CreateInstance(GetType().ToString());
            }
            catch
            {
                timerService = null;
            }

            if (timerService != null)
            {
                timerService.Logger = _logger;
                var endTime = DateTime.Now.AddSeconds(time);

                StartTimerTask(timerService, endTime);
            }
        }

        [HttpPost]
        public void StopTimer()
        {
            var timerService = CancellationTokenService.GetInstance(GetType().ToString());

            timerService?.CancelToken();
        }

        private Task StartTimerTask(CancellationTokenService cancellationTokenService, DateTime endTime) => Task.Run(() =>
            {
                do
                {
                    _logger.LogInformation("Current time: " + DateTime.Now + " in thread " + Thread.CurrentThread.ManagedThreadId);

                    Thread.Sleep(1000);

                    if(endTime < DateTime.Now) cancellationTokenService.Dispose();
                }
                while (endTime >= DateTime.Now && !cancellationTokenService.token.IsCancellationRequested);
            });
    }
}
