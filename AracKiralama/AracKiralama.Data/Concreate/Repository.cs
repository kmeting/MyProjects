using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AracKiralama.Data.Abstract;
using AracKiralama.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AracKiralama.Data.Concreate
{
    public class Repository<T> : IRepository<T> where T : class, IEntity, new()
    {
        internal DatabaseContext _context;
        internal DbSet<T> _dbset;

        public Repository(DatabaseContext context)
        {

            _context = context;
            _dbset= context.Set <T>();  
        }

        public void add(T entitiy)
        {
            _dbset.Add(entitiy);
        }

        public async Task AddAsync(T entitiy)
        {
            await _dbset.AddAsync(entitiy);
        }

        public void Delete(T entitiy)
        {
           _dbset.Remove(entitiy);
        }

        public async Task DeleteAsync(int id)
        {
            var deletedEntity = await _dbset.FirstOrDefaultAsync(x => x.Id == id);

            _dbset.Remove(deletedEntity);


        }

        public Task DeleteAsync(T entity)
        {
            _dbset.Remove(entity);

            return Task.CompletedTask;
        }

        public T Find(int id)
        {
            return _dbset.Find(id);
        }

        public async Task<T> FindAsync(int id)
        {
            return await _dbset.FindAsync(id);

        }

        public T Get(Expression<Func<T, bool>> expression)
        {
            return _dbset.FirstOrDefault(expression);
        }

        public List<T> GetAll()
        {
            return _dbset.ToList();
        }

        public List<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return _dbset.Where(expression).ToList();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbset.ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression)
        {
            var list = await _dbset.Where(expression).ToListAsync();

            return list;
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return _dbset.FirstOrDefaultAsync(expression);
        }

        public int save()
        {
            return _context.SaveChanges();
        }

        public async Task<int> saveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Update(T entitiy)
        {
           _context.Update(entitiy);
        }

        public void UpdateAsync(T entity)
        {
            _dbset.Update(entity);
        }
    }
}

