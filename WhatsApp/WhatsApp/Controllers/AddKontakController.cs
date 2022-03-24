using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WhatsApp.Models;
using WhatsApp.ViewModels;
using WhatsApp.Services;
using System.IO;
using System.Text;

namespace WhatsApp.Controllers
{
    public class AddKontakController : Controller
    {
        List<KontakViewModel> _listKontak = new List<KontakViewModel>();
        private readonly IKontakService _kontakService;
        private readonly IFileService _fileService;

        public AddKontakController(IKontakService kontakService, IFileService fileService)
        {
            _kontakService = kontakService;
            _fileService = fileService;
            _listKontak = new List<KontakViewModel>()
            {
                new KontakViewModel("Aldira","089639971606"),
                new KontakViewModel("Fitri","089987621623"),
                new KontakViewModel("Haniifah","089987621623"),
            };

        }
        // GET: AddKontakController
        public async Task<ActionResult> Index()
        {
            var data = await _fileService.Read();
            return View(data);
        }

        // GET: AddKontakController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View(new KontakViewModel());
        }

        // POST: AddKontakController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(KontakViewModel collection)
        {
            try
            {
                await _fileService.Write(collection);
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
