using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MediatR.Pipeline;
using MediatrTry.Commands;
using MediatrTry.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MediatrTry
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // TODO: Uncomment this line
            services.AddMediatR(typeof(Startup).Assembly);
            services.AddSingleton<WeatherLocationService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILoggerFactory _loggerFactory;

        public RequestLogger(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var logger = _loggerFactory.CreateLogger<TRequest>();

            // Do some logging to record the Request
            logger.LogWarning("Got object: {obj}", Helper.Dump(request));

            return Task.CompletedTask;
        }
    }

    public class ResponseLogger2 : IRequestPostProcessor<WeatherRequest, WeatherResponse>
    {
        private readonly ILoggerFactory _loggerFactory;

        public ResponseLogger2(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public Task Process(WeatherRequest request, WeatherResponse response, CancellationToken cancellationToken)
        {
            var logger = _loggerFactory.CreateLogger<WeatherRequest>();

            // Do some logging to record the Request
            logger.LogWarning("Got object: {obj}", string.Join(", ", response.Weather.Select(x => x.Summary)));

            return Task.CompletedTask;
        }
    }

    public class ResponseLogger<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    {
        private readonly ILoggerFactory _loggerFactory;

        public ResponseLogger(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            var logger = _loggerFactory.CreateLogger<TRequest>();

            // Do some logging to record the Request
            logger.LogWarning("Got object: {obj}", Helper.Dump(request));
            logger.LogWarning("With response: {obj}", Helper.Dump(response));

            return Task.CompletedTask;
        }
    }

    public static class Helper
    {
        public static string Dump(object resposne)
        {
            return string.Join(", ", resposne.GetType().GetProperties().Select(x => $"{x.Name}: {x.GetValue(resposne)}"));
        }
    }

}
