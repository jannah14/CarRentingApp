using CarRentingApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentingApp.Repositories
{
    public interface IVehicleRepository
    {
        Task<IEnumerable<VehicleDTO>> GetVehicles(byte type, string searchStr, DateTime? startDate, DateTime? endDate);


        Task<IEnumerable<MinVehicleDTO>> GetAllVehicles();
    }
}
