using Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Database.Entities
{
    public class Portfolio
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        public string Icon { get; set; }

        public PortfolioType PortfolioType { get; set; }

        [ForeignKey(nameof(UserId))]
        public Guid UserId { get; set; }

        // Navigation property
        public User User { get; set; } = null!;
        public List<Asset>? Assets { get; set; }
        public Address? Address { get; set; }
        public ApiKey? ApiKey { get; set; }
    }
}
