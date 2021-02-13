using PagedList;
using PagedList.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Service.Interfaces;
using Vehicle.Service.Models;

namespace Vehicle.Service.Classes
{
    public class ModelPage : IModelPage
    {
        public async Task<IPagedList<VehicleModel>> GetPagedListAsync(IQueryable<VehicleModel> vehicleModels, int? page)
        {
            int pageSize = 5;
            var pagedList = await vehicleModels.ToPagedListAsync(page ?? 1, pageSize);

            return pagedList;
        }
    }
}
