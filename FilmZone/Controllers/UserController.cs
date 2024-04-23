using Microsoft.AspNetCore.Mvc;

namespace FilmZone.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index(string LoginField, string PasswordField)
        {
            ViewData["LoginField"] = LoginField;
            return View();
        }
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
