using LiveShow.Services.Models.Following;
using LiveShow.Services.Models.User;
using LiveShowClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiveShowClient.Services
{
    public class FollowingService : IFollowingService
    {
        private readonly IApiService apiService;

        public FollowingService(IApiService apiService)
        {
            this.apiService = apiService;
        }

        public async Task<IEnumerable<UserDto>> FindPersons(Guid userId)
        {
            var result = await apiService.GetContentFromHttpAsync<IEnumerable<UserDto>>($"/following/find/{userId}");
            return result;
        }

        public async Task<string> Follow(FollowingDto followingDto)
        {
            var result = await apiService.PostDataAsync("/following/follow", followingDto);
            return result;
        }

        public async Task<IEnumerable<UserDto>> GetFollowees(Guid followerId)
        {
            return await apiService.GetContentFromHttpAsync<IEnumerable<UserDto>>($"/following/followees/{followerId}");
        }

        public async Task<IEnumerable<UserDto>> GetFollowers(Guid followeeId)
        {
            return await apiService.GetContentFromHttpAsync<IEnumerable<UserDto>>($"/following/followers/{followeeId}");
        }

        public async Task Unfollow(Guid followerId, Guid followeeId)
        {
            await apiService.DeleteDataAsync($"/following/unfollow/{followerId}/{followeeId}");
        }
    }
}
