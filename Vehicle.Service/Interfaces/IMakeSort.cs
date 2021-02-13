﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Service.Models;

namespace Vehicle.Service.Interfaces
{
    public interface IMakeSort
    {
        IQueryable<VehicleMake> GetSort(IQueryable<VehicleMake> vehicleMakes, string sortOrder);
    }
}
