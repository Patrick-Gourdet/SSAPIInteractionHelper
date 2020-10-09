using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpaca.Markets;
using SSAPIInteractionHelper._Helpers;
using SSAPIInteractionHelper.Account.AccountEnum;
using AccountStatus = Alpaca.Markets.AccountStatus;

namespace SSAPIInteractionHelper.Account.IronModules
{
    /// <summary>
    /// Local Account class emulates the Alpaca model for implementation
    /// through a direct connection avoiding the Alpaca API and the lag
    /// that comes with it  
    /// </summary>
    /// <remarks>
    /// Not yet implemented as the direct connection using HTTPCLIENT
    /// <see cref="HttpClientSingleton.cs"/>
    /// was my preferred implementation method but it is always nice to
    /// have the option.
    /// </remarks>
    public class IronUserAccount : IAccount
    {
        public Guid AccountId { get; set; }
        AccountStatus IAccountBase.Status { get; }
        public AccountStatus Status { get; set; }
        public string? Currency { get; set; }
        public decimal TradableCash { get; set; }
        public decimal WithdrawableCash { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public string? AccountNumber { get; set; }
        public bool IsDayPatternTrader { get; set; }
        public bool IsTradingBlocked { get; set; }
        public bool IsTransfersBlocked { get; set; }
        public bool TradeSuspendedByUser { get; set; }
        public bool ShortingEnabled { get; set; }
        public long Multiplier { get; set; }
        public decimal BuyingPower { get; set; }
        public decimal DayTradingBuyingPower { get; set; }
        public decimal RegulationBuyingPower { get; set; }
        public decimal LongMarketValue { get; set; }
        public decimal ShortMarketValue { get; set; }
        public decimal Equity { get; set; }
        public decimal LastEquity { get; set; }
        public decimal InitialMargin { get; set; }
        public decimal MaintenanceMargin { get; set; }
        public decimal LastMaintenanceMargin { get; set; }
        public long DayTradeCount { get; set; }
        public decimal Sma { get; set; }
        public bool IsAccountBlocked { get; set; }
    }


}
