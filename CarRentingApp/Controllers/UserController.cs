using CarRentingApp.DTOs;
using CarRentingApp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static CarRentingApp.Areas.Identity.Pages.Account.RegisterModel;

namespace CarRentingApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepo;
        private readonly IRoleRepository _roleRepo;

        public UserController(IUserRepository userRepo, IRoleRepository roleRepo)
        {
            _userRepo = userRepo;
            _roleRepo = roleRepo;
        }

        //return all the users registered in the application
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepo.GetUsers();

            if(users == null)
            {
                return NoContent();
            }

            return View(users);
        }

        //delete a user
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _userRepo.DeleteUser(id);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
            
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUser(string userId)
        {
            var user = await _userRepo.GetUserById(userId);

            if(user != null)
            {
                user.AllRoles = await _roleRepo.GetAllRoleNames();

                return View(user);
            }

            return NotFound();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> UpdateUser(UpdateUserDTO user)
        {
            var result = await _userRepo.UpdateUser(user);

            if (result)
                return RedirectToAction("GetUsers");

            return StatusCode(StatusCodes.Status304NotModified);
        }


        [HttpGet]
        public async Task<IActionResult> CreateUser()
        {
            var vm = new InputModel();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(InputModel newUser)
        {
            var result = await _userRepo.CreateUser(newUser);
            if (result)
            {
                return RedirectToAction("GetUsers");
            }

            return StatusCode(StatusCodes.Status304NotModified);
        }

    }
}
