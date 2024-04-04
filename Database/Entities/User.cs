using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Database.Entities
{
    public class User : IdentityUser<Guid>
    {
        public DateTime CreatedTimeStamp { get; set; }

        // Navigation property
        public List<Portfolio> Portfolios { get; set; } = new();
    }
}
