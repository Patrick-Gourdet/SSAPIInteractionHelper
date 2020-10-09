using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Alpaca.Markets;


namespace SSAPIInteractionHelper.Account
{
    public class IronAccount : IIronAccount
    {
        public CancellationToken Token;
        private static readonly IronFinancials _ironfe = new IronFinancials();
        public IronAccount()
        {
             _ironfe.token.Add("IronAccountAccess", new CancellationTokenSource());
             Token = _ironfe.token["IronAccountAccess"].Token;
        }

        /// Gets account information from Alpaca REST API endpoint.
        public async Task<IReadOnlyList<IOrder>> IronOrdersList()
        {
            var sort = new ListOrdersRequest();
            sort.OrderListSorting = SortDirection.Ascending;
            IReadOnlyList<IOrder> list = await _ironfe.IronTrade.ListOrdersAsync(sort,Token);
            return list;
        }
        /// <summary>
        /// GET request for Account information
        /// <code>IronFinancials.IronTradeReal.GetAccountAsync(Token)</code>
        /// </summary>
        /// <returns>IAccount</returns>
        public async Task<(IAccount,string,decimal,decimal,bool,long )> IronAccountValue()
        {
            var list = await _ironfe.IronTrade.GetAccountAsync(Token);
            var accountNumber = list.AccountNumber;
            var buyingPower = list.BuyingPower;
            var equity = list.Equity;
            var isBlocked = list.IsAccountBlocked;
            var multiplier = list.Multiplier;
            
            return (list,accountNumber,buyingPower,equity,isBlocked, multiplier);
        }
        // Make sure we know how much we should spend on our position.

        /// Gets account configuration settings from Alpaca REST API endpoint.
        //internal async Task<object> IronCurrentSettings()
        //{

        //}
        /// Updates account configuration settings using Alpaca REST API endpoint.
        /// Gets list of account activities from Alpaca REST API endpoint by specific activity.
        /// Gets portfolio equity history from Alpaca REST API endpoint.
        //public async Task<IPortfolioHistory> IronPortfolioHistory(AccountActivityType activityType)
        //{
        //    IPortfolioHistory info = await IronFinancials.IronTradeReal.GetPortfolioHistoryAsync(,Token);
        //    return info;
        //}
        /// Gets list of all available assets from Alpaca REST API endpoint.
        /// Gets list of available assets from Alpaca REST API endpoint.
        /// Get single asset information by asset name from Alpaca REST API endpoint.
        /// Gets list of available positions from Alpaca REST API endpoint.
        /// Gets position information by asset name from Alpaca REST API endpoint.
        /// Liquidates all open positions at market price using Alpaca REST API endpoint.
        /// Liquidate an open position at market price using Alpaca REST API endpoint.
        /// Get current time information from Alpaca REST API endpoint.
        /// Gets list of all trading days from Alpaca REST API endpoint.
        /// Gets list of trading days from Alpaca REST API endpoint.
    }
}
