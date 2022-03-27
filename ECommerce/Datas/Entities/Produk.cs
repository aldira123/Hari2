using System;
using System.Collections.Generic;

namespace eCommerce.Datas.Entities
{
    public partial class Produk
    {
        public Produk()
        {
            Keranjangs = new HashSet<Keranjang>();
            ProdukKategoris = new HashSet<ProdukKategori>();
        }

        public int IdProduk { get; set; }
        public string NamaProduk { get; set; } = null!;
        public string DeskripsiProduk { get; set; } = null!;
        public decimal HargaProduk { get; set; }
        public int Stok { get; set; }
        public string Gambar { get; set; } = null!;

        public virtual ICollection<Keranjang> Keranjangs { get; set; }
        public virtual ICollection<ProdukKategori> ProdukKategoris { get; set; }
    }
}
