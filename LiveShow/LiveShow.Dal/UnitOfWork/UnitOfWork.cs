using LiveShow.Dal.Interfaces;
using LiveShow.Dal.Models;
using LiveShow.Dal.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LiveShow.Dal.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ApplicationDbContext context)
        {
            NotificationRepository = new Repository<Notification>(context);
            UserNotificationRepository = new Repository<UserNotification>(context);
            UserRepository = new Repository<User>(context);
            ShowRepository = new Repository<Show>(context);
            AttendanceRepository = new Repository<Attendance>(context);
            GenreRepository = new Repository<Genre>(context);
            FollowingRepository = new Repository<Following>(context);
        }

        public IRepository<User> UserRepository { get; private set; }

        public IRepository<Show> ShowRepository { get; private set; }

        public IRepository<Attendance> AttendanceRepository { get; private set; }

        public IRepository<Genre> GenreRepository { get; private set; }

        public IRepository<Following> FollowingRepository { get; private set; }

        public IRepository<UserNotification> UserNotificationRepository { get; set; }

        public IRepository<Notification> NotificationRepository { get; private set; }
    }
}
