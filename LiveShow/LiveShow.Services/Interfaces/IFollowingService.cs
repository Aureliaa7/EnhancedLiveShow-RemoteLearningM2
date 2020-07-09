using LiveShow.Services.Models.Following;
using LiveShow.Services.Models.User;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LiveShow.Services.Interfaces
{
    public interface IFollowingService
    {
        Task<IEnumerable<UserDto>> GetFollowers(Guid followeeId);
        Task<IEnumerable<UserDto>> GetFollowees(Guid followerId);
        Task<FollowingDto> Follow(Guid followerId, Guid followeeId);
        Task Unfollow(Guid followerId, Guid followeeId);
        Task<IEnumerable<UserDto>> FindPersons(Guid userId);
    }
}
