using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AracKiralama.Data.Abstract;
using AracKiralama.Entities;

namespace AracKiralama.Services.Abstract
{
    public interface IService<T> : IRepository<T> where T : class, IEntity, new()
    {
        
    }
}
