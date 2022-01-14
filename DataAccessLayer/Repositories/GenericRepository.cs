using DataAccessLayer.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserrrrrSon.Models.Context;

namespace DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericDAL<T> where T : class
    {
        public void Delete(T t)
        {
            using var c = new AppDbContext();
            c.Remove(t);
            c.SaveChanges();
        }

        public T GetByID(int id)
        {
            using var c = new AppDbContext();
            return c.Set<T>().Find(id);
        }

        public async Task<List<T>> GetListAll()
        {
            using var c = new AppDbContext();
            return await c.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetListAll(Expression<Func<T, bool>> filter)
        {
            using var c = new AppDbContext();
            return await c.Set<T>().Where(filter).ToListAsync();
        }

        public T Insert(T t)
        {
            using var c = new AppDbContext();
            c.Add(t);
            c.SaveChanges();
            return t;
        }

        public T Update(T t)
        {
            using var c = new AppDbContext();
            c.Update(t);
            c.SaveChanges();
            return t;
        }
    }
}
