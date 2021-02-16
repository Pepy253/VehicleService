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
        public int Page { get; set; }

        public ModelPage(int page)
        {
            Page = page;
        }

        public async Task<IPagedList<VehicleModel>> GetPagedListAsync(IQueryable<VehicleModel> vehicleModels)
        {
            int pageSize = 5;
            var pagedList = await vehicleModels.ToPagedListAsync(Page, pageSize);

            return pagedList;
        }
    }
}
