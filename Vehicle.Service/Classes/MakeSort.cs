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
        public string SortOrder { get; set; }

        public MakeSort(string sortOrder)
        {
            SortOrder = sortOrder;
        }

        public IQueryable<VehicleMake> GetSort(IQueryable<VehicleMake> vehicleMakes)
        {

            switch (SortOrder)
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
