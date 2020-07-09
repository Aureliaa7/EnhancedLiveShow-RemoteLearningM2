using AutoMapper;
using LiveShow.Dal.Interfaces;
using LiveShow.Dal.Models;
using LiveShow.Services.Exceptions;
using LiveShow.Services.Interfaces;
using LiveShow.Services.Models.Following;
using LiveShow.Services.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LiveShow.Services.Services
{
    public class FollowingService : IFollowingService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public FollowingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<FollowingDto> Follow(Guid followerId, Guid followeeId)
        {
            Following createdFollowing = null;

            if(await unitOfWork.UserRepository.Exists(f => f.Id == followerId) && await unitOfWork.UserRepository.Exists(f => f.Id == followeeId))
            {
                var following = new Following { FollowerId = followerId, FolloweeId = followeeId};
                createdFollowing = await unitOfWork.FollowingRepository.Add(following);
                return mapper.Map<FollowingDto>(createdFollowing);
            }
            throw new ItemNotFoundException("The follower or the followee does not exist...");
        }

        public async Task Unfollow(Guid followerId, Guid followeeId)
        {
            if (await unitOfWork.FollowingRepository.Exists(f => f.FollowerId == followerId) && await unitOfWork.FollowingRepository.Exists(f => f.FolloweeId == followeeId))
            {
                var following = await unitOfWork.FollowingRepository.Find(f => f.FollowerId == followerId && f.FolloweeId == followeeId);
                await unitOfWork.FollowingRepository.RemoveRange(following);
            }
           else {
                throw new ItemNotFoundException("The user does not exist...");
            }
        }

        public async Task<IEnumerable<UserDto>> GetFollowers(Guid followeeId)
        {
            if (await unitOfWork.UserRepository.Exists(f => f.Id == followeeId))
            {
                var followings = await unitOfWork.FollowingRepository.Find(f => f.FolloweeId == followeeId);
                var followers = from u in await unitOfWork.UserRepository.GetAll() join f in followings on u.Id equals f.FollowerId select mapper.Map<UserDto>(u);
                return followers;
            }
            throw new ItemNotFoundException("The user does not exist...");
        }

        public async Task<IEnumerable<UserDto>> GetFollowees(Guid followerId)
        {
            if (await unitOfWork.UserRepository.Exists(f => f.Id == followerId))
            {
                var followings = await unitOfWork.FollowingRepository.Find(f => f.FollowerId == followerId);
                var followees = from u in await unitOfWork.UserRepository.GetAll() join f in followings on u.Id equals f.FolloweeId select mapper.Map<UserDto>(u);
                return followees;
            }
            throw new ItemNotFoundException("The user does not exist...");
        }

        public async Task<IEnumerable<UserDto>> FindPersons(Guid userId)
        {
            bool userExists = await unitOfWork.UserRepository.Exists(u => u.Id == userId);
            if (userExists)
            {
                var followings = await unitOfWork.FollowingRepository.Find(f => f.FollowerId == userId);
                var users = await unitOfWork.UserRepository.Find(u => u.Id != userId);
                var dtoUsers = ConvertFromUserCollectionToUserDto(users).ToList();
                foreach (var following in followings)
                {
                    dtoUsers.Remove(dtoUsers.Where(u => u.Id == following.FolloweeId).FirstOrDefault());
                }
                return dtoUsers;
            }
            throw new ItemNotFoundException("The user does not exist...");
        }

        private IEnumerable<UserDto> ConvertFromUserCollectionToUserDto(IEnumerable<User> users)
        {
            var dtoUsers = new List<UserDto>();

            foreach (var user in users)
            {
                dtoUsers.Add(mapper.Map<UserDto>(user));
            }
            return dtoUsers;
        }
    }
}
