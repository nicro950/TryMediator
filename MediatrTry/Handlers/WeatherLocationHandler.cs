using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatrTry.Commands;
using MediatrTry.Services;

namespace MediatrTry.Handlers
{
    public class WeatherLocationHandler : IRequestHandler<WeatherLocationRequest, WeatherLocationResponse>
    {
        private readonly WeatherLocationService service;

        public WeatherLocationHandler(WeatherLocationService service)
        {
            this.service = service;
        }


        public Task<WeatherLocationResponse> Handle(WeatherLocationRequest request, CancellationToken cancellationToken)
        {
            var response = service.GetForLocation(request.Place);

            if (response != null) return Task.FromResult(new WeatherLocationResponse(response));

            return Task.FromResult(new WeatherLocationResponse(WeatherLocationReponse.NotFound));
        }
    }
}
