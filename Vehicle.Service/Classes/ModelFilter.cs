using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Service.Interfaces;
using Vehicle.Service.Models;

namespace Vehicle.Service.Classes
{
    public class ModelFilter : IModelFilter
    {
        public string SearchString { get; set; }

        public ModelFilter(string searchString)
        {
            SearchString = searchString;
        }

        public IQueryable<VehicleModel> GetFilter(IQueryable<VehicleModel> vehicleModels)
        {
            if (!String.IsNullOrEmpty(SearchString))
            {
                vehicleModels = vehicleModels.Where(mo => mo.Name.ToUpper() == SearchString.ToUpper()
                        || mo.VehicleMake.Name.ToUpper() == SearchString.ToUpper());
            }

            return vehicleModels;
        }
    }
}
