using Xamarin.Android.Net;

namespace CryptoPortfolioTracker
{
    public class AndroidHttpMessageHandler : IPlatformHttpMessageHandler
    {
        public HttpMessageHandler GetHttpMessageHandler() => new AndroidMessageHandler
        {
            ServerCertificateCustomValidationCallback = (httpRequestMessage, certificate, chain, sslPolicyErrors) =>
            certificate?.Issuer == "CN==localhost" || sslPolicyErrors == System.Net.Security.SslPolicyErrors.None
        };
    }
}
