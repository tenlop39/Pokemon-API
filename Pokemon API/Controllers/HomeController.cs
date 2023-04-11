using Microsoft.AspNetCore.Mvc;
using Pokemon_API.Models;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;

namespace Pokemon_API.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //vytvori request na api
            WebRequest request = WebRequest.Create("http://pokeapi.co/api/v2/pokemon/1");
            //posle request pryc
            WebResponse response = request.GetResponse();
            //get back to response stream
            Stream stream = response.GetResponseStream();
            //make it accessible
            StreamReader reader = new StreamReader(stream);
            //vezme vse ze streamReadru a da to do stringu jako json format
            string responseFromServer = reader.ReadToEnd();
            JObject parsedString = JObject.Parse(responseFromServer);
            Pokemon myPokemon = parsedString.ToObject<Pokemon>();
            Debug.WriteLine(myPokemon.moves[0].move.name);

            return View();
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}