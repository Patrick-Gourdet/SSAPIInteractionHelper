using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace SSAPIInteractionHelper.Account
{
    /// <summary>
    /// Ticker cache to minimize the database calls
    /// </summary>
    public static class TickerCache 
    {
        private static ConcurrentQueue<string> _tickers;

        /// <summary>
        /// This is a thread safe collection of tickers due to multiple api access points
        /// </summary>
        public static ConcurrentQueue<string> Ticker
        {
            get => _tickers;
            set =>  _tickers = Update();
        }

        /// TODO update tickers to the current status
        /// TODO Call tickers to save potential database calls
        /// <summary>
        /// This is called from the controller to set the tickers currently traded;
        /// </summary>
        /// <param name="tickers"></param>
        private static ConcurrentQueue<string> Update()
        {
            return _tickers;
        }
        /// <summary>
        /// Slightly Rigged but only because there is more functionality coming
        /// </summary>
        /// <param name="tickers"></param>
        public static void CacheTickers(string[] tickers)
        {
            if (_tickers.IsEmpty)
            {
                _tickers = new ConcurrentQueue<string>(tickers);
            }
            else if (Ticker.Count != _tickers.Count && !_tickers.Any(tickers.Contains))
            {
                //Update
            }
            else
            {
                Debug.WriteLine("Nothing to insert");
            }
 
        }
    }
}
