using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using eCommerce.Models;
using eCommerce.ViewModels;
using Microsoft.EntityFrameworkCore;
using eCommerce.Interface;
using eCommerce.Datas.Entities;
using eCommerce.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eCommerce.Controllers;

public class KategoriController : Controller
{
    private readonly IKategoriService _kategoriService;
    private readonly ILogger<KategoriController> _logger;
    private readonly IWebHostEnvironment _iWebHost;

    public KategoriController(ILogger<KategoriController> logger, 
    IKategoriService kategoriService, IWebHostEnvironment iWebHost)
    {
        _logger = logger;
        _kategoriService = kategoriService;
        _iWebHost = iWebHost;
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

    public async Task<IActionResult> Details(int? id) {
       if (id == null){
                return BadRequest();
            }
            var detail = await _kategoriService.Get(id.Value);
             if (detail == null){
                return NotFound();
            }
           return View(new KategoriViewModel(detail));
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
             string fileName = string.Empty;
            
            if(request.IconFile != null) 
            {
                fileName = $"{Guid.NewGuid()}-{request.IconFile?.FileName}";

                string filePathName = _iWebHost.WebRootPath + $"/images/{fileName}";
                
                using(var streamWriter = System.IO.File.Create(filePathName)){
                    // await streamWriter.WriteAsync(Common.StreamToBytes(request.IconFile.OpenReadStream()));
                    //using extension to convert stream to bytes
                    await streamWriter.WriteAsync(request.IconFile.OpenReadStream().ToBytes());
                }
            }

            var kategori = request.ConvertToDbModel();
            kategori.Icon = $"images/{fileName}";

            await _kategoriService.Add(kategori);

            return Redirect(nameof(Index));   
        }catch(InvalidOperationException ex){
            ViewBag.ErrorMessage = ex.Message;
        }
        catch(Exception) {
            throw;
        }

        return View(request);
    }

    public async Task<IActionResult> Edit(int? id)
        {
            if (id == null){
                return BadRequest();
            }
            var update = await _kategoriService.Get(id.Value);
             if (update == null){
                return NotFound();
            }
           return View(new KategoriViewModel(update));
            
        }

        // POST: KategoriProduks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, KategoriViewModel request)
        {
             if (id == null){
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(request);
            }
            try
            {
                 await _kategoriService.Update(request.ConvertToDbModel());

                return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null){
                return BadRequest();
            }
            var delete = await _kategoriService.Get(id.Value);
             if (delete == null){
                return NotFound();
            }
           return View(new KategoriViewModel(delete));
        }

        // POST: KategoriProduks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, KategoriViewModel request)
        {
            if (id == null )
            {
                return BadRequest();
            }
            try
            {
                await _kategoriService.Delete(id.Value);

                return RedirectToAction(nameof(Index));
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