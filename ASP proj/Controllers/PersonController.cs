using ASP_proj.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP_proj.Controllers
{
    public class PersonController : Controller
    {
        private readonly SiteContext _siteContext;
        public PersonController(SiteContext siteContext)
        {
            _siteContext = siteContext;
        }
        [Route("/person/index")]
        public IActionResult Index()
        {
            return View(_siteContext.People.Include(x => x.firstName).ToList());
        }
        [HttpGet("/person/create")]
        public IActionResult Create()
        {
            ViewData["Title"] = "Add Person";
            return View(new Person());
        }
        [HttpPost("/person/create")]
        public async Task<IActionResult> Create([FromForm] Person people, IFormFile? image)

        {
            ViewData["Title"] = "Add Person";
            if (!ModelState.IsValid)
            {
                return View(people);
            }
            //if (image != null)
            //{
            //category.Image = await _imageStorage.UploadAsync(image);

            //}
            await _siteContext.People.AddAsync(people);
            await _siteContext.SaveChangesAsync();
            return RedirectToAction("Index", "Person");
        }

        [HttpGet("/person/edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Edit Person";
            return View(await _siteContext.People.FirstAsync(x => x.Id == id));
        }
        [HttpPost("/person/edit/{id}")]
        public async Task<IActionResult> Edit(int id, [FromForm] Person form, IFormFile? Image)
        {
            ViewData["Title"] = "Edit Person:";
            if (!ModelState.IsValid)
            {
                return View(form);
            }
            var people = await _siteContext.People.FirstOrDefaultAsync(x => x.Id == id);
            //if (Image != null && Image.Length > 0)
            //{
            //var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "images", Image.FileName);
            //using (var fileStream = new FileStream(imagePath, FileMode.Create))
            //{
            //await Image.CopyToAsync(fileStream);
            //}
            //events.ImagePath = imagePath;
            //}
            people.firstName = form.firstName;
            people.lastName = form.lastName;
            people.phoneNumber = form.phoneNumber;
            people.email = form.email;
            await _siteContext.SaveChangesAsync();
            return RedirectToAction("Index", "Person");
        }
        [HttpGet("person/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var person = await _siteContext.People.FindAsync(id);
            return View(person);
        }
        [HttpPost("person/delete{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _siteContext.People.FindAsync(id);
            _siteContext.People.Remove(person);
            await _siteContext.SaveChangesAsync();
            return RedirectToAction("Index", "Person");
        }
    }
}
