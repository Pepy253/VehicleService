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
        private readonly IMakeSort _sort;
        private readonly IMakeFilter _filter;
        private readonly IMakePage _page;

        public VehicleMakeService(VehicleDbContext context, IMakeFilter filter, IMakeSort sort, IMakePage page)
        {
            _context = context;
            _sort = sort;
            _filter = filter;
            _page = page;
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

        public async Task<IPagedList<VehicleMake>> Find(string sortOrder, string searchString, int pageNumber)
        {
            var vehicleMakes = _context.VehicleMakes.Select(ma => ma);
            var filteredMakes = _filter.GetFilter(vehicleMakes, searchString);
            var sortedMakes = _sort.GetSort(filteredMakes, sortOrder);
            var pagedList = await _page.GetPagedListAsync(sortedMakes, pageNumber);

            return pagedList;
        }
    }
}
