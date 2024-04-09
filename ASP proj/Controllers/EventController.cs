using ASP_proj.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP_proj.Controllers
{
    public class EventController : Controller
    {
        private readonly SiteContext _siteContext;
        public EventController(SiteContext siteContext)
        {
            _siteContext = siteContext;
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
        //[HttpPost("/event/create")]
        //public IActionResult Create()
        //{

        //}
        [HttpGet("/event/edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            ViewData["Title"] = "Edit Event";
            return View(await _siteContext.Events.FirstAsync(x => x.Id == id));
        }
    }
}
