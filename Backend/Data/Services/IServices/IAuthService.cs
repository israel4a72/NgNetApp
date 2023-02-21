using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;

namespace Data.Services.IServices
{
    public interface IAuthService
    {
        bool CheckPassword(User user, string password);
        string GenerateToken(User user);
    }
}