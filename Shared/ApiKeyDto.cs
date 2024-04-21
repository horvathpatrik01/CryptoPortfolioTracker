namespace Shared
{
    public class ApiKeyDto
    {
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public CexIdentifier CexIdentifier { get; set; }
    }

    public enum CexIdentifier
    {
        Binance,
        Okx,
        Bybit
    }
}
