using CarRentingApp.DTOs;
using CarRentingApp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using static CarRentingApp.Areas.Identity.Pages.Account.RegisterModel;

namespace CarRentingApp.Controllers
{
    [Authorize(Roles ="Admin")]  
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
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var users = await _userRepo.GetUsers(adminId);

            if(users == null)
            {
                return NoContent();
            }

            return View(users);
        }

        //delete a user
        [HttpDelete]
        public async Task<IActionResult> Delete(Identifier identifier)
        {
            if (identifier.UserId == null || identifier.UserId == "")
                return BadRequest();
            
            var result = await _userRepo.DeleteUser(identifier.UserId);

            if (result)
            {
                return Ok();
            }

            return NotFound();
            
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
            if(user.Id == null || user.Id == "" || user.Role == null || user.Role == "")
                return BadRequest();
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


        //validations
        [AcceptVerbs("GET", "POST", "PUT", "PATCH")]
        public  IActionResult VerifyBirthday(InputModel user)
        {
            if ((DateTime.Now.Year - user.Birthday.Year) >= 18)
            {
                return Json(true);
            }

            return Json("You should be at least 18 years old to register!");
        }

        [AcceptVerbs("GET", "POST", "PUT")]
        public  IActionResult VerifyUpdateBirthday(UpdateUserDTO user)
        {
            if ((DateTime.Now.Year - user.Birthday.Year) >= 18)
            {
                return Json(true);
            }

            return Json("You should be at least 18 years old to register!");
        }

    }

    public class Identifier
    {
        public string UserId { get; set; }
    }
}
