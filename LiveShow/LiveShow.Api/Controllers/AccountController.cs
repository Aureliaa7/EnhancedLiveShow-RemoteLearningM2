using LiveShow.Api.Filters;
using LiveShow.Services.Interfaces;
using LiveShow.Services.Models.User;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LiveShow.Api.Controllers
{
    public class AccountController : LiveShowApiControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto model)
        {
            var user = await accountService.Login(model);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        [HttpPost("register")]
        [ModelValidationFilter]
        public async Task<ActionResult> Register(UserRegisterDto user)
        {
            var createdUser = await accountService.Register(user);
            return Created(Url.Action("Register"), createdUser);
        }


        [HttpPost]
        [ModelValidationFilter]
        public async Task<IActionResult> AccountExists(UserLoginDto userLoginDto)
        {
            bool accountExists = await accountService.AccountExists(userLoginDto);
            if (accountExists)
            {
                return Ok(accountExists);
            }
            return NotFound(accountExists);
        }
    }
}
