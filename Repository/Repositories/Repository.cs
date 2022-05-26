using Data;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext context;

        public Repository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var list = await context.Set<T>().ToListAsync();
            return list;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var element = await context.Set<T>().FindAsync(id);
            return element;
        }

        public async Task RemoveAsync(int id)
        {
            var el = await context.Set<T>().FindAsync(id);
            context.Set<T>().Remove(el);
        }
    }
}
