using FilmZone.Service.Implementations;
using FilmZone.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace FilmZone.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IHttpContextAccessor httpcontextAccessor;
        protected readonly IUserService userService;
        protected readonly TimerHostedService timerHostedService;
        protected readonly IFeedbackService feedbackService;
        protected readonly ILogger<FilmController> _logger;
        protected readonly IFilmService filmService;
        public BaseController(IHttpContextAccessor httpcontextAccessor, IUserService userService, TimerHostedService timerHostedService)
        {
            this.httpcontextAccessor = httpcontextAccessor;
            this.userService = userService;
            this.timerHostedService = timerHostedService;

        }
        public BaseController(ILogger<FilmController> logger, IFilmService filmService, IHttpContextAccessor httpcontextAccessor, IFeedbackService feedbackService)
        {
            _logger = logger;
            this.filmService = filmService;
            this.httpcontextAccessor = httpcontextAccessor;
            this.feedbackService = feedbackService;
        }

        public bool IsUserInPrivateArea()
        {
            var sessionValue = httpcontextAccessor.HttpContext.Session.GetString("_Name");
            return sessionValue != null;
        }

        public override ViewResult View(string viewName, object model)
        {
            ViewBag.IsInPrivateArea = IsUserInPrivateArea();
            return base.View(viewName, model);
        }

    }
}
