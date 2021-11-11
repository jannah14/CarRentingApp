using CarRentingApp.DTOs;
using CarRentingApp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
            var users = await _userRepo.GetUsers();

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
            var result = await _userRepo.DeleteUser(identifier.UserId);

            if (result)
            {
                return Ok();
            }

            return NotFound();
            
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUser(string UserId)
        {
            var user = await _userRepo.GetUserById(UserId);

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


        //validations
        [AcceptVerbs("GET", "POST")]
        public  IActionResult VerifyBirthday(InputModel user)
        {
            if ((DateTime.Now.Year - user.Birthday.Year) >= 18)
            {
                return Json(true);
            }

            return Json("You should be at least 18 years old to register!");
        }

        [AcceptVerbs("GET", "POST", "PUT")]
        public  IActionResult VerifyBirthday(UpdateUserDTO user)
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
