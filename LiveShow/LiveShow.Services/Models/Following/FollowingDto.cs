using System;

namespace LiveShow.Services.Models.Following
{
    public class FollowingDto
    {
        public Guid FollowerId { get; set; }

        public Guid FolloweeId { get; set; }
    }
}
