using CarRentingApp.Areas.Identity.Data;
using CarRentingApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentingApp.Repositories.UserRepo
{
    public class UserRepository : IUserRepository
    {
        private readonly CarRentingAppContext _dbContext;

        public UserRepository(CarRentingAppContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<string>> GetUsers()
        
        {
            var users = _dbContext.Users.Select(u => u.Id).ToList();

            return users;
        }
    }
}
