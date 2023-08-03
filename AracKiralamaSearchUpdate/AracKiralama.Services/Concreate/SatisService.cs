using AracKiralama.Data;
using AracKiralama.Data.Concreate;
using AracKiralama.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracKiralama.Services.Concreate
{
    public class SatisService : SatisRepository, ISatisService
    {
        public SatisService(DatabaseContext context) : base(context)
        {
        }
    }
}
