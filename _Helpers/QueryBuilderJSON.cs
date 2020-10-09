// Patrick Gourdet Iron Financials 07/03/2020
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace SSAPIInteractionHelper._Helpers
{
    /// <summary>
    /// The Alpaca API only accepts JSON String for the requests made to buy and sell
    /// I have not checked all endpoints yet. Basic calls like Account Info can be made
    /// using only the URI string 
    /// </summary>
    static class QueryBuilderJSON
    {
        private static StringBuilder sb = new StringBuilder();
        private static StringWriter sw = new StringWriter(sb);
        private static JsonTextWriter write = new JsonTextWriter(sw);

        private static string _endPoint { get; set; } = "";
        //private static string example =
        //                "orders?symbol=nkla&qty=20&side=buy&type=market&time_in_force=day&limit_" +
        //                "price=500&stop_price=30&client_order_id=2686f5a8-69d3-41ab-ba03-6133077a2b6c&extended_hours=true&" +
        //                "order_class=simple&take_profit=100&stop_loss=30";
        /// <summary>
        /// NOT FULLY TESTED YET, THE CODE NEEDS TO BE STEP THROUGH TO ASSURE THAT THE DECENT PARSER
        /// IS ADDING THE CORRECT VALUES THIS DOES WORK LIKE A CHARM IN THE QUERY BUILDER
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>

        private static int GetStringLength(string query)
        {
            var addLength = query.IndexOf('&') + 1;
            var restLength = query.Length;
            return Math.Abs(addLength - restLength);
        }

        public static string GetBaseURI()
        {
            return $@"https://api.alpaca.markets/v2/{_endPoint}";
        }
        public static string Build(string query)
        {
            if(Regex.IsMatch(query,"(=[^a-zA-Z0-9])") || 
               Regex.IsMatch(query,"([^a-zA-Z\\d-.+&.+_.+=.+])")) 
                throw new InvalidExpressionException("Invalid query");
          
            write.WriteStartObject();
            
            if(query.Contains("Q"))
            {
                query = query.Replace('Q','?');
                
                _endPoint = query.Substring(0, query.IndexOf('?')+1);
                query = query.Substring(query.IndexOf("?", StringComparison.Ordinal) + 1);
            }
            else
            {
                throw new Exception("No query string available");
            }

            if (query.Contains("symbol") && query.Length > 0) query = SetSymbole(query);

            if (query.Contains("qty")) SetQuantity(ref query);

            if (query.Contains("side")) SetSide(ref query);

            if (query.Contains("type")) query = SetType(query);
           
            if (query.Contains("time_in_force")) query = SetTimeInForce(query);

            if (query.Contains("limit_price")) query = SetLimitPrice(query);

            if (query.Contains("stop_price")) query = SetStopPrice(query);

            if (query.Contains("extended_hours")) SetExtendedHours(ref query);

            if (query.Contains("client_order_id")) SetClientOrderId(ref query);

            if (query.Contains("order_class")) query = SetOrderClass(query);

            if (query.Contains("take_profit")) SetTakeProfit(ref query);

            if (query.Contains("stop_loss")) SetStopLoss(ref query);
            write.WriteEnd();
            write.WriteEndObject();
            var res = write.ToString();
            sb.Clear();
            return res;
        }
        #region Mwethods Private Query Builder application

        private static void SetValues(string val, string query)
        {
            write.WritePropertyName(val);
            write.WritePropertyName(query.IndexOf('&') != -1
                ? query.Substring(query.IndexOf('=') + 1, query.IndexOf('&'))
                : query.Substring(query.IndexOf('=') + 1, query.IndexOf(query[^1])));
        }
        private static void SetClientOrderId(ref string query)
        {
            if (query.Contains("client_order_id"))
                write.WritePropertyName($"client_order_id");
                write.WriteValue(Guid.NewGuid().ToString());
            query =  query.Substring(query.IndexOf('&') + 1);
        }

        private static void SetStopLoss(ref string query)
        {
            if (query.Contains("stop_loss")  && sb.ToString().Contains("bracket") &&  
                sb.ToString().Contains("limit_price") &&  
                sb.ToString().Contains("stop_price"))
                SetValues("stop_loss",query);
            else if (query.Contains("stop_loss") && query.Length > "stop_loss=".Length) throw new Exception("Invalid argument in SetStopLoss");

            query = "";
        }

        private static void SetTakeProfit(ref string query)
        {
            if (query.Contains("take_profit") && 
                sb.ToString().Contains("bracket") &&  
                sb.ToString().Contains("limit_price"))
            {
                SetValues("take_profit",query);
                query = query.Substring(query.IndexOf('&') + 1, GetStringLength(query));
            }
            else if (query.Contains("take_profit"))
            {
                query = query.Substring(query.IndexOf('&') + 1,GetStringLength(query));
            }
        }

        private static string SetOrderClass(string query)
        {
            if (!query.Contains("order_class")) throw new Exception("A ticker symbol is needed to place an order");
            SetValues("order_class",query);
            return query.Substring(query.IndexOf('&') + 1,GetStringLength(query));

        }

        private static void SetExtendedHours(ref string query)
        {
            if (query.Contains("extended_hours=true") && sb.ToString().Contains("limit") && sb.ToString().Contains("day"))
            {
                SetValues("extended_hours","true");
                query = query.Substring(query.IndexOf('&') + 1,GetStringLength(query));
            }
            else if(query.Contains("extended_hours=false"))
            {
                SetValues("extended_hours","false");
                query = query.Substring(query.IndexOf('&') + 1,GetStringLength(query));
            }
            else if(query.Contains("extended_hours"))
            {
                query = query.Substring(query.IndexOf('&') + 1,GetStringLength(query));
            }
        }

        private static string SetStopPrice(string query)
        {
            if (query.Contains("stop_price") && sb.ToString().Contains("stop") || sb.ToString().Contains("stop_limit"))
            {
                SetValues("stop_price",query);
                return query.Substring(query.IndexOf('&') + 1,GetStringLength(query));
            }
            if (query.Contains("stop_price") && query.IndexOf('&') < 21)
            {
                return query.Substring(query.IndexOf('&') + 1, GetStringLength(query));
            }
            throw new Exception("A ticker symbol is needed to place an order");
        }

        private static string SetLimitPrice(string query)
        {
            #pragma warning disable 1587
            /// Variable to check that if the lemit price is in string and the other conditions are not met
            /// then truncate string for that segment only
            #pragma warning restore 1587
            var nextAmpersandValueLocation = 20;
            if (query.Contains("limit_price") && sb.ToString().Contains("limit") || sb.ToString().Contains("stop_limit"))
            {
                SetValues("limit_price",query);
                return query.Substring(query.IndexOf('&') + 1,GetStringLength(query));
            }

            if(query.Contains("limit_price") && query.IndexOf('&') < 22) return query.Substring(query.IndexOf('&') + 1,GetStringLength(query));
            throw new Exception("A ticker symbol is needed to place an order");
        }

        private static string SetTimeInForce(string query)
        {
            if (query.Contains("time_in_force"))
            {
                SetValues("time_in_force",query);
                return query.Substring(query.IndexOf('&') + 1,GetStringLength(query));
            }

            throw new Exception("A ticker symbol is needed to place an order");
        }

        private static string SetType(string query)
        {
            if (query.Contains("type"))
            {
                SetValues("type",query);
                return query.Substring(query.IndexOf('&') + 1,GetStringLength(query));
            }

            throw new Exception("A ticker symbol is needed to place an order");
        }

        private static void SetSide(ref string query)
        {
            if (query.Contains("side"))
            {
               SetValues("side",query);
               query = query.Substring(query.IndexOf('&') + 1,GetStringLength(query));
            }
        }

        private static string SetSymbole(string query)
        {
            if (query.Contains("symbol"))
            {
                SetValues("symbol",query);

                return query.Substring(query.IndexOf('&') + 1,GetStringLength(query));
            }

            throw new Exception("A ticker symbol is needed to place an order");
        }

        private static void SetQuantity(ref string query)
        {
            if (query.Contains("qty"))
            {
                SetValues("qty",query);

                query = query.Substring(query.IndexOf('&') + 1,GetStringLength(query));
            }
            else
            {
                throw new Exception("A ticker symbol is needed to place an order");
            }
        }
       #endregion 
    }
    
}
