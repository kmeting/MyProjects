using AracKiralama.Entities;
using AracKiralama.Services.Abstract;
using AracKiralama.WebUI.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AracKiralama.WebUI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class UsersController : Controller
    {
        private readonly IService<Kullanici> _service;
        private readonly IService<Rol> _rolService;
        

        public UsersController(IService<Kullanici> service, IService<Rol> rolService = null)
        {
            _service = service;
            _rolService = rolService;
        }

        // GET: UsersController
        public async Task<ActionResult> IndexAsync()
        {
            var roller = _rolService.GetAll();
            var model = await _service.GetAllAsync();
            var model2 = model.Select(k => new Kullanici
            {
                Id = k.Id,
                Adi = k.Adi,
                Soyadi = k.Soyadi,
                Email = k.Email,
                Telefon = k.Telefon,
                Adres= k.Adres,
                Tc=k.Tc,
                Ehliyet= k.Ehliyet,
                KullaniciAdi = k.KullaniciAdi,
                Sifre = k.Sifre,
                AktifMi = k.AktifMi,
                EklenmeTarihi = k.EklenmeTarihi,
                Rol = roller.FirstOrDefault(r => r.Id == k.RolId)
            }).ToList();

            return View(model2);
        }

        // GET: UsersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsersController/Create
        public ActionResult Create()
        {



            ViewData["Roller"] = _rolService
        .GetAll()
        .Select(c => new SelectListItem() { Text = c.Adi, Value = c.Id.ToString() })
        .ToList();

            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Kullanici kullanici, IFormFile? ehliyet)
        {
            

            if (ModelState.IsValid)
            {
                try
                {
                    kullanici.Ehliyet = await FileHelper.FileLoaderAsync(ehliyet, "/Pdf/");
                    await _service.AddAsync(kullanici);
                    await _service.saveAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
                ViewBag.rolId = new SelectList(await _rolService.GetAllAsync(), "Id", "Adi");
                return View(kullanici);
            }

            
            return View(kullanici);
        }

        // GET: UsersController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var model = await _service.FindAsync(id);
            ViewData["Roller"] = _rolService;
            ViewBag.rolId = new SelectList(await _rolService.GetAllAsync(), "Id", "Adi");

            return View(model);
        }
      
        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Kullanici kullanici, IFormFile? ehliyet)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    kullanici.Ehliyet = await FileHelper.FileLoaderAsync(ehliyet, "/Pdf/");
                     _service.Update(kullanici);
                     _service.save();
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
                ViewBag.rolId = new SelectList(await _rolService.GetAllAsync(), "Id", "Adi");
                return View(kullanici);
            }


            return View(kullanici);
        }

        // GET: UsersController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var model = await _service.FindAsync(id);
            return View(model);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Kullanici kullanici)
        {
            try
            {
                _service.Delete(kullanici);
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
