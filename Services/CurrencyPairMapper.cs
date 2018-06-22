﻿using System.Globalization;
using TelegramBot.Models.API;
using TelegramBot.Models.API.JSON;

namespace TelegramBot.Services
{
    public static class CurrencyPairMapper
    {
        public static CurrencyPair Map(Poloniex poloniex)
        {
            var names = poloniex.CurrencyPair.Trim().Split('_');

            return new CurrencyPair
            {
                BaseVolume = double.Parse(poloniex.BaseVolume, CultureInfo.InvariantCulture),
                CurrencyPairName = poloniex.CurrencyPair,
                High = double.Parse(poloniex.High24Hr, CultureInfo.InvariantCulture),
                Last = double.Parse(poloniex.Last, CultureInfo.InvariantCulture),
                Low = double.Parse(poloniex.Low24Hr, CultureInfo.InvariantCulture),
                Volume = double.Parse(poloniex.QuoteVolume, CultureInfo.InvariantCulture),
                PercentChange = double.Parse(poloniex.PercentChange, CultureInfo.InvariantCulture),
                FirstCurrency = names[0],
                SecondCurrency = names[1]
            };
        }

        public static CurrencyPair Map(CoinMarket coinMarket)
        {
            return new CurrencyPair
            {
                BaseVolume = double.Parse(coinMarket.The24hVolumeUsd,CultureInfo.InvariantCulture),
                CurrencyPairName = coinMarket.Symbol,
                Last = double.Parse(coinMarket.PriceUsd, CultureInfo.InvariantCulture),
                High = 0,
                Low = 0,
                FirstCurrency = coinMarket.Symbol,
                SecondCurrency = "USD",
                Volume = 0,
                PercentChange = double.Parse(coinMarket.PercentChange24h, CultureInfo.InvariantCulture),
                Name = coinMarket.Name
            };
        }
    }
}