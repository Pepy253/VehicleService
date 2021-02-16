using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Service.Models;

namespace Vehicle.Service.Interfaces
{
    public interface IModelSort
    {
        string SortOrder { get; set; }
        IQueryable<VehicleModel> GetSort(IQueryable<VehicleModel> vehicleModels);
    }
}
