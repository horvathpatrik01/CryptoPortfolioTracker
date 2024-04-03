using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database.Entities
{
    public class User : IdentityUser<Guid>
    {
        [MaxLength(30)]
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public DateTime CreatedTimeStamp { get; set; }
    }
}
