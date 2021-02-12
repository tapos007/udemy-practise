using System;
using System.Linq;
using System.Security.Claims;

namespace Utility
{
    public static class ClaimsHelpersExtension
    {
        public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var id = claimsPrincipal.Claims.Where(c => c.Type == "sub").Select(c => c.Value).FirstOrDefault();

            return Convert.ToInt32(id);
        }
        
        public static string GetUserName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.Where(c => c.Type == "name").Select(c => c.Value).FirstOrDefault();
        }
        
        public static string GetUserRole(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.Where(c => c.Type == "role").Select(c => c.Value).FirstOrDefault();
        }
    }
}