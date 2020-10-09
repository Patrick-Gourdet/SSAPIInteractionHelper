using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alpaca.Markets;
using SSAPIInteractionHelper.Account;

namespace SSAPIInteractionHelper._AlgoLogic
{
    // This version of the mean reversion example algorithm uses only API features which
    // are available to users with a free account that can only be used for paper trading.
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal sealed  class MeanReversionPaperOnly : IMeanReversionPaperOnly
    {
        ///private string API_KEY = "REPLACEME";
        ///private string API_SECRET = "REPLACEME"; <see cref="IronFinancials"/>

        private string symbol = "SPY";
        public CancellationToken Token;
        private static readonly IronFinancials ironfe = new IronFinancials();
        private Guid lastTradeId = Guid.NewGuid();
        internal readonly CancellationToken token = new CancellationToken();
        public async Task Run()
        {
            //This is in the Data Folder
            while(!token.IsCancellationRequested)
            {
                if (token.IsCancellationRequested)
                {
                   ClosePositionAtMarket();
                }
            }
            var sort = new ListOrdersRequest();
            sort.OrderListSorting = SortDirection.Ascending;
            // First, cancel any existing orders so they don't impact our buying power.
            var orders = await ironfe.IronTrade.ListOrdersAsync(sort,Token);
            foreach (var order in orders)
            {
                _ = ironfe.IronTrade.DeleteOrderAsync(order.OrderId, token).Result;
            }

            // Figure out when the market will close so we can prepare to sell beforehand.
            var calendars = ( ironfe.IronTrade
                    .ListCalendarAsync(
                        new CalendarRequest().SetTimeInterval(DateTime.Today.GetInclusiveIntervalFromThat()), token)).Result
                .ToList();
            var calendarDate = calendars.First().TradingDateEst;
            var closingTime = calendars.First().TradingCloseTimeEst;

            closingTime = new DateTime(calendarDate.Year, calendarDate.Month, calendarDate.Day, closingTime.Hour,
                closingTime.Minute, closingTime.Second);

            Console.WriteLine("Waiting for market open...");
            _ = AwaitMarketOpen();
            Console.WriteLine("Market opened.");

            // Check every minute for price updates.
            TimeSpan timeUntilClose = closingTime - DateTime.UtcNow;
            while (timeUntilClose.TotalMinutes > 15)
            {
                // Cancel old order if it's not already been filled.
                _ = ironfe.IronTrade.DeleteOrderAsync(lastTradeId, token).Result;

                // Get information about current account value.
                var account =  ironfe.IronTrade.GetAccountAsync(token).Result;
                Decimal buyingPower = account.BuyingPower;
                Decimal portfolioValue = account.Equity;

                // Get information about our existing position.
                int positionQuantity = 0;
                Decimal positionValue = 0;
                try
                {
                    var currentPosition =  ironfe.IronTrade.GetPositionAsync(symbol, token).Result;
                    positionQuantity = currentPosition.Quantity;
                    positionValue = currentPosition.MarketValue;
                }
                catch (Exception)
                {
                    // No position exists. This exception can be safely ignored.
                }

                var barSet =  ironfe.IronData.GetBarSetAsync(
                    new BarSetRequest(symbol, TimeFrame.Minute) {Limit = 20}, token).Result;
                var bars = barSet[symbol].ToList();

                Decimal avg = bars.Average(item => item.Close);
                Decimal currentPrice = bars.Last().Close;
                Decimal diff = avg - currentPrice;

                if (diff <= 0)
                {
                    // Above the 20 minute average - exit any existing long position.
                    if (positionQuantity > 0)
                    {
                        Console.WriteLine("Setting position to zero.");
                         _ = SubmitOrder(positionQuantity, currentPrice, OrderSide.Sell);
                    }
                    else
                    {
                        Console.WriteLine("No position to exit.");
                    }
                }
                else
                {
                    // Allocate a percent of our portfolio to this position.
                    Decimal portfolioShare = diff / currentPrice * ironfe.Spec;
                    Decimal targetPositionValue = portfolioValue * portfolioShare;
                    Decimal amountToAdd = targetPositionValue - positionValue;

                    if (amountToAdd > 0)
                    {
                        // Buy as many shares as we can without going over amountToAdd.

                        // Make sure we're not trying to buy more than we can.
                        if (amountToAdd > buyingPower)
                        {
                            amountToAdd = buyingPower;
                        }

                        int qtyToBuy = (int) (amountToAdd / currentPrice);

                         _ = SubmitOrder(qtyToBuy, currentPrice, OrderSide.Buy);
                    }
                    else
                    {
                        // Sell as many shares as we can without going under amountToAdd.

                        // Make sure we're not trying to sell more than we have.
                        amountToAdd *= -1;
                        int qtyToSell = (int) (amountToAdd / currentPrice);
                        if (qtyToSell > positionQuantity)
                        {
                            qtyToSell = positionQuantity;
                        }

                        _ = SubmitOrder(qtyToSell, currentPrice, OrderSide.Sell);
                    }
                }

                // Wait another minute.
                Thread.Sleep(60000);
                timeUntilClose = closingTime - DateTime.UtcNow;
            }

            Console.WriteLine("Market nearing close; closing position.");
            _ = ClosePositionAtMarket();
        }

        public void Dispose()
        {
            ironfe.IronTrade?.Dispose();
            ironfe.IronTrade?.Dispose();
        }

        public async Task AwaitMarketOpen()
        {
            while (!(ironfe.IronTrade.GetClockAsync(token).Result).IsOpen)
            {
                _ = Task.Delay(60000, token);
            }
        }

        // Submit an order if quantity is not zero.
        public async Task SubmitOrder(int quantity, Decimal price, OrderSide side)
        {
            if (quantity == 0)
            {
                Console.WriteLine("No order necessary.");
                return;
            }

            Console.WriteLine($"Submitting {side} order for {quantity} shares at ${price}.");
            var order =  ironfe.IronTrade.PostOrderAsync(
                side.Limit(symbol, quantity, price), token).Result;
            lastTradeId = order.OrderId;
        }

        public async Task ClosePositionAtMarket()
        {
            try
            {
                var positionQuantity = ( ironfe.IronTrade.GetPositionAsync(symbol, token).Result).Quantity;
                _ = ironfe.IronTrade.PostOrderAsync(
                    OrderSide.Sell.Market(symbol, positionQuantity), token).Result ;
            }
            catch (Exception)
            {
                // No position to exit.
            }
        }
    }
}