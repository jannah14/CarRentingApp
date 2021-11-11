using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentingApp.Areas.Identity.Data;
using CarRentingApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarRentingApp.Data
{
    public class CarRentingAppContext : IdentityDbContext<AppUser>
    {
        public CarRentingAppContext(DbContextOptions<CarRentingAppContext> options)
            : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }


        public DbSet<VehicleModel> VehicleModels { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<Rentals> Rentals { get; set; }
        
    }
}
