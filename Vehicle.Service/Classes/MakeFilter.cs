using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Service.Interfaces;
using Vehicle.Service.Models;

namespace Vehicle.Service.Classes
{
    public class MakeFilter : IMakeFilter
    {
        public IQueryable<VehicleMake> GetFilter(IQueryable<VehicleMake> vehicleMakes, string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                vehicleMakes = vehicleMakes.Where(ma => ma.Name.ToUpper() == searchString.ToUpper());
            }

            return vehicleMakes; 
        }
    }
}
