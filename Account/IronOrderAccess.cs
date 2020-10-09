using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Alpaca.Markets;
using SSAPIInteractionHelper._Helpers;

namespace SSAPIInteractionHelper.Account
{

    public interface IIronOrderAccess
    {

        /// <summary>
        /// 
        /// </summary>
        void CloseAllOrders();

        Task IronOrderSubmit(string query);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="price"></param>
        /// <param name="side"></param>
        /// <param name="ticker"></param>
        /// <returns></returns>
        Task SubmitOrder(int quantity, decimal price, OrderSide side, string ticker = "");
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task ClosePositionAtMarket();
    }

    /// <summary>
    /// 
    /// </summary>
    public class IronOrderAccess : IIronOrderAccess
    {
        public CancellationToken Token;
        private static IronFinancials ironfe = new IronFinancials();
        private int positionQuantity;
        private decimal positionValue;
        private static HttpClient _localClient = _Helpers.HttpClientSingleton.IronClient;
        public IronOrderAccess()
        {
            ironfe.token.TryAdd("IronAccountAccess", new CancellationTokenSource());
            Token = ironfe.token["IronAccountAccess"].Token;
        }
        
        internal async Task<IReadOnlyList<IOrder>> UpdateOrderList()
        {
        var sort = new ListOrdersRequest();
        sort.OrderListSorting = SortDirection.Ascending;
            return await ironfe.IronTrade.ListOrdersAsync(sort,Token);
        }
        public async void CloseAllOrders()
        {
            await CloseAllOrdersAccess();
        }

        private async 
        Task
        CloseAllOrdersAccess()
        {
            try
            {
                foreach (var order in UpdateOrderList().Result)
                    await ironfe.IronTrade.DeleteOrderAsync(order.OrderId, Token);
            }
            catch (Exception e)
            {
                try
                {
                    ironfe.IronOrdersFull = UpdateOrderList().Result;
                }
                catch (HttpRequestException ex)
                {
                    Debug.WriteLine(
                        "Retrieving open orders there might be a connectivity issue ",
                        e);
                    throw new HttpRequestException(
                        "Error closing open order and attempting to retrieve open orders there might be a connectivity issue ",
                        ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("Its all wrong its just wrong in attempting to retrieve Orders", ex);
                }

                Debug.WriteLine("Error closing open orders ", e);
                throw new Exception("Error closing open orders ", e);
            }

            await SubmitOrder(4, (decimal) 3.3, OrderSide.Sell);
            await SubmitOrder(4, (decimal) 3.3, OrderSide.Buy);
            try

            {
                var currentPosition = await ironfe.IronTrade.GetPositionAsync("", Token);
                positionQuantity = currentPosition.Quantity;
                positionValue = currentPosition.MarketValue;
            }
            catch (Exception)
            {
                // No position exists. This exception can be safely ignored.
            }
        }

        public async Task IronOrderSubmit(string query)
        {
            HttpClientSingleton.SetHeaders(ironfe.KEY_ID,ironfe.APCA_API_KEY_ID,ironfe.SECRET,ironfe.APCA_API_SECRET_KEY);
            var test = QueryBuilderJSON.Build(query);
            var res = await _localClient.GetAsync(test,Token);
        }
        /// <summary>
        ///     Submit Order takes the quantaty price you are willing to pay and if it is a sell or buy order
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="price"></param>
        /// <param name="side"></param>
        /// <param name="ticker" </param>
        /// <returns></returns>
        ///
        /// Alpaca API version
        public async Task SubmitOrder(int quantity, decimal price, OrderSide side, string ticker = "")
        {
            if (quantity == 0) return;

            try
            {
                var order = await ironfe.IronTrade.PostOrderAsync(
                    side.Limit(ticker, quantity, price), Token);

                var lastTradeId = order.OrderId;
                var lastTradeOpen = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Warning: " + e.Message);
            }
        }

        public async Task ClosePositionAtMarket()
        {
            try
            {
                var positionQuantity = (await ironfe.IronTrade.GetPositionAsync("", Token)).Quantity;
                Console.WriteLine("Closing position at market price.");
                if (positionQuantity > 0)
                    await ironfe.IronTrade.PostOrderAsync(
                        OrderSide.Sell.Market("", positionQuantity), Token);
                else
                    await ironfe.IronTrade.PostOrderAsync(
                        OrderSide.Buy.Market("", Math.Abs(positionQuantity)), Token);
            }
            catch (Exception)
            {
                // No position to exit.
            }
        }
    }
}