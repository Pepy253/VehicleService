﻿using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Service.Models;

namespace Vehicle.Service.Interfaces
{
    public interface IMakePage
    {
        int Page { get; set; }
        Task<IPagedList<VehicleMake>> GetPagedListAsync(IQueryable<VehicleMake> vehicleMakes);
    }
}
