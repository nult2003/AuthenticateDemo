using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using TokenBaseAuthetication.Models;

namespace TokenBaseAuthetication.JwtAuth
{
    public interface IAuthService
    {
        string SecretKey { get; set; }
        bool IsTokenValid(string token);
        string GenerateToken(IAuthContainerModel model);
        IEnumerable<Claim> GetTokenClaims(string token);
    }
}