using LiveShow.Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace LiveShow.Dal
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications{get; set;}
        public DbSet<Genre> Genres { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            FollowersRelations(modelBuilder);
            modelBuilder.Entity<Attendance>().HasKey(a => new { a.ShowId, a.AttendeeId });
            modelBuilder.Entity<UserNotification>().HasKey(u => new { u.UserId, u.NotificationId });
            AddShowGenres(modelBuilder);

            modelBuilder.Entity<Notification>()
                .HasMany(n => n.UserNotifications)
                .WithOne(n => n.Notification)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.Show)
                .WithMany(s => s.Attendances)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
             .HasMany(u => u.Followees)
             .WithOne(u => u.Follower)
             .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
            .HasMany(u => u.Followers)
            .WithOne(u => u.Followee)
            .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }

        private static void FollowersRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Following>()
                .HasKey(c => new { c.FollowerId, c.FolloweeId });

            modelBuilder.Entity<User>()
                .HasMany(u => u.Followers)
                .WithOne(f => f.Followee)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Followees)
                .WithOne(f => f.Follower)
                .OnDelete(DeleteBehavior.NoAction);
        }

        private void AddShowGenres(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasData(
                new Genre
                {
                    Id = 1,
                    Name = "Jazz"
                },
                new Genre
                {
                    Id = 2,
                    Name = "Electronic"
                },
                new Genre
                {
                    Id = 3,
                    Name = "Hip-Hop"
                },
                new Genre
                {
                    Id = 4,
                    Name = "Rock"
                },
                new Genre
                {
                    Id = 5,
                    Name = "Dance"
                },
                new Genre
                {
                    Id = 6,
                    Name = "Drum and base" 
                });
        }
    }
}
