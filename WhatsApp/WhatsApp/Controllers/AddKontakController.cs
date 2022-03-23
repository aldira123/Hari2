using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhatsApp.Models;
using WhatsApp.Services;
using System.IO;
using System.Text;

namespace WhatsApp.Controllers
{
    public class AddKontakController : Controller
    {
        List<KontakViewModel> _listKontak = new List<KontakViewModel>();
        private readonly IKontakService _kontakService;

        public AddKontakController(IKontakService kontakService)
        {
            _kontakService = kontakService;
            _listKontak = new List<KontakViewModel>()
            {
                new KontakViewModel("Aldira","089639971606"),
                new KontakViewModel("Fitri","089987621623"),
                new KontakViewModel("Haniifah","089987621623"),
            };
            using (TextWriter tw = new StreamWriter("D:\\WhatsApp.txt"))
            {
                foreach (var s in _listKontak)
                    tw.WriteLine("Nama: " + s.Nama +" Nomor: "+ s.Nomor);
            }
        }
        // GET: AddKontakController
        public ActionResult Index()
        {
            return View(_kontakService.GetKontaks());
        }

        // GET: AddKontakController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View(new Kontak());
        }

        // POST: AddKontakController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Kontak collection)
        {
            //using (TextWriter tw = new StreamWriter("D:\\WhatsApp.txt", append: true))
            //{
            //    tw.WriteLine(new KontakViewModel());
            //}

            using (FileStream fs = new FileStream("D:\\WhatsApp.txt", FileMode.Append, FileAccess.Write))
            {
                using (TextWriter tw = new StreamWriter(fs))
                 tw.WriteLine(_kontakService.GetKontaks());
                //System.IO.File.AppendAllLines(new Kontak());
            }

            try
            {
                System.IO.File.ReadAllLines("D:\\WhatsApp.txt");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AddKontakController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AddKontakController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AddKontakController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AddKontakController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
