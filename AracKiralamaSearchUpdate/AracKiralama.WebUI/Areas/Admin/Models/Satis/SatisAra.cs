using AracKiralama.Entities.Concrete.SatisFile;

namespace AracKiralama.WebUI.Areas.Admin.Models.Satis
{
    public class SatisAra
    {
        public string AdSoyad { get;  set; }
        public List<SatisDetay> KiralikBilgi { get; set; }
        public string Tc { get; internal set; }
    }
}
