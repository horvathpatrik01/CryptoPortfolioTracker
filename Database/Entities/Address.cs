using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string WalletAddress {  get; set; }

        [ForeignKey(nameof(PortfolioId))]
        public int PortfolioId { get; set; }

        // Navigation property
        public Portfolio Portfolio { get; set; } = null!;
    }
}
