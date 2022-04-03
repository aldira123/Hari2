using eCommerce.Datas.Entities;
using eCommerce.ViewModels;
namespace eCommerce.Interface
{
    public interface IAlamatService : ICrudService<Alamat>
    {
         Task<List<AlamatViewModel>> Get(int idCustomer);
    }
}