using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IBasketRepository : IRepository<Basket>
    {
        Task<Basket> GetByOwnerAsync(string id);
    }
}
