using CarRentingApp.Data;
using CarRentingApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentingApp.Models;

namespace CarRentingApp.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly CarRentingAppContext _dbContext;

        public VehicleRepository(CarRentingAppContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<MinVehicleDTO>> GetAllVehicles()
        {
            var vehicles = from v in _dbContext.Vehicles
                           join vm in _dbContext.VehicleModels on v.VehicleModelId equals vm.Id
                           orderby vm.Brand, vm.Model, v.Color
                           select new MinVehicleDTO
                           {
                               Id = v.Id,
                               BrandModel = v.Color + " " + vm.Brand + " " + vm.Model
                           };
            return vehicles;
        }

        public async Task<IEnumerable<VehicleDTO>> GetVehicles(byte type, string searchStr, DateTime? startDate, DateTime? endDate)
        {
            //var vehicles = from v in _dbContext.Vehicles
            //               join vm in _dbContext.VehicleModels on v.VehicleModelId equals vm.Id
            //               join r in (
            //                   from rentals in _dbContext.Rentals
            //                   where rentals.Status == (byte)RentalStatus.Approved &&
            //                   ((startDate == null && endDate == null) || (rentals.StartDate <= endDate && rentals.EndDate >= startDate))
            //                   group rentals by rentals.VehicleId into groupedRentals
            //                   select new
            //                   {
            //                       VehicleId = groupedRentals.Key,
            //                       Rented = groupedRentals.Count()
            //                   }
            //               ) on v.Id equals r.VehicleId into rental
            //               from r in rental.DefaultIfEmpty()
            //               where vm.Type == type 
            //                     && (searchStr == null || vm.Brand.Contains(searchStr) || vm.Model.Contains(searchStr))
            //                     && (v.ItemsInStock > r.Rented || r.VehicleId != v.Id)
            //               select new VehicleDTO
            //               {
            //                   Id = v.Id,
            //                   Photo = v.Photo,
            //                   BrandModel = vm.Brand + " " + vm.Model,
            //                   PricePerDay = v.PricePerDay
            //               };



            var rentedVehicles = from r in _dbContext.Rentals
                               where r.Status == (byte)RentalStatus.Approved &&
                               ((startDate == null && endDate == null) || (r.StartDate <= endDate && r.EndDate >= startDate))
                               group r by r.VehicleId into groupedRentals
                               select new
                               {
                                   VehicleId = groupedRentals.Key,
                                   Rented = groupedRentals.Count()
                               };

            var allVehicles = from v in _dbContext.Vehicles
                              join vm in _dbContext.VehicleModels on v.VehicleModelId equals vm.Id
                              join r in rentedVehicles on v.Id equals r.VehicleId into rental
                              from subr in rental.DefaultIfEmpty()
                              where vm.Type == type
                                    && (searchStr == null || vm.Brand.Contains(searchStr) || vm.Model.Contains(searchStr))
                                    && (v.ItemsInStock > subr.Rented || subr.VehicleId != v.Id)
                              select new VehicleDTO
                              {
                                  Id = v.Id,
                                  BrandModel = v.Color + " " +vm.Brand + " " + vm.Model,
                                  PricePerDay = v.PricePerDay
                              };

            return allVehicles;
        }
    }
}
