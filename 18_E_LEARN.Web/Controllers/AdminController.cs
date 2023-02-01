using _18_E_LEARN.DataAccess.Data.Models.User;
using _18_E_LEARN.DataAccess.Data.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _18_E_LEARN.Web.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AdminController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Users()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignIn(LoginUserVM model)
        {
            var user = await  _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return View("User not found!");
            }
            else
            {

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }

           
        }

        [AllowAnonymous]
        public IActionResult SignUp()
        {
            return View();
        }
    }
}
