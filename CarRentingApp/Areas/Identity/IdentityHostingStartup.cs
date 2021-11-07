using System;
using CarRentingApp.Areas.Identity.Data;
using CarRentingApp.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(CarRentingApp.Areas.Identity.IdentityHostingStartup))]
namespace CarRentingApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<CarRentingAppContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("CarRentingAppContextConnection")));

                services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<CarRentingAppContext>();
            });
        }
    }
}