using MediatR;

namespace MediatrTry.Commands
{
    public class WeatherRequest : IRequest<WeatherResponse> // TODO: Add interface
    {
        // TODO: Add request fields
        public WeatherRequest()
        {

        }
    }

    public class WeatherResponse
    {
        // TODO: Add reponse field
        public WeatherResponse(WeatherForecast[] weather)
        {
            Weather = weather;
        }

        public WeatherForecast[] Weather { get; }
    }
}
