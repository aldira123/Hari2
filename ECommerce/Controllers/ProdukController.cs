#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using eCommerce.ViewModels;
using eCommerce.Interface;
using eCommerce.Helpers;
using Microsoft.AspNetCore.Authorization;


namespace eCommerce.Controllers
{
   [Authorize(Roles = AppConstant.ADMIN)]
    public class ProdukController : Controller
    {
        private readonly IProdukService _produkService;
        private readonly IKategoriService _kategoriService;
        private readonly IProdukKategoriService _produkKategoriService;
        private readonly IWebHostEnvironment _iWebHost;
        private readonly ILogger<ProdukController> _logger;


        public ProdukController(ILogger<ProdukController> logger, IProdukService produkService,
        IKategoriService kategoriService, IProdukKategoriService produkKategoriService,
        IWebHostEnvironment iWebHost)
        {
            _logger = logger;
            _iWebHost = iWebHost;
            _produkService = produkService;
            _kategoriService = kategoriService;
            _produkKategoriService = produkKategoriService;
        }

        // GET: Produk
        public async Task<IActionResult> Index()
        {
            var dbResult = await _produkService.GetAll();

            var viewModels = new List<ProdukViewModel>();

            for (int i = 0; i < dbResult.Count; i++)
            {
                viewModels.Add(new ProdukViewModel
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

        // GET: Produk/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produk = await _produkService.Get(id.Value);
            if (produk == null)
            {
                return NotFound();
            }
            var kategoriIds = await _produkKategoriService.GetKategoriIds(produk.IdProduk);

            await SetKategoriDataSource(kategoriIds);
            return View(new ProdukViewModel()
            {
                IdProduk = produk.IdProduk,
                NamaProduk = produk.NamaProduk,
                DeskripsiProduk = produk.DeskripsiProduk,
                HargaProduk = produk.HargaProduk,
                Gambar = produk.Gambar,
                KategoriId = kategoriIds
            });
        }

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

        private async Task SetKategoriDataSource(int[] kategori)
        {
            var kategoriViewModels = await _kategoriService.GetAll();

            ViewBag.KategoriDataSource = kategoriViewModels.Select(x => new SelectListItem
            {
                Value = x.IdKategori.ToString(),
                Text = x.NamaKategori,
                Selected = kategori.FirstOrDefault(y => y == x.IdKategori) == 0 ? false : true
            }).ToList();
        }

        // GET: Produk/Create
        public async Task<IActionResult> Create()
        {
            await SetKategoriDataSource();
            return View(new ProdukViewModel());
        }

        // POST: Produk/Create

        [HttpPost]
        public async Task<IActionResult> Create(ProdukViewModel request)
        {
            if (!ModelState.IsValid)
            {
                await SetKategoriDataSource();
                return View(request);
            }
            if (request == null)
            {
                await SetKategoriDataSource();
                return View(request);
            }
            try
            {


                string fileName = string.Empty;

                if (request.GambarFile != null)
                {
                    fileName = $"{Guid.NewGuid()}-{request.GambarFile?.FileName}";

                    string filePathName = _iWebHost.WebRootPath + $"/images/{fileName}";

                    using (var streamWriter = System.IO.File.Create(filePathName))
                    {
                        //await streamWriter.WriteAsync(Common.StreamToBytes(request.GambarFile.OpenReadStream()));
                        //using extension to convert stream to bytes
                        await streamWriter.WriteAsync(request.GambarFile.OpenReadStream().ToBytes());
                    }
                }

                var product = request.ConvertToDbModel();
                product.Gambar = $"images/{fileName}";

                //Insert to ProdukKategori table
                for (int i = 0; i < request.KategoriId.Length; i++)
                {
                    product.ProdukKategoris.Add(new Datas.Entities.ProdukKategori
                    {
                        IdKategori = request.KategoriId[i],
                        IdProduk = product.IdProduk
                    });
                }


                await _produkService.Add(product);

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


            await SetKategoriDataSource();
            return View(request);
        }
        // GET: Produk/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produk = await _produkService.Get(id.Value);
            if (produk == null)
            {
                return NotFound();
            }
            var kategoriIds = await _produkKategoriService.GetKategoriIds(produk.IdProduk);

            await SetKategoriDataSource(kategoriIds);
            return View(new ProdukViewModel()
            {
                IdProduk = produk.IdProduk,
                NamaProduk = produk.NamaProduk,
                DeskripsiProduk = produk.DeskripsiProduk,
                HargaProduk = produk.HargaProduk,
                Gambar = produk.Gambar,
                KategoriId = kategoriIds
            });
        }

        // POST: Produk/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int? id, ProdukViewModel request)
        {
            if (!ModelState.IsValid)
            {
                await SetKategoriDataSource(request.KategoriId);
                return View(request);
            }

            if (request == null)
            {
                await SetKategoriDataSource();
                return View(request);
            }
          
            try
            {

                string fileName = string.Empty;

                if (request.GambarFile != null)
                {
                    fileName = $"{Guid.NewGuid()}-{request.GambarFile?.FileName}";

                    string filePathName = _iWebHost.WebRootPath + $"/images/{fileName}";

                    using (var streamWriter = System.IO.File.Create(filePathName))
                    {
                        //await streamWriter.WriteAsync(Common.StreamToBytes(request.GambarFile.OpenReadStream()));
                        //using extension to convert stream to bytes
                        await streamWriter.WriteAsync(request.GambarFile.OpenReadStream().ToBytes());
                    }
                } 

                var product = request.ConvertToDbModel();
                if (request.GambarFile != null)
                {
                    product.Gambar = $"images/{fileName}";
                }

                //Update ProdukKategori
                var productKategories = await _produkKategoriService.GetKategoriIds(request.IdProduk);

                for (int i = 0; i < request.KategoriId.Length; i++)
                {
                    if (productKategories != null && productKategories.Any())
                    {
                        if (!productKategories.Any(x => x == request.KategoriId[i]))
                        {
                            product.ProdukKategoris.Add(new Datas.Entities.ProdukKategori
                            {
                                IdKategori = request.KategoriId[i],
                                IdProduk = product.IdProduk
                            });
                        }
                    }
                    else
                    {
                        product.ProdukKategoris.Add(new Datas.Entities.ProdukKategori
                        {
                            IdKategori = request.KategoriId[i],
                            IdProduk = product.IdProduk
                        });
                    }
                }

                //Remove kategori from product
                if (productKategories != null && (product.ProdukKategoris != null && product.ProdukKategoris.Any()))
                {
                    foreach (var item in productKategories)
                    {
                        if (!product.ProdukKategoris.Any(x => x.IdKategori == item))
                        {
                            await _produkKategoriService.Remove(request.IdProduk, item);
                        }
                    }
                }


                await _produkService.Update(product);

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


            await SetKategoriDataSource();
            return View(request);
        }


        // GET: Produk/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produk = await _produkService.Get(id.Value);
            if (produk == null)
            {
                return NotFound();
            }
            var kategoriIds = await _produkKategoriService.GetKategoriIds(produk.IdProduk);

            await SetKategoriDataSource(kategoriIds);
            return View(new ProdukViewModel()
            {
                IdProduk = produk.IdProduk,
                NamaProduk = produk.NamaProduk,
                DeskripsiProduk = produk.DeskripsiProduk,
                HargaProduk = produk.HargaProduk,
                Gambar = produk.Gambar,
                KategoriId = kategoriIds
            });
        }

        // POST: Produk/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id, ProdukViewModel request)
        {
            if (id == null)
            {
                return BadRequest();
            }
            try
            {
                await _produkService.Delete(id.Value);

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
}
