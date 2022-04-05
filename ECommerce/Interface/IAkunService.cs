using eCommerce.Datas.Entities;
using eCommerce.ViewModels;
namespace eCommerce.Interface
{
    public interface IAkunService
    {
        Task<Admin> Login(string username, string password);
        Task<Customer> Register(RegisterViewModel request);
         Task<Customer> LoginCustomer(string username, string password);
         Task<List<Tuple<int, string>>> GetAlamat(int idCustomer); 
    }
}