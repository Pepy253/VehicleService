using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Vehicle.Service.Data;
using Vehicle.Service.Models;
using Vehicle.Service.Interfaces;
using PagedList;
using PagedList.EntityFramework;

namespace Vehicle.Service.Classes
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly VehicleDbContext _context;

        public VehicleMakeService(VehicleDbContext context)
        {
            _context = context;
        }

        public async void CreateAsync(VehicleMake vehicleMake)
        {
            _context.VehicleMakes.Add(vehicleMake);
            await _context.SaveChangesAsync();
        }   
        
        public async void UpdateAsync(VehicleMake vehicleMake)
        {
            _context.Entry(vehicleMake).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async void DeleteAsync(VehicleMake vehicleMake)
        {
            _context.VehicleMakes.Remove(vehicleMake);
            await _context.SaveChangesAsync();
        }

        public async Task<VehicleMake> GetByIdAsync(int? id)
        {
            return  await _context.VehicleMakes.FindAsync(id);
        }

        public async Task<IPagedList<VehicleMake>> Find(IMakeFilter makeFilter, IMakeSort makeSort, IMakePage makePage)
        {
            var vehicleMakes = _context.VehicleMakes.Select(ma => ma);
            var filteredMakes = makeFilter.GetFilter(vehicleMakes);
            var sortedMakes = makeSort.GetSort(filteredMakes);
            var pagedList = await makePage.GetPagedListAsync(sortedMakes);

            return pagedList;
        }
    }
}
