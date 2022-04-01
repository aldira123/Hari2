using eCommerce.Interface;
using eCommerce.Datas;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Services{
public class ProdukKategoriService : BaseDbService, IProdukKategoriService
{
    public ProdukKategoriService(eCommerceDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<int[]> GetKategoriIds(int produkId){
        var result = await DbContext.ProdukKategoris.
        Where(x=>x.IdProduk == produkId).Select(x=>x.IdKategori).Distinct().ToArrayAsync();
        return result;
    }

           public async Task Remove(int produkId, int idKategori)
            {
                var item = await DbContext.ProdukKategoris.FirstOrDefaultAsync(x => x.IdProduk == produkId && x.IdKategori == idKategori);

                if (item == null)
                {
                    return;
                }

                DbContext.ProdukKategoris.Remove(item);

                await DbContext.SaveChangesAsync();
            }
        
    }
}
