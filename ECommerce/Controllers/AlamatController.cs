using Microsoft.AspNetCore.Mvc;
using eCommerce.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using eCommerce.Helpers;
using eCommerce.ViewModels;

namespace eCommerce.Controllers;
[Authorize(Roles = AppConstant.CUSTOMER)]

public class AlamatController : Controller
{
    private readonly IAlamatService _alamatService;
    private readonly ILogger<AlamatController> _logger;

    public AlamatController(ILogger<AlamatController> logger,
    IAlamatService alamatService)
    {
        _logger = logger;
        _alamatService = alamatService;
       
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

        var result = await _alamatService.Get
        (HttpContext.User.Claims.FirstOrDefault(x=>x.Type == ClaimTypes.NameIdentifier).Value.ToInt());
        
        return View(result);
    }

     public IActionResult Create()
    {
        return View(new AlamatViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AlamatViewModel request)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }
        try
        {  
            await _alamatService.Add(new Datas.Entities.Alamat{
                IdAlamat = request.IdAlamat,
                IdCustomer =  HttpContext.User.Claims.FirstOrDefault(x=>x.Type == ClaimTypes.NameIdentifier).Value.ToInt(),
                Kecamatan = request.Kecamatan,
                Kelurahan = request.Kelurahan,
                Rt = request.Rt,
                Rw = request.Rw,
                KodePos = request.KodePos,
               Detail= request.Detail,

            });

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


    //  public async Task<IActionResult> Edit(int? id)
    // {
    //     if (id == null)
    //     {
    //         return BadRequest();
    //     }
    //     var update = await _alamatService.Get()
    //     if (update == null)
    //     {
    //         return NotFound();
    //     }
    //     return View(new AlamatViewModel(update));

    // }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, AlamatViewModel request)
    {
        if (id == null)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(request);
        }


        try
        {
            var alamat = request.ConvertToDbModel();
            await _alamatService.Update(alamat);
            
            // await _alamatService.Update(new Datas.Entities.Alamat{
            //     IdAlamat = request.IdAlamat,
            //     IdCustomer =  HttpContext.User.Claims.FirstOrDefault(x=>x.Type == ClaimTypes.NameIdentifier).Value.ToInt(),
            //     Kecamatan = request.Kecamatan,
            //     Kelurahan = request.Kelurahan,
            //     Rt = request.Rt,
            //     Rw = request.Rw,
            //     KodePos = request.KodePos,
            //    Detail= request.Detail,

            // });


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

    //  public async Task<IActionResult> Delete(int? id)
    // {
    //     if (id == null)
    //     {
    //         return BadRequest();
    //     }
    //     var delete = await _alamatService.Get(id.Value);
    //     if (delete == null)
    //     {
    //         return NotFound();
    //     }
    //     return View(new KategoriViewModel(delete));
    // }

    // POST: KategoriProduks/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int? id, KategoriViewModel request)
    {
        if (id == null)
        {
            return BadRequest();
        }
        try
        {
            await _alamatService.Delete(id.Value);

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