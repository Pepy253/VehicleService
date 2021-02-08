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

        public async Task<IPagedList<VehicleMake>> GetFilterAndSort(string sortOrder, string searchString, int pageNumber, int pageSize)
        {
            var vehicleMakes = _context.VehicleMakes.Select(ma => ma);

            if (!String.IsNullOrEmpty(searchString))
            { 
                vehicleMakes = vehicleMakes.Where(ma => ma.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "make_name_desc":
                    vehicleMakes = vehicleMakes.OrderByDescending(ma => ma.Name);
                    break;
                default:
                    vehicleMakes = vehicleMakes.OrderBy(ma => ma.Name);
                    break;
            }

            var pagedList = await vehicleMakes.ToPagedListAsync(pageNumber, pageSize);

            return pagedList;
        }
    }
}
