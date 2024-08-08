﻿using System.Security.Cryptography;
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
using FilmZone.Domain.ViewModels;

namespace FilmZone.Controllers
{
    public class UserController : BaseController
    {
        const string SessionKeyLogin = "_Name";
        const string SessionKeyDate = "_Date";
        public UserController(IHttpContextAccessor httpContextAccessor, IUserService userService, TimerHostedService timerHostedService, IFilmFeedbackService filmFeedbackService, IBestFilmService bestFilmService, IFilmService filmService) : base(httpContextAccessor, userService, timerHostedService, filmFeedbackService, bestFilmService, filmService) { }
        //public UserController(IUserService userService, TimerHostedService timerHostedService)
        //{
        //    this.userService = userService;
        //    this.timerHostedService = timerHostedService;
        //}

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

        public async Task<IActionResult> FilmFeedback(string ReviewHeading, string ReviewText, int FilmId)
        {
            string? login = httpcontextAccessor.HttpContext?.Session.GetString(SessionKeyLogin);
            var response = await filmFeedbackService.GetFeedbackByLoginAndFilmName(login, FilmId);
            FilmFeedback ff = new FilmFeedback()
            {
                Heading = ReviewHeading,
                Text = ReviewText,
                Name = login,
                FilmId = FilmId
            };
            if (response.Data == null)
            {
                await filmFeedbackService.CreateFeedback(ff);
            }
            else
            {
                await filmFeedbackService.UpdateEmptyFeedbackWithRating(login, FilmId, ff);
            }
            return RedirectToAction("SearchById", "Film", new { Id = FilmId });
        }

        public IActionResult ChangeBackgroundColor(string color) 
        {
            HttpContext.Session.SetString("_BackgroundColor", color);
            return View("Options");
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var response = await userService.GetUserByLogin(HttpContext.Session.GetString(SessionKeyLogin));
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View(null); 
        }

        [HttpGet]
        public IActionResult Options()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Favourites()
        {
            List<FilmViewModel> listOfFilms = new List<FilmViewModel>();
            var resp = await bestFilmService.GetListOfUserFilm(httpcontextAccessor.HttpContext.Session.GetString(SessionKeyLogin));
            if(resp.StatusCode == Domain.Enum.StatusCode.OK && resp.Data != null)
            {
                foreach (var f in resp.Data)
                {
                    var response = await filmService.GetFilmByName(f.FilmName);
                    if(response.StatusCode == Domain.Enum.StatusCode.OK)
                    {
                        listOfFilms.Add((TransformToFilmViewModel(response.Data)));
                    }
                }
            }

            return View(listOfFilms);
        }


        [HttpGet]
        public IActionResult ExitFromAccount()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Film");
        }

        [HttpGet]
        public IActionResult RegistrationPage(List<bool> Errors)
        {
            if (Errors.Count == 0)
                Errors = new List<bool>(){false, false, false, false, false, false, false, false, false};
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
                errorPasswords = false,
                errorSearchLogin = false,
                errorSearchMail = false;
            errorSearchLogin = await loginSearchInDB(LoginName);
            errorSearchMail = await mailSearchInDB(Email);
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

            if (errorLengthLastName || errorLengthFirstName || errorLengthNickName || errorEmail || errorPassword1 || errorPassword1Length ||errorPasswords || errorSearchLogin || errorSearchMail)
            {
                List<bool> listErrors = new List<bool>() { errorLengthLastName, errorLengthFirstName, errorLengthNickName, errorEmail, errorPassword1, errorPassword1Length ,errorPasswords, errorSearchLogin, errorSearchMail };
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
            async Task<bool> loginSearchInDB(string login)
            {
                bool search = false;
                var response = await userService.GetUserByLogin(login);
                if(response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    search = true;
                }
                return search;
            }
            async Task<bool> mailSearchInDB(string mail)
            {
                bool search = false;
                var response = await userService.GetUserByMail(mail);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    search = true;
                }
                return search;
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
        private FilmViewModel TransformToFilmViewModel(ref readonly Film film)
        {
            FilmViewModel model = new FilmViewModel()
            {
                Id = film.Id,
                Name = film.Name,
                PathToImage = film.PathToImage,
                FilmOrSerial = film.FilmOrSerial,
                Description = film.Description,
                ReleaseFilmDate = film.ReleaseFilmDate,
                Type = film.Type,
                Director = film.Director,
                Preview = film.Preview,
                Links = new List<string>(film.Links),
                Price = new List<string>(film.Price),
                Advertisement = new List<string>(film.Advertisement)
            };
            return model;
        }
    }
}
