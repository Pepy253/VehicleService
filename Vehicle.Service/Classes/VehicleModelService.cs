using PagedList;
using PagedList.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Vehicle.Service.Data;
using Vehicle.Service.Interfaces;
using Vehicle.Service.Models;

namespace Vehicle.Service.Classes
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly VehicleDbContext _context;
        private readonly IModelFilter _filter;
        private readonly IModelSort _sort;
        private readonly IModelPage _page;

        public VehicleModelService(VehicleDbContext context, IModelFilter filter, IModelSort sort, IModelPage page)
        {
            _context = context;
            _filter = filter;
            _sort = sort;
            _page = page;
        }

        public async void CreateAsync(VehicleModel vehicleModel)
        {
            _context.VehicleModels.Add(vehicleModel);
            await _context.SaveChangesAsync();
        }

        public async void UpdateAsync(VehicleModel vehicleModel)
        {
            _context.Entry(vehicleModel).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async void DeleteAsync(VehicleModel vehicleModel)
        {
            _context.VehicleModels.Remove(vehicleModel);
            await _context.SaveChangesAsync();
        }

        public async Task<VehicleModel> GetByIdAsync(int? id)
        {
            return await _context.VehicleModels.Include(ma => ma.VehicleMake).FirstOrDefaultAsync(mo => mo.Id == id);
        }

        public async Task<IPagedList<VehicleModel>> Find(string sortOrder, string searchString, int pageNumber)
        {
            var vehicleModels = _context.VehicleModels.Include(ma => ma.VehicleMake).Select(mo => mo);
            var filteredModels = _filter.GetFilter(vehicleModels, searchString);
            var sortedModels = _sort.GetSort(filteredModels, sortOrder);
            var pagedList = await _page.GetPagedListAsync(sortedModels, pageNumber);

            return pagedList;
        }

        public async Task<List<VehicleMake>> GetMakes()
        { 
            return await _context.VehicleMakes.ToListAsync();
        }
    }
}
