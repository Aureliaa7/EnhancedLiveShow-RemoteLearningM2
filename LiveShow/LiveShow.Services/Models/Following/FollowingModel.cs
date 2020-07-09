using LiveShow.Services.Models.User;

namespace LiveShow.Services.Models.Following
{
    public class FollowingModel
    {
        public UserDto Follower { get; set; }
        public UserDto Followee { get; set; }
    }
}
