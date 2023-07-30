using AracKiralama.Entities;
using AracKiralama.Services.Abstract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AracKiralama.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly IService<Kullanici> _service;

        public LoginController(IService<Kullanici> service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        
        public async  Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect("Admin/Login");
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(string email, string password)
        {

            try
            {
                var account = _service.Get(k=>k.Email == email && k.Sifre == password);

                if (account == null)
                {
                    TempData["mesaj"] = "Giriş Başarısız";
                }
                else
                {
                    var claims = new List<Claim>()
                    {
                      new Claim(ClaimTypes.Name, account.Adi),
                      new Claim("Role", "Admin")
                    };

                   
                    var userIdentity= new ClaimsIdentity(claims,"Login");
                    ClaimsPrincipal principal= new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(principal);
                    return RedirectToAction("Index","Main");
                }
            }
            catch (Exception)
            {

                TempData["mesaj"] = "Hata Oluştu";
            }
            return View();
        }
    }
}
