using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace TokenBaseAuthetication.Models
{
    public class JwtContainerModel : IAuthContainerModel
    {       
        public string SecretKey { get; set; } = "sjjZlW4VXCtqgcOYxAXtaxu2QJLCzwQR";
        public string SecurityAlgorithm { get  ; set  ; } = SecurityAlgorithms.HmacSha256Signature;
        public int ExpireMinutes { get; set; } = 10080;
        public Claim[] Claims { get; set; } 
    }
}