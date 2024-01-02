using ITSTEPRabbitMQ.Messages;
using ITSTEPRabbitMQ.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ITSTEPRabbitMQ.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPublishEndpoint _publishEndpoint;

        public HomeController(ILogger<HomeController> logger, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<IActionResult> Index()
        {
            await _publishEndpoint.Publish<IRabbitMQMessage>(new
            {
                Id = 1,
                Message = "Hello world"

            },
            context => context.TimeToLive = TimeSpan.FromMinutes(5));
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
