using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WhatsApp.Models;

namespace WhatsApp.Controllers
{
    public class HomeController : Controller
    {
        List<Kontak> _listKontak = new List<Kontak>();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _listKontak = new List<Kontak>(){
             new Kontak("Aldira","089639971606"),
             new Kontak("Fitri","089987621623"),
             new Kontak("Haniifah","089987621623"),
           };
        }

        public IActionResult Index()
        {
            ViewData["ListKontak"] = _listKontak;
            ViewBag.ListKontak = _listKontak;
            return View(_listKontak);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}