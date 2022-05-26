namespace API.DTOs
{
    public class RegisterDTO : LoginDTO
    {
        public string Email { get; set; }
        public IFormFile? Image { get; set; }
    }
}
