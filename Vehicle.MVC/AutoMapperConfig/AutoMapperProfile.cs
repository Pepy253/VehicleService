using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Vehicle.Service.Models;
using Vehicle.MVC.ViewModels;

namespace Vehicle.MVC.AutoMapperConfig
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<VehicleMake, VehicleMakeViewModel>().ReverseMap();
            CreateMap<VehicleModel, VehicleModelViewModel>().ReverseMap();
        }
    }
}