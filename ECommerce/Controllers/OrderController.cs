using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eCommerce.Models;
using eCommerce.Interface;
using System.Security.Claims;
using eCommerce.Helpers;
using eCommerce.ViewModels;
using eCommerce.Datas.Entities;
using eCommerce.Datas;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eCommerce.Controllers;

[Authorize]

public class OrderController : BaseController
{
    private readonly ILogger<OrderController> _logger;
    private readonly IOrderService _orderService;
    private readonly IKeranjangService _keranjangService;
    private readonly IStatusService _statusService;
    private readonly IWebHostEnvironment _iWebHost;
    private readonly eCommerceDbContext _dbContext;


    public OrderController(ILogger<OrderController> logger,
    IOrderService orderService, IKeranjangService keranjangService,
    IStatusService statusService, IWebHostEnvironment iWebHost,
    eCommerceDbContext dbContext )
    {
        _logger = logger;
        _orderService = orderService;
        _keranjangService = keranjangService;
        _statusService = statusService;
        _iWebHost = iWebHost;
        _dbContext = dbContext;

    }

    [Authorize(Roles = AppConstant.ADMIN)]
    public async Task<IActionResult> Index(int? page, int? pageCount)
    {
        var tuplePagination = Common.ToLimitOffset(page, pageCount);

        var result = await _orderService.GetV1(tuplePagination.Item1, tuplePagination.Item2);

        await SetStatusListAsSelectListItem();
        ViewBag.FilterDate = null;

        return View(result);
    }

    [HttpPost]
    public async Task<IActionResult> Index([FromQuery] int? page, [FromQuery] int? pageCount, int? status, DateTime? date)
    {
        var tuplePagination = Common.ToLimitOffset(page, pageCount);

        var result = await _orderService.GetV1(tuplePagination.Item1, tuplePagination.Item2, status, date);

        await SetStatusListAsSelectListItem(status);
        if (date != null)
        {
            ViewBag.FilterDate = date.Value.ToString("MM/dd/yyyy");
        }
        return View(result);
    }

    private async Task SetStatusListAsSelectListItem(int? status = null)
    {
        var statusList = await _statusService.Get();

        if (statusList == null || !statusList.Any())
        {
            ViewBag.StatusList = new List<SelectListItem>();
        }
        else
        {
            ViewBag.StatusList = statusList.Select(x => new SelectListItem
            {
                Value = x.IdStatus.ToString(),
                Text = x.Nama,
                Selected = status != null && status.Value == x.IdStatus
            }).ToList();
        }
    }

