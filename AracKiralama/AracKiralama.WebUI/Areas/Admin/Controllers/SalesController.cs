using AracKiralama.Entities;
using AracKiralama.Services.Abstract;
using AracKiralama.Services.Concreate;
using AracKiralama.WebUI.Areas.Admin.Models.Satis;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AracKiralama.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class SalesController : Controller
    {
        private readonly IService<Satis> _Service;
        private readonly IService<Arac> _ServiceArac;
        private readonly IService<Musteri> _ServiceMusteri;
        private readonly IService<Marka> _ServiceMarka;
        private readonly ISatisService _satisService;




        public SalesController(IService<Satis> service, IService<Arac> serviceArac, IService<Musteri> serviceMusteri, IService<Marka> serviceMarka, ISatisService satisService)
        {
            _Service = service;
            _ServiceArac = serviceArac;
            _ServiceMusteri = serviceMusteri;
            _ServiceMarka = serviceMarka;
            _satisService = satisService;
        }



        // GET: SalesController
        public async Task<ActionResult> IndexAsync()
        {
            ViewData["Markalar"] = _ServiceMarka
      .GetAll()
      .Select(c => new SelectListItem() { Text = c.Adi, Value = c.Id.ToString() })
      .ToList();
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
        public async Task<IActionResult> Ara(String q)
        {
            var arananMusteri = _ServiceMusteri.GetAll().FirstOrDefault(m => m.TcNo == q);
            var kiralikBilgi = await _satisService.GetCustomListSatisByMusteriId(arananMusteri?.Id);

            if (arananMusteri != null)
            {
                if (kiralikBilgi.Count==0) 
                {
                    return RedirectToAction("Index"); 
                }

                var model = new SatisAra
                {
                    AdSoyad = $"{arananMusteri.Adi} {arananMusteri.Soyadi}",
                    Tc = $"{arananMusteri.TcNo}",
                    KiralikBilgi = kiralikBilgi,
                };

                return View(model);
            }
            else
            {
                TempData["TcNo"] = q;
                return View("popup");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Kaydet(Musteri model)
        {
            if (ModelState.IsValid)
            {
                await _ServiceMusteri.AddAsync(model);
                await _Service.saveAsync();

                return RedirectToAction("Index");
            }
            return View("YeniMusteriPopup", model);
        }
        // GET: SalesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SalesController/Create
        public async Task<ActionResult> CreateAsync()
        {
            ViewBag.AracId = new SelectList(await _ServiceArac.GetAllAsync(), "Id", "Modeli");
            ViewBag.MusteriId = new SelectList(await _ServiceMusteri.GetAllAsync(), "Id", "Adi");
            return View();
        }

        // POST: SalesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Satis satis)
        {
            var kiralikArac = _Service.Get(a => a.AracId == satis.AracId);

            if(kiralikArac.İadeTarihi > satis.KiralamaTarihi)
            {
                ModelState.AddModelError("AracDolu", $"Bu arac {kiralikArac.İadeTarihi} tarihine kadar doludur.");
                return View();
            }

            if (ModelState.IsValid)
            {


                try
                {
                    var arac = await _ServiceArac.FindAsync(satis.AracId);
                    TimeSpan ts = satis.İadeTarihi - satis.KiralamaTarihi;
                    decimal fiyat = arac.Fiyati * (int)ts.TotalDays;
                    satis.KiralamaFiyati = fiyat;

                    if (arac == null)
                    {
                        ModelState.AddModelError("", "Geçersiz araç seçimi!");
                        ViewData["AracId"] = _ServiceArac
                            .GetAll()
                            .Select(c => new SelectListItem() { Text = c.Modeli, Value = c.Id.ToString() })
                            .ToList();

                        ViewData["MusteriId"] = _ServiceMusteri
                            .GetAll()
                            .Select(c => new SelectListItem() { Text = c.Adi, Value = c.Id.ToString() })
                            .ToList();

                        return View();
                    }

                    await _Service.AddAsync(satis);
                    await _Service.saveAsync();

                    return RedirectToAction("Index");



                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu");
                }
            }

            //ViewData["AracId"] = _ServiceArac
            //    .GetAll()
            //    .Select(c => new SelectListItem() { Text = c.Modeli, Value = c.Id.ToString() })
            //    .ToList();

            //ViewData["MusteriId"] = _ServiceMusteri
            //    .GetAll()
            //    .Select(c => new SelectListItem() { Text = c.Adi, Value = c.Id.ToString() })
            //    .ToList();

            return View();
        }




        // GET: SalesController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            ViewBag.AracId = new SelectList(await _ServiceArac.GetAllAsync(), "Id", "Modeli");
            ViewBag.MusteriId = new SelectList(await _ServiceMusteri.GetAllAsync(), "Id", "Adi");
            var model = await _Service.FindAsync(id);
            return View(model);
        }

        // POST: SalesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Satis satis)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _Service.Update(satis);
                    _Service.save();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
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
