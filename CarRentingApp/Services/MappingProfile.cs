using AutoMapper;
using CarRentingApp.Areas.Identity.Data;
using CarRentingApp.DTOs;
using CarRentingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CarRentingApp.Areas.Identity.Pages.Account.RegisterModel;

namespace CarRentingApp.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UpdateUserDTO, AppUser>();

            CreateMap<InputModel, AppUser>();
            
            CreateMap<CreateRentalDTO, Rentals>();
        }
    }
}
