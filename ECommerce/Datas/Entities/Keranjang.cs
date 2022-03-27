using System;
using System.Collections.Generic;

namespace eCommerce.Datas.Entities
{
    public partial class Keranjang
    {
        public Keranjang()
        {
            Orders = new HashSet<Order>();
        }

        public int IdKeranjang { get; set; }
        public int IdProduk { get; set; }
        public int IdCustomer { get; set; }
        public int JumlahBarang { get; set; }
        public decimal Subtotal { get; set; }

        public virtual Customer IdCustomerNavigation { get; set; } = null!;
        public virtual Produk IdProdukNavigation { get; set; } = null!;
        public virtual ICollection<Order> Orders { get; set; }
    }
}
