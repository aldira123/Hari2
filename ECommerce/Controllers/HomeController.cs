using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eCommerce.Models;
using eCommerce.Interface;
using eCommerce.Datas;
using eCommerce.ViewModels;
using Microsoft.AspNetCore.Mvc.Filters;

namespace eCommerce.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProdukService _produkService;
    private readonly IKategoriService _kategoriService;
    

    public HomeController(ILogger<HomeController> logger, IProdukService produkService,
        IKategoriService kategoriService)
    {
        _logger = logger;
        _produkService = produkService;
         _kategoriService = kategoriService;

    }

    public async  Task<IActionResult> Produk(int? page, int? pageCount){
        var viewModels = new List<ProdukCustomerViewModel>();
             var dbResult = await _produkService.Get(pageCount??10, (page??1 - 1) * (pageCount??10), string.Empty);

            for (int i = 0; i < dbResult.Count; i++)
            {
                viewModels.Add(new ProdukCustomerViewModel
                {
                    IdProduk = dbResult[i].IdProduk,
                    NamaProduk = dbResult[i].NamaProduk,
                    DeskripsiProduk = dbResult[i].DeskripsiProduk,
                    Gambar = dbResult[i].Gambar,
                    HargaProduk = dbResult[i].HargaProduk,
                    Kategories = dbResult[i].ProdukKategoris.Select(x => new KategoriViewModel
                    {
                        IdKategori = x.IdKategori,
                        NamaKategori = x.IdKategoriNavigation.NamaKategori,
                        Icon = x.IdKategoriNavigation.Icon
                    }).ToList()
                });
            }
            return View(viewModels);
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if(HttpContext.User == null || HttpContext.User.Identity == null){
            ViewBag.IsLogged = false;
        } else {
            ViewBag.IsLogged = HttpContext.User.Identity.IsAuthenticated;
        }

        base.OnActionExecuted(context);
    }

    public async Task<IActionResult> Index(int? page, int? pageCount)
    {

            var viewModels = new List<ProdukCustomerViewModel>();
             var dbResult = await _produkService.Get(pageCount??10, (page??1 - 1) * (pageCount??10), string.Empty);

            for (int i = 0; i < dbResult.Count; i++)
            {
                viewModels.Add(new ProdukCustomerViewModel
                {
                    IdProduk = dbResult[i].IdProduk,
                    NamaProduk = dbResult[i].NamaProduk,
                    DeskripsiProduk = dbResult[i].DeskripsiProduk,
                    Gambar = dbResult[i].Gambar,
                    HargaProduk = dbResult[i].HargaProduk,
                    Kategories = dbResult[i].ProdukKategoris.Select(x => new KategoriViewModel
                    {
                        IdKategori = x.IdKategori,
                        NamaKategori = x.IdKategoriNavigation.NamaKategori,
                        Icon = x.IdKategoriNavigation.Icon
                    }).ToList()
                });
            }
            return View(viewModels);
    }

    //page = Halaman
    //PageCount = Jumlah data yang ditampilkan per halaman
    public async Task<IActionResult> Kategori(int? page, int? pageCount)
    {
         var viewModels = new List<KategoriCustomerViewModel>();
         //Nilai limit +2
         int limit = pageCount ?? 2;
         int offset = 0;
         if(page == null){
             offset = 0;
         }else {
             offset = (page.Value - 1) * limit;
         }
         var dbResult = await _kategoriService.Get(limit, offset, string.Empty);

         if(dbResult == null || !dbResult.Any())
        {
            return RedirectToAction(nameof(Kategori), new {
                page = page > 1 ? page - 1 : 1,
                pageCount = pageCount
            });
        }

            

            for (int i = 0; i < dbResult.Count; i++)
            {
                viewModels.Add(new KategoriCustomerViewModel
                {
                    NamaKategori = dbResult[i].NamaKategori,
                    DeskripsiKategori = dbResult[i].DeskripsiKategori,
                    Icon = dbResult[i].Icon,
                });
            }
            ViewBag.HalamanSekarang = page ?? 1;
            return View(viewModels);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public async Task<IActionResult> ProdukDetail(int? id)
    {
         if(id == null) 
        {
            return NotFound();
        }

        var produk = await _produkService.Get(id.Value);

        if (produk == null)
        {
            return NotFound();
        }

        return View(new ProdukCustomerViewModel()
        {
            IdProduk =  produk.IdProduk,
            NamaProduk= produk.NamaProduk,
            DeskripsiProduk = produk.DeskripsiProduk,
            HargaProduk = produk.HargaProduk,
            Gambar = produk.Gambar,
            Stok = 100
        });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
