using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using eCommerce.Models;
using eCommerce.ViewModels;
using Microsoft.EntityFrameworkCore;
using eCommerce.Interface;
using eCommerce.Datas.Entities;
using eCommerce.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace eCommerce.Controllers;



public class AdminController : Controller
{
    private readonly IAdminService _adminService;
    private readonly ILogger<AdminController> _logger;


    public AdminController(ILogger<AdminController> logger,
    IAdminService adminService)
    {
        _logger = logger;
        _adminService = adminService;
    }

    public async Task<IActionResult> Index()
    {
        var dbResult = await _adminService.GetAll();

        var viewModels = new List<AdminViewModel>();

        for (int i = 0; i < dbResult.Count; i++)
        {
            viewModels.Add(new AdminViewModel
                {
                    IdAdmin = dbResult[i].IdAdmin,
                    Nama = dbResult[i].Nama,
                    NoHp = dbResult[i].NoHp,
                    Username = dbResult[i].Username,
                    Password = dbResult[i].Password,
                    Email = dbResult[i].Email,
                });
        }

        return View(viewModels);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return BadRequest();
        }
        var detail = await _adminService.Get(id.Value);
        if (detail == null)
        {
            return NotFound();
        }
        return View(new AdminViewModel(detail));
    }

    public IActionResult Create()
    {
        return View(new AdminViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(AdminViewModel request)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }
        try
        {
            var admin = request.ConvertToDbModel();
            await _adminService.Add(admin);

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

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return BadRequest();
        }
        var update = await _adminService.Get(id.Value);
        if (update == null)
        {
            return NotFound();
        }
        return View(new AdminViewModel(update));

    }

    // POST: KategoriProduks/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, AdminViewModel request)
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
            await _adminService.Update(request.ConvertToDbModel());

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
        if (id == null)
        {
            return BadRequest();
        }
        var delete = await _adminService.Get(id.Value);
        if (delete == null)
        {
            return NotFound();
        }
        return View(new AdminViewModel(delete));
    }

    // POST: KategoriProduks/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int? id, AdminViewModel request)
    {
        if (id == null)
        {
            return BadRequest();
        }
        try
        {
            await _adminService.Delete(id.Value);

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