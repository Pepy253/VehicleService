using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Vehicle.MVC.ViewModels;
using Vehicle.Service.Classes;
using Vehicle.Service.Interfaces;
using Vehicle.Service.Models;

namespace Vehicle.MVC.Controllers
{
    public class MakeController : Controller
    {
        private readonly IVehicleMakeService _service;
        private readonly IMapper _mapper;

        public MakeController(IVehicleMakeService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SortOrder = sortOrder;
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "make_name_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            int pageNum = page ?? 1;
            
            IMakeFilter makeFilter = new MakeFilter(searchString);
            IMakeSort makeSort = new MakeSort(sortOrder);
            IMakePage makePage = new MakePage(pageNum);

            var makes = await _service.Find(makeFilter, makeSort, makePage);
            var makesVM = _mapper.Map<IEnumerable<VehicleMake>, IEnumerable<VehicleMakeViewModel>>(makes.ToArray());
            var pagedMakesVM = new StaticPagedList<VehicleMakeViewModel>(makesVM, makes.GetMetaData());

            Response.StatusCode = 200;
            return View(pagedMakesVM);
        }
        [HttpGet]       
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 400;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vehicleMake = _mapper.Map<VehicleMakeViewModel>(await _service.GetByIdAsync(id));

            if (vehicleMake == null)
            {
                Response.StatusCode = 404;
                return HttpNotFound();
            }

            Response.StatusCode = 200; 
            return View(vehicleMake);
        }
        
        [HttpGet]
        public ActionResult Create()
        { 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VehicleMakeViewModel makeVM)
        {
            if(ModelState.IsValid)
            {
                var vehicleMake = _mapper.Map<VehicleMake>(makeVM);
                await Task.Run(() => _service.CreateAsync(vehicleMake));

                Response.StatusCode = 201;
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");

                Response.StatusCode = 400;
                return View();
            }
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var makeVM = _mapper.Map<VehicleMakeViewModel>(await _service.GetByIdAsync(id));

            return View(makeVM);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPost(VehicleMakeViewModel modelVM)
        {

            var makeToUpdate = _mapper.Map<VehicleMake>(modelVM);

            if (TryUpdateModel(makeToUpdate))
            {
                try
                {
                    await Task.Run(() => _service.UpdateAsync(makeToUpdate));

                    Response.StatusCode = 200;
                    return RedirectToAction("Index");
                }
                catch 
                {
                    Response.StatusCode = 400;
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");                   
                }
            }

            return View();
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
            var makeVM = _mapper.Map<VehicleMakeViewModel>(await _service.GetByIdAsync(id));

            Response.StatusCode = 200;
            return View(makeVM);
        }

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var vehicleMake = await _service.GetByIdAsync(id);
                await Task.Run(() => _service.DeleteAsync(vehicleMake));

                Response.StatusCode = 200;
                return RedirectToAction("Index");
            }
            catch
            {
                Response.StatusCode = 400;
                ModelState.AddModelError("", "Unable to delete. Try again, and if the problem persists, see your system administrator.");
                return View();
            }
        }
    }
}