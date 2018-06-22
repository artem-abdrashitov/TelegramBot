using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TelegramBot.Services;
using TelegramBot.Services.Stores;

namespace TelegramBot.BackgroundTasks
{
    public class CoinMarketTask : IScheduledTask
    {
        private readonly HttpClientWrapper _client;
        private readonly ILogger<CoinMarketTask> _logger;

        public CoinMarketTask(HttpClientWrapper client, ILogger<CoinMarketTask> logger)
        {
            _client = client;
            _logger = logger;
        }

        public string Schedule => (5 * 60 * 1000).ToString();

        public async Task Invoke(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Invoked timercoinmarket");
            try
            {
                var currencyPairs = await _client.GetCoinMarket();
                CurrencyInfoStore.AddCoinMarket(currencyPairs);
            }
            catch (Exception e)
            {
                _logger.LogError("Timer coinmarket", e.Message);
            }
        }
    }
}