<a name='assembly'></a>
# IronFinancialsAPI

## Contents

- [APIConnector](#T-SSAPIInteractionHelper-APIConnector 'SSAPIInteractionHelper.APIConnector')
- [APIDataHub](#T-SSAPIInteractionHelper-APIDataHub 'SSAPIInteractionHelper.APIDataHub')
  - [GetExchangeList()](#M-SSAPIInteractionHelper-APIDataHub-GetExchangeList 'SSAPIInteractionHelper.APIDataHub.GetExchangeList')
- [AccountStatus](#T-SSAPIInteractionHelper-Account-AccountEnum-AccountStatus 'SSAPIInteractionHelper.Account.AccountEnum.AccountStatus')
- [BackUpCacheActionStorage](#T-SSAPIInteractionHelper-_Helpers-BackUpCacheActionStorage 'SSAPIInteractionHelper._Helpers.BackUpCacheActionStorage')
- [FMPCommon](#T-SSAPIInteractionHelper-FMPCommon 'SSAPIInteractionHelper.FMPCommon')
  - [_limit](#F-SSAPIInteractionHelper-FMPCommon-_limit 'SSAPIInteractionHelper.FMPCommon._limit')
  - [stFmpParametersDataTypeCsv](#F-SSAPIInteractionHelper-FMPCommon-stFmpParametersDataTypeCsv 'SSAPIInteractionHelper.FMPCommon.stFmpParametersDataTypeCsv')
- [Fidelity](#T-SSAPIInteractionHelper-Data-Fidelity 'SSAPIInteractionHelper.Data.Fidelity')
  - [FidelityConnectionString](#F-SSAPIInteractionHelper-Data-Fidelity-FidelityConnectionString 'SSAPIInteractionHelper.Data.Fidelity.FidelityConnectionString')
  - [GetFidelityString(date)](#M-SSAPIInteractionHelper-Data-Fidelity-GetFidelityString-System-String- 'SSAPIInteractionHelper.Data.Fidelity.GetFidelityString(System.String)')
- [Files](#T-SSAPIInteractionHelper-_Helpers-Files 'SSAPIInteractionHelper._Helpers.Files')
- [FinancialStatements](#T-SSAPIInteractionHelper-Data-FinancialStatements 'SSAPIInteractionHelper.Data.FinancialStatements')
  - [_stFmpIncomeStatement](#F-SSAPIInteractionHelper-Data-FinancialStatements-_stFmpIncomeStatement 'SSAPIInteractionHelper.Data.FinancialStatements._stFmpIncomeStatement')
- [IInteractionHelper\`1](#T-SSAPIInteractionHelper-Data-Interfaces-IInteractionHelper`1 'SSAPIInteractionHelper.Data.Interfaces.IInteractionHelper`1')
- [IIronAccount](#T-SSAPIInteractionHelper-Account-IIronAccount 'SSAPIInteractionHelper.Account.IIronAccount')
  - [IronOrdersList()](#M-SSAPIInteractionHelper-Account-IIronAccount-IronOrdersList 'SSAPIInteractionHelper.Account.IIronAccount.IronOrdersList')
- [IIronFinancials](#T-SSAPIInteractionHelper-Account-IIronFinancials 'SSAPIInteractionHelper.Account.IIronFinancials')
  - [SetHistoryRequest()](#M-SSAPIInteractionHelper-Account-IIronFinancials-SetHistoryRequest 'SSAPIInteractionHelper.Account.IIronFinancials.SetHistoryRequest')
- [IIronOrderAccess](#T-SSAPIInteractionHelper-Account-IIronOrderAccess 'SSAPIInteractionHelper.Account.IIronOrderAccess')
  - [CloseAllOrders()](#M-SSAPIInteractionHelper-Account-IIronOrderAccess-CloseAllOrders 'SSAPIInteractionHelper.Account.IIronOrderAccess.CloseAllOrders')
  - [ClosePositionAtMarket()](#M-SSAPIInteractionHelper-Account-IIronOrderAccess-ClosePositionAtMarket 'SSAPIInteractionHelper.Account.IIronOrderAccess.ClosePositionAtMarket')
  - [SubmitOrder(quantity,price,side,ticker)](#M-SSAPIInteractionHelper-Account-IIronOrderAccess-SubmitOrder-System-Int32,System-Decimal,Alpaca-Markets-OrderSide,System-String- 'SSAPIInteractionHelper.Account.IIronOrderAccess.SubmitOrder(System.Int32,System.Decimal,Alpaca.Markets.OrderSide,System.String)')
- [IronAccount](#T-SSAPIInteractionHelper-Account-IronAccount 'SSAPIInteractionHelper.Account.IronAccount')
  - [IronAccountValue()](#M-SSAPIInteractionHelper-Account-IronAccount-IronAccountValue 'SSAPIInteractionHelper.Account.IronAccount.IronAccountValue')
  - [IronOrdersList()](#M-SSAPIInteractionHelper-Account-IronAccount-IronOrdersList 'SSAPIInteractionHelper.Account.IronAccount.IronOrdersList')
- [IronFinancials](#T-SSAPIInteractionHelper-Account-IronFinancials 'SSAPIInteractionHelper.Account.IronFinancials')
  - [IronAccountActivityTypes](#F-SSAPIInteractionHelper-Account-IronFinancials-IronAccountActivityTypes 'SSAPIInteractionHelper.Account.IronFinancials.IronAccountActivityTypes')
  - [_ironlClientPaper](#P-SSAPIInteractionHelper-Account-IronFinancials-_ironlClientPaper 'SSAPIInteractionHelper.Account.IronFinancials._ironlClientPaper')
  - [SetHistoryRequest()](#M-SSAPIInteractionHelper-Account-IronFinancials-SetHistoryRequest 'SSAPIInteractionHelper.Account.IronFinancials.SetHistoryRequest')
  - [SetTradingEnvironment()](#M-SSAPIInteractionHelper-Account-IronFinancials-SetTradingEnvironment 'SSAPIInteractionHelper.Account.IronFinancials.SetTradingEnvironment')
- [IronOrder](#T-SSAPIInteractionHelper-Account-IronModules-IronOrder 'SSAPIInteractionHelper.Account.IronModules.IronOrder')
- [IronOrderAccess](#T-SSAPIInteractionHelper-Account-IronOrderAccess 'SSAPIInteractionHelper.Account.IronOrderAccess')
- [IronUserAccount](#T-SSAPIInteractionHelper-Account-IronModules-IronUserAccount 'SSAPIInteractionHelper.Account.IronModules.IronUserAccount')
- [MeanReversionPaperOnly](#T-SSAPIInteractionHelper-_AlgoLogic-MeanReversionPaperOnly 'SSAPIInteractionHelper._AlgoLogic.MeanReversionPaperOnly')
  - [symbol](#F-SSAPIInteractionHelper-_AlgoLogic-MeanReversionPaperOnly-symbol 'SSAPIInteractionHelper._AlgoLogic.MeanReversionPaperOnly.symbol')
- [QueryBuilder](#T-SSAPIInteractionHelper-_Helpers-QueryBuilder 'SSAPIInteractionHelper._Helpers.QueryBuilder')
  - [sb](#F-SSAPIInteractionHelper-_Helpers-QueryBuilder-sb 'SSAPIInteractionHelper._Helpers.QueryBuilder.sb')
  - [Build(query)](#M-SSAPIInteractionHelper-_Helpers-QueryBuilder-Build-System-String- 'SSAPIInteractionHelper._Helpers.QueryBuilder.Build(System.String)')
- [QueryBuilderJSON](#T-SSAPIInteractionHelper-_Helpers-QueryBuilderJSON 'SSAPIInteractionHelper._Helpers.QueryBuilderJSON')
  - [GetStringLength(query)](#M-SSAPIInteractionHelper-_Helpers-QueryBuilderJSON-GetStringLength-System-String- 'SSAPIInteractionHelper._Helpers.QueryBuilderJSON.GetStringLength(System.String)')
- [SetUp](#T-SSAPIInteractionHelper-SetUp 'SSAPIInteractionHelper.SetUp')
  - [ConfigureApi()](#M-SSAPIInteractionHelper-SetUp-ConfigureApi 'SSAPIInteractionHelper.SetUp.ConfigureApi')
- [StockDecisionMetrics](#T-SSAPIInteractionHelper-Data-StockDecisionMetrics 'SSAPIInteractionHelper.Data.StockDecisionMetrics')
  - [SSAPIInteractionHelper#Data#Interfaces#IStockDecisionMetrics#CompanyDiscountCashFlowValue()](#M-SSAPIInteractionHelper-Data-StockDecisionMetrics-SSAPIInteractionHelper#Data#Interfaces#IStockDecisionMetrics#CompanyDiscountCashFlowValue 'SSAPIInteractionHelper.Data.StockDecisionMetrics.SSAPIInteractionHelper#Data#Interfaces#IStockDecisionMetrics#CompanyDiscountCashFlowValue')
- [StockInformation](#T-SSAPIInteractionHelper-Data-StockInformation 'SSAPIInteractionHelper.Data.StockInformation')
  - [#ctor()](#M-SSAPIInteractionHelper-Data-StockInformation-#ctor 'SSAPIInteractionHelper.Data.StockInformation.#ctor')
  - [_stFmpCompanyStockList](#F-SSAPIInteractionHelper-Data-StockInformation-_stFmpCompanyStockList 'SSAPIInteractionHelper.Data.StockInformation._stFmpCompanyStockList')
  - [_stfmpStockRealTime](#F-SSAPIInteractionHelper-Data-StockInformation-_stfmpStockRealTime 'SSAPIInteractionHelper.Data.StockInformation._stfmpStockRealTime')
- [TickerCache](#T-SSAPIInteractionHelper-Account-TickerCache 'SSAPIInteractionHelper.Account.TickerCache')
  - [Ticker](#P-SSAPIInteractionHelper-Account-TickerCache-Ticker 'SSAPIInteractionHelper.Account.TickerCache.Ticker')
  - [CacheTickers(tickers)](#M-SSAPIInteractionHelper-Account-TickerCache-CacheTickers-System-String[]- 'SSAPIInteractionHelper.Account.TickerCache.CacheTickers(System.String[])')
  - [Update(tickers)](#M-SSAPIInteractionHelper-Account-TickerCache-Update 'SSAPIInteractionHelper.Account.TickerCache.Update')

<a name='T-SSAPIInteractionHelper-APIConnector'></a>
## APIConnector `type`

##### Namespace

SSAPIInteractionHelper

##### Summary

This is where Auto Fac implementation will sit and all API

<a name='T-SSAPIInteractionHelper-APIDataHub'></a>
## APIDataHub `type`

##### Namespace

SSAPIInteractionHelper

<a name='M-SSAPIInteractionHelper-APIDataHub-GetExchangeList'></a>
### GetExchangeList() `method`

##### Summary

This endpoint creates and calls and retrieves the stock list offerings from
the information api.

##### Returns



##### Parameters

This method has no parameters.

<a name='T-SSAPIInteractionHelper-Account-AccountEnum-AccountStatus'></a>
## AccountStatus `type`

##### Namespace

SSAPIInteractionHelper.Account.AccountEnum

##### Summary

ONBOARDING The account is onboarding.
SUBMISSION_FAILED The account application submission failed for some reason.
SUBMITTED The account application has been submitted for review.
ACCOUNT_UPDATED The account information is being updated.
APPROVAL_PENDING The final account approval is pending.
ACTIVE The account is active for trading.
REJECTED The account application has been rejected

##### Remarks

This is to query current account standing
This emulates exact structure of the C#
Alpaca API which would allow to create
different query methods using said API

<a name='T-SSAPIInteractionHelper-_Helpers-BackUpCacheActionStorage'></a>
## BackUpCacheActionStorage `type`

##### Namespace

SSAPIInteractionHelper._Helpers

##### Summary

This is the lock to make the reading from the data pipline thread safe for any data type
that does not support thread safety

<a name='T-SSAPIInteractionHelper-FMPCommon'></a>
## FMPCommon `type`

##### Namespace

SSAPIInteractionHelper

<a name='F-SSAPIInteractionHelper-FMPCommon-_limit'></a>
### _limit `constants`

##### Summary

Use the following endpoint for ratios calculated from Balance sheet

```
string Base = "base" + "endpoint" + ?"{TICKER}"
```

<a name='F-SSAPIInteractionHelper-FMPCommon-stFmpParametersDataTypeCsv'></a>
### stFmpParametersDataTypeCsv `constants`

<a name='T-SSAPIInteractionHelper-Data-Fidelity'></a>
## Fidelity `type`

##### Namespace

SSAPIInteractionHelper.Data

<a name='F-SSAPIInteractionHelper-Data-Fidelity-FidelityConnectionString'></a>
### FidelityConnectionString `constants`

##### Summary

Fidelity string is used in conjunction with the FIdelityDividend date
    which is built in the Interaction Helper Class for use where the Library is
    distributed

<a name='M-SSAPIInteractionHelper-Data-Fidelity-GetFidelityString-System-String-'></a>
### GetFidelityString(date) `method`

##### Summary

This concatenates the string to query the fidelity dividend calendar. This is not official
and the returned http response parser remains to be implemented but it is a great resource
or potential fall back resource should alpaca and or

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| date | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-SSAPIInteractionHelper-_Helpers-Files'></a>
## Files `type`

##### Namespace

SSAPIInteractionHelper._Helpers

<a name='T-SSAPIInteractionHelper-Data-FinancialStatements'></a>
## FinancialStatements `type`

##### Namespace

SSAPIInteractionHelper.Data

<a name='F-SSAPIInteractionHelper-Data-FinancialStatements-_stFmpIncomeStatement'></a>
### _stFmpIncomeStatement `constants`

<a name='T-SSAPIInteractionHelper-Data-Interfaces-IInteractionHelper`1'></a>
## IInteractionHelper\`1 `type`

##### Namespace

SSAPIInteractionHelper.Data.Interfaces

##### Summary



##### Generic Types

| Name | Description |
| ---- | ----------- |
| T |  |

<a name='T-SSAPIInteractionHelper-Account-IIronAccount'></a>
## IIronAccount `type`

##### Namespace

SSAPIInteractionHelper.Account

<a name='M-SSAPIInteractionHelper-Account-IIronAccount-IronOrdersList'></a>
### IronOrdersList() `method`

##### Parameters

This method has no parameters.

<a name='T-SSAPIInteractionHelper-Account-IIronFinancials'></a>
## IIronFinancials `type`

##### Namespace

SSAPIInteractionHelper.Account

<a name='M-SSAPIInteractionHelper-Account-IIronFinancials-SetHistoryRequest'></a>
### SetHistoryRequest() `method`

##### Parameters

This method has no parameters.

<a name='T-SSAPIInteractionHelper-Account-IIronOrderAccess'></a>
## IIronOrderAccess `type`

##### Namespace

SSAPIInteractionHelper.Account

<a name='M-SSAPIInteractionHelper-Account-IIronOrderAccess-CloseAllOrders'></a>
### CloseAllOrders() `method`

##### Summary



##### Parameters

This method has no parameters.

<a name='M-SSAPIInteractionHelper-Account-IIronOrderAccess-ClosePositionAtMarket'></a>
### ClosePositionAtMarket() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='M-SSAPIInteractionHelper-Account-IIronOrderAccess-SubmitOrder-System-Int32,System-Decimal,Alpaca-Markets-OrderSide,System-String-'></a>
### SubmitOrder(quantity,price,side,ticker) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| quantity | [System.Int32](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Int32 'System.Int32') |  |
| price | [System.Decimal](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Decimal 'System.Decimal') |  |
| side | [Alpaca.Markets.OrderSide](#T-Alpaca-Markets-OrderSide 'Alpaca.Markets.OrderSide') |  |
| ticker | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-SSAPIInteractionHelper-Account-IronAccount'></a>
## IronAccount `type`

##### Namespace

SSAPIInteractionHelper.Account

<a name='M-SSAPIInteractionHelper-Account-IronAccount-IronAccountValue'></a>
### IronAccountValue() `method`

##### Summary

GET request for Account information

```
IronFinancials.IronTradeReal.GetAccountAsync(Token)
```

##### Returns

IAccount

##### Parameters

This method has no parameters.

<a name='M-SSAPIInteractionHelper-Account-IronAccount-IronOrdersList'></a>
### IronOrdersList() `method`

##### Parameters

This method has no parameters.

<a name='T-SSAPIInteractionHelper-Account-IronFinancials'></a>
## IronFinancials `type`

##### Namespace

SSAPIInteractionHelper.Account

##### Summary

This is the IronFinancials client library which will be shared throughout the
    Entire eco system of micro services via import of DLL

```
var foo = Config.Client
```

will return a fully qualified alpaca client with all its attributes
    for API specifics

<a name='F-SSAPIInteractionHelper-Account-IronFinancials-IronAccountActivityTypes'></a>
### IronAccountActivityTypes `constants`

##### Summary

TODO Need orders updater

<a name='P-SSAPIInteractionHelper-Account-IronFinancials-_ironlClientPaper'></a>
### _ironlClientPaper `property`

##### Summary

This is the access to either the paper or live access

<a name='M-SSAPIInteractionHelper-Account-IronFinancials-SetHistoryRequest'></a>
### SetHistoryRequest() `method`

##### Parameters

This method has no parameters.

<a name='M-SSAPIInteractionHelper-Account-IronFinancials-SetTradingEnvironment'></a>
### SetTradingEnvironment() `method`

##### Summary

This is the platform of choice to place bids for stock and holder of the account data

##### Parameters

This method has no parameters.

##### Remarks

API_BASE_URL =
    
    API_BASE_URL_LIVE

<a name='T-SSAPIInteractionHelper-Account-IronModules-IronOrder'></a>
## IronOrder `type`

##### Namespace

SSAPIInteractionHelper.Account.IronModules

##### Summary

Local order class emulates the Alpaca model for implementation
through a direct connection avoiding the Alpaca API and the lag
that comes with it

##### Remarks

Not yet implemented as the direct connection using HTTPCLIENT
[](#!-HttpClientSingleton-cs 'HttpClientSingleton.cs')
was my preferred implementation method but it is always nice to
have the option.

<a name='T-SSAPIInteractionHelper-Account-IronOrderAccess'></a>
## IronOrderAccess `type`

##### Namespace

SSAPIInteractionHelper.Account

##### Summary



<a name='T-SSAPIInteractionHelper-Account-IronModules-IronUserAccount'></a>
## IronUserAccount `type`

##### Namespace

SSAPIInteractionHelper.Account.IronModules

##### Summary

Local Account class emulates the Alpaca model for implementation
through a direct connection avoiding the Alpaca API and the lag
that comes with it

##### Remarks

Not yet implemented as the direct connection using HTTPCLIENT
[](#!-HttpClientSingleton-cs 'HttpClientSingleton.cs')
was my preferred implementation method but it is always nice to
have the option.

<a name='T-SSAPIInteractionHelper-_AlgoLogic-MeanReversionPaperOnly'></a>
## MeanReversionPaperOnly `type`

##### Namespace

SSAPIInteractionHelper._AlgoLogic

<a name='F-SSAPIInteractionHelper-_AlgoLogic-MeanReversionPaperOnly-symbol'></a>
### symbol `constants`

<a name='T-SSAPIInteractionHelper-_Helpers-QueryBuilder'></a>
## QueryBuilder `type`

##### Namespace

SSAPIInteractionHelper._Helpers

<a name='F-SSAPIInteractionHelper-_Helpers-QueryBuilder-sb'></a>
### sb `constants`

<a name='M-SSAPIInteractionHelper-_Helpers-QueryBuilder-Build-System-String-'></a>
### Build(query) `method`

##### Summary

The query string uses a Q in the request which is the start of the query
you may change this as you see fit, the thoughts where to further obfuscate
the query for security and avoidance basic SQL fuzzing techniques.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| query | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-SSAPIInteractionHelper-_Helpers-QueryBuilderJSON'></a>
## QueryBuilderJSON `type`

##### Namespace

SSAPIInteractionHelper._Helpers

##### Summary

The Alpaca API only accepts JSON String for the requests made to buy and sell
I have not checked all endpoints yet. Basic calls like Account Info can be made
using only the URI string

<a name='M-SSAPIInteractionHelper-_Helpers-QueryBuilderJSON-GetStringLength-System-String-'></a>
### GetStringLength(query) `method`

##### Summary

NOT FULLY TESTED YET, THE CODE NEEDS TO BE STEP THROUGH TO ASSURE THAT THE DECENT PARSER
IS ADDING THE CORRECT VALUES THIS DOES WORK LIKE A CHARM IN THE QUERY BUILDER

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| query | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |

<a name='T-SSAPIInteractionHelper-SetUp'></a>
## SetUp `type`

##### Namespace

SSAPIInteractionHelper

##### Summary

This is called to setup all the financial API endpoints

<a name='M-SSAPIInteractionHelper-SetUp-ConfigureApi'></a>
### ConfigureApi() `method`

##### Summary



##### Parameters

This method has no parameters.

<a name='T-SSAPIInteractionHelper-Data-StockDecisionMetrics'></a>
## StockDecisionMetrics `type`

##### Namespace

SSAPIInteractionHelper.Data

<a name='M-SSAPIInteractionHelper-Data-StockDecisionMetrics-SSAPIInteractionHelper#Data#Interfaces#IStockDecisionMetrics#CompanyDiscountCashFlowValue'></a>
### SSAPIInteractionHelper#Data#Interfaces#IStockDecisionMetrics#CompanyDiscountCashFlowValue() `method`

##### Summary

Discount Cash Flow is an income based method of valuation
of a Company
1. Discounted cash flow (DCF) helps determine the value of an investment
    based on its future cash flows.
2. The present value of expected future cash flows is arrived at by
    using a discount rate to calculate the discounted cash flow (DCF).
3. If the discounted cash flow (DCF) is above the current cost of the
    investment, the opportunity could result in positive returns.
4. Companies typically use the weighted average cost of capital for the
    discount rate, as it takes into consideration the rate of return expected
    by shareholders.
5. The DCF has limitations, primarily that it relies on estimations on future
    cash flows, which could prove to be inaccurate.

##### Returns



##### Parameters

This method has no parameters.

<a name='T-SSAPIInteractionHelper-Data-StockInformation'></a>
## StockInformation `type`

##### Namespace

SSAPIInteractionHelper.Data

##### Summary

- This class supplies all real time data with all
    ticker symbols available in real time
- The realtime data is for a single ticker symbol or
    all symbols
- This also allows to search for ticker symbols with an
    estimated string value to search for
- Complete ticker list offered from FMP

<a name='M-SSAPIInteractionHelper-Data-StockInformation-#ctor'></a>
### #ctor() `constructor`

##### Summary



##### Parameters

This constructor has no parameters.

<a name='F-SSAPIInteractionHelper-Data-StockInformation-_stFmpCompanyStockList'></a>
### _stFmpCompanyStockList `constants`

<a name='F-SSAPIInteractionHelper-Data-StockInformation-_stfmpStockRealTime'></a>
### _stfmpStockRealTime `constants`

<a name='T-SSAPIInteractionHelper-Account-TickerCache'></a>
## TickerCache `type`

##### Namespace

SSAPIInteractionHelper.Account

##### Summary

Ticker cache to minimize the database calls

<a name='P-SSAPIInteractionHelper-Account-TickerCache-Ticker'></a>
### Ticker `property`

##### Summary

This is a thread safe collection of tickers due to multiple api access points

<a name='M-SSAPIInteractionHelper-Account-TickerCache-CacheTickers-System-String[]-'></a>
### CacheTickers(tickers) `method`

##### Summary

Slightly Rigged but only because there is more functionality coming

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| tickers | [System.String[]](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String[] 'System.String[]') |  |

<a name='M-SSAPIInteractionHelper-Account-TickerCache-Update'></a>
### Update(tickers) `method`

##### Summary

This is called from the controller to set the tickers currently traded;

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| tickers | [M:SSAPIInteractionHelper.Account.TickerCache.Update](#T-M-SSAPIInteractionHelper-Account-TickerCache-Update 'M:SSAPIInteractionHelper.Account.TickerCache.Update') |  |
