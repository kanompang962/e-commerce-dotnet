using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class AppUser : IdentityUser
    {
        public string Img { get; set; } = string.Empty;
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}