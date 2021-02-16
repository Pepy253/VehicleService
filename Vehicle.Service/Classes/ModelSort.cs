using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Service.Interfaces;
using Vehicle.Service.Models;

namespace Vehicle.Service.Classes
{
    public class ModelSort : IModelSort
    {
        public string SortOrder { get; set; }

        public ModelSort(string sortOrder)
        {
            SortOrder = sortOrder;
        }

        public IQueryable<VehicleModel> GetSort(IQueryable<VehicleModel> vehicleModels)
        {
            switch (SortOrder)
            {
                case "make_name_desc":
                    vehicleModels = vehicleModels.OrderByDescending(ma => ma.VehicleMake.Name);
                    break;
                case "model_name":
                    vehicleModels = vehicleModels.OrderBy(mo => mo.Name);
                    break;
                case "model_name_desc":
                    vehicleModels = vehicleModels.OrderByDescending(mo => mo.Name);
                    break;
                default:
                    vehicleModels = vehicleModels.OrderBy(ma => ma.VehicleMake.Name);
                    break;                    
            }

            return vehicleModels;
        }
    }
}
