namespace eCommerce.ViewModels
{
    public class PengirimanViewModel
    {
        public int IdPengiriman { get; set; }
        public string Kurir { get; set; } = null!;
        public string NoResi { get; set; } = null!;
        public decimal Ongkir { get; set; }
        public string Status { get; set; }
        public string Keterangan { get; set; } = null!;
    }
}