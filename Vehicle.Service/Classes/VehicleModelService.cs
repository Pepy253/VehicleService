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

        public VehicleModelService(VehicleDbContext context)
        {
            _context = context;
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

        public async Task<IPagedList<VehicleModel>> Find(IModelFilter modelFilter, IModelSort modelSort, IModelPage modelPage)
        {
            var vehicleModels = _context.VehicleModels.Include(ma => ma.VehicleMake).Select(mo => mo);
            var filteredModels = modelFilter.GetFilter(vehicleModels);
            var sortedModels = modelSort.GetSort(filteredModels);
            var pagedList = await modelPage.GetPagedListAsync(sortedModels);

            return pagedList;
        }

        public async Task<List<VehicleMake>> GetMakes()
        { 
            return await _context.VehicleMakes.ToListAsync();
        }
    }
}
