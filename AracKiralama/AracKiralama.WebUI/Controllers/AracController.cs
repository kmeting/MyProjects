using AracKiralama.Entities;
using AracKiralama.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace AracKiralama.WebUI.Controllers
{
    public class AracController : Controller
    {
        private readonly IcarService _serviceArac;
        private readonly ISatisService _satisService;
        private readonly IService<Musteri> _musteriService;

        public AracController(IcarService serviceArac, ISatisService satisService, IService<Musteri> musteriService)
        {
            _serviceArac = serviceArac;
            _satisService = satisService;
            _musteriService = musteriService;
        }

        public async Task<IActionResult> IndexAsync(int id)
        {
            var model = await _serviceArac.GetCustomCar(id);

            var kiralamalar = await _satisService.GetCustomListSatisByAracId(id);


            //var eskiMusteriler = _musteriService.GetAll(m => m.AracId == id);
            
            ViewBag.EskiMusteriler = kiralamalar;



            return View(model);
        }
       [Route("tum-araclarimiz")]
        public async Task<IActionResult> List()
        {
            var model = await _serviceArac.GetCustomCarList(c=>c.KiralikMi);
            return View(model);
        }

        public async Task<IActionResult> Ara(string q)
        {
            var model = await _serviceArac.GetCustomCarList(c => c.KiralikMi && c.Marka.Adi.Contains(q) || c.KasaTipi.Contains(q) || c.Modeli.Contains(q));
            return View(model);
        }
    }
}
