using System;
using System.Net;
using System.Net.Http;

namespace SSAPIInteractionHelper._Helpers
{
    class HttpClientSingleton : IDisposable
    {
        private HttpClientSingleton(){}
        private static readonly object Lock = new object();
        private static HttpClient _localClient = new HttpClient();
        private static SocketsHttpHandler socketHandler;

        static SocketsHttpHandler GetScoketHandler()
        {
            return  socketHandler = new SocketsHttpHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
                PooledConnectionLifetime = TimeSpan.FromMinutes(5),
                PooledConnectionIdleTimeout = TimeSpan.FromMinutes(2),
                MaxConnectionsPerServer = 20
            };
        }
        public static void SetHeaders(string apiKeyTitle,string apiKey, string apiSecretTitle, string apiSecret)
        {
            _localClient.DefaultRequestHeaders.Add(apiKeyTitle,apiKey);
            _localClient.DefaultRequestHeaders.Add(apiSecretTitle,apiSecret);
        }
        public static HttpClient IronClient
        {
            get
            {
                lock (Lock)
                {
                    if(_localClient is null) return _localClient = new HttpClient(GetScoketHandler());
                    return _localClient;
                }
            }
        }
        public void Dispose()
        {
            socketHandler?.Dispose();
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_localClient != null)
                {
                    _localClient.Dispose();
                }
                _localClient = null!;
            }
        }
    }
}
