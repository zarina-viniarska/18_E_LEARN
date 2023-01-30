using Microsoft.AspNetCore.Mvc;

namespace _18_E_LEARN.Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
