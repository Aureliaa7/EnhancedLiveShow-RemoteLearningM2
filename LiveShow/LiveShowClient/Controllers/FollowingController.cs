using LiveShow.Services.Models.Following;
using LiveShowClient.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LiveShowClient.Controllers
{
    [Authorize]
    public class FollowingController : Controller
    {
        private readonly IFollowingService followingService;
        private readonly IGettingCurrentUser currentUser;
        private readonly IHttpContextAccessor httpContext;

        public FollowingController(IFollowingService followingService, IGettingCurrentUser currentUser, IHttpContextAccessor httpContext)
        {
            this.followingService = followingService;
            this.currentUser = currentUser;
            this.httpContext = httpContext;
        }

        public async Task<IActionResult> Follow(Guid id)
        {
            var userId = currentUser.GetCurrentUserId(httpContext);
            var followingDto = new FollowingDto { FollowerId = userId, FolloweeId = id };
            await followingService.Follow(followingDto);
            return RedirectToAction("Persons", "Following");
        }

        [HttpGet]
        public async Task<IActionResult> Followers()
        {
            var id = currentUser.GetCurrentUserId(httpContext);
            var followers = await followingService.GetFollowers(id);
            return View(followers);
        }

        [HttpGet]
        public async Task<IActionResult> Followees()
        {
            var id = currentUser.GetCurrentUserId(httpContext);
            var followees = await followingService.GetFollowees(id);
            return View(followees);
        }

        [HttpGet]
        public async Task<IActionResult> Persons()
        {
            var userId = currentUser.GetCurrentUserId(httpContext);
            var persons = await followingService.FindPersons(userId);
            return View(persons);
        }

        public async Task<IActionResult> Unfollow(Guid id)
        {
            var followerId = currentUser.GetCurrentUserId(httpContext);
            await followingService.Unfollow(followerId,id);
            return RedirectToAction("Followees", "Following");
        }

        public async Task<IActionResult> RemoveFollower(Guid id)
        {
            var followeeId = currentUser.GetCurrentUserId(httpContext);
            await followingService.Unfollow(id, followeeId);
            return RedirectToAction("Followers", "Following");
        }
    }
}