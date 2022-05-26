using Data;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;

        public IAdRepository AdRepository { get; set; }
        public IBasketRepository BasketRepository { get; set; }

        public UnitOfWork(AppDbContext context, IAdRepository adRepository, IBasketRepository basketRepository)
        {
            this.context = context;
            AdRepository = adRepository;
            BasketRepository = basketRepository;
        }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
