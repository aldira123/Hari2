using System;
using System.Collections.Generic;

namespace eCommerce.Datas.Entities
{
    public partial class Pengiriman
    {
        public int IdPengiriman { get; set; }
        public int IdOrder { get; set; }
        public string Kurir { get; set; } = null!;
        public decimal Ongkir { get; set; }
        public int IdAlamat { get; set; }
        public int Status { get; set; }
        public string Keterangan { get; set; } = null!;

        public virtual Alamat IdAlamatNavigation { get; set; } = null!;
        public virtual Order IdOrderNavigation { get; set; } = null!;
        public virtual StatusOrder StatusNavigation { get; set; } = null!;
    }
}
