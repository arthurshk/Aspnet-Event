using ASP_proj.Models;
using ASP_proj.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace ASP_proj.Controllers
{
    public class EventController : Controller
    {
        private readonly SiteContext _siteContext;
        public readonly ImageStorage _imageStorage;
        public EventController(SiteContext siteContext, ImageStorage imageStorage)
        {
            _siteContext = siteContext;
            _imageStorage = imageStorage;
        }
        [Route("/event/index")]
        public IActionResult Index()
        {
            return View(_siteContext.Events.Include(x => x.Name).ToList());
        }
        [HttpGet("/event/create")]
        public IActionResult Create()
        {
            ViewData["Title"] = "Create Event";
            return View(new Event());
        }
        [HttpPost("/event/create")]
        public async Task<IActionResult> Create([FromForm] Event events, IFormFile? image)
        
            {
                ViewData["Title"] = "Create event";
                if (!ModelState.IsValid)
                {
                    return View(events);
                }
                //if (image != null)
                //{
                    //category.Image = await _imageStorage.UploadAsync(image);

                //}
                await _siteContext.Events.AddAsync(events);
                await _siteContext.SaveChangesAsync();
                return Redirect("/event/index");
            }
        
        [HttpGet("/event/edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Edit Event";
            return View(await _siteContext.Events.FirstAsync(x => x.Id == id));
        }
        [HttpPost("/event/edit/{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] Event form, IFormFile? Image)
        {
            ViewData["Title"] = "Edit Event:";
            if(!ModelState.IsValid)
            {
                return View(form);
            }
            var events = await _siteContext.Events.FirstOrDefaultAsync(x => x.Id == id);
            //if (Image != null && Image.Length > 0)
            //{
                //var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images", Image.FileName);
                //using (var fileStream = new FileStream(imagePath, FileMode.Create))
                //{
                    //await Image.CopyToAsync(fileStream);
                //}
                //events.ImagePath = imagePath;
            //}
            events.Name = form.Name;
            events.Location = form.Location;
            events.Description = form.Description;
            await _siteContext.SaveChangesAsync();
            return Redirect("event/index");
        }

    }
}
