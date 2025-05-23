﻿using Binance.Net.Converters;
using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Binance.Net.Enums
{
    /// <summary>
    /// Plan status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AutoInvestPlanStatus>))]
    public enum AutoInvestPlanStatus
    {
        /// <summary>
        /// Ongoing
        /// </summary>
        [Map("ONGOING")]
        Ongoing,
        /// <summary>
        /// Paused
        /// </summary>
        [Map("PAUSED")]
        Paused,
        /// <summary>
        /// Removed
        /// </summary>
        [Map("REMOVED")]
        Removed
    }
}
