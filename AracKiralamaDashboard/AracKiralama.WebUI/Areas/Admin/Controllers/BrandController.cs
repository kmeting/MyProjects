using AracKiralama.Entities;
using AracKiralama.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AracKiralama.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly IService< Marka> _service;

        public BrandController(IService<Marka> service)
        {
            _service = service;
        }

        // GET: BrandController
        public async Task<ActionResult> IndexAsync()
        {
            var model = await _service.GetAllAsync();
            return View(model);
        }

        // GET: BrandController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BrandController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BrandController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Marka marka)
        {
            try
            {
                await _service.AddAsync(marka);
                await _service.saveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu");
            }
            return View(marka);
        }

        // GET: BrandController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: BrandController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Marka marka)
        {
            try
            {
                _service.Update(marka);
                await _service.saveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu");
            }
            return View(marka);
        }

        // GET: BrandController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {

            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: BrandController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Marka marka)
        {
            try
            {
               _service.Delete(marka);
                _service.save();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
            
        }
    }
}
