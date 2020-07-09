using LiveShow.Dal.Models;

namespace LiveShow.Dal.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<User> UserRepository { get; }
        IRepository<Show> ShowRepository { get; }
        IRepository<Attendance> AttendanceRepository { get; }
        IRepository<Genre> GenreRepository { get; }
        IRepository<Following> FollowingRepository { get; }
        IRepository<UserNotification> UserNotificationRepository { get; }
        IRepository<Notification> NotificationRepository { get; }
    }
}
