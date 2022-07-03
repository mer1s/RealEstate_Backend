using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class AppUser : IdentityUser
    {
        public string ImagePath { get; set; }
        [ForeignKey("AppUserId")]
        public List<Ad> MyAds { get; set; } = new();
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PasswordHint { get; set; }
    }
}
