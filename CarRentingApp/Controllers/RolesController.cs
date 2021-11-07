using CarRentingApp.Areas.Identity.Data;
using CarRentingApp.Models;
using CarRentingApp.Repositories.UserRepo;
using CarRentingApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentingApp.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserRepository _userRepo;

        public RolesController(RoleManager<IdentityRole> roleManager, IUserRepository userRepo, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userRepo = userRepo;
            _userManager = userManager;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectRole newRole)
        {
            //check if the role exists in the database
            var roleExists = await _roleManager.RoleExistsAsync(newRole.Name);

            //add the role in the database only if it doesn't exist
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new IdentityRole(newRole.Name));
                return Ok();
            }

            return BadRequest();
        }

        public async Task<IActionResult> AssignUserToRole()
        {
            var roles = _roleManager.Roles.Select(r => r.Name).ToList();
            var users = await _userRepo.GetUsers();

            var viewModel = new AssignUsersToRolesViewModel
            {
                Roles = roles
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AssignUserToRole(AddtoRole addtoRole)
        {
            var user = await _userManager.FindByEmailAsync(addtoRole.UserEmail);
            await _userManager.AddToRoleAsync(user, addtoRole.Role);
            
            return Ok();
            
        }


    }
}
