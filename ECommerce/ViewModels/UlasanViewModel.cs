using eCommerce.Datas.Entities;

namespace eCommerce.ViewModels
{
    public partial class UlasanViewModel
    {
        public UlasanViewModel()
        {
            
        }
       
        public int IdUlasan {get; set; }
        public int IdOrder {get; set; }
        public string Komentar {get; set;}
        public string Gambar {get; set;}
        public int Rating {get; set;}
   
    }
}