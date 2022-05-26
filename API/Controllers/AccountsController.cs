using API.DTOs;
using API.Services;
using AutoMapper;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly IMailService mailer;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly TokenService tokenService;
        private readonly IWebHostEnvironment host;

        public AccountsController(UserManager<AppUser> userManager, IMailService mailer, TokenService tokenService, IWebHostEnvironment host, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.userManager = userManager;
            this.mailer = mailer;
            this.tokenService = tokenService;
            this.host = host;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await userManager.FindByNameAsync(loginDTO.Username);
            if (user == null || !await userManager.CheckPasswordAsync(user, loginDTO.Password))
                return Unauthorized();

            if (!user.EmailConfirmed) return BadRequest("Not verified");


            var userBasket = await unitOfWork.BasketRepository.GetByOwnerAsync(user.Id);
            var basketToReturn = mapper.Map<BasketDTO>(userBasket);

            return new UserDTO
            {
                Email = user.Email,
                Username = user.UserName,
                ProfilePic = user.ImagePath,
                Token = await tokenService.GenerateToken(user),
                Role = await userManager.GetRolesAsync(user),
                Basket = basketToReturn
            };
        }
        [Authorize]
        [HttpPost("basket/add/{id}")]
        public async Task<IActionResult> Follow(int id)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var ad = await unitOfWork.AdRepository.GetByIdAsync(id);
            var basket = await unitOfWork.BasketRepository.GetByOwnerAsync(user.Id);

            basket.AddItem(ad);

            await unitOfWork.CompleteAsync();
            return Ok();
        }
        [Authorize]
        [HttpPost("basket/remove/{id}")]
        public async Task<IActionResult> Unollow(int id)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var basket = await unitOfWork.BasketRepository.GetByOwnerAsync(user.Id);

            basket.RemoveItem(id);

            await unitOfWork.CompleteAsync();
            return Ok();
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromForm] RegisterDTO registerDTO)
        {
            var user = new AppUser { UserName = registerDTO.Username, Email = registerDTO.Email };
            if (registerDTO.Image == null) user.ImagePath = "defaultUser.png";
            else user.ImagePath = await UploadImage(registerDTO.Image);
            var result = await userManager.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded)
            {

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return ValidationProblem();
            }

            await unitOfWork.BasketRepository.AddAsync(new Basket { OwnerId = user.Id });
            await userManager.AddToRoleAsync(user, "Member");
            var token =await userManager.GenerateEmailConfirmationTokenAsync(user);
            await unitOfWork.CompleteAsync();

            mailer.SendVerificationMail(user.Id, token);

            return StatusCode(201);
        }

        [HttpPost("verify")]
        public async Task<ActionResult> Verify([FromQuery] string id, [FromQuery] string token)
        {
            var user = await userManager.FindByIdAsync(id);

            if (user == null) return BadRequest();

            var result = await userManager.ConfirmEmailAsync(user, token);

            if(result.Succeeded) return Ok();

            return BadRequest();
        }

        [HttpPut("edit")]
        public async Task<ActionResult> Edit([FromForm] RegisterDTO registerDTO)
        {
            //var checkUsername = userManager.username
            var user = await userManager.FindByNameAsync(User.Identity.Name);

            var checkMail = await userManager.FindByEmailAsync(registerDTO.Email);
            if (checkMail != null)
                return BadRequest();

            if(registerDTO.Image != null)
            {
                if(user.ImagePath != "defaultUser.png")
                {
                    var imagePath = Path.Combine(host.ContentRootPath, "wwwroot", "Images", user.ImagePath);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
                user.ImagePath = await UploadImage(registerDTO.Image);
            }

            userManager.PasswordHasher.HashPassword(user, registerDTO.Password);
            user.UserName = user.UserName;
            user.Email = user.Email;

            await userManager.UpdateAsync(user);

            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult> Unregister()
        {
            return Ok();
        }
        [NonAction]
        public async Task<string> UploadImage(IFormFile file)
        {
            string imageName = new string(Path.GetFileNameWithoutExtension(file.FileName).Take(10).ToArray()).Replace(' ', '.');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(file.FileName);
            var imagePath = Path.Combine(host.ContentRootPath, "wwwroot", "Images", imageName);
            using (var fs = new FileStream(imagePath, FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }
            return imageName;
        }

        [HttpPost("mail")]
        public IActionResult SendMail()
        {
            mailer.SendMail();
            return Ok();
        }
    }
}
