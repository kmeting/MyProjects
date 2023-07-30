using AracKiralama.Entities;
using AracKiralama.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace AracKiralama.WebUI.Areas.Admin.Controllers
{
    public class AracController : Controller
    {
        private readonly IcarService _serviceArac;

        public AracController(IcarService serviceArac)
        {
            _serviceArac = serviceArac;
        }

        public async Task<IActionResult> IndexAsync(int id)
        {
            var model = await _serviceArac.GetCustomCar(id);
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
