using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Service.Models;

namespace Vehicle.Service.Interfaces
{
    public interface IMakeFilter
    {
        IQueryable<VehicleMake> GetFilter(IQueryable<VehicleMake> makes, string searchString);
    }
}
