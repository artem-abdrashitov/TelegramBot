using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TelegramBot.Services;
using TelegramBot.Services.Stores;

namespace TelegramBot.BackgroundTasks
{
    public class PoloniexTask : IScheduledTask
    {
        private readonly HttpClientWrapper _client;
        private readonly ILogger<PoloniexTask> _logger;

        public PoloniexTask(HttpClientWrapper client, ILogger<PoloniexTask> logger)
        {
            _client = client;
            _logger = logger;
        }

        public string Schedule => (5 * 60 * 1000).ToString();

        public async Task Invoke(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Invoked timerpoloniex");
            try
            {
                var currencyPairs = await _client.GetPoloniex();
                CurrencyInfoStore.AddPoloniex(currencyPairs);
            }
            catch (Exception e)
            {
                _logger.LogError("Timer poloniex", e.Message);
            }
        }
    }
}