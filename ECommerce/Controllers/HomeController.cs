using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eCommerce.Datas.Entities;
using eCommerce.Models;
using eCommerce.Datas;
using eCommerce.ViewModels;

namespace eCommerce.Controllers;

public class HomeController : Controller
{
    private readonly eCommerce.Datas.eCommerceDbContext _dbContext;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, eCommerce.Datas.eCommerceDbContext dbContext)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        // var dbResult = await _dbContext.KategoriProduks.ToListAsync();
        var dbResult = await _dbContext.KategoriProduks.Select(x => new KategoriViewModel {
            NamaKategori = x.NamaKategori,
            DeskripsiKategori = x.DeskripsiKategori
        }).ToListAsync();
        return View(dbResult);
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
