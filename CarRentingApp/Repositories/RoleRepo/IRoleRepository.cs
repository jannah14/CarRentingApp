using CarRentingApp.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRentingApp.Repositories
{
    public interface IRoleRepository
    {
        Task<IEnumerable<string>> GetAllRoleNames();  //return names of all the roles in the database

        Task<bool> CreateNewRole(string roleName); //add a new Role in the database

        Task<bool> AssignUserToRole(AddtoRole addToRole); //link a user with a specific Role
    }
}
