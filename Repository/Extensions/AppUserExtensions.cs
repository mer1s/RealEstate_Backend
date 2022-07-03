using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class AppUserExtensions
    {
        public static IQueryable<AppUser> SearchByUsename(this IQueryable<AppUser> query, string term)
        {
            if(term != string.Empty && !string.IsNullOrEmpty(term)) 
                return query.Where(n => n.UserName.ToLower().Contains(term.Trim(' ').ToLower()));
            return query;
        }
    }
}
