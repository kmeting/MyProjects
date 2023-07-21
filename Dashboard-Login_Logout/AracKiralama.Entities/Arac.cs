using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace AracKiralama.Entities
{
    public class Arac : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Marka Adı"), Required(ErrorMessage = "{0} Boş Bırakılamaz!")]
        public int MarkaId { get; set; }
        [StringLength(50), Required(ErrorMessage = "{0} Boş Bırakılamaz!")]
        public string? Renk { get; set; }
        [Display(Name = "Fiyatı")]
        public decimal Fiyati { get; set; }
        [StringLength(50), Required(ErrorMessage = "{0} Boş Bırakılamaz!")]
        public string? Modeli { get; set; }
        [StringLength(50), Required(ErrorMessage = "{0} Boş Bırakılamaz!")]
        [Display(Name = "Kasa Tipi")]
        public string? KasaTipi { get; set; }
        [Display(Name = "Model Yılı")]
        public int ModelYili { get; set; }
        [Display(Name = "Kiralık Mı")]
        public bool KiralikMi { get; set; }
        [Required(ErrorMessage = "{0} Boş Bırakılamaz!")]
        public string? Notlar { get; set; }
        public virtual Marka? Marka { get; set; }
        [StringLength(100)]
        public String? Resim1 { get; set; }
        [StringLength(100)]
        public String? Resim2 { get; set; } 
        [StringLength(100)]
        public String? Resim3 { get; set; }

        public static Arac FirstOrDefault(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}
