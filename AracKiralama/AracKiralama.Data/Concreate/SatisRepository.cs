using AracKiralama.Data.Abstract;
using AracKiralama.Entities;
using AracKiralama.Entities.Concrete.SatisFile;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AracKiralama.Data.Concreate
{
    public class SatisRepository : Repository<Satis>, ISatislarRepository
    {
        public SatisRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<List<KiralamaMusteriDTO>> GetCustomListSatisByAracId(int? id)
        {
            using (var context = new DatabaseContext())
            {
                var result = (from satis in context.Satislar
                              join musteri in context.Musteriler on satis.MusteriId equals musteri.Id
                              where satis.AracId == id
                              select new KiralamaMusteriDTO
                              {
                                  MusteriId = musteri.Id,
                                  MusteriAdi = musteri.Adi,
                                  MusteriSoyadi = musteri.Soyadi,
                                  SatisId = satis.Id,
                                  KiralamaFiyati = satis.KiralamaFiyati,
                                  KiralamaTarihi = satis.KiralamaTarihi,
                                  IadeTarihi = satis.İadeTarihi
                              });
                return await result.ToListAsync();
            }
        }

        public async Task<List<SatisDetay>> GetCustomListSatisByMusteriId(int? id)
        {
            using (var context = new DatabaseContext())
            {
                var result = (from satis in context.Satislar
                              join musteri in context.Musteriler on satis.MusteriId equals musteri.Id
                              where satis.MusteriId == id
                              select new SatisDetay
                              {
                                  MusteriId= musteri.Id,
                                  SatisId= satis.Id,
                                  KiralamaFiyati= satis.KiralamaFiyati,
                                  KiralamaTarihi=satis.KiralamaTarihi,
                                  IadeTarihi=satis.İadeTarihi
                              });
                return await result.ToListAsync();
            }
        }
    }
}
