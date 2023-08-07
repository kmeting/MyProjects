using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracKiralama.Entities.Concrete.SatisFile
{
    public class KiralamaMusteriDTO
    {
        public int MusteriId { get; set; }
        public string? MusteriAdi { get; set; }
        public string? MusteriSoyadi { get; set; }
        public int SatisId { get; set; }
        public decimal KiralamaFiyati { get; set; }
        public DateTime KiralamaTarihi { get; set; }
        public DateTime IadeTarihi { get; set; }
    }
}
