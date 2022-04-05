using eCommerce.Interface;
using eCommerce.Datas.Entities;
using eCommerce.Datas;
using eCommerce.ViewModels;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Services{
public class OrderService : BaseDbService, IOrderService
{
    public OrderService(eCommerceDbContext dbContext) : base(dbContext)
    {
    }

     public async Task<Order> Checkout(Order newOrder)
    {
        await DbContext.AddAsync(newOrder);
        await DbContext.SaveChangesAsync();

        return newOrder;
    }

     async Task<List<OrderViewModel>> IOrderService.Get(int idCustomer)
    {
        //Inner Join
        var result = await (from a in DbContext.Orders
        join b in DbContext.StatusOrders on a.Status equals b.IdStatus
        where a.IdCustomer == idCustomer
        select new OrderViewModel 
        {
           IdOrder = a.IdOrder,
           Status = b.Nama,
           TglTransaksi = a.TglTransaksi,
           JumlahBayar = a.JumlahBayar,
           Details = (from c in DbContext.DetailOrders
           join d in DbContext.Produks on c.IdProduk equals d.IdProduk
           where c.IdOrder == a.IdOrder
           select new OrderDetailViewModel{
               IdOrder = c.IdOrder,
               NamaProduk = d.NamaProduk,
               HargaBarang = d.HargaProduk,
               Quantity = c.JumlahBarang,
               Subtotal = c.SubTotal
           }).ToList()    
        }).ToListAsync();
        return result;
    }
    }
}
