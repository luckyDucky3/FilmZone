using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FilmZone.Domain.Enum
{
    public enum TypeFilm
    {
        [Display(Name = "Аниме")]
        Anime = 0,
        [Display(Name = "Биография")]
        Biography,
        [Display(Name = "Боевик")]
        ActionMovie,
        [Display(Name = "Вестерн")]
        Western,
        [Display(Name = "Военный")]
        Military,
        [Display(Name = "Детектив")]
        Detective,
        [Display(Name = "Документальный")]
        Documentary,
        [Display(Name = "Драма")]
        Drama,
        [Display(Name = "История")]
        History,
        [Display(Name = "Комедия")]
        Comedy,
        [Display(Name = "Короткометражка")]
        ShortFilm,
        [Display(Name = "Мелодрама")]
        Melodrama,
        [Display(Name = "Мультфильм")]
        Cartoon,
        [Display(Name = "Мюзикл")]
        Musical,
        [Display(Name = "Приключения")]
        Adventures,
        [Display(Name = "Триллер")]
        Thriller,
        [Display(Name = "Ужасы")]
        Horrors,
        [Display(Name = "Фантастика")]
        Fantasy
    }
}
