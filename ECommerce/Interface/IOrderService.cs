using eCommerce.Datas.Entities;
using eCommerce.ViewModels;

namespace eCommerce.Interface
{
    public interface IOrderService 
    {
        Task<Order> Checkout(Order newOrder);
        Task<List<OrderViewModel>> Get(int idCustomer); 
    }
}
