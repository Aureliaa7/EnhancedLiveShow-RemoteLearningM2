using LiveShow.Services.Models.Following;
using LiveShow.Services.Models.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiveShowClient.Interfaces
{
    public interface IFollowingService
    {
        Task<IEnumerable<UserDto>> GetFollowers(Guid followeeId);
        Task<IEnumerable<UserDto>> GetFollowees(Guid followerId);
        Task<string> Follow(FollowingDto followingDto);
        Task Unfollow(Guid followerId, Guid followeeId);
        Task<IEnumerable<UserDto>> FindPersons(Guid userId);
    }
}
