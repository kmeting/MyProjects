using AracKiralama.Entities;
using AracKiralama.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace AracKiralama.WebUI.Areas.User.Controllers
{
    [Area("User")]

    public class MainController : Controller
    {
        private readonly IService<Arac> _serviceArac;

        public MainController(IService<Arac> serviceArac)
        {
            _serviceArac = serviceArac;
        }

        [Route("/user")]
        public IActionResult Index()
        {
            var araclar = _serviceArac.GetAll();

            return View(araclar);
        }
    }
}
