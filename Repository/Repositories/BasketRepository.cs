using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class BasketRepository : Repository<Basket>, IBasketRepository
    {
        private readonly AppDbContext context;

        public BasketRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Basket> GetByOwnerAsync(string id)
        {
            var basket = await context.Baskets
                .Include(i => i.Items)
                .ThenInclude(i => i.Ad)
                .FirstOrDefaultAsync(b => b.OwnerId == id);

            return basket;
        }
    }
}
