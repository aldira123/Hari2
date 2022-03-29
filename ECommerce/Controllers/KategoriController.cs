using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using eCommerce.Models;
using eCommerce.ViewModels;
using Microsoft.EntityFrameworkCore;
using eCommerce.Interface;

namespace eCommerce.Controllers;

public class KategoriController : Controller
{
    private readonly IKategoriService _kategoriService;
    private readonly ILogger<KategoriController> _logger;

    public KategoriController(ILogger<KategoriController> logger, IKategoriService kategoriService)
    {
        _logger = logger;
        _kategoriService = kategoriService;
    }

    public async Task<IActionResult> Index()
    {
        var dbResult = await _kategoriService.GetAll();

        var viewModels = new List<KategoriViewModel>();

        for (int i = 0; i < dbResult.Count; i++)
        {
            viewModels.Add(new KategoriViewModel
                {
                    IdKategori = dbResult[i].IdKategori,
                    NamaKategori = dbResult[i].NamaKategori,
                    DeskripsiKategori = dbResult[i].DeskripsiKategori,
                    Icon = dbResult[i].Icon,
                });
        }

        return View(viewModels);
    }

    public async Task<IActionResult> Details(KategoriViewModel request ) {
        if (request.IdKategori == null)
            {
                return NotFound();
            }
        
        var dbResult = await _kategoriService.Get(request.IdKategori);
        return View(dbResult);
    }

    public IActionResult Create() {
        return View(new KategoriViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(KategoriViewModel request) {
        if(!ModelState.IsValid){
            return View(request);
        }
        try{
            await _kategoriService.Add(request.ConvertToDbModel());

            return Redirect(nameof(Index));   
        }catch(InvalidOperationException ex){
            ViewBag.ErrorMessage = ex.Message;
        }
        catch(Exception) {
            throw;
        }

        return View(request);
    }

    // GET: KategoriProduks/Edit/5
        public IActionResult Edit()
        {
           return View(new KategoriViewModel());
        }

        // POST: KategoriProduks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(KategoriViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            try
            {
                 await _kategoriService.Update(request.ConvertToDbModel());

                return Redirect(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
            catch (Exception)
            {
                throw;
            }

            return View(request);
        }

         // GET: KategoriProduks/Delete/5
        public IActionResult Delete()
        {
            return View(new KategoriViewModel());
        }

        // POST: KategoriProduks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(KategoriViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            try
            {
                await _kategoriService.Delete(request.IdKategori);

                return Redirect(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }
            catch (Exception)
            {
                throw;
            }

            return View(request);
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