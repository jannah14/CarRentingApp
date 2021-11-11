using CarRentingApp.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentingApp.Repositories
{
    public interface IRentalRepository
    {
        Task<bool> ApplyForRent(CreateRentalDTO rentalDTO);

        Task<IEnumerable<UserRentalsDTO>> GetUserRentals(string userId, byte? filterByStatus); //return all the rentals of a user 

        Task<bool> DeleteRental(int rentalId, string loggedUser);

        Task<IEnumerable<AgentRentalsDTO>> GetAgentRentals(string userId);


        Task<bool> Approve(int rentalId, string loggedUser);

        Task<bool> Reject(int rentalId, string loggedUser, string rejectionReason);

        Task<EditRentalDTO> GetRentalById(int rentalId);

        Task<bool> Edit(EditRentalDTO rentalDTO);

        Task<bool> IsAvailable(DateTime startDate, DateTime endDate, int vehicleId);
    }
}
