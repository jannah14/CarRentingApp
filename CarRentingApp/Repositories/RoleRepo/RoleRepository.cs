using CarRentingApp.Areas.Identity.Data;
using CarRentingApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentingApp.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleRepository(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> AssignUserToRole(AddtoRole addToRole)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(addToRole.UserEmail);
                await _userManager.AddToRoleAsync(user, addToRole.Role);

                return true;
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }

            return false;
        }

        public async Task<bool> CreateNewRole(string roleName)
        {
            //check if the role exists in the database
            var roleExists = await _roleManager.RoleExistsAsync(roleName);

            //add the role in the database only if it doesn't exist
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<string>> GetAllRoleNames()
        {
            var roles = _roleManager.Roles.Select(r => r.Name);

            return roles;
        }
    }
}
