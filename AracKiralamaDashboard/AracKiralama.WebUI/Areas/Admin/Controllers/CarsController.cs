using AracKiralama.Entities;
using AracKiralama.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AracKiralama.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarsController : Controller
    {
        private readonly IService<Arac> _service;
        private readonly IService<Marka> _serviceMArka;

        public CarsController(IService<Arac> service, IService<Marka> serviceMArka)
        {
            _service = service;
            _serviceMArka = serviceMArka;
        }

        // GET: CarsController
        public async Task<IActionResult> IndexAsync()
        {
            var model = await _service.GetAllAsync();
            return View(model);
        }

        // GET: CarsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CarsController/Create
        public ActionResult Create()
        {
            var markalar = _serviceMArka.GetAll();

            ViewData["Markalar"] = _serviceMArka
       .GetAll()
       .Select(c => new SelectListItem() { Text = c.Adi, Value = c.Id.ToString() })
       .ToList();
            return View();
        }

        // POST: CarsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Arac arac)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _service.AddAsync(arac);
                    await _service.saveAsync(); 
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu");
                }
               
            }
            ViewData["Markalar"] = _serviceMArka
       .GetAll()
       .Select(c => new SelectListItem() { Text = c.Adi, Value = c.Id.ToString() })
       .ToList();
            return View(arac);
        }

        // GET: CarsController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            ViewData["Markalar"] = _serviceMArka
       .GetAll()
       .Select(c => new SelectListItem() { Text = c.Adi, Value = c.Id.ToString() })
       .ToList();
            var model =await _service.FindAsync(id);
            return View(model);
        }

        // POST: CarsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Arac arac)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _service.Update(arac);
                    _service.save();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu");
                }

            }
            ViewData["Markalar"] = _serviceMArka
       .GetAll()
       .Select(c => new SelectListItem() { Text = c.Adi, Value = c.Id.ToString() })
       .ToList();
            return View(arac);
        }

        // GET: CarsController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: CarsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, Arac arac)
        {
            try
            {
                _service.Delete(arac);
                await _service.saveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
