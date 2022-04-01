using eCommerce.Datas.Entities;
using eCommerce.ViewModels;

namespace eCommerce.Interface
{
    public interface IKeranjangService: ICrudService<Keranjang> 
    {
        Task<List<KeranjangViewModel>> Get(int idCustomer); 
    }
}