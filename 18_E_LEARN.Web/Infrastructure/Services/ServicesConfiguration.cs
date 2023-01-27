using _18_E_LEARN.DataAccess.Data.Context;
using _18_E_LEARN.DataAccess.Data.Models.User;
using Microsoft.AspNetCore.Identity;

namespace _18_E_LEARN.Web.Infrastructure.Services
{
    public class ServicesConfiguration
    {
        public static void Config(IServiceCollection services){
            // Add services to the container.
            services.AddControllersWithViews();

            // Add razor pages
            services.AddRazorPages();

            // Add application database context
            services.AddDbContext<AppDbContext>();

            // Add Identity
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.User.RequireUniqueEmail = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

        }
    }
}
