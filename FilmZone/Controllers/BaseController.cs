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
        protected readonly ISiteFeedbackService feedbackService;
        protected readonly ILogger<FilmController> _logger;
        protected readonly IFilmService filmService;
        protected readonly IFilmFeedbackService filmFeedbackService;
        protected readonly IBestFilmService bestFilmService;
        public BaseController(IHttpContextAccessor httpcontextAccessor, IUserService userService, TimerHostedService timerHostedService, IFilmFeedbackService filmfeedbackService, IBestFilmService best, IFilmService filmService)
        {
            this.httpcontextAccessor = httpcontextAccessor;
            this.userService = userService;
            this.timerHostedService = timerHostedService;
            this.filmFeedbackService = filmfeedbackService;
            bestFilmService = best;
            this.filmService = filmService;
        }
        public BaseController(ILogger<FilmController> logger, IFilmService filmService, IHttpContextAccessor httpcontextAccessor, ISiteFeedbackService feedbackService)
        {
            _logger = logger;
            this.filmService = filmService;
            this.httpcontextAccessor = httpcontextAccessor;
            this.feedbackService = feedbackService;
        }
        public BaseController(IHttpContextAccessor httpcontextAccessor, IFilmFeedbackService filmFeedbackService, IBestFilmService bestFilmService, IFilmService filmService)
        {
            this.httpcontextAccessor = httpcontextAccessor;
            this.filmFeedbackService = filmFeedbackService;
            this.bestFilmService = bestFilmService;
            this.filmService = filmService;
        }

        public bool IsUserInPrivateArea()
        {
            var sessionValue = httpcontextAccessor.HttpContext.Session.GetString("_Name");
            ViewBag.UserName = sessionValue;
            return sessionValue != null;
        }

        public override ViewResult View(string viewName, object model)
        {
            ViewBag.IsInPrivateArea = IsUserInPrivateArea();
            return base.View(viewName, model);
        }

    }
}
