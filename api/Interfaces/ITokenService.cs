using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace app_dotnet.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}