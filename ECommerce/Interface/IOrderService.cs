using eCommerce.Datas.Entities;
using eCommerce.ViewModels;

namespace eCommerce.Interface
{
    public interface IOrderService
    {
        Task<Order> Checkout(Order newOrder);
        Task<List<OrderViewModel>> Get(int idCustomer);
        Task<OrderViewModel> GetDetail(int idCustomer,int idOrder);
        Task<OrderViewModel> GetDetail(int idOrder);
        Task<List<OrderViewModel>> GetV1(int limit, int offset, int? status = null, DateTime? date = null);
        Task UpdateStatus(int idOrder, short DIBAYAR);
        Task Bayar(Pembayaran newBayar);
        Task<bool> SudahDibayar (int idOrder);
        Task Kirim(Pengiriman dataPengiriman);
        Task Ulas(Ulasan ulasan);
       
    }
}
