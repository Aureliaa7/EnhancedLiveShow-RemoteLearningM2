using LiveShowClient.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace LiveShowClient.Services
{
    public class GettingCurrentUser : IGettingCurrentUser
    {
        public Guid GetCurrentUserId(IHttpContextAccessor httpContext)
        {
            var id = GetValue(httpContext.HttpContext.User, ClaimTypes.NameIdentifier);
            return new Guid(id);
        }

        private string GetValue(ClaimsPrincipal principal, string key)
        {
            if (principal == null)
                return string.Empty;

            return principal.FindFirstValue(key);
        }
    }
}
