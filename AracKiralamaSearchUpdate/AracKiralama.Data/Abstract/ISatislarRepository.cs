using AracKiralama.Entities;
using AracKiralama.Entities.Concrete.SatisFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AracKiralama.Data.Abstract
{
    public interface ISatislarRepository :IRepository<Satis>
    {
        Task<List<SatisDetay>> GetCustomListSatisByMusteriId(int? id);
    }
}
