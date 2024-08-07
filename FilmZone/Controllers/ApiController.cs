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
        public ApiController(IHttpContextAccessor httpcontextAccessor, IFilmFeedbackService filmFeedbackService, IBestFilmService bestFilmService) : base(httpcontextAccessor, filmFeedbackService, bestFilmService) { }
        [HttpPost("addFilm")]
        public async Task<OkObjectResult> AddFilmToFavourites(JsonElement jsonElement)
        {
            string login = httpcontextAccessor.HttpContext?.Session.GetString("_Name");
            if (login != null)
            {
                if (jsonElement.TryGetProperty("name", out var filmNameElement))
                {
                    string filmName = filmNameElement.GetString();
                    //var checkBestFilm = await bestFilmService.GetListOfUserFilm(httpcontextAccessor.HttpContext?.Session.GetString("_Name"));
                    //foreach (var f in checkBestFilm.Data)
                    //{
                    //    if (f.FilmName == filmName)
                    //    {
                    //        return Ok(new { success = true });
                    //    }
                    //}
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
            string login = httpcontextAccessor.HttpContext?.Session.GetString("_Name");
            if (login != null)
            {
                if (jsonElement.TryGetProperty("name", out var filmNameElement))
                {
                    string filmName = filmNameElement.GetString();
                    var resp = await bestFilmService.DeleteFilmByUser(HttpContext.Session.GetString("_Name"), filmName);
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
            string login = httpcontextAccessor.HttpContext?.Session.GetString("_Name");
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
                        if (response.StatusCode == Domain.Enum.StatusCode.OK)
                        {
                            return Ok(new { success = true });
                        }
                    }
                    else if(resp.StatusCode == Domain.Enum.StatusCode.OK)
                    {
                        var response = await filmFeedbackService.UpdateFeedbackRating(login, filmId, rating);
                        if (response.StatusCode == Domain.Enum.StatusCode.OK)
                        {
                            return Ok(new { success = true });
                        }
                    }
                }
            }
            return Ok(new { success = false });
        }
    }
}
