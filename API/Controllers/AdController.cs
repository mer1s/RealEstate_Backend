using API.DTOs;
using AutoMapper;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Interfaces;
using System.Security.Claims;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdsController : ControllerBase
    {
        private readonly IWebHostEnvironment host;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<AppUser> userManager;

        public AdsController(IUnitOfWork unitOfWork, IWebHostEnvironment host, IMapper mapper, UserManager<AppUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.host = host;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ad>>> GetAll()
        {
            var list = await unitOfWork.AdRepository.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("searcher")]
        public async Task<ActionResult<IEnumerable<Ad>>> GetAllSorted([FromQuery] Searcher s)
        {
            var list = await unitOfWork.AdRepository.GetAllSortedAsync(s);
            var count = await unitOfWork.AdRepository.GetSortedCount(s);
            return Ok(new
            {
                count = count,
                list = list
            });
        }

        [HttpGet("new-ads")]
        public async Task<ActionResult<IEnumerable<Ad>>> GetNewAds()
        {
            var list = await unitOfWork.AdRepository.GetNewest();
            return Ok(list);
        }

        [HttpGet("same-type/typeSearcher")]
        public async Task<ActionResult<IEnumerable<Ad>>> GetSameTypeAds([FromQuery] TypeSearcher typeSearcher)
        {
            var list = await unitOfWork.AdRepository.GetSameType(typeSearcher.Type, typeSearcher.ThisId);
            return Ok(list);
        }

        [HttpGet("same-owner/{id}")]
        public async Task<IActionResult> GetSameOwnerAds(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var list = await unitOfWork.AdRepository.GetSameOwner(id);
            return Ok(new
            {
                User = new
                {
                    Id = user.Id,
                    Mail = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    ImagePath = user.ImagePath,
                    Verified = user.EmailConfirmed ? true : false
                },
                List = mapper.Map<IEnumerable<Ad>, IEnumerable<GetAdDTO>>(list)
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ad>> GetSingle(int id)
        {
            var ad = await unitOfWork.AdRepository.GetAdWithImages(id);
            if (ad == null) return NotFound();
            return Ok(ad);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<string>> AddAd([FromForm] SaveAdResource resource)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var ad = mapper.Map<SaveAdResource, Ad>(resource);

            foreach (var image in resource.Images)
            {
                var path = await UploadImage(image);
                ad.Images.Add(new Image
                {
                    Path = path
                });
            }

            ad.TitlePath = ad.Images[0].Path;

            await unitOfWork.AdRepository.AddAsync(ad);
            user.MyAds.Add(ad);
            await unitOfWork.CompleteAsync();

            //return Ok(ad.Id);
            return Ok(ad.Id);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<string>> UpdateAd([FromForm] SaveAdResource resource, int id)
        {
            bool hasImages = false;
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var adToUpdate = await unitOfWork.AdRepository.GetAdWithImages(id);

            if (adToUpdate == null) return NotFound();

            if (resource.Images != null)
            {
                hasImages = true;
                foreach (var image in adToUpdate.Images)
                {
                    var imagePath = Path.Combine(host.ContentRootPath, "wwwroot", "Images", image.Path);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
                adToUpdate.Images.Clear();
            }
            //adToUpdate = mapper.Map<SaveAdResource, Ad>(resource);
            mapper.Map<SaveAdResource, Ad>(resource, adToUpdate);

            if (hasImages) { 
                foreach (var image in resource.Images)
                {
                    var path = await UploadImage(image);
                    adToUpdate.Images.Add(new Image
                    {
                        Path = path
                    });
                }

                adToUpdate.TitlePath = adToUpdate.Images[0].Path; 
            }
            await unitOfWork.CompleteAsync();

            //return Ok(ad.Id);
            return Ok(adToUpdate.Id);
        }

        [Authorize]
        [HttpGet("my-ads")]
        public async Task<IEnumerable<GetAdDTO>> GetByUser()
        {
            //promenili smo korisnicko ime ali token je isti a on sadrzi stari username po kom ga i trazimo ovde ispod
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var ads = await unitOfWork.AdRepository.GetByOwner(user.Id);

            var adsDTO = mapper.Map<IEnumerable<Ad>, IEnumerable<GetAdDTO>>(ads);

            return adsDTO;
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAd(int id)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var ad = await unitOfWork.AdRepository.GetAdWithImages(id);

            if (ad.Images.Any())
            {
                foreach (var image in ad.Images)
                {
                    var imagePath = Path.Combine(host.ContentRootPath, "wwwroot", "Images", image.Path);
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }

                await unitOfWork.AdRepository.RemoveAsync(id);
                await unitOfWork.CompleteAsync();

                return Ok();
            }
            else return NotFound();
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

            // implementirati optimizaciju rezolucije...
        }
    }
}
