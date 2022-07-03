using API.DTOs;
using API.Services;
using AutoMapper;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
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

        //[Authorize]
        [HttpGet("all-users/term")]
        public async Task<IActionResult> AllUsers([FromQuery] string? term, string thisId)
        {
            var list = await userManager.Users.Where(n => n.Id != thisId).SearchByUsename(term).ToListAsync();
            return Ok(list);
        }

        [Authorize]
        [HttpDelete("all-users/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                mailer.AccountDeletedNotify(user.Email);
                return Ok("Ok je sve");
            }
            return BadRequest();
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await userManager.FindByNameAsync(loginDTO.Username);
            if (user == null || !await userManager.CheckPasswordAsync(user, loginDTO.Password))
                return BadRequest("Pogrešna šifra ili korisničko ime...");

            if (!user.EmailConfirmed) return BadRequest("Nalog nije verifikovan...");


            var userBasket = await unitOfWork.BasketRepository.GetByOwnerAsync(user.Id);
            var basketToReturn = mapper.Map<BasketDTO>(userBasket);

            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
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
            var check = await userManager.FindByEmailAsync(registerDTO.Email);
            if (check != null) return BadRequest("E-mail adresa je zauzeta");

            var checkU = await userManager.FindByNameAsync(registerDTO.Username);
            if (checkU != null) return BadRequest("Korisničko ime je zauzeto");

            var user = new AppUser 
            { 
                UserName = registerDTO.Username, 
                Email = registerDTO.Email,
                PasswordHint = registerDTO.PasswordHint,
                LastName = registerDTO.LastName,
                FirstName = registerDTO.FirstName
            };

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

            mailer.SendVerificationMail(user.Email,user.Id, token);

            return StatusCode(201);
        }

        [HttpPost("verify")]
        public async Task<ActionResult> Verify(VerificationDTO verification)
        {
            var user = await userManager.FindByIdAsync(verification.Id);

            if (user == null) return NotFound();

            var result = await userManager.ConfirmEmailAsync(user, verification.Token);
            if (!result.Succeeded)
            {
                //return Unauthorized();
            }
            return Ok("tjt");
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword([FromBody]ResetPass r)
        {

            var user = await userManager.FindByEmailAsync(r.Mail);

            if (user == null) return BadRequest("Nepostojeca e-mail adresa");

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var final = new String(stringChars);

            final += "1@Re";

            var token = await userManager.GeneratePasswordResetTokenAsync(user); 
            await userManager.ResetPasswordAsync(user, token, final);
            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded) { 
                mailer.SendNewPassword(user.Email, final);
                return Ok();
            }

            return BadRequest();
        }

        [Authorize]
        [HttpPost("contact-user")]
        public async Task<ActionResult> Contact([FromBody] EmailContact e)
        {
            var initiator = await userManager.FindByNameAsync(User.Identity.Name);
            var reciever = await userManager.FindByIdAsync(e.Id);

            mailer.ContactUser(e.Subject, e.Content, reciever, initiator);

            return Ok("Sent!");
        }

        [Authorize]
        [HttpPut("edit-username")]
        public async Task<ActionResult> Edit(string newUsername)
        {
            var check = await userManager.FindByNameAsync(newUsername);
            if (check != null) return BadRequest("Potvrdjeno korisnicko ime je vec zauzeto");
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            user.UserName = newUsername;
            var result = await userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                var token = await tokenService.GenerateToken(user);
                return Ok(token);
            }
            else return BadRequest();
        }

        [Authorize]
        [HttpPut("edit-password")]
        public async Task<ActionResult> EditPassword(string newPass, string hint)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            await userManager.ResetPasswordAsync(user, token, newPass);
            user.PasswordHint = hint;

            var result = await userManager.UpdateAsync(user);


            if (result.Succeeded)
            {
                var newtoken = await tokenService.GenerateToken(user);
                return Ok(newtoken);
            }
            else return BadRequest();
        }

        [Authorize]
        [HttpPut("edit-name")]
        public async Task<ActionResult> EditName(string first, string last)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);

            user.FirstName = first;
            user.LastName = last;

            //await userManager.ResetPasswordAsync(user, token, newPass);

            var result = await userManager.UpdateAsync(user);


            if (result.Succeeded)
            {
                var newtoken = await tokenService.GenerateToken(user);
                return Ok(newtoken);
            }
            else return BadRequest("Neuspesno");
        }

        [Authorize]
        [HttpPut("remove-pic")]
        public async Task<ActionResult> RemovePic()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);

            user.ImagePath = "defaultUser.png";

            //await userManager.ResetPasswordAsync(user, token, newPass);

            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded) return Ok();
            else return BadRequest("Neuspesno");
        }

        [Authorize]
        [HttpPut("change-pic")]
        public async Task<ActionResult> ChangePic([FromForm] ChangePicDTO c)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);

            var imagePath = await UploadImage(c.Image);

            user.ImagePath = imagePath;

            //await userManager.ResetPasswordAsync(user, token, newPass);

            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded) return Ok(imagePath);
            else return BadRequest("Neuspesno");
        }

        [HttpPost("report/{id}")]
        public async Task<ActionResult> Report(string id, ReportInfo r)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            mailer.SendReport(user.UserName, user.Email, r);

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
    }
}
