using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AracKiralama.Entities
{
    public class Satis : IEntity
    {
        public int Id { get; set; }
        [Display(Name = "Araç")]
        public int AracId { get; set; }
        [Display(Name = "Müşteri")]
        public int MusteriId { get; set; }
        [Display(Name = "Kiralama Fiyatı")]
        public decimal KiralamaFiyati { get; set; }
        [Display(Name = "Kiralama Tarihi")] 
        public DateTime KiralamaTarihi { get; set; }

        [Display(Name = "Araç İade Tarihi")]
        public DateTime İadeTarihi { get; set; }
        [Display(Name = "Araç")]
        public virtual Arac? Arac { get; set; }
        [Display(Name = "Müşteri")]
        public virtual Musteri? Musteri { get; set; }
    }
}
