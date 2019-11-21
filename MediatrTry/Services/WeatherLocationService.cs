using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatrTry.Controllers;
using Microsoft.Extensions.Logging;

namespace MediatrTry.Services
{
    public class WeatherLocationService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching
        };

        Dictionary<string, (int, int)> places = new Dictionary<string, (int, int)>()
        {
            ["Stavanger"] = (-5, 20),
            ["Kristiansand"] = (0, 30),
            ["Bergen"] = (0, 30),
            ["Oslo"] = (-18, 30),
            ["Stockholm"] = (-12, 34),
            ["København"] = (-10, 25),
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherLocationService(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        public IEnumerable<WeatherForecast> GetForLocation(string location)
        {
            if (places.TryGetValue(location, out (int, int) minMax))
            {
                var rng = new Random();
                return Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(minMax.Item1, minMax.Item2),
                    Summary = location == "Bergen" ? "Rain" : Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();
            }
            return null;
        }

        public IEnumerable<string> GetLocations()
        {
            return places.Keys;
        }
    }
}
