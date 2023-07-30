using AracKiralama.Entities;

namespace AracKiralama.WebUI.Areas.Admin.Models
{
    public class HomePageViewModel
    {
        public  List<Slider> Sliders { get; set; }
        public List<Arac> Araclar { get; set; }
    }
}
