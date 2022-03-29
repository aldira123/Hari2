#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eCommerce.Datas;
using eCommerce.Datas.Entities;
using eCommerce.ViewModels;
using eCommerce.Interface;

namespace eCommerce.Controllers
{
    public class ProdukController : Controller
    {
        private readonly IProdukService _produkService;
        private readonly IKategoriService _kategoriService;
        private readonly ILogger<ProdukController> _logger;


        public ProdukController(ILogger<ProdukController> logger, IProdukService produkService, IKategoriService kategoriService)
        {
           _logger = logger;
           _produkService = produkService;
           _kategoriService = kategoriService;
        }

        // GET: Produk
        public async Task<IActionResult> Index()
        {
            var dbResult = await _produkService.GetAll();

        var viewModels = new List<ProdukViewModel>();

        for (int i = 0; i < dbResult.Count; i++)
        {
            viewModels.Add(new ProdukViewModel{
                IdProduk = dbResult[i].IdProduk,
                NamaProduk = dbResult[i].NamaProduk,
                DeskripsiProduk = dbResult[i].DeskripsiProduk,
                Gambar = dbResult[i].Gambar,
                HargaProduk = dbResult[i].HargaProduk,
                Stok = dbResult[i].Stok
            });
        }
         return View(viewModels);
    }

        // GET: Produk/Details/5
        // public async Task<IActionResult> Details(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var produk = await _context.Produks
        //         .FirstOrDefaultAsync(m => m.IdProduk == id);
        //     if (produk == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(produk);
        // }

        //Transfer data list of kategori ke view dimasukan dalam selectlistitem
    private async Task SetKategoriDataSource()
    {
        var kategoriViewModels = await _kategoriService.GetAll();

        ViewBag.KategoriDataSource = kategoriViewModels.Select(x => new SelectListItem
        {
            Value = x.IdKategori.ToString(),
            Text = x.NamaKategori,
            Selected = false
        }).ToList();
    }

        // GET: Produk/Create
        public async Task<IActionResult> Create()
        {
            await SetKategoriDataSource();
            return View(new ProdukViewModel());
        }

        // POST: Produk/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
    public async Task<IActionResult> Create(ProdukViewModel request) {
        if(!ModelState.IsValid){
            await SetKategoriDataSource();
            return View(request);
        }
        try{

            var product = request.ConvertToDbModel();

            //Insert to ProdukKategori table
            product.ProdukKategoris.Add(new Datas.Entities.ProdukKategori 
            {
                IdKategori = request.KategoriId,
                IdProduk = product.IdProduk
            });

            await _produkService.Add(product);

            return Redirect(nameof(Index));
        }catch(InvalidOperationException ex){
            ViewBag.ErrorMessage = ex.Message;
        }
        catch(Exception) {
            throw;
        }


        await SetKategoriDataSource();
        return View(request);
    }
        // GET: Produk/Edit/5
        // public async Task<IActionResult> Edit(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var produk = await _context.Produks.FindAsync(id);
        //     if (produk == null)
        //     {
        //         return NotFound();
        //     }
        //     return View(produk);
        // }

        // POST: Produk/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(int id, [Bind("IdProduk,NamaProduk,DeskripsiProduk,HargaProduk,Stok,Gambar")] Produk produk)
        // {
        //     if (id != produk.IdProduk)
        //     {
        //         return NotFound();
        //     }

        //     if (ModelState.IsValid)
        //     {
        //         try
        //         {
        //             _context.Update(produk);
        //             await _context.SaveChangesAsync();
        //         }
        //         catch (DbUpdateConcurrencyException)
        //         {
        //             if (!ProdukExists(produk.IdProduk))
        //             {
        //                 return NotFound();
        //             }
        //             else
        //             {
        //                 throw;
        //             }
        //         }
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(produk);
        // }

        // GET: Produk/Delete/5
        // public async Task<IActionResult> Delete(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var produk = await _context.Produks
        //         .FirstOrDefaultAsync(m => m.IdProduk == id);
        //     if (produk == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(produk);
        // }

        // POST: Produk/Delete/5
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> DeleteConfirmed(int id)
        // {
        //     var produk = await _context.Produks.FindAsync(id);
        //     _context.Produks.Remove(produk);
        //     await _context.SaveChangesAsync();
        //     return RedirectToAction(nameof(Index));
        // }

        // private bool ProdukExists(int id)
        // {
        //     return _context.Produks.Any(e => e.IdProduk == id);
        // }
    }
}
