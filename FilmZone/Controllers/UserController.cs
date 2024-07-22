using System.Security.Cryptography;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using MailKit.Net.Smtp;
using MimeKit;
using System.Text.RegularExpressions;
using FilmZone.Domain.Models;
using FilmZone.Domain.Response;
using FilmZone.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using FilmZone.DAL;
using FilmZone.Service.Implementations;

namespace FilmZone.Controllers
{
    public class UserController : Controller
    {
        private IUserService userService;
        private TimerHostedService timerHostedService;
        const string SessionKeyLogin = "_Name";
        const string SessionKeyDate = "_Date";
        public UserController(IUserService userService, TimerHostedService timerHostedService)
        {
            this.userService = userService;
            this.timerHostedService = timerHostedService;
        }

        [HttpGet]
        public IActionResult LoginPage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string LoginField, string PasswordField)
        {
            var response = await userService.GetUserByLogin(LoginField);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                if (response.Data.Password == PasswordField)
                {
                    //Session["SuccecfullRegistration"] = true;
                    HttpContext.Session.SetString(SessionKeyLogin, response.Data.Login);
                    HttpContext.Session.SetString(SessionKeyDate, DateTime.Now.ToString());
                    return View("Index", LoginField);
                }
                else
                {
                    return View("Error", FilmZone.Domain.Enum.RegistrationError.PasswordError);
                }
            }
            else if (response.StatusCode == Domain.Enum.StatusCode.UserNotFound)
            {
                return View("Error", FilmZone.Domain.Enum.RegistrationError.LoginError);
            }
            return View("Error", null);
        }

        public RedirectToPageResult ChangeBackgroundColor(string color) 
        {
            HttpContext.Session.SetString("_BackgroundColor", color);
            return RedirectToPage("Options");
        }

        [HttpGet]
        public IActionResult Profile()
        {

        return View(); 
        }

        [HttpGet]
        public IActionResult Options()
        {

            return View();
        }

        [HttpGet]
        public IActionResult ExitFromAccount()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult RegistrationPage(List<bool> Errors)
        {
            if (Errors.Count == 0)
                Errors = new List<bool>(){false, false, false, false, false, false, false};
            return View(Errors);
        }

        [HttpPost]
        public async Task<IActionResult> Registration (string LastName, string FirstName, string LoginName, string Email, string Password1, string Password2)
        {
            bool errorLengthLastName = false,
                errorLengthFirstName = false,
                errorLengthNickName = false,
                errorEmail = false,
                errorPassword1 = false,
                errorPassword1Length = false,
                errorPasswords = false;
            if (LastName.Length > 20 || LastName.Length < 2)
                errorLengthLastName = true;
            if (FirstName.Length > 15 || FirstName.Length < 2)
                errorLengthFirstName = true;
            if(LoginName.Length > 20 || LoginName.Length < 3)
                errorLengthNickName = true;
            if(!Email.Contains('@'))
                errorEmail = true;
            if (Password1 != Password2)
                errorPasswords = true;
            Regex pattern = new Regex("^[a-zA-Z0-9!?,.%#*\\$]+$");
            if (!pattern.IsMatch(Password1))
            {
                errorPassword1 = true;
            }
            if (Password1.Length < 8)
                errorPassword1Length = true;
            if (errorLengthLastName || errorLengthFirstName || errorLengthNickName || errorEmail || errorPassword1 || errorPassword1Length ||errorPasswords)
            {
                List<bool> listErrors = new List<bool>() { errorLengthLastName, errorLengthFirstName, errorLengthNickName, errorEmail, errorPassword1, errorPassword1Length ,errorPasswords };
                return RedirectToAction("RegistrationPage", new { Errors = listErrors}); 
                
            }
            else
            {
                byte[] tokenbytes = new byte[32];
                using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(tokenbytes);
                }
                string token = Convert.ToBase64String(tokenbytes);
                User user = new User()
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Email = Email,
                    Login = LoginName,
                    Password = Password1,
                    Token = token
                };
                var response = await userService.CreateUser(user);
                if (response.Data)
                {
                    SendMessageAboutRegistrationToEmail(user.Id, LoginName, Email, token);
                    await timerHostedService.StartAsync(user);
                }
                return View("SendMessageToEmail");
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

        //[HttpGet]
        //public IActionResult SendMessageToEmail(string mail, string login, string token)
        //{

        //    return View();
        //}

        [HttpGet]
        public async Task<IActionResult> ConfirmRegistration(int id, string token)
        {
            var response = await userService.GetUserById(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                var user = response.Data;
                user.EmailConfirmation = true;
                var confirm = await userService.UpdateUserById(user);
                if (confirm.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return View(true);
                }
                else
                {
                    return View("Error");
                }
            }
            else
            {
                return View(false);
            }

        }


        private void SendMessageAboutRegistrationToEmail(int id, string login, string mail, string token)
        {
            using var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("FilmZone (Подтверждение регистрации)", "makslebed04@mail.ru"));
            emailMessage.To.Add(new MailboxAddress("Подтверждение регистрации", mail));
            emailMessage.Subject = "Завершите регистрацию";
            var builder = new BodyBuilder();
            
            string confirmationLink = Url.Action("ConfirmRegistration", "User", new { id = id, token = token });
            builder.HtmlBody = string.Format(@$"<p>Привет, {login}!<br>
<p>Поздравляем с регистрацией на FilmZone! Просим вас подтвердить Email адрес {mail}, для продолжения регистрации на нашем сайте перейдите по ссылке: <a href =""https://film-zone.ru{confirmationLink}"">Кликни вот сюды</a><br>
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
                        $"Ошибка клиентского метода: {ex.Message}");
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
        }
    }
}
