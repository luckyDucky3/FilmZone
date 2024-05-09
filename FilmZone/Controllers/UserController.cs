using System.Security.Cryptography;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MimeKit;

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
        public IActionResult RegistrationPage(List<bool> Errors)
        {
            if (Errors.Count == 0)
                Errors = new List<bool>(){false, false, false, false, false};
            return View(Errors);
        }

        [HttpPost]
        public RedirectToActionResult Registration (string LastName, string FirstName, string NickName, string Email, string Password1, string Password2)
        {
            bool errorLengthLastName = false,
                errorLengthFirstName = false,
                errorLengthNickName = false,
                errorEmail = false,
                errorPasswords = false;
            if (LastName.Length > 20 || LastName.Length < 2)
                errorLengthLastName = true;
            if (FirstName.Length > 15 || FirstName.Length < 2)
                errorLengthFirstName = true;
            if(NickName.Length > 20 || NickName.Length < 3)
                errorLengthNickName = true;
            if(!Email.Contains('@'))
                errorEmail = true;
            if (Password1 != Password2)
                errorPasswords = true;
            if (errorLengthLastName || errorLengthFirstName || errorLengthNickName || errorEmail || errorPasswords)
            {
                List<bool> listErrors = new List<bool>() { errorLengthLastName, errorLengthFirstName, errorLengthNickName, errorEmail, errorPasswords };
                return RedirectToAction("RegistrationPage", new { Errors = listErrors}); 
                
            }
            else
            {
                return RedirectToAction("SendMessageToEmail", new {mail = Email, login = NickName});
            }
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

        [HttpGet]
        public IActionResult SendMessageToEmail(string mail, string login)
        {
            using var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("FilmZone (Подтверждение регистрации)", "makslebed04@mail.ru"));
            emailMessage.To.Add(new MailboxAddress("Подтверждение регистрации", mail));
            emailMessage.Subject = "Завершите регистрацию";
            var builder = new BodyBuilder();
            byte[] tokenbytes = new byte[32];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(tokenbytes);
            }

            string token = Convert.ToBase64String(tokenbytes);
            string confirmationLink = Url.Action("ConfirmRegistration", "User", new { token = token });
            builder.HtmlBody = string.Format(@$"<p>Привет, {login}!<br>
<p>Поздравляем с регистрацией на FilmZone! Просим вас подтвердить Email адрес {mail}, для продолжения регистрации на нашем сайте перейдите по ссылке: {confirmationLink}<br>
<p>Вы получили это письмо, поскольку являетесь зарегистрированным пользователем нашего сайта и указали {mail} при регистрации");
            emailMessage.Body = builder.ToMessageBody();
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("smtp.mail.ru", 587, false);
                    client.Authenticate("makslebed04@mail.ru", "6C8uUq5ivrhTKKR534sA");
                    client.Send(emailMessage);
                }
                catch (MailKit.ServiceNotConnectedException ex)
                {
                    Console.WriteLine($"Ошибка подключения сервера: {ex.Message}");
                    Console.WriteLine(ex.Data);
                }
                catch (MailKit.ServiceNotAuthenticatedException ex)
                {
                    Console.WriteLine($"Ошибка аутентификации сервера : {ex.Message}");
                    Console.WriteLine(ex.Data);
                }
                catch (MailKit.CommandException ex)
                {
                    Console.WriteLine(
                        $"Ошибка клиентского метода(скорее всего пользователь ввел неверный адрес эл. почты): {ex.Message}");
                    Console.WriteLine(ex.Data);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.Data);
                }
                finally
                {
                    client.Disconnect(true);
                }
            }
            return View();
        }
    }
}
