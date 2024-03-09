using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.User
{
    public class AppUserDto
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Img { get; set; }
    }
}