using AracKiralama.Entities;
using AracKiralama.Services.Abstract;
using AracKiralama.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AracKiralama.WebUI.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly IService<Slider> _service;
        private readonly IcarService _serviceArac;

        public HomeController(IService<Slider> service, IcarService serviceArac)
        {
            _service = service;
            _serviceArac = serviceArac;
        }

        public async Task<IActionResult> IndexAsync()
        {
            Slider model = new Slider();
            var araclar = await _serviceArac.GetAllAsync(a => a.Anasayfa);

            model?.Araclar?.AddRange(araclar);

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

       
    }
}