#nullable enable
using System;
using System.Collections.Generic;
using Alpaca.Markets;
using SSAPIInteractionHelper._Helpers;


namespace SSAPIInteractionHelper.Account.IronModules
{
    /// <summary>
    /// Local order class emulates the Alpaca model for implementation
    /// through a direct connection avoiding the Alpaca API and the lag
    /// that comes with it  
    /// </summary>
    /// <remarks>
    /// Not yet implemented as the direct connection using HTTPCLIENT
    /// <see cref="HttpClientSingleton.cs"/>
    /// was my preferred implementation method but it is always nice to
    /// have the option.
    /// </remarks>
    public partial class IronOrder : IOrder
    {
        public Guid OrderId { get; set; }
        public string? ClientOrderId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? CreatedAtUtc { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public DateTime? SubmittedAtUtc { get; set; }
        public DateTime? FilledAt { get; set; }
        public DateTime? FilledAtUtc { get; set; }
        public DateTime? ExpiredAt { get; set; }
        public DateTime? ExpiredAtUtc { get; set; }
        public DateTime? CancelledAt { get; set; }
        public DateTime? CancelledAtUtc { get; set; }
        public DateTime? FailedAt { get; set; }
        public DateTime? FailedAtUtc { get; set; }
        public Guid AssetId { get; set; }
        public string Symbol { get; set; }
        public AssetClass AssetClass { get; set; }
        public long Quantity { get; set; }
        public long FilledQuantity { get; set; }
        public OrderType OrderType { get; set; }
        public OrderSide OrderSide { get; set; }
        public TimeInForce TimeInForce { get; set; }
        public decimal? LimitPrice { get; set; }
        public decimal? StopPrice { get; set; }
        public decimal? TrailOffsetInDollars { get; set; }
        public decimal? TrailOffsetInPercent { get; set; }
        public decimal? HighWaterMark { get; set; }
        public decimal? AverageFillPrice { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public IReadOnlyList<IOrder>? Legs { get; set; }
    }
}