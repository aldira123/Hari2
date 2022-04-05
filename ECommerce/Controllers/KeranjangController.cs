using Microsoft.AspNetCore.Mvc;
using eCommerce.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using eCommerce.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using eCommerce.ViewModels;

namespace eCommerce.Controllers;
[Authorize(Roles = AppConstant.CUSTOMER)]

public class KeranjangController : Controller
{
    private readonly IKeranjangService _keranjangService;
    private readonly IAlamatService _alamatService;
    private readonly IAkunService _akunService;
    private readonly ILogger<KeranjangController> _logger;

    public KeranjangController(ILogger<KeranjangController> logger,
    IKeranjangService keranjangService, IAlamatService alamatService,
    IAkunService akunService)
    {
        _logger = logger;
        _keranjangService = keranjangService;
        _alamatService = alamatService;
        _akunService = akunService;

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
        var result = await _keranjangService.Get(idCustomer);
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
    public async Task<IActionResult> Add(int? produkId)
    {
        if (produkId == null)
        {
            return BadRequest();
        }

        await _keranjangService.Add(new Datas.Entities.Keranjang
        {
            IdProduk = produkId.Value,
            JumlahBarang = 1,
            IdCustomer = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt(),
        });

        return RedirectToAction(nameof(Index));
    }

}