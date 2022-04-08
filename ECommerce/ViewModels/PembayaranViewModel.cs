using eCommerce.Datas.Entities;

namespace eCommerce.ViewModels
{
    public partial class PembayaranViewModel
    {
        public PembayaranViewModel()
        {
            
        }
       
        public int IdPembayaran { get; set; }
        public string MetodePembayaran { get; set; } = null!;
        public DateTime TanggalBayar { get; set; }
        public decimal JumlahBayar { get; set; }
        public string Tujuan { get; set; } = null!;
        public decimal Pajak { get; set; }
        public string Status { get; set; }
        public string? Catatan {get; set;}
        public string? BuktiPembayaran {get; set;}
   
    }
}