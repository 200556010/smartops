using Data.Entity;
using Data.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service.Contract;
using Service.Implementation;
using Utility.Contract;

namespace Service.Config
{
    public static class ServiceConfig
    {
        public static void AddSmartOpsServices(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
        {

            // DbContext Configuration
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (!string.IsNullOrEmpty(connectionString))
            {
                services.AddDbContext<SmartOpsContext>(options => options.UseSqlServer(connectionString));
                services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            }

            // Identity Configuration
            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
                options.SlidingExpiration = true;
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
            });

            services.AddHttpContextAccessor();
            services.AddHttpClient();

            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddTransient<IProductService, ProductService>();
        }
    }
}
