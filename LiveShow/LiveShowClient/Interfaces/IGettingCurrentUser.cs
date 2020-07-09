using LiveShow.Services.Models.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace LiveShowClient.Interfaces
{
    public interface IGettingCurrentUser
    {
        Guid GetCurrentUserId(IHttpContextAccessor httpContext);
    }
}
