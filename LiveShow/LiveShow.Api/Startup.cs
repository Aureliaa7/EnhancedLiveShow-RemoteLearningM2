using AutoMapper;
using LiveShow.Api.Filters;
using LiveShow.Dal;
using LiveShow.Dal.Interfaces;
using LiveShow.Dal.Repositories;
using LiveShow.Dal.UnitOfWork;
using LiveShow.Services.Interfaces;
using LiveShow.Services.MappingConfigurations;
using LiveShow.Services.PasswordService;
using LiveShow.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LiveShow.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly($"LiveShow.Dal"))
            );

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IShowGettingService, ShowGettingService>();
            services.AddScoped<IShowCancellationService, ShowCancellationService>();
            services.AddScoped<IShowCreationService, ShowCreationService>();
            services.AddScoped<IShowUpdatingService, ShowUpdatingService>();
            services.AddScoped<IFollowingService, FollowingService>();
            services.AddScoped<IShowNotificationService, ShowNotificationService>();
            services.AddScoped<IShowAttendanceService, ShowAttendanceService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IUserNotificationService, UserNotificationService>();
            services.AddScoped<IShowDeletionService, ShowDeletionService>();
            services.AddScoped<IPasswordEncryption, PasswordEncryption>();
            services.AddScoped<IPasswordValidation, PasswordValidation>();
            services.AddScoped<IAccountService, AccountService>();

            services.AddAutoMapper(typeof(UserProfile));
            services.AddAutoMapper(typeof(AttendanceProfile));
            services.AddAutoMapper(typeof(ShowProfile));
            services.AddAutoMapper(typeof(NotificationProfile));
            services.AddAutoMapper(typeof(UserRegistrationProfile));
            services.AddAutoMapper(typeof(FollowingProfile));
            services.AddAutoMapper(typeof(GenreProfile));
            services.AddAutoMapper(typeof(ShowWithoutIdProfile));
            services.AddAutoMapper(typeof(NotificationTypeProfile));
            services.AddAutoMapper(typeof(UserNotificationProfile));
            services.AddApiVersioning(options => options.ReportApiVersions = true);

            services.AddControllersWithViews().AddNewtonsoftJson();

            services.AddMvc().AddMvcOptions(options =>
            {
                options.Filters.Add(new LiveShowExceptionFilter());
                options.Filters.Add(new ModelValidationFilter());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
