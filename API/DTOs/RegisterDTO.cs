namespace API.DTOs
{
    public class RegisterDTO : LoginDTO
    {
        public string Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PasswordHint { get; set; }
        public IFormFile? Image { get; set; }
    }
}
