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
        public IQueryable<VehicleModel> GetFilter(IQueryable<VehicleModel> models, string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                models = models.Where(mo => mo.Name.ToUpper() == searchString.ToUpper()
                        || mo.VehicleMake.Name.ToUpper() == searchString.ToUpper());
            }

            return models;
        }
    }
}
