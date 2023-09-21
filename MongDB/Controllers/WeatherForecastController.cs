using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace MongDB.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        IMongoCollection<WeatherForecast> _forecasts;
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
            var client = new MongoClient("mongodb+srv://sa:12345@mongdbtest.9eafppu.mongodb.net/?retryWrites=true&w=majority&appName=AtlasApp");
            var database = client.GetDatabase("MongoDB");
            _forecasts = database.GetCollection<WeatherForecast>("Weather");
        }

        [HttpPost]
        public IActionResult Post([FromBody] WeatherForecast forecast)
        {


            _forecasts.InsertOne(new WeatherForecast
            {
                Date = DateTime.Now,
                Summary = forecast.Summary,
                TemperatureC = forecast.TemperatureC
            });
            return Ok();
        }
    }
}