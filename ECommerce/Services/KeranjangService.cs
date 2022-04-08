using eCommerce.Interface;
using eCommerce.Datas;
using eCommerce.Datas.Entities;
using eCommerce.ViewModels;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Services;
public class KeranjangService : BaseDbService, IKeranjangService
{
    private readonly IProdukService _produkService;
    public KeranjangService(eCommerceDbContext dbContext, IProdukService produkService) : base(dbContext)
    {
        _produkService = produkService;
    }

    public async Task<Keranjang> Add(Keranjang obj)
    {
       if(await DbContext.Keranjangs.AnyAsync(x=>x.IdProduk == obj.IdProduk && x.IdCustomer == obj.IdCustomer))
        {
            return obj;
        }

        //get data produk
        var produk = await _produkService.Get(obj.IdProduk);

        if(produk == null)
        {
            throw new InvalidOperationException("Produk tidak ditemukan");
        }

        if(obj.JumlahBarang < 1) 
        {
            obj.JumlahBarang = 1;
        }

        //rumus subtotal = harga * jumlah produk
        obj.Subtotal = produk.HargaProduk * obj.JumlahBarang;

        await DbContext.AddAsync(obj);
        await DbContext.SaveChangesAsync();

        return obj;
    }

    public async Task<bool> Delete(int id)
    {
        var Keranjang = await DbContext.Keranjangs.FirstOrDefaultAsync(x=>x.IdKeranjang == id);

        if(Keranjang == null) {
            throw new InvalidOperationException($"Keranjang with ID {id} doesn't exist");
        }

        DbContext.Remove(Keranjang);
        await DbContext.SaveChangesAsync();

        return true;
    }

    public async Task<List<Keranjang>> Get(int limit, int offset, string keyword)
    {
        if(string.IsNullOrEmpty(keyword)){
            keyword = "";
        }

        return await DbContext.Keranjangs
        .Skip(offset)
        .Take(limit).ToListAsync();

      
    }

    public async Task<Keranjang?> Get(int id)
    {
        var result = await DbContext.Keranjangs.FirstOrDefaultAsync(x=>x.IdKeranjang == id);

        if(result == null)
        {
            throw new InvalidOperationException($"Keranjang with ID {id} doesn't exist");
        }

        return result;
    }

    public async Task<Keranjang?> GetProduk(int id)
    {
        var result = await DbContext.Keranjangs.FirstOrDefaultAsync(x=>x.IdProduk == id);
       

        if(result == null)
        {
            throw new InvalidOperationException($"Keranjang with ID {id} doesn't exist");
        }

        return result;
    }

    public Task<Keranjang?> Get(Expression<Func<Keranjang, bool>> func)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Keranjang>> GetAll()
    {
        return await DbContext.Keranjangs.ToListAsync();
    }

    public async Task<Keranjang> Update(Keranjang obj)
    {
        var Keranjang = await DbContext.Keranjangs.FirstOrDefaultAsync(x=>x.IdKeranjang == obj.IdKeranjang);
        if(obj == null)
        {
            throw new ArgumentNullException("Keranjang cannot be null");
        }

        var produk = await _produkService.Get(obj.IdProduk);
       

        if(Keranjang == null) {
            throw new InvalidOperationException($"Keranjang with ID {obj.IdKeranjang} doesn't exist in database");
        }

         if(obj.JumlahBarang < 1) 
        {
            obj.JumlahBarang = 1;
        }

        Keranjang.IdProduk = produk.IdProduk;
        Keranjang.JumlahBarang = obj.JumlahBarang;
        Keranjang.Subtotal = produk.HargaProduk * Keranjang.JumlahBarang;
       
        DbContext.Update(Keranjang);
        await DbContext.SaveChangesAsync();

        return Keranjang;
    }

    async Task<List<KeranjangViewModel>> IKeranjangService.GetId(int idCustomer)
    {
        //Inner Join
        var result = await (from a in DbContext.Keranjangs
        join b in DbContext.Produks on a.IdProduk equals b.IdProduk
        where a.IdCustomer == idCustomer
        select new KeranjangViewModel 
        {
            IdKeranjang = a.IdKeranjang,
            IdCustomer = a.IdCustomer,
            IdProduk = a.IdProduk,
            Image = b.Gambar,
            JumlahBarang  = a.JumlahBarang,
            Subtotal  = a.Subtotal,
            NamaProduk = b.NamaProduk,
            HargaBarang = b.HargaProduk
        }).ToListAsync();

        return result;
    }

    public async Task Clear(int idCustomer)
    {
        DbContext.RemoveRange(DbContext.Keranjangs.Where(x=>x.IdCustomer == idCustomer));
        await DbContext.SaveChangesAsync();
    }

}