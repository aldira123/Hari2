using eCommerce.Datas.Entities;
using System.ComponentModel.DataAnnotations;
namespace eCommerce.ViewModels
{
    public partial class KeranjangUpdateViewModel
    {
        public KeranjangUpdateViewModel()
        {
            
        }

       [Required]
       public int Id{get; set;}
       [Required]
       public int JumlahBarang{get; set;}

    }
}