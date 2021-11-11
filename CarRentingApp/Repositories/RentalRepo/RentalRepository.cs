using AutoMapper;
using CarRentingApp.Data;
using CarRentingApp.DTOs;
using CarRentingApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentingApp.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly CarRentingAppContext _dbContext;
        private readonly IMapper _mapper;

        public RentalRepository(CarRentingAppContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<bool> ApplyForRent(CreateRentalDTO rentalDTO)
        {
            var newRental = new Rentals();

            var rentalInDb = _mapper.Map<CreateRentalDTO, Rentals>(rentalDTO, newRental);

            rentalInDb.Status = (byte)RentalStatus.Pending;

            _dbContext.Rentals.Add(rentalInDb);

            try
            {
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }


            return false;

        }

        public async Task<bool> Approve(int rentalId, string loggedUser)
        {

            var affecteddRows = _dbContext.Database.ExecuteSqlCommand($"Update Rentals Set Status = {RentalStatus.Approved} where Id = {rentalId}");

            return affecteddRows > 0 ? true : false;
        }

        public async Task<bool> DeleteRental(int rentalId, string loggedUser)
        {
            try
            {
                var deletedRows = _dbContext.Database.ExecuteSqlCommand($"Delete from Rentals where Id = {rentalId} and AppUserId = {loggedUser}");

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return false;
           
        }

        public async Task<bool> Edit(EditRentalDTO rentalDTO)
        {
            var updatedRows = _dbContext.Database.ExecuteSqlCommand($"Update Rentals Set StartDate = {rentalDTO.StartDate}, EndDate = {rentalDTO.EndDate}, VehicleId = {rentalDTO.VehicleId} where Id = {rentalDTO.Id}");

            return updatedRows > 0;
        }

        public async Task<IEnumerable<AgentRentalsDTO>> GetAgentRentals(string userId)
        {
            var rentals = from r in _dbContext.Rentals
                          join v in _dbContext.Vehicles on r.VehicleId equals v.Id
                          join vm in _dbContext.VehicleModels on v.VehicleModelId equals vm.Id
                          join u in _dbContext.Users on r.AppUserId equals u.Id
                          where v.AgentId == userId && r.Status == (byte) RentalStatus.Pending
                          select new AgentRentalsDTO
                          {
                              Id = r.Id,
                              Vehicle = v.Color + " " + vm.Brand + " " + vm.Model,
                              StartDate = r.StartDate,
                              EndDate = r.EndDate,
                              User = u.FirstName + " " + u.LastName
                          };

            return rentals;
        }

        public async Task<EditRentalDTO> GetRentalById(int rentalId)
        {
            var rental = from r in _dbContext.Rentals
                         where r.Id == rentalId
                         select new EditRentalDTO
                         {
                             Id = r.Id,
                             StartDate = r.StartDate,
                             EndDate = r.EndDate,
                             VehicleId = r.VehicleId,
                             Status = r.Status,
                             RejectionReason = r.RejectionReason
                         };
            return rental.FirstOrDefault();
        }

        public async Task<IEnumerable<UserRentalsDTO>> GetUserRentals(string userId, byte? filterByStatus)
        {
            var rentals = from r in _dbContext.Rentals
                          join v in _dbContext.Vehicles on r.VehicleId equals v.Id
                          join vm in _dbContext.VehicleModels on v.VehicleModelId equals vm.Id
                          join u in _dbContext.Users on v.AgentId equals u.Id
                          where r.AppUserId == userId && (filterByStatus == null || r.Status == filterByStatus)
                          select new UserRentalsDTO
                          {
                              Id = r.Id,
                              VehicleModel = v.Color + " " + vm.Brand + " " + vm.Model,
                              StartDate = r.StartDate,
                              EndDate = r.EndDate,
                              TotalPrice = r.TotalPrice,
                              Status = r.Status,
                              Agent = u.FirstName + " " + u.LastName,
                              DaysLeft = ((int)Math.Ceiling((r.EndDate - DateTime.UtcNow).TotalDays) + 1),
                              RejectionReason = r.RejectionReason
                          };

            return rentals;

        }

        public async Task<bool> Reject(int rentalId, string loggedUser, string rejectionReason)
        {
            var affecteddRows = _dbContext.Database.ExecuteSqlCommand($"Update Rentals Set Status = {RentalStatus.Rejected}, RejectionReason= {rejectionReason} where Id = {rentalId}");

            return affecteddRows > 0;
        }

        public async Task<bool> IsAvailable(DateTime startDate, DateTime endDate, int vehicleId)
        {
            var sql = await (from v in _dbContext.VehicleModels
                      join r in _dbContext.Rentals on v.Id equals r.VehicleId
                      where r.Status == (byte)RentalStatus.Approved && v.Id == vehicleId &&
                      (r.StartDate <= endDate && r.EndDate >= startDate)
                      group r by r.VehicleId into groupedRentals
                      select groupedRentals.Count()).FirstOrDefaultAsync();
            var available = await _dbContext.Vehicles.FirstOrDefaultAsync(v => v.Id == vehicleId && v.ItemsInStock > sql);

            return available != null; 
        }
    }
}
