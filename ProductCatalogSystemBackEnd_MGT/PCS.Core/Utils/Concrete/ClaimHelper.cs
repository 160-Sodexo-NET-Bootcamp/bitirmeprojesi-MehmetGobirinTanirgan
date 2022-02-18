using System;
using System.Linq;
using System.Security.Claims;

namespace PCS.Core.Utils.Concrete
{
    public static class ClaimHelper
    {
        public static string GetClaimValue(this ClaimsPrincipal claimsPrincipal, string claimType)
        {
            return claimsPrincipal.Claims.FirstOrDefault(x => x.Type == claimType).Value;
        }

        public static Guid GetMainUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return Guid.Parse(claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "userId").Value);
        }

        public static string GetMainUserFullname(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "fullname").Value;
        }
    }
}
