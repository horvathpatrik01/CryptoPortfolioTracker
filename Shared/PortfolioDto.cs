using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public class PortfolioDto
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        public string Icon { get; set; }

        public PortfolioType PortfolioType { get; set; }
        public List<AssetDto>? Assets { get; set; }
        public AddressDto? Address { get; set; }
        public ApiKeyDto? ApiKey { get; set; }
    }

    public enum PortfolioType
    {
        Default,
        CexAccount,
        Wallet
    }
}
