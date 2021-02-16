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
        public string SearchString { get; set; }

        public MakeFilter(string searchString)
        {
            SearchString = searchString;
        }

        public IQueryable<VehicleMake> GetFilter(IQueryable<VehicleMake> vehicleMakes)
        {
            if (!String.IsNullOrEmpty(SearchString))
            {
                vehicleMakes = vehicleMakes.Where(ma => ma.Name.ToUpper() == SearchString.ToUpper());
            }

            return vehicleMakes; 
        }
    }
}
