using AracKiralama.Data;
using AracKiralama.Data.Abstract;
using AracKiralama.Data.Concreate;
using AracKiralama.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AracKiralama.Services.Concreate
{
    public class CarService : CarRepository, IcarService 
    {
        public CarService(DatabaseContext context) : base(context)
        {

        }
    }
}
