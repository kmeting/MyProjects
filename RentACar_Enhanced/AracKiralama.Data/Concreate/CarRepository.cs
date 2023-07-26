using AracKiralama.Data.Abstract;
using AracKiralama.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AracKiralama.Data.Concreate
{
    public  class CarRepository : Repository<Arac>, ICarRepository
    {
        public CarRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<Arac> GetCustomCar(int id)
        {
            return await _dbset.AsNoTracking().Include(x => x.Marka).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Arac>> GetCustomCarList()
        {
            return await _dbset.AsNoTracking().Include(x=> x.Marka).ToListAsync();
        }

        public async Task<List<Arac>> GetCustomCarList(Expression<Func<Arac, bool>> expression)
        {
            return await _dbset.Where(expression).AsNoTracking().Include(x => x.Marka).ToListAsync();
        }
    }
}
