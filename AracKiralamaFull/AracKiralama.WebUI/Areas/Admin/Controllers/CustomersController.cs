using AracKiralama.Entities;
using AracKiralama.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AracKiralama.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class CustomersController : Controller
    {
        private readonly IMusteriService _musteriService;
        private readonly IService<Arac> _serviceArac;
        private readonly IService<Musteri> _musteriServiceEntity;




        public CustomersController(IService<Arac> serviceArac, IMusteriService musteriService, IService<Musteri> musteriServiceEntity)
        {

            _serviceArac = serviceArac;
            _musteriService = musteriService;
            _musteriServiceEntity = musteriServiceEntity;
        }

        // GET: CustomersController
        public async Task<IActionResult> IndexAsync()
        {
            var model = await _musteriService.GetAllAsync();
            var arac = _serviceArac.GetAll();
            var model2 = model.Select(k => new Musteri
            {
                Id = k.Id,
                Adi = k.Adi,
                Soyadi = k.Soyadi,
                Email = k.Email,
                Telefon = k.Telefon,
                TcNo=k.TcNo,
                Adres=k.Adres,
                Notlar=k.Notlar,
                Arac= arac.FirstOrDefault(o => o.Id == k.Id),
            }).ToList();
            return View(model2);
        }

        // GET: CustomersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomersController/Create
        public ActionResult Create()
        {
            ViewData["AracId"] = _serviceArac
       .GetAll()
       .Select(c => new SelectListItem() { Text = c.Modeli, Value = c.Id.ToString() })
       .ToList();
            return View();
        }

        // POST: CustomersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Musteri musteri)
          {
            if (ModelState.IsValid)
            {
                try
                {
                    await _musteriService.AddAsync(musteri);
                    await _musteriService.saveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu");
                }
            }
            ViewData["AracId"] = _serviceArac
      .GetAll()
      .Select(c => new SelectListItem() { Text = c.Modeli, Value = c.Id.ToString() })
      .ToList();
            return View(musteri);
        }

        // GET: CustomersController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            ViewData["AracId"] = _serviceArac
       .GetAll()
       .Select(c => new SelectListItem() { Text = c.Modeli, Value = c.Id.ToString() })
       .ToList();
            var model = await _musteriServiceEntity.FindAsync(id);
            return View(model);
        }

        // POST: CustomersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Musteri musteri)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _musteriService.Update(musteri);
                    await _musteriService.saveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu");
                }
            }
            ViewData["AracId"] = _serviceArac
      .GetAll()
      .Select(c => new SelectListItem() { Text = c.Modeli, Value = c.Id.ToString() })
      .ToList();
            return View(musteri);
        }

        // GET: CustomersController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _musteriServiceEntity.FindAsync(id);
            return View(model);
        }

        // POST: CustomersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Musteri musteri)
        {
            try
            {
                _musteriService.Delete(musteri);
                _musteriService.save();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
