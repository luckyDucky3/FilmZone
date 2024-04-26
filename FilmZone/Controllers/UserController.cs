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
        public IActionResult RegistrationPage(bool PasswordError)
        {
            return View(PasswordError);
        }

        [HttpPost]
        public RedirectToActionResult Registration (string LastName, string FirstName, string NickName, string Email, string Password1, string Password2)
        {
            if (Password1 == Password2)
                return RedirectToAction("SendMessageToEmail");
            else
                return RedirectToAction("RegistrationPage", new { PasswordError = true });
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SendMessageToEmail()
        {
            return View();
        }
        [HttpPost]
        public string ForgotPassword(string Email)
        {
            return "Ссылка о смене пароля отправленна вам на почту:)\nЕсли ссылка не приходит, пожалуйста, еще раз проверьте правильность введенной почты или проверьте папку спам";
        }
    }
}
