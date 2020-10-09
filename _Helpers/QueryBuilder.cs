using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace SSAPIInteractionHelper._Helpers
{
    public static class QueryBuilder
    {
        /// Buy and sell Query string
        ///
        private static StringBuilder sb;
        // This EXAMPLE uri string has all the elements that can be in a query string to alpaca
       // private static string example =
            //"orders?symbol=nkla&qty=20&side=buy&type=market&time_in_force=day&limit_" +
            //"price=500&stop_price=30&client_order_id=2686f5a8-69d3-41ab-ba03-6133077a2b6c&extended_hours=true&" +
            //"order_class=simple&take_profit=100&stop_loss=30";

        private static int GetStringLength(string query)
        {
            var addLength = query.IndexOf('&') + 1;
            var restLength = query.Length;
            return Math.Abs(addLength - restLength);
        }
        /// <summary>
        /// The query string uses a Q in the request which is the start of the query
        /// you may change this as you see fit, the thoughts where to further obfuscate
        /// the query for security and avoidance basic SQL fuzzing techniques.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static string Build(string query)
        {
            if(Regex.IsMatch(query,"(=[^a-zA-Z0-9])") || 
               Regex.IsMatch(query,"([^a-zA-Z\\d-.+&.+_.+=.+])")) 
                throw new InvalidExpressionException("Invalid query");
            sb = new StringBuilder(@"https://api.alpaca.markets");
            sb.Append(@"/v2/");
            if(query.Contains("Q"))
            {
                query = query.Replace('Q','?');
                sb.Append(query.Substring(0, query.IndexOf('?')+1));
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
            var res = sb.ToString();
            sb.Clear();
            return res[^1] == '&' ? res.Substring(0, res.Length - 1) : res;
        }
        #region Mwethods Private Query Builder application

        private static void SetClientOrderId(ref string query)
        {
            if (query.Contains("client_order_id"))
                sb.Append($"client_order_id={Guid.NewGuid()}");
            query =  query.Substring(query.IndexOf('&') + 1);
        }

        private static void SetStopLoss(ref string query)
        {
            if (query.Contains("stop_loss")  && sb.ToString().Contains("bracket") &&  
                sb.ToString().Contains("limit_price") &&  
                sb.ToString().Contains("stop_price"))
                sb.Append(query);
            else if (query.Contains("stop_loss") && query.Length > "stop_loss=".Length) throw new Exception("Invalid argument in SetStopLoss");

            query = "";
        }

        private static void SetTakeProfit(ref string query)
        {
            if (query.Contains("take_profit") && 
                sb.ToString().Contains("bracket") &&  
                sb.ToString().Contains("limit_price"))
            {
                sb.Append(query.Substring(0, query.IndexOf('&')+1));
                query = query.Substring(query.IndexOf('&') + 1, GetStringLength(query));
            }
            else if (query.Contains("take_profit"))
            {
                query = query.Substring(query.IndexOf('&') + 1,GetStringLength(query));
            }
        }

        private static string SetOrderClass(string query)
        {
            if (query.Contains("order_class"))
            {
                sb.Append(query.Substring(0, query.IndexOf('&')+1));
                return query.Substring(query.IndexOf('&') + 1,GetStringLength(query));
            }

            throw new Exception("A ticker symbol is needed to place an order");
        }

        private static void SetExtendedHours(ref string query)
        {
            if (query.Contains("extended_hours=true") && sb.ToString().Contains("limit") && sb.ToString().Contains("day"))
            {
                sb.Append(query.Substring(0, query.IndexOf('&')+1));
                query = query.Substring(query.IndexOf('&') + 1,GetStringLength(query));
            }
            else if(query.Contains("extended_hours=false"))
            {
                sb.Append(query.Substring(0, query.IndexOf('&')+1));
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
                sb.Append(query.Substring(0, query.IndexOf('&')+1));
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
                sb.Append(query.Substring(0, query.IndexOf('&')+1));
                return query.Substring(query.IndexOf('&') + 1,GetStringLength(query));
            }

            if(query.Contains("limit_price") && query.IndexOf('&') < 22) return query.Substring(query.IndexOf('&') + 1,GetStringLength(query));
            throw new Exception("A ticker symbol is needed to place an order");
        }

        private static string SetTimeInForce(string query)
        {
            if (query.Contains("time_in_force"))
            {
                sb.Append(query.Substring(0, query.IndexOf('&')+1));
                return query.Substring(query.IndexOf('&') + 1,GetStringLength(query));
            }

            throw new Exception("A ticker symbol is needed to place an order");
        }

        private static string SetType(string query)
        {
            if (query.Contains("type"))
            {
                sb.Append(query.Substring(0, query.IndexOf('&')+1));
                return query.Substring(query.IndexOf('&') + 1,GetStringLength(query));
            }

            throw new Exception("A ticker symbol is needed to place an order");
        }

        private static void SetSide(ref string query)
        {
            if (query.Contains("side"))
            {
                sb.Append(query.Substring(0, query.IndexOf('&')+1));
                query = query.Substring(query.IndexOf('&') + 1,GetStringLength(query));
            }
        }

        private static string SetSymbole(string query)
        {
            if (query.Contains("symbol"))
            {
                sb.Append(query.Substring(0, query.IndexOf('&')+1));
                return query.Substring(query.IndexOf('&') + 1,GetStringLength(query));
            }

            throw new Exception("A ticker symbol is needed to place an order");
        }

        private static void SetQuantity(ref string query)
        {
            if (query.Contains("qty"))
            {
                sb.Append(query.Substring(0, query.IndexOf('&')+1));
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
