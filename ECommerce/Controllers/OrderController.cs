using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eCommerce.Models;
using eCommerce.Interface;
using System.Security.Claims;
using eCommerce.Helpers;
using eCommerce.ViewModels;
using eCommerce.Datas.Entities;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;

namespace eCommerce.Controllers;

[Authorize(Roles = AppConstant.CUSTOMER)]

public class OrderController : Controller
{
    private readonly ILogger<OrderController> _logger;
    private readonly IOrderService _orderService;
    private readonly IKeranjangService _keranjangService;
    

    public OrderController(ILogger<OrderController> logger, 
    IOrderService orderService, IKeranjangService keranjangService)
    {
        _logger = logger;
        _orderService = orderService;
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

    public async Task<IActionResult> Index()
    {
        var result = await _orderService.Get(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt());

        return View(result);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout(CheckoutViewModel? request)
    {
        if(request == null)
        {
            return BadRequest();
        }

        if(request.Alamat == 0)
        {
            return BadRequest();
        }

        int idCustomer = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt();
        var result = await _keranjangService.Get(idCustomer);

        if(result == null || !result.Any())
        {
            return BadRequest();
        }

        foreach (var item in result)
        {
            int keranjangId = request.Id.FirstOrDefault(x=> item.IdKeranjang == x);
            
            if(keranjangId < 1)
            {
                continue;
            }
            int jumlahBarangBaru = request.Qty[Array.IndexOf(request.Id, keranjangId)];

            item.JumlahBarang = jumlahBarangBaru;
            item.Subtotal = item.HargaBarang * jumlahBarangBaru;
        }

        var newOrder = new Order();

        newOrder.IdCustomer = idCustomer;
        newOrder.JumlahBayar = result.Sum(x=>x.Subtotal);
        newOrder.Catatan = string.Empty;
        newOrder.Status = 1;
        newOrder.IdAlamat = request.Alamat;
        newOrder.TglTransaksi = DateTime.Now;
        newOrder.DetailOrders = new List<DetailOrder>();

        foreach(var item in result)
        {
            newOrder.DetailOrders.Add(new DetailOrder
            {
                IdOrder = newOrder.IdOrder,
                Harga = item.HargaBarang,
                JumlahBarang = item.JumlahBarang,
                SubTotal = item.Subtotal,
                IdProduk = item.IdProduk
            });
        }
        
        await _orderService.Checkout(newOrder);

        await _keranjangService.Clear(idCustomer);

        return RedirectToAction(nameof(CheckoutBerhasil));
    }

    public IActionResult CheckoutBerhasil(){
        return View();
    }

    
}
