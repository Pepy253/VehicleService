using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Vehicle.Service.Interfaces;
using Vehicle.Service.Models;

namespace Vehicle.MVC.Controllers
{
    public class MakeController : Controller
    {
        private readonly IVehicleMakeService _service;

        public MakeController(IVehicleMakeService service)
        {
            _service = service;
            
        }
        // GET: Make
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

            var vehicleMake = await _service.GetByIdAsync(id);

            if (vehicleMake == null)
            {
                return HttpNotFound();
            }

            return View(vehicleMake);
        }

        public ActionResult Create()
        { 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VehicleMake vehicleMake)
        {
            if(ModelState.IsValid)
            {
                await Task.Run(() => _service.CreateAsync(vehicleMake));

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

            var makeToUpdate = await _service.GetByIdAsync(id);

            if (TryUpdateModel(makeToUpdate))
            {
                try
                {
                    await Task.Run(() => _service.UpdateAsync(makeToUpdate));

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
            return View(await _service.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id, VehicleMake vehicleMake)
        {
            try
            {
                vehicleMake = await _service.GetByIdAsync(id);
                await Task.Run(() => _service.DeleteAsync(vehicleMake));

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}