using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Banksystem.Models;
using Banksystem.Repositories.Classes;
using Banksystem.Repositories.Interfaces;
using Banksystem.Services.Classes;
using Banksystem.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Banksystem
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
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddDbContext<BankDBContext>(options =>
            options.UseSqlServer(
            Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>().AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<BankDBContext>();

            services.AddControllersWithViews();

            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<ITransactionService, TransactionService>();

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            var cultureInfo = new CultureInfo("en-US");
            cultureInfo.NumberFormat.CurrencySymbol = "€";

            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
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


            app.UseRouting();
            app.UseSession(new SessionOptions { IdleTimeout = new TimeSpan(0, 10, 0) });
            app.UseAuthorization();
            app.UseAuthentication();
            MyIdentityDataInitializer.SeedData(userManager, roleManager);
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
        public static class MyIdentityDataInitializer
        {
            public static void SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
            {
                SeedRoles(roleManager);
                SeedUsers(userManager);
            }

            public static void SeedUsers(UserManager<IdentityUser> userManager)
            {
                if (userManager.FindByNameAsync("stefan.holmberg@systementor.se").Result == null)
                {
                    IdentityUser user = new IdentityUser();
                    user.UserName = "stefan.holmberg@systementor.se";
                    user.Email = "stefan.holmberg@systementor.se";
                    //user.PasswordHash = "Hejsan123#";

                    IdentityResult result = userManager.CreateAsync(user, "Hejsan123#").Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Admin").Wait();
                    }
                }


                if (userManager.FindByNameAsync("stefan.holmberg@nackademin.se").Result == null)
                {
                    IdentityUser user = new IdentityUser();
                    user.UserName = "stefan.holmberg@nackademin.se";
                    user.Email = "stefan.holmberg@nackademin.se";
                    //user.PasswordHash = "Hejsan123#";

                    IdentityResult result = userManager.CreateAsync(user, "Hejsan123#").Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Cashier").Wait();
                    }
                }
            }
            public static void SeedRoles(RoleManager<IdentityRole> roleManager)
            {
                if (!roleManager.RoleExistsAsync("Admin").Result)
                {
                    IdentityRole role = new IdentityRole();
                    role.Name = "Admin";
                    IdentityResult roleResult = roleManager.CreateAsync(role).Result;
                }


                if (!roleManager.RoleExistsAsync("Cashier").Result)
                {
                    IdentityRole role = new IdentityRole();
                    role.Name = "Cashier";
                    IdentityResult roleResult = roleManager.CreateAsync(role).Result;
                }
            }
        }
    }
}
