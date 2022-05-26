using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IAdRepository : IRepository<Ad>
    {   
        Task<Ad> GetAdWithImages(int id);
        Task<IEnumerable<Ad>> GetAllSortedAsync(Searcher s);
        Task<IEnumerable<Ad>> GetSameType(string type);
        Task<IEnumerable<Ad>> GetSameOwner(string id);
        Task<IEnumerable<Ad>> GetNewest();
        Task<IEnumerable<Ad>> GetByOwner(string id);
        Task<int> GetSortedCount(Searcher s);
    }
}
