using CarRentingApp.Models;
using CarRentingApp.Repositories;
using CarRentingApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentingApp.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RolesController : Controller
    {
        
        private readonly IRoleRepository _roleRepo;

        public RolesController(IRoleRepository roleRepo)
        {
            _roleRepo = roleRepo;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectRole newRole)
        {
            var result = await _roleRepo.CreateNewRole(newRole.Name);
            if (result)
                return RedirectToAction("AssignUserToRole");

            return BadRequest();
        }

        public async Task<IActionResult> AssignUserToRole()
        {
            var roles = await _roleRepo.GetAllRoleNames();

            var viewModel = new AssignUsersToRolesViewModel
            {
                Roles = roles.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AssignUserToRole(AddtoRole addtoRole)
        {
            var result = await _roleRepo.AssignUserToRole(addtoRole);

            if (result)
            {
                return Ok();
            }

            return StatusCode(StatusCodes.Status304NotModified);
            
        }

    }
}
