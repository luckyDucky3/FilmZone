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

namespace FilmZone.Controllers
{
    public class UserController : Controller
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Index(string LoginField, string PasswordField)
        {
            ViewData["LoginField"] = LoginField;
            return View();
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
                    LoginName = LoginName,
                    Password = Password1,
                    Token = token
                };
                var response = await userService.CreateUser(user);
                if (response.Data)
                {
                    SendMessageAboutRegistrationToEmail(LoginName, Email, token);
                    Timer timer = new Timer(RemoveTokenCallback, user, TimeSpan.FromMinutes(40),
                        Timeout.InfiniteTimeSpan);
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
        public async Task<IActionResult> ConfirmRegistration(string login, string token)
        {
            var response = await userService.GetUserByLogin(login);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                var user = response.Data;
                if (user.Token == token)
                    return View(true);
                else
                    return View(false);
            }
            else
            {
                return View("Error");
            }

        }





        private void SendMessageAboutRegistrationToEmail(string login, string mail, string token)
        {
            using var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("FilmZone (Подтверждение регистрации)", "makslebed04@mail.ru"));
            emailMessage.To.Add(new MailboxAddress("Подтверждение регистрации", mail));
            emailMessage.Subject = "Завершите регистрацию";
            var builder = new BodyBuilder();
            
            string confirmationLink = Url.Action("ConfirmRegistration", "User", new { login = login, token = token });
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

        private void RemoveTokenCallback(object state)
        {
            var user = (User)state;
            userService.UpdateUser(user.LoginName, new User()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                LoginName = user.LoginName,
                Password = user.Password,
                Token = null
            });
        }
    }
}
