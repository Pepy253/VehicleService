using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Service.Data;
using Vehicle.Service.Interfaces;
using Vehicle.Service.Models;

namespace Vehicle.Service.Classes
{
    public class MakeSort : IMakeSort
    {
        public IQueryable<VehicleMake> GetSort(IQueryable<VehicleMake> vehicleMakes, string sortOrder)
        {

            switch (sortOrder)
            {
                case "make_name_desc":
                    vehicleMakes = vehicleMakes.OrderByDescending(ma => ma.Name);
                    break;
                default:
                    vehicleMakes = vehicleMakes.OrderBy(ma => ma.Name);
                    break;
            }

            return vehicleMakes;                
        }
    }
}
