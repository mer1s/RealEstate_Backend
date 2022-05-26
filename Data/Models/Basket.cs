using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Basket
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
        
        public void AddItem(Ad ad)
        {
            if(Items.All(item => item.AdId != ad.Id))
            {
                Items.Add(new BasketItem
                {
                    Ad = ad
                });
            }
        }
        public void RemoveItem(int adId)
        {
            var item = Items.FirstOrDefault(item => item.AdId == adId);
            if (item == null) return;
            Items.Remove(item);

        }
    }
    public class BasketItem
    {
        public int Id { get; set; }
        public int AdId { get; set; }
        public Ad Ad { get; set; }
        public int BasketId { get; set; }
        public Basket Basket { get; set; }
    }
}
