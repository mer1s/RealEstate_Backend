namespace API.DTOs
{
    public class SaveAdResource
    {
        public string Name { get; set; }
        public string Location { get; set; }
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
        public List<IFormFile>? Images { get; set; }
    }
}
