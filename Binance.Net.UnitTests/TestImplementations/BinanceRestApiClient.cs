﻿using Binance.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Logging;
using System;

namespace Binance.Net.UnitTests.TestImplementations
{
    public class BinanceRestApiClient : RestApiClient
    {
        public BinanceRestApiClient(ILogger logger, BinanceRestOptions options, BinanceRestApiOptions apiOptions) : base(logger, null, "https://test.com", options, apiOptions)
        {
        }

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode futuresType, DateTime? deliverDate = null) => $"{baseAsset.ToUpperInvariant()}{quoteAsset.ToUpperInvariant()}";
        public override TimeSpan? GetTimeOffset() => null;
        public override TimeSyncInfo GetTimeSyncInfo() => null;
        protected override IStreamMessageAccessor CreateAccessor() => throw new NotImplementedException();
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials) => throw new NotImplementedException();
        protected override IMessageSerializer CreateSerializer() => throw new NotImplementedException();
    }
}
