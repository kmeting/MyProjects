using AracKiralama.Entities;
using AracKiralama.Services.Abstract;
using AracKiralama.Services.Concreate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AracKiralama.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SalesController : Controller
    {
        private readonly IService<Satis> _Service;
        private readonly IService<Arac> _ServiceArac; 
        private readonly IService<Musteri> _ServiceMusteri;

        public SalesController(IService<Satis> service, IService<Arac> serviceArac, IService<Musteri> serviceMusteri)
        {
            _Service = service;
            _ServiceArac = serviceArac;
            _ServiceMusteri = serviceMusteri;
        }

        // GET: SalesController
        public async Task<ActionResult> IndexAsync()
        {
            var model = await _Service.GetAllAsync();
            var arac = _ServiceArac.GetAll();
            var musteri = _ServiceMusteri.GetAll();
            var model2 = model.Select(k => new Satis
            {
                Id = k.Id,
                MusteriId = k.MusteriId,
                AracId = k.AracId,
                KiralamaTarihi = k.KiralamaTarihi,
                KiralamaFiyati = k.KiralamaFiyati,
                //Musteri = k.Musteri,
                Arac = arac.FirstOrDefault(o => o.Id == k.AracId),
                Musteri = musteri.FirstOrDefault(y => y.Id == k.MusteriId)
            });
            return View(model);
        }

        // GET: SalesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SalesController/Create
        public async Task<ActionResult> CreateAsync()
        {
            ViewBag.AracId = new SelectList(await _ServiceArac.GetAllAsync(),"Id","Modeli");
            ViewBag.MusteriId = new SelectList(await _ServiceMusteri.GetAllAsync(),"Id","Adi");
            return View();
        }

        // POST: SalesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Satis satis)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _Service.AddAsync(satis);
                    await _Service.saveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu");
                }
            }
            ViewData["AracId"] = _ServiceArac
      .GetAll()
      .Select(c => new SelectListItem() { Text = c.Modeli, Value = c.Id.ToString() })
      .ToList();

            ViewData["MusteriId"] = _ServiceMusteri
      .GetAll()
      .Select(c => new SelectListItem() { Text = c.Adi, Value = c.Id.ToString() })
      .ToList();
            return View(satis);
        }

        // GET: SalesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SalesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: SalesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SalesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }
    }
}
