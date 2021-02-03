using Bingol.Areas.Identity.Data;
using Bingol.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace Bingol
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddSingleton(_ => Configuration);
            services.AddDbContext<BingolContext>
           (options => options.UseSqlServer(Configuration.GetConnectionString("AuthDbContextConnection")));
            services.AddDefaultIdentity<BingolUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<BingolContext>();
            services.AddScoped<DbContext, BingolContext>();
            //In-Memory
            services.AddDistributedMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(1);
            });
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = _ => true;
                options.ConsentCookie.IsEssential = true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public  void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseCookiePolicy();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            CreateRoles(serviceProvider);
        }



        private static void CreateRoles(IServiceProvider serviceProvider)
        {

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<BingolUser>>();
            Task<IdentityResult> roleResult;
            const string username = "admin@bingol.com";

            //Check that there is an Administrator role and create if not
            var hasAdminRole = roleManager.RoleExistsAsync("Admin");
            hasAdminRole.Wait();

            var hasCustomerRole = roleManager.RoleExistsAsync("Customer");
            hasCustomerRole.Wait();

            if (!hasAdminRole.Result)
            {
                roleResult = roleManager.CreateAsync(new IdentityRole("Admin"));
                roleResult.Wait();
            }
            if (!hasCustomerRole.Result)
            {
                roleResult = roleManager.CreateAsync(new IdentityRole("Customer"));
                roleResult.Wait();
            }

            //Check if the admin user exists and create it if not
            //Add to the Administrator role

            var testUser = userManager.FindByNameAsync(username);
            testUser.Wait();

            if (testUser.Result != null) return;
            var administrator = new BingolUser
            {
                UserFirstName = "Bingol",
                UserLastName = "Bingol",
                PhoneNumber = "+880123493249",
                EmailConfirmed = true,
                Email = username,
                UserName = username
            };

            var newUser = userManager.CreateAsync(administrator, "Bingol123@");
            newUser.Wait();

            if (!newUser.Result.Succeeded) return;
            var newUserRole = userManager.AddToRoleAsync(administrator, "Admin");
            newUserRole.Wait();

        }
    }
}
