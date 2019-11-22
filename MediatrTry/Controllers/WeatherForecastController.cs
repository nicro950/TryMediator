using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using MediatrTry.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MediatrTry.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMediator mediator;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediator)
        {
            _logger = logger;
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> GetAsync()
        {
            var weatherResponse = await mediator.Send(new WeatherRequest());
            return weatherResponse.Weather;
        }

        [HttpGet("place/{name}")]
        public async Task<ActionResult<IEnumerable<WeatherForecast>>> GetAsync(string name)
        {
            var response = await mediator.Send(new WeatherLocationRequest(name));

            if (response.Status == WeatherLocationReponse.NotFound) return NotFound();

            if (response.Status != WeatherLocationReponse.Ok || response.Weather == null) return BadRequest();

            return Ok(response.Weather);
        }
    }
}
