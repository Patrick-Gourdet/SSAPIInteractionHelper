/// Patrick Gourdet Iron Digital Iron Financials 07/3/2020
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using Alpaca.Markets;
// ReSharper disable InconsistentNaming

namespace SSAPIInteractionHelper.Account
{
    public interface IIronFinancials
    {
        /// TODO NICE TO HAVE ACCOUNT HISTORY REQUEST
        void SetHistoryRequest();

        void SetEnvironmentVariable(string str);
    }

    /// <summary>
    ///     This is the IronFinancials client library which will be shared throughout the
    ///     Entire eco system of micro services via import of DLL
    ///     <code>var foo = Config.Client</code>
    ///     will return a fully qualified alpaca client with all its attributes
    ///     for API specifics
    ///     <see>
    ///         <cref>https://docs.alpaca.markets/api-documentation</cref>
    ///     </see>
    ///     <returns>name="alpaca client"</returns>
    /// </summary>
    public class IronFinancials : IIronFinancials
    {
        public  AlpacaTradingClient IronTrade;
        public  AlpacaDataClient IronData;

        internal  Dictionary<string, CancellationTokenSource> token = new Dictionary<string, CancellationTokenSource>();
        internal  IReadOnlyList<IOrder> IronOrdersFull;
        public static CancellationToken Token;
        /// <summary>
        /// TODO Need orders updater 
        /// </summary>
        public  List<AccountActivityType> IronAccountActivityTypes;

       
        public  IronFinancials()
        {
            
            IronAccountActivityTypes = _ironAccountActivityType;
            token.TryAdd("IronFinancials", new CancellationTokenSource());
            Token = token["IronFinancials"].Token;
        }
        public static PortfolioHistoryRequest history = new PortfolioHistoryRequest();
        /// TODO NICE TO HAVE ACCOUNT HISTORY REQUEST
        public void SetHistoryRequest()
        {
            
        }
        private  string PaperOrReal { get; set; }

        public  void SetEnvironmentVariable(string str)
        {
            PaperOrReal = str;
        }

        private  readonly List<AccountActivityType> _ironAccountActivityType = new List<AccountActivityType>
        {
            AccountActivityType.ACATCash,
            AccountActivityType.ACATSecurities,
            AccountActivityType.CashDisbursement,
            AccountActivityType.CashReceipt,
            AccountActivityType.Dividend,
            AccountActivityType.DividendCapitalGainsLongTerm,
            AccountActivityType.DividendCapitalGainsShortTerm,
            AccountActivityType.DividendFee,
            AccountActivityType.DividendForeignTaxWithheld,
            AccountActivityType.DividendNRAWithheld,
            AccountActivityType.DividendReturnOfCapital,
            AccountActivityType.DividendTaxExempt,
            AccountActivityType.DividendTefraWithheld,
            AccountActivityType.Fill,
            AccountActivityType.Interest,
            AccountActivityType.InterestNRAWithheld,
            AccountActivityType.InterestTefraWithheld,
            AccountActivityType.JournalEntry,
            AccountActivityType.JournalEntryCash,
            AccountActivityType.JournalEntryStock,
            AccountActivityType.MergerAcquisition,
            AccountActivityType.Miscellaneous,
            AccountActivityType.NameChange,
            AccountActivityType.PassThruCharge,
            AccountActivityType.PassThruRebate,
            AccountActivityType.Reorg,
            AccountActivityType.SymbolChange,
            AccountActivityType.StockSpinoff,
            AccountActivityType.StockSplit
        };

        internal  int Spec { get; set; } = 100;
        internal  readonly string KEY_ID = "APCA-API-KEY-ID";
        internal  readonly string SECRET = "APCA-API-SECRET-KEY";
        internal  readonly string APCA_API_KEY_ID_PAPER = "";

        
        internal  readonly string APCA_API_SECRET_KEY_PAPER = "33b4910f4a693f37452a36c97cb21e11b124cbc5";
        internal  readonly string APCA_API_KEY_ID = "2e4be347d0f4a3fd11bbf0943f85d6c8";

        // ReSharper disable once InconsistentNaming
        internal  readonly string APCA_API_SECRET_KEY = "33b4910f4a693f37452a36c97cb21e11b124cbc5";
       // https://paper-api.alpaca.markets
        /// <summary>
        ///     This is the access to either the paper or live access
        /// </summary>
        private  AlpacaTradingClient _ironlClientPaper => (AlpacaTradingClient) Environments.Paper.GetAlpacaTradingClient(
            new SecretKey(APCA_API_KEY_ID_PAPER, APCA_API_SECRET_KEY_PAPER));

        private  AlpacaTradingClient _ironClientReal  => (AlpacaTradingClient) Environments.Live
            .GetAlpacaTradingClient(new SecretKey(APCA_API_KEY_ID, APCA_API_SECRET_KEY));

        /// <summary>
        ///     This is the platform of choice to place bids for stock and holder of the account data
        ///     <see>
        ///         <cref>https://docs.alpaca.markets/api-documentation/</cref>
        ///     </see>
        /// </summary>
        /// <remarks>
        ///     API_BASE_URL =
        ///     <see>
        ///         <cref>https://paper-api.alpaca.markets</cref>
        ///     </see>
        ///     API_BASE_URL_LIVE
        ///     <see>
        ///         <cref>https://live-api.alpaca.markets</cref>
        ///     </see>
        /// </remarks>
        private AlpacaTradingClient SetTradingEnvironment()
        {
            return  PaperOrReal switch
            {
                "real" => _ironClientReal,
                "paper" => _ironlClientPaper,
                _ => throw new InvalidOleVariantTypeException("You did not select an Environment")
            };
        }




        //BLHC0G4UHEADY626 May-12-2020	RAND
    }
}