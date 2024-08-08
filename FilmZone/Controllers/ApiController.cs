using FilmZone.Domain.Models;
using FilmZone.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;


namespace FilmZone.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiController : BaseController
    {
        public ApiController(IHttpContextAccessor httpcontextAccessor, IFilmFeedbackService filmFeedbackService, IBestFilmService bestFilmService, IFilmService filmService) : base(httpcontextAccessor, filmFeedbackService, bestFilmService, filmService) { }
        [HttpPost("addFilm")]
        public async Task<OkObjectResult> AddFilmToFavourites(JsonElement jsonElement)
        {
            string? login = httpcontextAccessor.HttpContext?.Session.GetString("_Name");
            if (login != null)
            {
                if (jsonElement.TryGetProperty("name", out var filmNameElement))
                {
                    string? filmName = filmNameElement.GetString();
                    BestFilm film = new BestFilm();
                    film.UserName = login;
                    film.FilmName = filmName;
                    var resp = await bestFilmService.CreateFilm(film);
                    if (resp.StatusCode == Domain.Enum.StatusCode.OK)
                    {
                        return Ok(new { success = true });
                    }
                }
            }
            return Ok(new { success = false });
        }
        [HttpPost("removeFilm")]
        public async Task<OkObjectResult> RemoveFilmToFavourites(JsonElement jsonElement)
        {
            string? login = httpcontextAccessor.HttpContext?.Session.GetString("_Name");
            if (login != null)
            {
                if (jsonElement.TryGetProperty("name", out var filmNameElement))
                {
                    string? filmName = filmNameElement.GetString();
                    var resp = await bestFilmService.DeleteFilmByUser(login, filmName);
                    if (resp.StatusCode == Domain.Enum.StatusCode.OK)
                    {
                        return Ok(new { success = true });
                    }
                }
            }
            return Ok(new { success = false });
        }
        [HttpPost("rating")]
        public async Task<OkObjectResult> UpdateRating(JsonElement jsonElement)
        {
            string? login = httpcontextAccessor.HttpContext?.Session.GetString("_Name");
            if (login != null)
            {
                if (jsonElement.TryGetProperty("rating", out var rate) && jsonElement.TryGetProperty("id", out var id))
                {
                    int rating = rate.GetInt32();
                    int filmId = id.GetInt32();
                    var resp = await filmFeedbackService.GetFeedbackByLoginAndFilmName(login, filmId);
                    if (resp.Data == null && resp.StatusCode == Domain.Enum.StatusCode.OK)
                    {
                        FilmFeedback filmFeedback = new FilmFeedback();
                        filmFeedback.Value = rating;
                        filmFeedback.FilmId = filmId;
                        filmFeedback.Name = login;
                        var response = await filmFeedbackService.CreateFeedback(filmFeedback);
                        if (response.StatusCode != Domain.Enum.StatusCode.OK)
                        {
                            return Ok(new { success = false });
                        }
                    }
                    else if(resp.StatusCode == Domain.Enum.StatusCode.OK)
                    {
                        var response = await filmFeedbackService.UpdateFeedbackWithoutRating(login, filmId, rating);
                        
                        if (response.StatusCode != Domain.Enum.StatusCode.OK)
                        {    
                            return Ok(new { success = false });
                        }
                    }
                    if(resp.StatusCode == Domain.Enum.StatusCode.OK)
                    {
                        var getListResponse = await filmFeedbackService.GetListOfValues(filmId);
                        if (getListResponse.StatusCode == Domain.Enum.StatusCode.OK)
                        {
                            double ratingFilm = 0, sum = 0, col = 0;
                            foreach (var v in getListResponse.Data)
                            {
                                if(v > 0)
                                {
                                    sum += v;
                                    col++;
                                }
                            }
                            ratingFilm = sum / col;
                            var updateResponse = await filmService.UpdateFilmRating(filmId, ratingFilm);
                            if(updateResponse.StatusCode == Domain.Enum.StatusCode.OK)
                            {
                                return Ok(new { success =  true });
                            }
                        }
                    }
                }
            }
            return Ok(new { success = false });
        }
        [HttpPost("acceptCookie")]
        public void acceptCookie()
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(14)
            };
            httpcontextAccessor.HttpContext?.Response.Cookies.Append("IKnowAboutCookie", "Yes", cookieOptions);
        }
    }
}
