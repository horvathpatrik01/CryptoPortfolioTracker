using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities
{
    public class Asset
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? IconUrl { get; set; }

        [Precision(32, 16)]
        public decimal AvrgBuyPrice { get; set; }
        [Precision(32, 16)]
        public decimal Amount { get; set; }

        [ForeignKey(nameof(PortfolioId))]
        public int PortfolioId { get; set; }

        // Navigation property
        public Portfolio Portfolio { get; set; } = null!;
        public List<Transaction> Transactions { get; set; } = new();
    }
}
