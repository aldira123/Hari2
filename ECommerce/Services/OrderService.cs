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
        var result = await (from a in DbContext.DetailOrders
        join b in DbContext.Produks on a.IdProduk equals b.IdProduk
        join c in DbContext.Orders on a.IdOrder equals c.IdOrder 
        join d in DbContext.StatusOrders on c.Status equals d.IdStatus
        where c.IdCustomer == idCustomer
        select new OrderViewModel 
        {
           IdOrder = a.IdOrder,
           TglTransaksi = c.TglTransaksi,
           JumlahBayar = c.JumlahBayar,
           Status = d.Nama,
           Gambar = b.Gambar,
           NamaProduk = b.NamaProduk,
           Subtotal = a.SubTotal,
           quantity = a.JumlahBarang,
           HargaBarang = b.HargaProduk
        }).ToListAsync();

        return result;
    }

        
    }
}
