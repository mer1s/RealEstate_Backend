using Data;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class AdRepository : Repository<Ad>, IAdRepository
    {
        private readonly AppDbContext context;

        public AdRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Ad> GetAdWithImages(int id)
        {
            var ad = await context.Ads.Include(n => n.Images).SingleOrDefaultAsync(n => n.Id == id);
            return ad;
        }

        public async Task<IEnumerable<Ad>> GetAllSortedAsync(Searcher s)
        {
            var list = await context.Ads
                                    .Order(s.Order)
                                    .InPriceRange(s.MinPrice, s.MaxPrice)
                                    .TypeAndLoc(s.Locations, s.Types)
                                    .Paginate(s.PageNum)
                                    .ToListAsync();
            return list;
        }
        public async Task<int> GetSortedCount(Searcher s)
        {
            var count = await context.Ads
                                    .Order(s.Order)
                                    .InPriceRange(s.MinPrice, s.MaxPrice)
                                    .TypeAndLoc(s.Locations, s.Types)
                                    .CountAsync();
            return count;
        }

        public async Task<IEnumerable<Ad>> GetNewest()
        {
            var list = await context.Ads
                                    .Skip(Math.Max(0, context.Ads.Count() - 4)).Take(4)
                                    .OrderByDescending(n => n.Created)
                                    .ToListAsync();

            return list;
        }

        public async Task<IEnumerable<Ad>> GetByOwner(string id)
        {
            var list = await context.Ads
                                    .Where(n => n.AppUserId == id).ToListAsync();

            return list;
        }

        public async Task<IEnumerable<Ad>> GetSameType(string type)
        {
            var list = await context.Ads
                .Where(n => n.Type == type)
                .Take(4)
                .ToListAsync();

            return list;
        }

        public async Task<IEnumerable<Ad>> GetSameOwner(string id)
        {
            var list = await context.Ads
                .Where(n => n.AppUserId == id)
                .ToListAsync();

            return list;
        }
    }
}
