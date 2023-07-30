using AracKiralama.Data;
using AracKiralama.Data.Abstract;
using AracKiralama.Data.Concreate;
using AracKiralama.Entities;
using AracKiralama.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AracKiralama.Services.Concreate
{
    public class Service<T> : Repository<T>, IService<T> where T : class, IEntity, new()
    {
        public Service(DatabaseContext context) : base(context)
        {
        }
    }
}
