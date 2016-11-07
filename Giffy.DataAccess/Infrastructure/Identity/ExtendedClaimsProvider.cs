using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Giffy.Entities.Models;

namespace Giffy.DataAccess.Infrastructure.Identity
{
    public static class ExtendedClaimsProvider
    {
        public static IEnumerable<Claim> GetClaims(User user)
        {

            List<Claim> claims = new List<Claim>();

            var daysInWork = (DateTime.UtcNow.Date - user.JoinedDate).TotalDays;

            if (daysInWork > 90)
            {
                claims.Add(CreateClaim("FTE", "1"));

            }
            else
            {
                claims.Add(CreateClaim("FTE", "0"));
            }

            return claims;
        }

        public static Claim CreateClaim(string type, string value)
        {
            return new Claim(type, value, ClaimValueTypes.String);
        }
    }
}
