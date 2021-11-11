using CarRentingApp.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentingApp.Repositories
{
    public static class ServiceModuleExtension
    {
        public static void RegisterRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUserRepository, UserRepository>();
            serviceCollection.AddTransient<IRoleRepository, RoleRepository>();
            serviceCollection.AddTransient<IVehicleRepository, VehicleRepository>();
            serviceCollection.AddTransient<IRentalRepository, RentalRepository>();
        }
    }
}
