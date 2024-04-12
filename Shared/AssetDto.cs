using System.ComponentModel.DataAnnotations.Schema;

namespace Shared
{
    public class AssetDto
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? IconUrl { get; set; }
        public decimal AvrgBuyPrice { get; set; }
        public decimal Amount { get; set; }
        public List<TransactionDto> Transactions { get; set; }
    }
}
