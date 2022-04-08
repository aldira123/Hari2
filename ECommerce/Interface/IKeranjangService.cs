using eCommerce.Datas.Entities;
using eCommerce.ViewModels;

namespace eCommerce.Interface
{
    public interface IKeranjangService: ICrudService<Keranjang> 
    {
        Task<List<KeranjangViewModel>> GetId(int idCustomer); 
        Task<Keranjang?> GetProduk(int IdProduk); 
        Task Clear(int idCustomer);   
    }
}