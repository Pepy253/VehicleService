using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Vehicle.Service.Interfaces;
using Vehicle.Service.Models;

namespace Vehicle.MVC.Controllers
{
    public class ModelController: Controller
    {
        private readonly IVehicleModelService _service;

        public ModelController(IVehicleModelService service)
        {
            _service = service;

        }
        // GET: Make
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

            int pageNumber = (page ?? 1);
            int pageSize = 5;

            return View(await _service.GetFilterAndSort(sortOrder, searchString, pageNumber, pageSize));
        }

        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var vehicleModel = await _service.GetByIdAsync(id);

            if (vehicleModel == null)
            {
                return HttpNotFound();
            }

            return View(vehicleModel);
        }

        public async Task<ActionResult> Create()
        {
            var makes = await _service.GetMakes();

            var makeList = new SelectList(makes, "Id", "Name");

            ViewBag.MakesList = makeList;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VehicleModel vehicleModel)
        {
            if (ModelState.IsValid)
            {
                await Task.Run(() => _service.CreateAsync(vehicleModel));

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");

                return View();
            }
        }
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var makes = await _service.GetMakes();

            var makeList = new SelectList(makes, "Id", "Name");

            ViewBag.MakesList = makeList;

            return View(await _service.GetByIdAsync(id));
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var modelToUpdate = await _service.GetByIdAsync(id);

            if (TryUpdateModel(modelToUpdate))
            {
                try
                {
                    await Task.Run(() => _service.UpdateAsync(modelToUpdate));

                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }

            return View();
        }
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(await _service.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id, VehicleModel vehicleModel)
        {
            try
            {
                vehicleModel = await _service.GetByIdAsync(id);
                await Task.Run(() => _service.DeleteAsync(vehicleModel));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}