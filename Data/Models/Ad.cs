using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Ad
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime Created { get; set; }
        public string Type { get; set; }
        public int ParkingSize { get; set; }
        public string PropState { get; set; }
        public int Rooms { get; set; }
        public int Bathrooms { get; set; }
        public string StandardEquipment { get; set; }
        public string TechEquipment { get; set; }
        public string SecurityEquipment { get; set; }
        public string Description { get; set; }
        public int Size { get; set; }
        public int Price { get; set; }
        public string TitlePath { get; set; }
        public List<Image> Images { get; set; } = new();

        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
    }
}
