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
        public IQueryable<VehicleModel> GetSort(IQueryable<VehicleModel> models, string sortOrder)
        {
            switch (sortOrder)
            {
                case "make_name_desc":
                    models = models.OrderByDescending(ma => ma.VehicleMake.Name);
                    break;
                case "model_name":
                    models = models.OrderBy(mo => mo.Name);
                    break;
                case "model_name_desc":
                    models = models.OrderByDescending(mo => mo.Name);
                    break;
                default:
                    models = models.OrderBy(ma => ma.VehicleMake.Name);
                    break;                    
            }

            return models;
        }
    }
}
