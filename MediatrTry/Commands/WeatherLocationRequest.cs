using System;
using System.Collections.Generic;
using MediatR;

namespace MediatrTry.Commands
{
    public class WeatherLocationRequest : IRequest<WeatherLocationResponse>
    {
        public WeatherLocationRequest(string place)
        {
            Place = place;
        }

        public string Place { get; }
    }
#nullable enable
    public class WeatherLocationResponse
    {
        public WeatherLocationResponse(IEnumerable<WeatherForecast> weather)
        {
            if (weather == null) throw new ArgumentNullException(nameof(weather));

            Status = WeatherLocationReponse.Ok;
            Weather = weather;
        }

        public WeatherLocationResponse(WeatherLocationReponse status, IEnumerable<WeatherForecast>? weather = null)
        {
            Status = status;
            Weather = weather;
        }

        public WeatherLocationReponse Status { get; }
        public IEnumerable<WeatherForecast>? Weather { get; }
    }
#nullable disable

    public enum WeatherLocationReponse
    {
        Ok,
        NotFound,
    }
}
