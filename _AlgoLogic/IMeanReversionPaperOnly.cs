using System;
using System.Threading.Tasks;
using Alpaca.Markets;

namespace SSAPIInteractionHelper._AlgoLogic
{
    internal interface IMeanReversionPaperOnly : IDisposable
    {
        Task Run();
        new void Dispose();
        Task AwaitMarketOpen();
        Task SubmitOrder(int quantity, Decimal price, OrderSide side);
        Task ClosePositionAtMarket();
    }
}