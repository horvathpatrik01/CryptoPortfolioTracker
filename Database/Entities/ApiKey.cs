using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Entities
{
    [PrimaryKey("PublicKey")]
    public class ApiKey
    {
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }

        [ForeignKey(nameof(PortfolioId))]
        public int PortfolioId { get; set; }

        // Navigation property
        public Portfolio Portfolio { get; set; } = null!;
    }
}
