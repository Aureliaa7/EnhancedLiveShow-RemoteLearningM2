using LiveShow.Api.Filters;
using LiveShow.Services.Interfaces;
using LiveShow.Services.Models.Following;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LiveShow.Api.Controllers
{
    public class FollowingController : LiveShowApiControllerBase
    {
        private readonly IFollowingService followingService;

        public FollowingController(IFollowingService followingService)
        {
            this.followingService = followingService;
        }

        [HttpPost("follow")]
        [ModelValidationFilter]
        public async Task<IActionResult> Follow([FromBody] FollowingDto followingDto)
        {
            var following = await followingService.Follow(followingDto.FollowerId, followingDto.FolloweeId);
            return Created(Url.Action("Follow"), following);
        }

        [HttpDelete("unfollow/{followerId}/{followeeId}")]
        [ModelValidationFilter]
        public async Task<IActionResult> Unfollow(Guid followerId, Guid followeeId)
        {
            await followingService.Unfollow(followerId, followeeId);
            return NoContent();
        }

        [HttpGet("followers/{id}")]
        [ModelValidationFilter]
        public async Task<IActionResult> GetFollowers(Guid id)
        {
            var followers = await followingService.GetFollowers(id);
            return Ok(followers);
        }

        [HttpGet("followees/{id}")]
        [ModelValidationFilter]
        public async Task<IActionResult> GetFollowees(Guid id)
        {
            var followees = await followingService.GetFollowees(id);
            return Ok(followees);
        }

        [HttpGet("find/{userId}")]    
        public async Task<IActionResult> FindPersons(Guid userId)
        {
            var users = await followingService.FindPersons(userId);
            return Ok(users);
        }
    }
}