using System;
using System.Collections.Generic;

namespace eCommerce.Datas.Entities
{
    public partial class StatusOrder
    {
        public StatusOrder()
        {
            Orders = new HashSet<Order>();
            Pembayarans = new HashSet<Pembayaran>();
            Pengirimen = new HashSet<Pengiriman>();
        }

        public int IdStatus { get; set; }
        public string Nama { get; set; } = null!;
        public string Deskripsi { get; set; } = null!;

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Pembayaran> Pembayarans { get; set; }
        public virtual ICollection<Pengiriman> Pengirimen { get; set; }
    }
}
