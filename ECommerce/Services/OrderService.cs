using eCommerce.Interface;
using eCommerce.Datas.Entities;
using eCommerce.Datas;
using eCommerce.ViewModels;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Services
{
    public class OrderService : BaseDbService, IOrderService

    {
        public OrderService(eCommerceDbContext dbContext) : base(dbContext)
        {
        }

        public async Task Bayar(Pembayaran newBayar)
        {
            if (await DbContext.Pembayarans.AnyAsync(x => x.IdOrder == newBayar.IdOrder))
            {
                throw new InvalidOperationException("Pembayaran sudah dilakukan");
            }

            await DbContext.AddAsync(newBayar);
            await DbContext.SaveChangesAsync();
        }

        public async Task<Order> Checkout(Order newOrder)
        {
            await DbContext.AddAsync(newOrder);
            await DbContext.SaveChangesAsync();

            return newOrder;
        }

        public async Task<OrderViewModel> GetDetail(int idOrder)
        {
            var result = await (from a in DbContext.Orders
            //Inner Join
            join b in DbContext.StatusOrders on a.Status equals b.IdStatus
            join c in DbContext.Alamats on a.IdAlamat equals c.IdAlamat
            join f in DbContext.Customers on a.IdCustomer equals f.IdCustomer
            //End Inner Join

            //Left Join
            join d in DbContext.Pembayarans on a.IdOrder equals d.IdOrder
            into tempPembayaran from d in tempPembayaran.DefaultIfEmpty()
            //End Left Join

            join e in DbContext.Ulasans on a.IdOrder equals e.IdOrder
            into tempUlasan from e in tempUlasan.DefaultIfEmpty()

            where a.IdOrder == idOrder
            select new OrderViewModel{
                IdOrder = a.IdOrder,
                Status = b.Nama,
                IdStatus = b.IdStatus,
                TglTransaksi = a.TglTransaksi,
                JumlahBayar = a.JumlahBayar,
                IdAlamat = c.IdAlamat,
                Alamat = c.Detail,
                NamaCustomer = f.Nama,
                NoHp = f.NoHp,
                Details = (from c in DbContext.DetailOrders
                           join d in DbContext.Produks on c.IdProduk equals d.IdProduk
                           where c.IdOrder == a.IdOrder
                           select new OrderDetailViewModel
                            {
                             IdOrder = c.IdOrder,
                             NamaProduk = d.NamaProduk,
                             HargaBarang = d.HargaProduk,
                             Quantity = c.JumlahBarang,
                             Subtotal = c.SubTotal
                        }).ToList(),

                //Mendapatkan data pembayaran jika sudah ada
                Pembayaran = d == null ? new PembayaranViewModel() : new PembayaranViewModel{
                    IdPembayaran = d.IdPembayaran,
                    MetodePembayaran = d.MetodePembayaran,
                    Tujuan = d.Tujuan,
                    JumlahBayar = d.JumlahBayar,
                    Catatan = d.Catatan,
                    Pajak = d.Pajak,
                    Status = d.Status,
                    TanggalBayar = d.TanggalBayar,
                    BuktiPembayaran = d.BuktiPembayaran     
                },
                Ulasan = e == null? new UlasanViewModel() : new UlasanViewModel{
                   IdUlasan = e.IdUlasan,
                   Komentar = e.Komentar,
                   Gambar = e.Gambar,
                   Rating = e.Rating
               }
               }).FirstOrDefaultAsync();
               return result; 
            }
            
        

        public async Task<List<OrderViewModel>> GetV1(int limit, int offset, int? status = null, DateTime? date = null)
        {
            if (status != null && date != null)
            {
                return await (from a in DbContext.Orders
                              join b in DbContext.StatusOrders on a.Status equals b.IdStatus
                              join c in DbContext.Alamats on a.IdAlamat equals c.IdAlamat
                              where a.Status == status.Value &&
                              date.Value.Date == a.TglTransaksi.Date
                              select new OrderViewModel
                              {
                                  IdOrder = a.IdOrder,
                                  Status = b.Nama,
                                  TglTransaksi = a.TglTransaksi,
                                  JumlahBayar = a.JumlahBayar
                              }).Skip(offset)
                                    .Take(limit).ToListAsync();
            }
            else if (status != null && date == null)
            {
                return await (from a in DbContext.Orders
                              join b in DbContext.StatusOrders on a.Status equals b.IdStatus
                              join c in DbContext.Alamats on a.IdAlamat equals c.IdAlamat
                              where a.Status == status.Value
                              select new OrderViewModel
                              {
                                  IdOrder = a.IdOrder,
                                  Status = b.Nama,
                                  TglTransaksi = a.TglTransaksi,
                                  JumlahBayar = a.JumlahBayar
                              }).Skip(offset)
                                    .Take(limit).ToListAsync();
            }
            else if (status == null && date != null)
            {
                return await (from a in DbContext.Orders
                              join b in DbContext.StatusOrders on a.Status equals b.IdStatus
                              join c in DbContext.Alamats on a.IdAlamat equals c.IdAlamat
                              where a.TglTransaksi.Date == date.Value.Date
                              select new OrderViewModel
                              {
                                  IdOrder = a.IdOrder,
                                  Status = b.Nama,
                                  TglTransaksi = a.TglTransaksi,
                                  JumlahBayar = a.JumlahBayar
                              }).Skip(offset)
                                    .Take(limit).ToListAsync();
            }
            else
            {
                return await (from a in DbContext.Orders
                              join b in DbContext.StatusOrders on a.Status equals b.IdStatus
                              join c in DbContext.Alamats on a.IdAlamat equals c.IdAlamat
                              select new OrderViewModel
                              {
                                  IdOrder = a.IdOrder,
                                  Status = b.Nama,
                                  TglTransaksi = a.TglTransaksi,
                                  JumlahBayar = a.JumlahBayar
                              }).Skip(offset)
                                    .Take(limit).ToListAsync();
            }
        }

        public async Task Kirim(Pengiriman dataPengiriman)
        {
            if (await DbContext.Pengirimen.AnyAsync(x => x.IdOrder == dataPengiriman.IdOrder))
            {
                throw new InvalidOperationException("Pengiriman sudah dilakukan");
            }

            await DbContext.AddAsync(dataPengiriman);
            await DbContext.SaveChangesAsync();
        }

        public async Task<bool> SudahDibayar(int idOrder)
        {
            return await DbContext.Orders.AnyAsync(x => x.IdOrder == idOrder && x.Status == AppConstant.StatusOrder.DIBAYAR);
        }

        public async Task Ulas(Ulasan ulasan)
        {
            if(await DbContext.Ulasans.AnyAsync(x => x.IdOrder == ulasan.IdOrder)){
                throw new InvalidOperationException("Anda sudah memberikan review");
            }

            await DbContext.AddAsync(ulasan);
            await DbContext.SaveChangesAsync();
        }

        public async Task UpdateStatus(int idOrder, short DIBAYAR)
        {
           var order = await DbContext.Orders.FirstOrDefaultAsync(x=>x.IdOrder == idOrder);

           if(order == null){
               throw new ArgumentNullException("Data order tidak ditemukan");
           }

           order.Status = DIBAYAR;

           DbContext.Update(order);
           await DbContext.SaveChangesAsync();
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
                                    IdStatus = b.IdStatus,
                                    TglTransaksi = a.TglTransaksi,
                                    JumlahBayar = a.JumlahBayar,
                                    Details = (from c in DbContext.DetailOrders
                                               join d in DbContext.Produks on c.IdProduk equals d.IdProduk
                                               where c.IdOrder == a.IdOrder
                                               select new OrderDetailViewModel
                                               {
                                                   IdOrder = c.IdOrder,
                                                   NamaProduk = d.NamaProduk,
                                                   HargaBarang = d.HargaProduk,
                                                   Quantity = c.JumlahBarang,
                                                   Subtotal = c.SubTotal
                                               }).ToList()
                                }).ToListAsync();
            return result;
        }
        async Task<OrderViewModel> IOrderService.GetDetail(int idCustomer, int idOrder)
        {
             var result = await (from a in DbContext.Orders
            //Inner Join
            join b in DbContext.StatusOrders on a.Status equals b.IdStatus
            join c in DbContext.Alamats on a.IdAlamat equals c.IdAlamat
            join f in DbContext.Customers on a.IdCustomer equals f.IdCustomer
    
            //End Inner Join

            //Left Join Table Pembayaran
            join d in DbContext.Pembayarans on a.IdOrder equals d.IdOrder
            into tempPembayaran from d in tempPembayaran.DefaultIfEmpty()
            //End Left Join

            //Left Join Table Pengiriman
            join e in DbContext.Pengirimen on a.IdOrder equals e.IdOrder
            into tempPengiriman from e in tempPengiriman.DefaultIfEmpty()
            //End Left Join

            join g in DbContext.Ulasans on a.IdOrder equals g.IdOrder
            into tempUlasan from g in tempUlasan.DefaultIfEmpty()

            where a.IdCustomer == idCustomer && a.IdOrder == idOrder
            select new OrderViewModel{
                IdOrder = a.IdOrder,
                Status = b.Nama,
                IdStatus = b.IdStatus,
                TglTransaksi = a.TglTransaksi,
                JumlahBayar = a.JumlahBayar,
                IdAlamat = c.IdAlamat,
                Alamat = c.Detail,
                NamaCustomer = f.Nama,
                NoHp = f.NoHp,
                Details = (from c in DbContext.DetailOrders
                           join d in DbContext.Produks on c.IdProduk equals d.IdProduk
                           where c.IdOrder == a.IdOrder
                           select new OrderDetailViewModel
                            {
                             IdOrder = c.IdOrder,
                             NamaProduk = d.NamaProduk,
                             HargaBarang = d.HargaProduk,
                             Quantity = c.JumlahBarang,
                             Subtotal = c.SubTotal
                        }).ToList(),
                //Mendapatkan data pembayaran jika sudah ada
                Pembayaran = d == null ? new PembayaranViewModel() : new PembayaranViewModel{
                    IdPembayaran = d.IdPembayaran,
                    MetodePembayaran = d.MetodePembayaran,
                    Tujuan = d.Tujuan,
                    JumlahBayar = d.JumlahBayar,
                    Catatan = d.Catatan,
                    Pajak = d.Pajak,
                    Status = d.Status,
                    TanggalBayar = d.TanggalBayar,
                    BuktiPembayaran = d.BuktiPembayaran     
                },
               Pengiriman = e == null? new PengirimanViewModel() : new PengirimanViewModel{
                   IdPengiriman = e.IdPengiriman,
                   Keterangan = e.Keterangan,
                   Kurir = e.Kurir,
                   NoResi = e.Noresi,
                   Ongkir = e.Ongkir,
                   Status = e.Status
               },
               Ulasan = g == null? new UlasanViewModel() : new UlasanViewModel{
                   IdUlasan = g.IdUlasan,
                   Komentar = g.Komentar,
                   Gambar = g.Gambar,
                   Rating = g.Rating
               }
               }).FirstOrDefaultAsync();
               return result; 
            }
    }
}
