using AracKiralama.Data;
using AracKiralama.Entities;
using AracKiralama.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracKiralama.Services.Concreate
{
    public class MusteriService : Service<Musteri>, IMusteriService
    {
        public MusteriService(DatabaseContext context) : base(context)
        {
        }
    }
}
