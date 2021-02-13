using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Service.Interfaces;
using Vehicle.Service.Models;
using PagedList.EntityFramework;
using PagedList;

namespace Vehicle.Service.Classes
{
    public class MakePage : IMakePage
    {
        public async Task<IPagedList<VehicleMake>> GetPagedListAsync(IQueryable<VehicleMake> vehicleMakes, int? page)
        {
            int pageSize = 5;
            var pagedList = await vehicleMakes.ToPagedListAsync(page ?? 1, pageSize);

            return pagedList;
        }
    }
}
