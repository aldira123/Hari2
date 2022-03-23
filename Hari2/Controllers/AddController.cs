using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Hari2.Models;

namespace Hari2.Controllers;

public class AddController : Controller
{
    List<Kontak> _listKontak = new List<Kontak>();

    public IActionResult Index()
    {
        return View(_listKontak);
    }
        // public ActionResult AddProfile(){
        //     return View;
        // }

    [HttpPost]
        public ActionResult AddProfile(Models.Kontak model){
            return View("Index",model);
        }

}
