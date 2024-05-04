using Microsoft.AspNetCore.Identity;

namespace Database.Entities
{
    public class User : IdentityUser<Guid>
    {
        public DateTime CreatedTimeStamp { get; set; }

        // Navigation property
        public List<Portfolio> Portfolios { get; set; } = new();
    }
}
