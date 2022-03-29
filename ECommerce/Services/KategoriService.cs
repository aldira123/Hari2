using eCommerce.Interface;
using eCommerce.Datas;
using eCommerce.Datas.Entities;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Services;
public class KategoriService : BaseDbService, IKategoriService
{
    public KategoriService(eCommerceDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Kategori> Add(Kategori obj)
    {
        if(await DbContext.Kategoris.AnyAsync(x=>x.IdKategori == obj.IdKategori)){
            throw new InvalidOperationException($"Kategori with ID {obj.IdKategori} is already exist");
        }

        await DbContext.AddAsync(obj);
        await DbContext.SaveChangesAsync();

        return obj;
    }

    public async Task<bool> Delete(int id)
    {
        var kategori = await DbContext.Kategoris.FirstOrDefaultAsync(x=>x.IdKategori == id);

        if(kategori == null) {
            throw new InvalidOperationException($"Kategori with ID {id} doesn't exist");
        }

        DbContext.Remove(kategori);
        await DbContext.SaveChangesAsync();

        return true;
    }

    public async Task<List<Kategori>> Get(int limit, int offset, string keyword)
    {
        if(string.IsNullOrEmpty(keyword)){
            keyword = "";
        }

        return await DbContext.Kategoris
        .Skip(offset)
        .Take(limit).ToListAsync();
    }

    public async Task<Kategori?> Get(int id)
    {
        var result = await DbContext.Kategoris.FirstOrDefaultAsync(x=>x.IdKategori == id);

        if(result == null)
        {
            throw new InvalidOperationException($"Kategori with ID {id} doesn't exist");
        }

        return result;
    }

    public Task<Kategori?> Get(Expression<Func<Kategori, bool>> func)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Kategori>> GetAll()
    {
        return await DbContext.Kategoris.ToListAsync();
    }

    public async Task<Kategori> Update(Kategori obj)
    {
        if(obj == null)
        {
            throw new ArgumentNullException("Kategori cannot be null");
        }

        var kategori = await DbContext.Kategoris.FirstOrDefaultAsync(x=>x.IdKategori == obj.IdKategori);

        if(kategori == null) {
            throw new InvalidOperationException($"Kategori with ID {obj.IdKategori} doesn't exist in database");
        }

        kategori.NamaKategori = obj.NamaKategori;
        kategori.DeskripsiKategori = obj.DeskripsiKategori;
        kategori.Icon = obj.Icon;
        
        DbContext.Update(kategori);
        await DbContext.SaveChangesAsync();

        return kategori;
    }
}