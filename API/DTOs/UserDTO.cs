﻿namespace API.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string ProfilePic { get; set; }
        public ICollection<string> Role { get; set; }
        public BasketDTO Basket { get; set; }
    }
}
