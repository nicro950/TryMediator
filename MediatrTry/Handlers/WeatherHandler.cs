using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatrTry.Commands;

namespace MediatrTry.Handlers
{
    public class WeatherHandler : IRequestHandler<WeatherRequest, WeatherResponse> // TODO: Implement request handler
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public Task<WeatherResponse> Handle(WeatherRequest request, CancellationToken cancellationToken)
        {
            var rng = new Random();
            var weather = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            return Task.FromResult(new WeatherResponse(weather));
        }
    }
}
