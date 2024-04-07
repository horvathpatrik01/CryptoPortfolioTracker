namespace Server.DTOs
{
    public class PortfolioToAddDto
    {
        public string Name { get; set; }
        public PortfolioType PortfolioType { get; set; }
        public string? PublicKey { get; set; }
        public string? PrivateKey { get; set; }
        public string? WalletAddress { get; set; }
    }
}
