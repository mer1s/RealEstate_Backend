using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string Path { get; set; }
        [ForeignKey("Ad")]
        public int AdId { get; set; }
    }
}
