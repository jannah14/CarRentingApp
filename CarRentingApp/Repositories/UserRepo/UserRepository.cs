using CarRentingApp.Areas.Identity.Data;
using CarRentingApp.Data;
using CarRentingApp.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using static CarRentingApp.Areas.Identity.Pages.Account.RegisterModel;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace CarRentingApp.Repositories 
{ 
    public class UserRepository : IUserRepository
    {
        private readonly CarRentingAppContext _dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public UserRepository(CarRentingAppContext dbContext, UserManager<AppUser> userManager, IMapper mapper)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<bool> CreateUser(InputModel user)
        {
            try
            {
                //map the data to the AppUser object
                var newAppUser = new AppUser();
                var appUser = _mapper.Map<InputModel, AppUser>(user, newAppUser);

                //set email verified by default
                appUser.EmailConfirmed = true;
                //set username as email
                appUser.UserName = newAppUser.Email;


                //create new user with password
                await _userManager.CreateAsync(appUser, user.Password);

                //add user in db
                _dbContext.Users.Add(appUser);

                // set a user with role User by default
                await _userManager.AddToRoleAsync(appUser, "User");

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }

            return false;
            
        }

        public async Task<bool> DeleteUser(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                await _userManager.DeleteAsync(user);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return false;

        }

        public async Task<UpdateUserDTO> GetUserById(string userId)
        {
            var users = from u in _dbContext.Users
                        join ur in _dbContext.UserRoles on u.Id equals ur.UserId
                        join r in _dbContext.Roles on ur.RoleId equals r.Id
                        where u.Id == userId
                        select new UpdateUserDTO
                        {
                            Id = u.Id,
                            Firstname = u.FirstName,
                            Lastname = u.LastName,
                            Birthday = u.Birthday,
                            PhoneNumber = u.PhoneNumber,
                            Role = r.Name
                        };

            if (users.Any())
            {
                return await users.FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<List<AllUserDTO>> GetUsers(string adminId)
        {
            var users = from u in _dbContext.Users
                        join ur in _dbContext.UserRoles on u.Id equals ur.UserId
                        join r in _dbContext.Roles on ur.RoleId equals r.Id
                        where u.Id != adminId
                        select new AllUserDTO
                        {
                            Id = u.Id,
                            Firstname = u.FirstName,
                            Lastname = u.LastName,
                            Birthday = u.Birthday,
                            PhoneNumber = u.PhoneNumber,
                            Email = u.Email,
                            Username = u.UserName,
                            Role = r.Name
                        };
            
            if(users.Any())
            {
                return users.ToList();
            }

            return null;
        }

        public async Task<bool> UpdateUser(UpdateUserDTO user)
        {
            try
            {
                var userInDb = await _userManager.FindByIdAsync(user.Id);

                var updatedUser = _mapper.Map<UpdateUserDTO, AppUser>(user, userInDb);

                //update the user
                await _userManager.UpdateAsync(updatedUser);

                //if user changed the role, save the changes 
                if (!(await _userManager.IsInRoleAsync(userInDb, user.Role)))
                {
                    //remove from the previous role linked with the user
                    var previousRoles = await _userManager.GetRolesAsync(userInDb);
                    await _userManager.RemoveFromRolesAsync(userInDb, previousRoles);

                    //assign the new role to the user
                    await _userManager.AddToRoleAsync(userInDb, user.Role);
                }

                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

            return false;
        }
    }
}
