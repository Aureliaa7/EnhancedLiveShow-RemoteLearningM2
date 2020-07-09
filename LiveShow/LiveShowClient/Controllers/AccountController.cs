using LiveShow.Services.Models.User;
using LiveShowClient.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LiveShowClient.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;
        private readonly IUserService userService;
        private readonly IGettingCurrentUser gettingCurrentUserService;
        private readonly IHttpContextAccessor httpContext;

        public AccountController(IAccountService accountService, IUserService userService, 
            IGettingCurrentUser gettingCurrentUserService, IHttpContextAccessor httpContext)
        {
            this.accountService = accountService;
            this.userService = userService;
            this.gettingCurrentUserService = gettingCurrentUserService;
            this.httpContext = httpContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginDto userLogin)
        {
            bool accountExists = await accountService.AccountExists(userLogin);
            if(accountExists)
            {
                var user = await accountService.Login(userLogin);
                string role = (user.Type == UserTypeEnum.Artist) ? "Artist" : "User";
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.GivenName, user.FirstName),
                    new Claim(ClaimTypes.Surname, user.LastName),
                    new Claim(ClaimTypes.Role, role)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(principal);
                if(role.Equals("Artist"))
                {
                    return RedirectToAction("Home", "Artist");
                }
                else
                {
                    return RedirectToAction("Home", "User");
                }
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto userRegister)
        {
            if(ModelState.IsValid)
            {
                await accountService.Register(userRegister);
                return RedirectToAction("Login", "Account");
            }
            return View(userRegister);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var userEditDto = await userService.GetUserToBeEdited(id);
            return View(userEditDto);
        }

        public async Task<IActionResult> Edit(UserEditDto userDto)
        {
            Guid userId = gettingCurrentUserService.GetCurrentUserId(httpContext);
            userDto.Id = userId;
            await accountService.EditName(userDto);
            var user = await userService.GetUser(userId);
            string role = (user.Type == UserTypeEnum.Artist) ? "Artist" : "User";
            if (role.Equals("Artist"))
            {
                return RedirectToAction("Home", "Artist");
            }
            else
            {
                return RedirectToAction("Home", "User");
            }
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        public async Task<IActionResult> ChangePassword(UserChangePasswordDto userDto)
        {
            var user = await userService.GetUser(gettingCurrentUserService.GetCurrentUserId(httpContext));
            userDto.Id = user.Id;
            userDto.Username = user.Username;
            await accountService.ChangePassword(userDto);
            string role = (user.Type == UserTypeEnum.Artist) ? "Artist" : "User";
            if (role.Equals("Artist"))
            {
                return RedirectToAction("Home", "Artist");
            }
            else
            {
                return RedirectToAction("Home", "User");
            }
        }
    }
}