using CarRentingApp.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentingApp.ViewModels
{
    public class AssignUsersToRolesViewModel
    {
     
        public AddtoRole AddToRole { get; set; }
        public List<string> Roles { get; set; }
    }

    public class AddtoRole
    {
        public string Role { get; set; }
        public string UserEmail { get; set; }
    }
}
