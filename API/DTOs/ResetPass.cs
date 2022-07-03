namespace API.DTOs
{
    public class ResetPass
    {
        public string Mail { get; set; }
    }
    public class EmailContact
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string Subject { get; set; }
    }
}
