using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace FilmZone.Service
{
    public class KinopoiskParser
    {
        public async Task<IEnumerable<string>> ParseImages()
        {
            IConfiguration config = Configuration.Default.WithDefaultLoader();
            string address = "https://www.imdb.com/";
            IBrowsingContext context = BrowsingContext.New(config);
            IDocument document = await context.OpenAsync(address);
            string rateSelector = "ipc-rating-star--rating";
            var images = document.Images;
            List<string> strings = new List<string>();
            for (int i = 0; i < images.Count(); i++)
            {
                strings.Add(images.ElementAt(i).ToHtml());
            }
            return strings;
        }
    }
}
