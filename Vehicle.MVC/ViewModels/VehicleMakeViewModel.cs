using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vehicle.MVC.ViewModels
{
    public class VehicleMakeViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Make")]
        [Required(ErrorMessage = "Make name is required!")]
        [Remote("NameExists", "Model", ErrorMessage = "This Model already exists")]
        public string Name { get; set; }
        [Display(Name = "Abbreviation")]
        [Required(ErrorMessage = "Abbrevation is required!")]
        public string Abrv { get; set; }
    }
}