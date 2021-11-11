using CarRentingApp.DTOs;
using CarRentingApp.Repositories;
using CarRentingApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarRentingApp.Controllers
{
    public class RentalsController : Controller
    {
        private readonly IVehicleRepository _vehicleRepo;
        private readonly IRentalRepository _rentalRepository;

        

        public RentalsController(IVehicleRepository vehicleRepo, IRentalRepository rentalRepository)
        {
            _vehicleRepo = vehicleRepo;
            _rentalRepository = rentalRepository;
        }

        [Authorize(Roles ="User")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> GetVehicles(DateTime? startDate, DateTime? endDate, string searchStr, byte vehicleType)
        {
            var result = await _vehicleRepo.GetVehicles(vehicleType, searchStr, startDate, endDate);

            return Json(result);
        }


        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> CreateRental(byte vehicleType)
        {
            var vm = new CreateRentalViewModel();

            vm.Rental = new CreateRentalDTO();
            vm.Type = vehicleType;

            return View(vm);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> CreateRental(CreateRentalDTO newRental)
        {
           
            newRental.AppUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);  //find the id of the loggeduser

            var result = await _rentalRepository.ApplyForRent(newRental);

            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> UserRentals(byte? filterByStatus)
        {
            var loggedUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var vm = await _rentalRepository.GetUserRentals(loggedUser, filterByStatus);

            return View(vm);
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> UserRentalsFiltered(byte? filterByStatus)
        {
            var loggedUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var rentals = await _rentalRepository.GetUserRentals(loggedUser, filterByStatus);

            return Json(rentals);

        }

        [Authorize(Roles = "User")]
        [HttpDelete]
        public async Task<IActionResult> Delete(IntIdentifier rentalId)
        {
            var loggedUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _rentalRepository.DeleteRental(rentalId.Id, loggedUser);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        [Authorize(Roles = "Agent")]
        [HttpGet]
        public async Task<IActionResult> AgentRentals()
        {
            var loggedUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var vm = await _rentalRepository.GetAgentRentals(loggedUser);

            return View(vm);
        }

        [Authorize(Roles = "Agent")]
        [HttpPut]
        public async Task<IActionResult> ApproveRental(IntIdentifier rentalId)
        {
            var loggedUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _rentalRepository.Approve(rentalId.Id, loggedUser);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        [Authorize(Roles = "Agent")]
        [HttpPut]
        public async Task<IActionResult> RejectRental(RejectRental rejectRental)
        {
            var loggedUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _rentalRepository.Reject(rejectRental.RentalId, loggedUser, rejectRental.RejectionReason);

            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> Edit(int rentalId)
        {
            var vm = new EditRentalViewModel();
            vm.Rental = await  _rentalRepository.GetRentalById(rentalId);
            vm.Vehicles = await _vehicleRepo.GetAllVehicles();

            return View(vm);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditRentalDTO Rental)
        {
            var result = await _rentalRepository.Edit(Rental);

            if (result)
            {
                return RedirectToAction("UserRentals");
            }

            return BadRequest();
        }


        //validations
        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> VerifyVehicle(EditRentalDTO rental)
        {
            if (await _rentalRepository.IsAvailable(rental.StartDate, rental.EndDate, rental.VehicleId))
            {
                return Json(true);
            }

            return Json("This vehicle is not available for the selected dates!");
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> VerifyStartDate(EditRentalDTO rental)
        {
            if (rental.StartDate < DateTime.UtcNow)
            {
                return Json("Please provide a future date.");
            }

            return Json(true);
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> VerifyEndDate(EditRentalDTO rental)
        {
            if (rental.EndDate < DateTime.UtcNow)
            {
                return Json("Please provide a future date.");
            }

            if (rental.EndDate <= rental.StartDate)
            {
                return Json("End date must be greater than the start date.");
            }

            return Json(true);
        }
    }

    public class IntIdentifier
    {
        public int Id { get; set; }
    }

    public class RejectRental
    {
        public int RentalId { get; set; }
        public string RejectionReason { get; set; }
    }
}
