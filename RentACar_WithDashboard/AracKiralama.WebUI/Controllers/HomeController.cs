﻿using AracKiralama.Entities;
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
            var model = new HomePageViewModel()
            {
                Sliders = await _service.GetAllAsync(),
                Araclar = await _serviceArac.GetCustomCarList(a=> a.Anasayfa)
            };
                

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}