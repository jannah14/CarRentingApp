using CarRentingApp.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentingApp.Repositories.UserRepo
{
    public interface IUserRepository
    {
        Task<List<string>> GetUsers(); //return all the users of the database
    }
}
