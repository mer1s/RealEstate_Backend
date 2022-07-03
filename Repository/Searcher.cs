using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class Searcher
    {
        public string? Order { get; set; }
        public string? Types { get; set; }
        public string? Locations { get; set; }
        public int PageNum { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
    }
    public class TypeSearcher
    {
        public string Type { get; set; }
        public int ThisId { get; set; }
    }
}
