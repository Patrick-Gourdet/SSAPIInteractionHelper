using System.Collections.Generic;
using System.Threading.Tasks;
using Alpaca.Markets;

namespace SSAPIInteractionHelper.Account
{
    public interface IIronAccount
    {
        /// Gets account information from Alpaca REST API endpoint.
        Task<IReadOnlyList<IOrder>> IronOrdersList();
        Task<(IAccount, string, decimal, decimal, bool, long )> IronAccountValue();
    }
}