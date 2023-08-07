using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracKiralama.Entities.Concrete.SatisFile
{
    public class SatisDetay
    {
        public int MusteriId { get; set; }
        public int SatisId { get; set; }
        public decimal KiralamaFiyati { get; set; }
        public DateTime KiralamaTarihi { get; set; }
        public DateTime IadeTarihi { get; set; }
      
    }
}
