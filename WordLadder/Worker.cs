using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WordLadder
{
    public class Worker : BackgroundService
    {
        private readonly WordLadderApp _app;
        private readonly IOptions<ApplicationArguments> _options;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly ILogger _logger;

        public Worker(WordLadderApp app, IOptions<ApplicationArguments> options, IHostApplicationLifetime hostApplicationLifetime, ILogger<Worker> logger)
        {
            _app = app ?? throw new ArgumentNullException(nameof(app));
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _hostApplicationLifetime = hostApplicationLifetime ?? throw new ArgumentNullException(nameof(hostApplicationLifetime));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var result = _app.CalculateWordLadder(_options.Value.StartWord, _options.Value.EndWord);

            if (result.Success)
                _logger.LogInformation($"Word Ladder successfully generated:{Environment.NewLine}{result.Result}");
            else
                _logger.LogError($"Word Ladder was not successful, Errors: {Environment.NewLine}{result.Result}");

            _hostApplicationLifetime.StopApplication();
            return Task.CompletedTask;
        }
    }
}
