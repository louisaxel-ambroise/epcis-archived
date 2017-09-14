using System.Web.Mvc;

namespace FasTnT.Web.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        public ActionResult Index(string type, string input)
        {
            return View();
        }

        public ActionResult Inline(string input)
        {
            // TODO: perform guesses on what the user wants based on the input.
            var guesses = new []
            {
                new
                {
                    Type = "EpcSearch",
                    Link = $"Search/Index?type=EPC&pattern={input}"
                },
                new
                {
                    Type = "UserSearch",
                    Link = $"Search/Index?type=user&pattern={input}"
                }
            };

            return PartialView(guesses);
        }
    }
}