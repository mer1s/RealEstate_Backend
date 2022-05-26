namespace API.DTOs
{
    public class BasketDTO
    {
        public List<BasketItemDTO> Items { get; set; }
    }
    public class BasketItemDTO
    {
        public BasketAdDTO Ad { get; set; }
    }
}
