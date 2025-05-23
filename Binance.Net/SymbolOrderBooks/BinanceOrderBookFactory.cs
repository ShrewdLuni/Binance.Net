﻿using Binance.Net.Interfaces;
using Binance.Net.Interfaces.Clients;
using Binance.Net.Objects.Options;
using CryptoExchange.Net.OrderBook;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.DependencyInjection;

namespace Binance.Net.SymbolOrderBooks
{
    /// <summary>
    /// Binance order book factory
    /// </summary>
    public class BinanceOrderBookFactory : IBinanceOrderBookFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public BinanceOrderBookFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            Spot = new OrderBookFactory<BinanceOrderBookOptions>(CreateSpot, Create);
            UsdFutures = new OrderBookFactory<BinanceOrderBookOptions>(CreateUsdtFutures, Create);
            CoinFutures = new OrderBookFactory<BinanceOrderBookOptions>(CreateCoinFutures, Create);
        }

        /// <inheritdoc />
        public IOrderBookFactory<BinanceOrderBookOptions> Spot { get; }
        /// <inheritdoc />
        public IOrderBookFactory<BinanceOrderBookOptions> UsdFutures { get; }
        /// <inheritdoc />
        public IOrderBookFactory<BinanceOrderBookOptions> CoinFutures { get; }

        /// <inheritdoc />
        public ISymbolOrderBook Create(SharedSymbol symbol, Action<BinanceOrderBookOptions>? options = null)
        {
            var symbolName = symbol.GetSymbol(BinanceExchange.FormatSymbol);
            if (symbol.TradingMode == TradingMode.Spot)
                return CreateSpot(symbolName, options);
            if (symbol.TradingMode.IsLinear())
                return CreateUsdtFutures(symbolName, options);

            return CreateCoinFutures(symbolName, options);
        }

        /// <inheritdoc />
        public ISymbolOrderBook CreateSpot(string symbol, Action<BinanceOrderBookOptions>? options = null)
            => new BinanceSpotSymbolOrderBook(symbol,
                                             options,
                                             _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                             _serviceProvider.GetRequiredService<IBinanceRestClient>(),
                                             _serviceProvider.GetRequiredService<IBinanceSocketClient>());


        /// <inheritdoc />
        public ISymbolOrderBook CreateUsdtFutures(string symbol, Action<BinanceOrderBookOptions>? options = null)
            => new BinanceFuturesUsdtSymbolOrderBook(symbol,
                                             options,
                                             _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                             _serviceProvider.GetRequiredService<IBinanceRestClient>(),
                                             _serviceProvider.GetRequiredService<IBinanceSocketClient>());


        /// <inheritdoc />
        public ISymbolOrderBook CreateCoinFutures(string symbol, Action<BinanceOrderBookOptions>? options = null)
            => new BinanceFuturesCoinSymbolOrderBook(symbol,
                                             options,
                                             _serviceProvider.GetRequiredService<ILoggerFactory>(),
                                             _serviceProvider.GetRequiredService<IBinanceRestClient>(),
                                             _serviceProvider.GetRequiredService<IBinanceSocketClient>());
    }
}
