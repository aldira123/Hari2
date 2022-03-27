using System;
using System.Collections.Generic;

namespace eCommerce.Datas.Entities
{
    public partial class Pembayaran
    {
        public int IdPembayaran { get; set; }
        public string MetodePembayaran { get; set; } = null!;
        public DateOnly TanggalBayar { get; set; }
        public int IdOrder { get; set; }
        public int IdCustomer { get; set; }
        public decimal JumlahBayar { get; set; }
        public string Tujuan { get; set; } = null!;
        public decimal Pajak { get; set; }
        public int Status { get; set; }

        public virtual Customer IdCustomerNavigation { get; set; } = null!;
        public virtual Order IdOrderNavigation { get; set; } = null!;
        public virtual StatusOrder StatusNavigation { get; set; } = null!;
    }
}
