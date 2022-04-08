using System.ComponentModel.DataAnnotations;
using eCommerce.Datas.Entities;

namespace eCommerce.ViewModels
{
    public partial class BayarRequestViewModel
    {
        public BayarRequestViewModel()
        {

        }
        [Required]
        public int IdOrder {get; set;}
        [Required]
        public string MetodePembayaran { get; set; } = null!;
        [Required]
        public DateTime TanggalBayar { get; set; }
        [Required]
        public decimal JumlahBayar { get; set; }
        [Required]
        public string Tujuan { get; set; } = null!;
        public string? Catatan { get; set; }
        [Required]
        public IFormFile BuktiPembayaran { get; set; }

    }
}