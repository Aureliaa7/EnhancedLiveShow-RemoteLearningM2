using LiveShow.Api.Filters;
using LiveShow.Services.Interfaces;
using LiveShow.Services.Models.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LiveShow.Api.Controllers
{
    public class UserController : LiveShowApiControllerBase
    {
        private readonly IUserService userService;
        private readonly IShowGettingService showService;

        public UserController(IUserService userService, IShowGettingService showService)
        {
            this.userService = userService;
            this.showService = showService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Profile(Guid id)
        {
            var user = await userService.GetUser(id);
            if(user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        [HttpPut]
        [ModelValidationFilter]
        public async Task<IActionResult> UpdateProfile(UserEditDto user)
        {
            var updatedUser = await userService.ChangeName(user);
            return NoContent();
        }

        [HttpGet("shows/{attendeeid}")]
        public async Task<IActionResult> GetShowsToBeAttended(Guid attendeeId)
        {
            var showsToBeAttended = await showService.GetShowsToBeAttended(attendeeId);
            return Ok(showsToBeAttended);
        }

        [HttpPut("changepassword")]
        [ModelValidationFilter]
        public async Task<IActionResult> ChangePassword(UserChangePasswordDto  userDto)
        {
            await userService.ChangePassword(userDto);
            return NoContent();
        }
    }
}
