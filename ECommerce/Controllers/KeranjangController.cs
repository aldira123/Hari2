using Microsoft.AspNetCore.Mvc;
using eCommerce.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using eCommerce.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using eCommerce.ViewModels;
using eCommerce.Datas;
using eCommerce.Datas.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace eCommerce.Controllers;
[Authorize(Roles = AppConstant.CUSTOMER)]

public class KeranjangController : Controller
{
    private readonly IKeranjangService _keranjangService;
    private readonly IAlamatService _alamatService;
    private readonly IAkunService _akunService;
    private readonly IProdukService _produkService;
    private readonly eCommerceDbContext _dbContext;
    private readonly ILogger<KeranjangController> _logger;

    public KeranjangController(ILogger<KeranjangController> logger,
    IKeranjangService keranjangService, IAlamatService alamatService,
    IAkunService akunService,eCommerceDbContext dbContext, IProdukService produkService)
    {
        _logger = logger;
        _keranjangService = keranjangService;
        _alamatService = alamatService;
        _akunService = akunService;
        _dbContext = dbContext;
        _produkService = produkService;

    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (HttpContext.User == null || HttpContext.User.Identity == null)
        {
            ViewBag.IsLogged = false;
        }
        else
        {
            ViewBag.IsLogged = HttpContext.User.Identity.IsAuthenticated;
        }

        base.OnActionExecuted(context);
    }

    public async Task<IActionResult> Index()
    {
        await SetAlamatDataSource();
        int idCustomer = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt();
        var result = await _keranjangService.GetId(idCustomer);
        // var alamat = await _akunService.GetAlamat(idCustomer);
        return View(result);
    }

     private async Task SetAlamatDataSource()
    {
        var alamatViewModels = await _alamatService.GetId
        (HttpContext.User.Claims.FirstOrDefault(x=>x.Type == ClaimTypes.NameIdentifier).Value.ToInt());

        ViewBag.AlamatDataSource = alamatViewModels.Select(x => new SelectListItem
        {
            Value = x.IdAlamat.ToString(),
            Text = x.Detail,
            Selected = false
        }).ToList();
    }
    private async Task SetAlamatDataSource(int[] alamat)
    {
        var alamatViewModels = await _alamatService.GetId
        (HttpContext.User.Claims.FirstOrDefault(x=>x.Type == ClaimTypes.NameIdentifier).Value.ToInt());

        ViewBag.AlamatDataSource = alamatViewModels.Select(x => new SelectListItem
        {
            Value = x.IdAlamat.ToString(),
            Text = x.Detail,
            Selected = alamat.FirstOrDefault(y => y == x.IdAlamat) == 0 ? false : true
        }).ToList();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(int? produkId, ProdukCustomerViewModel request)
    {
        if (produkId == null)
        {
            return BadRequest();
        }

        await _keranjangService.Add(new Datas.Entities.Keranjang
        {
            IdProduk = produkId.Value,
            JumlahBarang = request.JumlahBarang,
            IdCustomer = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt(),
        });

        return RedirectToAction(nameof(Index));
    }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int IdProduk, int JumlahBarang)
        {
            var keranjang = await _dbContext.Keranjangs.FirstOrDefaultAsync(x=> x.IdProduk == IdProduk);
            var produk = await _dbContext.Produks.FirstOrDefaultAsync(x=> x.IdProduk == keranjang.IdProduk);

            keranjang.JumlahBarang = JumlahBarang;
            keranjang.Subtotal = JumlahBarang * produk.HargaProduk;
            await _dbContext.SaveChangesAsync();


            return Redirect(nameof(Index));
        }

     public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return BadRequest();
        }
        var delete = await _keranjangService.Get(id.Value);
        if (delete == null)
        {
            return NotFound();
        }
        return View(new KeranjangViewModel(delete));
    }

    // POST: KategoriProduks/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int? id, KeranjangViewModel request)
    {
        if (id == null)
        {
            return BadRequest();
        }
        try
        {
            await _keranjangService.Delete(id.Value);

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
      

}