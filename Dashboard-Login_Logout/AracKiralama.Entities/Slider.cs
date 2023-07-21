using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracKiralama.Entities
{
    public class Slider:IEntity
    {
        public int Id { get; set; }
        [StringLength(150)]
        public String? Baslik { get; set; }
        [StringLength(500)]
        public String? Aciklama { get; set; }
        [StringLength(100)]
        public String? Resim { get; set; }
        [StringLength(100)]
        public String? Link { get; set; }



    }
}
