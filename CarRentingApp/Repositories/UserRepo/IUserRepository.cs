using CarRentingApp.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using static CarRentingApp.Areas.Identity.Pages.Account.RegisterModel;

namespace CarRentingApp.Repositories
{
    public interface IUserRepository
    {
        Task<List<AllUserDTO>> GetUsers(); //return data for all the users in the database

        Task<UpdateUserDTO> GetUserById(string userId); //return data for one use based on Id

        Task<bool> DeleteUser(string userId);
        Task<bool> UpdateUser(UpdateUserDTO user);

        Task<bool> CreateUser(InputModel user);
    }
}
