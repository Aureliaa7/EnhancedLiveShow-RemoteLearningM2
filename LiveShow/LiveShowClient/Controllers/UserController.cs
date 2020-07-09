using LiveShowClient.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LiveShowClient.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IGettingCurrentUser currentUser;
        private readonly IUserService userService;
        private readonly IHttpContextAccessor httpContext;
        public UserController(IGettingCurrentUser currentUser, IUserService userService, IHttpContextAccessor httpContext)
        {
            this.currentUser = currentUser;
            this.userService = userService;
            this.httpContext = httpContext;
        }
        public async Task<IActionResult> Home()
        {
            var userDetails = await userService.GetUser(currentUser.GetCurrentUserId(httpContext));
            return View(userDetails);
        }
    }
}