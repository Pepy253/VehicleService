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

        public async Task<IPagedList<VehicleModel>> GetFilterAndSort(string sortOrder, string searchString, int pageNumber, int pageSize)
        {
            var vehicleModels = _context.VehicleModels.Include(ma => ma.VehicleMake).Select(mo => mo);

            if (!String.IsNullOrEmpty(searchString))
            {
                vehicleModels = vehicleModels.Where(mo => mo.Name.Contains(searchString)
                                                || mo.VehicleMake.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "model_name_desc":
                    vehicleModels = vehicleModels.OrderByDescending(mo => mo.Name);
                    break;
                case "model_name":
                    vehicleModels = vehicleModels.OrderBy(mo => mo.Name);
                    break;
                case "make_name_desc":
                    vehicleModels = vehicleModels.OrderByDescending(ma => ma.VehicleMake.Name);
                    break;
                default:
                    vehicleModels = vehicleModels.OrderBy(ma => ma.VehicleMake.Name);
                    break;
            }

            var pagedList = await vehicleModels.ToPagedListAsync(pageNumber, pageSize);

            return pagedList;
        }

        public async Task<List<VehicleMake>> GetMakes()
        { 
            return await _context.VehicleMakes.ToListAsync();
        }

    }
}
