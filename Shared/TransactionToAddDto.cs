namespace Shared
{
    public class TransactionToAddDto
    {
        public int PortfolioId { get; set; }
        public int? AssetId { get; set; }
        public string? Symbol { get; set; }
        public string? Name { get; set; }
        public string? IconUrl { get; set; }
        public decimal Price { get; set; }
        public decimal Fee { get; set; }
        public string? Note { get; set; }

        public DateTime Time { get; set; }
        public decimal Amount { get; set; }

        public TransactionType TransactionType { get; set; }
    }
}