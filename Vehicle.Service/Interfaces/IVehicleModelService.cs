using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Service.Models;

namespace Vehicle.Service.Interfaces
{
    public interface IVehicleModelService
    {
        void CreateAsync(VehicleModel vehicleModel);
        void UpdateAsync(VehicleModel vehicleModel);
        void DeleteAsync(VehicleModel vehiclemodel);
        Task<VehicleModel> GetByIdAsync(int? id);
        Task<IPagedList<VehicleModel>> Find(string sortOrder, string searchString, int pageNumber);
        Task<List<VehicleMake>> GetMakes();
    }
}
