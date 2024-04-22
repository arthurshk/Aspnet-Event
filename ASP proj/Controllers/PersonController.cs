using ASP_proj.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP_proj.Controllers
{
    public class PersonController : Controller
    {
        private readonly SiteContext _siteContext;
        public PersonController(SiteContext siteContext)
        {
            _siteContext = siteContext;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
