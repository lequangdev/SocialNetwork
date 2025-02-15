using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Jwt
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string userName, string role);
    }
}
