using Microsoft.AspNetCore.Mvc;
using eCommerce.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using eCommerce.Helpers;

namespace eCommerce.Controllers;
[Authorize(Roles = AppConstant.CUSTOMER)]

public class KeranjangController : Controller
{
    private readonly IKeranjangService _keranjangService;
    private readonly ILogger<KeranjangController> _logger;

    public KeranjangController(ILogger<KeranjangController> logger,
    IKeranjangService keranjangService)
    {
        _logger = logger;
        _keranjangService = keranjangService;
       
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

    public async Task<IActionResult> Index(){

        var result = await _keranjangService.Get(HttpContext.User.Claims.FirstOrDefault(x=>x.Type == ClaimTypes.NameIdentifier).Value.ToInt());
        
        return View(result);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(int? produkId)
    {
        if(produkId == null)
        {
            return BadRequest();
        }

        await _keranjangService.Add(new Datas.Entities.Keranjang
        {
            IdProduk = produkId.Value,
            JumlahBarang = 1,
            IdCustomer = HttpContext.User.Claims.FirstOrDefault(x=>x.Type == ClaimTypes.NameIdentifier).Value.ToInt()
        });

        return RedirectToAction(nameof(Index));
    }
 
}