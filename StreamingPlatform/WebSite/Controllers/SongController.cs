using Microsoft.AspNetCore.Mvc;

namespace StreamingPlatform.WebSite.Controllers
{
    public class SongController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
