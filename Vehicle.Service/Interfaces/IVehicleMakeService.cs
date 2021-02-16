using System.Collections.Generic;
using System.Threading.Tasks;
using Vehicle.Service.Models;
using System.Linq;
using PagedList;

namespace Vehicle.Service.Interfaces
{
    public interface IVehicleMakeService
    {
        void CreateAsync(VehicleMake vehicleMake);
        void UpdateAsync(VehicleMake vehicleMake);
        void DeleteAsync(VehicleMake vehicleMake);
        Task<VehicleMake> GetByIdAsync(int? id);
        Task<IPagedList<VehicleMake>> Find(IMakeFilter makeFilter, IMakeSort makeSort, IMakePage makePage);
    }
}
