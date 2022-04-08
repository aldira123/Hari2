using System;
using System.Collections.Generic;

namespace eCommerce.Datas.Entities
{
    public partial class Pembayaran
    {
        public int IdPembayaran { get; set; }
        public string MetodePembayaran { get; set; } = null!;
        public DateTime TanggalBayar { get; set; }
        public int IdOrder { get; set; }
        public int IdCustomer { get; set; }
        public decimal JumlahBayar { get; set; }
        public string Tujuan { get; set; } = null!;
        public decimal Pajak { get; set; }
        public string? Status { get; set; }
        public string? Catatan {get; set; }
        public string? BuktiPembayaran {get; set;}

        public virtual Customer IdCustomerNavigation { get; set; } = null!;
        public virtual Order IdOrderNavigation { get; set; } = null!;
    }
}
