using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vehicle.Service.Models;

namespace Vehicle.MVC.ViewModels
{
    public class VehicleModelViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Make")]
        public int MakeId { get; set; }
        [Display(Name = "Model")]
        [Required(ErrorMessage = "Model name is required!")]
        public string Name { get; set; }
        [Display(Name = "Abbreviation")]
        [Required(ErrorMessage = "Abbreviation is required!")]
        public string Abrv { get; set; }

        public VehicleMake VehicleMake { get; set; }
    }
}