using eCommerce.Datas.Entities;
using eCommerce.ViewModels;
namespace eCommerce.Interface
{
    public interface IAlamatService : ICrudService<Alamat>
    {
         Task<List<AlamatViewModel>> GetId(int idCustomer);
    }
}