using eCommerce.Datas.Entities;
namespace eCommerce.Interface
{
    public interface IAkunService
    {
        Task<Admin> Login(string username, string password);
        Task<Customer> Register(Customer obj);
         Task<Customer> LoginCustomer(string username, string password);
    }
}