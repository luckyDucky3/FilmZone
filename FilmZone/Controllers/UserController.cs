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

        [HttpPost]
        public string Registration(string LastName, string FirstName, string NickName, string Email, string Password1, string Password2)
        {
            if (Password1 == Password2)
                return
                    $"Your lastname: {LastName}, firstname: {FirstName}, nickname: {NickName}, email: {Email}, password: {Password1}";
            else
                return "Пароли не совпадают, повторите попытку";
        }
        [HttpGet]
        public IActionResult ForgotPassword()
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
