using LiveShowClient.Interfaces;
using LiveShowClient.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LiveShowClient
{
    public class Startup
    {
    public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IShowGettingService, ShowGettingService>();
            services.AddHttpClient<IApiService, ApiService>();
            services.AddScoped<IFollowingService, FollowingService>();
            services.AddScoped<IAccountService, AccountService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IGettingCurrentUser, GettingCurrentUser>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAttendanceService, AttendanceService>();
            services.AddScoped<IUserNotificationService, UserNotificationService>();
            services.AddScoped<IShowUpdatingService, ShowUpdatingService>();
            services.AddScoped<IShowCreationService, ShowCreationService>();
            services.AddScoped<IShowDeletionService, ShowDeletionService>();
            services.AddScoped<IShowCancellationService, ShowCancellationService>();

            services.AddCors();
            services.AddControllersWithViews();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
