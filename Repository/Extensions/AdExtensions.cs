using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class AdExtensions
    {
        public static IQueryable<Ad> Paginate(this IQueryable<Ad> query, int page)
        {
            if (page > 0)
                return query.Skip((page - 1) * 15).Take(15);
            return query.Take(15);
        }

        public static IQueryable<Ad> Order(this IQueryable<Ad> query, string order)
        {
            if (string.IsNullOrWhiteSpace(order)) return query;

            query = order switch
            {
                "last" => query.OrderByDescending(p => p.Created),
                "pASC" => query.OrderBy(p => p.Price),
                "pDESC" => query.OrderByDescending(p => p.Price),
                "sASC" => query.OrderBy(p => p.Size),
                "sDESC" => query.OrderByDescending(p => p.Size),
            };
            return query;
        }

        public static IQueryable<Ad> InPriceRange(this IQueryable<Ad> query, int min, int max)
        {
            if (min >= max) return query.Where(n => n.Price >= min);

            return query.Where(p => p.Price >= min & p.Price <= max);
        }

        public static IQueryable<Ad> TypeAndLoc(this IQueryable<Ad> query, string locations, string types)
        {
            var locList = new List<string>();
            var typeList = new List<string>();

            if (!string.IsNullOrWhiteSpace(locations))
                locList.AddRange(locations.ToLower().Split(",").ToList());

            if (!string.IsNullOrWhiteSpace(types))
                typeList.AddRange(types.ToLower().Split(",").ToList());

            query = query.Where(p => locList.Count() == 0 || locList.Contains(p.Location.ToLower()));
            query = query.Where(p => typeList.Count() == 0 || typeList.Contains(p.Type.ToLower()));

            return query;
        }
    }
}
