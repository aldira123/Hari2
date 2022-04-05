using eCommerce.Datas.Entities;

namespace eCommerce.ViewModels
{
    public partial class OrderDetailViewModel
    {
        public OrderDetailViewModel()
        {

        }

        public int IdOrder { get; set; }
        public string NamaProduk { get; set; }
        public decimal HargaBarang { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
          public string Gambar { get; set; }


    }
}