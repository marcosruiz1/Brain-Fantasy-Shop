using Microsoft.AspNetCore.Mvc;
using PracticaBlazor.UI.Shared;
using PracticaBlazor.UI.Shared.Models.Email;

namespace PracticaBlazor.UI.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
   
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly IEmailSender _emailSender;


        public WeatherForecastController(IEmailSender emailSender)
        {
 
            _emailSender = emailSender;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            var message = new Message(new string[] { "marcos.ruiz16@iesdoctorbalmis.com" }, "Test email", "This is the content from our email.");
            _emailSender.SendEmail(message);
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}