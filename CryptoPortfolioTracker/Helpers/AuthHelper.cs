using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPortfolioTracker.Helpers
{
    public static class AuthHelper
    {
        public static ClaimsPrincipal GetClaimsFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var claims = jwtToken.Claims;
            return new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
        }

        public static string? GetClaimValue(ClaimsPrincipal user, string claimType)
        {
            var claim = user.Claims.FirstOrDefault(c => c.Type == claimType);
            return claim?.Value;
        }
    }
}