    [Authorize(Roles = AppConstant.CUSTOMER)]
    public async Task<IActionResult> MyOrder()
    {
        var result = await _orderService.Get(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt());

        return View(result);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout(CheckoutViewModel? request)
    {
        if (request == null)
        {
            return BadRequest();
        }

        if (request.Alamat == 0)
        {
            return BadRequest();
        }

        int idCustomer = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt();
        var result = await _keranjangService.GetId(idCustomer);

        if (result == null || !result.Any())
        {
            return BadRequest();
        }

        foreach (var item in result)
        {
            int keranjangId = request.Id.FirstOrDefault(x => item.IdKeranjang == x);

            if (keranjangId < 1)
            {
                continue;
            }
            int jumlahBarangBaru = request.Qty[Array.IndexOf(request.Id, keranjangId)];

            item.JumlahBarang = jumlahBarangBaru;
            item.Subtotal = item.HargaBarang * jumlahBarangBaru;
        }

        var newOrder = new Order();

        newOrder.IdCustomer = idCustomer;
        newOrder.JumlahBayar = result.Sum(x => x.Subtotal);
        newOrder.Catatan = string.Empty;
        newOrder.Status = 1;
        newOrder.IdAlamat = request.Alamat;
        newOrder.TglTransaksi = DateTime.Now;
        newOrder.DetailOrders = new List<DetailOrder>();

        foreach (var item in result)
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

    public IActionResult CheckoutBerhasil()
    {
        return View();
    }

    [Authorize(Roles = AppConstant.CUSTOMER)]
    public async Task<IActionResult> DetailOrder(int? id)
    {
        if (id == null){
            return BadRequest();
        }

        var produk = await _orderService.GetDetail(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt(),id.Value);

        if (produk == null)
        {
            return NotFound();
        }

        return View(produk);
    }

    [Authorize(Roles = AppConstant.ADMIN)]
    public async Task<IActionResult> DetailAdmin(int? id)
    {
        if (id == null){
            return BadRequest();
        }
        var produk = await _orderService.GetDetail(id.Value);

        if (produk == null)
        {
            return NotFound();
        }

        return View(produk);
       
    }
    [HttpPost]
    public async Task<IActionResult> Konfirmasi(int? IdOrder) {
        if(IdOrder == null)
        {
            return BadRequest();
        }
        
        //SOLID Principle
        if(!await _orderService.SudahDibayar(IdOrder.Value))
        {
            return BadRequest();   
        }

        await _orderService.UpdateStatus(IdOrder.Value, AppConstant.StatusOrder.DIPROSES);

        return RedirectToAction(nameof(DetailAdmin), new
        {
            id = IdOrder.Value
        });
    }

     public async Task<IActionResult> Bayar(BayarRequestViewModel request)
    {
        

        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(DetailOrder), new
            {
                id = request.IdOrder
            });
        }

        //Simpan file
        string fileName = string.Empty;
        int idCustomer = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt();
       
        if (request.BuktiPembayaran != null)
        {
            fileName = $"{Guid.NewGuid()}-{request.BuktiPembayaran?.FileName}";

            string filePathName = _iWebHost.WebRootPath + $"/images/{fileName}";

            using (var streamWriter = System.IO.File.Create(filePathName))
            {
                //await streamWriter.WriteAsync(Common.StreamToBytes(request.GambarFile.OpenReadStream()));
                //using extension to convert stream to bytes
                await streamWriter.WriteAsync(request.BuktiPembayaran.OpenReadStream().ToBytes());
            }
        }

        Pembayaran newBayar = new Pembayaran()
        {
            BuktiPembayaran = $"images/{fileName}",
            IdCustomer = idCustomer,
            IdOrder = request.IdOrder,
            TanggalBayar = request.TanggalBayar,
            Tujuan = request.Tujuan,
            JumlahBayar = request.JumlahBayar,
            MetodePembayaran = request.MetodePembayaran,
            Catatan = request.Catatan,
            Pajak = 11000,
            Status = string.Empty
        };

        await _orderService.Bayar(newBayar);

        await _orderService.UpdateStatus(request.IdOrder, AppConstant.StatusOrder.DIBAYAR);

        return RedirectToAction(nameof(DetailOrder), new
        {
            id = request.IdOrder
        });
    }

    public async Task<IActionResult> Review(UlasanRequestViewModel request)
    {
        int idCustomer = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value.ToInt();

        string fileName = string.Empty;
        
        if (request.Gambar != null)
        {
            fileName = $"{Guid.NewGuid()}-{request.Gambar?.FileName}";

            string filePathName = _iWebHost.WebRootPath + "\\images\\" + fileName;

            using (var streamWriter = System.IO.File.Create(filePathName))
            {
                //await streamWriter.WriteAsync(Common.StreamToBytes(request.GambarFile.OpenReadStream()));
                //using extension to convert stream to bytes
                await streamWriter.WriteAsync(request.Gambar.OpenReadStream().ToBytes());
            }
        }

        Ulasan ulasan = new Ulasan{
            IdOrder = request.IdOrder,
            IdCustomer = idCustomer,
            Komentar = request.Komentar,
            Rating = request.Rating,
            Gambar = string.IsNullOrEmpty(fileName) ? string.Empty : "images/" + fileName
        };


        await _orderService.Ulas(ulasan);

        await _orderService.UpdateStatus(request.IdOrder, AppConstant.StatusOrder.DITERIMA);

        return RedirectToAction(nameof(DetailOrder), new {
            id = request.IdOrder
        });
    }

 public async Task<IActionResult> Kirim(KirimRequestViewModel request){
        if(!ModelState.IsValid)
        {
            return BadRequest();
        }

        Pengiriman dataPengiriman = new Pengiriman
        {
            IdOrder = request.IdOrder,
            IdAlamat = request.IdAlamat,
            Kurir = request.Kurir,
            Noresi = request.NoResi,
            Ongkir = request.Ongkir,
            Status = string.Empty,
            Keterangan = request.Keterangan
        };

        await _orderService.Kirim(dataPengiriman);

        await _orderService.UpdateStatus(dataPengiriman.IdOrder, AppConstant.StatusOrder.DIKIRIM);

        return RedirectToAction(nameof(DetailAdmin), new {
            id = request.IdOrder
        });
    }
}
