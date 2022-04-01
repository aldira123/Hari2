using eCommerce.Datas.Entities;

namespace eCommerce.Interface
{
    public interface IProdukKategoriService 
    {
        Task<int[]> GetKategoriIds(int produkId);
        Task Remove(int produkId, int idKategori);
    }
}
