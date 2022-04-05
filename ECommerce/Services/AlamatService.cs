using eCommerce.Interface;
using eCommerce.Datas;
using eCommerce.Datas.Entities;
using eCommerce.ViewModels;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Services;
public class AlamatService : BaseDbService, IAlamatService
{
    public AlamatService(eCommerceDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Alamat> Add(Alamat obj)
    {
        if(await DbContext.Alamats.AnyAsync(x=>x.IdAlamat == obj.IdAlamat)){
            throw new InvalidOperationException($"Alamat with ID {obj.IdAlamat} is already exist");
        }

        await DbContext.AddAsync(obj);
        await DbContext.SaveChangesAsync();

        return obj;
    }

    public async Task<bool> Delete(int id)
    {
        var alamat = await DbContext.Alamats.FirstOrDefaultAsync(x=>x.IdAlamat == id);

        if(alamat == null) {
            throw new InvalidOperationException($"Alamat with ID {id} doesn't exist");
        }

        DbContext.Remove(alamat);
        await DbContext.SaveChangesAsync();

        return true;
    }

    public async Task<List<Alamat>> Get(int limit, int offset, string keyword)
    {
        if(string.IsNullOrEmpty(keyword)){
            keyword = "";
        }

        return await DbContext.Alamats
        .Skip(offset)
        .Take(limit).ToListAsync();
    }

    public async Task<Alamat?> Get(int id)
    {
        var result = await DbContext.Alamats.FirstOrDefaultAsync(x=>x.IdAlamat == id);

        if(result == null)
        {
            throw new InvalidOperationException($"Kategori with ID {id} doesn't exist");
        }

        return result;
    }

    public Task<Alamat?> Get(Expression<Func<Alamat, bool>> func)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Alamat>> GetAll()
    {
        return await DbContext.Alamats.ToListAsync();
    }

    public async Task<Alamat> Update(Alamat obj)
    {
        if(obj == null)
        {
            throw new ArgumentNullException("Alamat cannot be null");
        }

        var alamat = await DbContext.Alamats.FirstOrDefaultAsync(x=>x.IdAlamat == obj.IdAlamat);

        if(alamat == null) {
            throw new InvalidOperationException($"Alamat with ID {obj.IdAlamat} doesn't exist in database");
        }

        alamat.Kecamatan = obj.Kecamatan;
       alamat.Kelurahan = obj.Kelurahan;
       alamat.Rt = obj.Rt;
       alamat.Rw = obj.Rw;
       alamat.KodePos = obj.KodePos;
       alamat.Detail = obj.Detail;

        DbContext.Update(alamat);
        await DbContext.SaveChangesAsync();

        return alamat;
    }

    async Task<List<AlamatViewModel>> IAlamatService.GetId(int idCustomer)
    {
        var result = await (from a in DbContext.Alamats where a.IdCustomer == idCustomer
        select new AlamatViewModel 
        {
            IdAlamat = a.IdAlamat,
            IdCustomer = a.IdCustomer,
            Kecamatan = a.Kecamatan,
            Kelurahan = a.Kelurahan,
            Rt = a.Rt,
            Rw = a.Rw,
            KodePos = a.KodePos,
            Detail = a.Detail,   
        }).ToListAsync();

        return result;
    }

}