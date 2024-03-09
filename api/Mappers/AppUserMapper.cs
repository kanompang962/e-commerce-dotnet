using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.User;
using api.Models;

namespace api.Mappers
{
    public static class AppUserMapper
    {
        public static AppUserDto ToAppUserDto(this AppUser appUser)
        {
            return new AppUserDto
            {
                Username = appUser.UserName,
                Email = appUser.Email,
                Img = appUser.Img,   
            };
        }
    }
}