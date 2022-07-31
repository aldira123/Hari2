using eCommerce.Datas.Entities;

namespace eCommerce.Interface
{
    public interface IProdukService : ICrudService<Produk>
    {
        Task<Produk> Add(Produk obj, int idKategori);

    }
}
