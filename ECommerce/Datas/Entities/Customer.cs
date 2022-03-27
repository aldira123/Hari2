using System;
using System.Collections.Generic;

namespace eCommerce.Datas.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Alamats = new HashSet<Alamat>();
            Keranjangs = new HashSet<Keranjang>();
            Orders = new HashSet<Order>();
            Pembayarans = new HashSet<Pembayaran>();
        }

        public int IdCustomer { get; set; }
        public string Nama { get; set; } = null!;
        public string NoHp { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? FotoProfile { get; set; }
        public string Email { get; set; } = null!;

        public virtual ICollection<Alamat> Alamats { get; set; }
        public virtual ICollection<Keranjang> Keranjangs { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Pembayaran> Pembayarans { get; set; }
    }
}
