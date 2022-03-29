using System;
using System.Collections.Generic;

namespace eCommerce.Datas.Entities
{
    public partial class Kategori
    {
        public Kategori()
        {
            ProdukKategoris = new HashSet<ProdukKategori>();
            NamaKategori = string.Empty;
            DeskripsiKategori = string.Empty;
        }

        public int IdKategori { get; set; }
        public string NamaKategori { get; set; } = null!;
        public string DeskripsiKategori { get; set; } = null!;
        public string? Icon { get; set; }

        public virtual ICollection<ProdukKategori> ProdukKategoris { get; set; }
    }
}
