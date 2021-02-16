using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Vehicle.Service.Interfaces;
using Vehicle.Service.Models;
using AutoMapper;
using Vehicle.MVC.ViewModels;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using Vehicle.Service.Classes;

namespace Vehicle.MVC.Controllers
{
    public class ModelController: Controller
    {
        private readonly IVehicleModelService _service;
        private readonly IMapper _mapper;

        public ModelController(IVehicleModelService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;

        }

        public async Task<ActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.SortOrder = sortOrder;
            ViewBag.MakeNameSortParam = String.IsNullOrEmpty(sortOrder) ? "make_name_desc" : "";
            ViewBag.ModelNameSortParam = sortOrder == "model_name" ? "model_name_desc" : "model_name";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            int pageNum = (page ?? 1);

            IModelFilter modelFilter = new ModelFilter(searchString);
            IModelSort modelSort = new ModelSort(sortOrder);
            IModelPage modelPage = new ModelPage(pageNum);


            var models = await _service.Find(modelFilter, modelSort, modelPage);
            var modelsVM = _mapper.Map<IEnumerable<VehicleModel>, IEnumerable<VehicleModelViewModel>>(models.ToArray());
            var pagedModelsVM = new StaticPagedList<VehicleModelViewModel>(modelsVM, models.GetMetaData());

            Response.StatusCode = 200;
            return View(pagedModelsVM);
        }
        [HttpGet]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            VehicleModelViewModel modelVM = _mapper.Map<VehicleModelViewModel>(await _service.GetByIdAsync(id));

            if (modelVM == null)
            {
                Response.StatusCode = 404;
                return HttpNotFound();
            }

            return View(modelVM);
        }
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var makes = _mapper.Map<IEnumerable<VehicleMakeViewModel>>(await _service.GetMakes());

            var makeList = new SelectList(makes, "Id", "Name");

            ViewBag.MakesList = makeList;
            
            Response.StatusCode = 200;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VehicleModelViewModel modelVM)
        {
            if (ModelState.IsValid)
            {
                var vehicleModel = _mapper.Map<VehicleModel>(modelVM);
                
                await Task.Run(() => _service.CreateAsync(vehicleModel));

                Response.StatusCode = 201;
                return RedirectToAction("Index");
            }
            else
            {
                Response.StatusCode = 400;
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");

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

            var makes = _mapper.Map<IEnumerable<VehicleMakeViewModel>>(await _service.GetMakes());
            var makeList = new SelectList(makes, "Id", "Name");

            ViewBag.MakesList = makeList;

            var makeVM = _mapper.Map<VehicleModelViewModel>(await _service.GetByIdAsync(id));

            Response.StatusCode = 200;
            return View(makeVM);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPost(VehicleModelViewModel modelVM, int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var modelToUpdate = _mapper.Map<VehicleModel>(modelVM);

            if (TryUpdateModel(modelToUpdate))
            {
                try
                {
                    await Task.Run(() => _service.UpdateAsync(modelToUpdate));

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
            if (id == null)
            {
                Response.StatusCode = 404;
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var makeVM = _mapper.Map<VehicleModelViewModel>(await _service.GetByIdAsync(id));

            Response.StatusCode = 200;
            return View(makeVM);
        }

        public async Task<ActionResult> Delete(int id)
        {
            try
            { 
                var vehicleModel = await _service.GetByIdAsync(id);
                await Task.Run(() => _service.DeleteAsync(vehicleModel));

                Response.StatusCode = 200;
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Unable to delete. Try again, and if the problem persists, see your system administrator.");

                Response.StatusCode = 400;
                return View();
            }
        }
    }
}