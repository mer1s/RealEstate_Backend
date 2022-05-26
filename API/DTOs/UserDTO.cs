namespace API.DTOs
{
    public class UserDTO
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string ProfilePic { get; set; }
        public ICollection<string> Role { get; set; }
        public BasketDTO Basket { get; set; }
    }
}
