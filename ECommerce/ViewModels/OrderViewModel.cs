namespace eCommerce.ViewModels
{
    public partial class OrderViewModel
    {
        public OrderViewModel()
        {
        
        }

        public int IdOrder { get; set; }
        public DateTime TglTransaksi { get; set; }
        public decimal JumlahBayar { get; set; }
        public string Status { get; set; }
        public string Gambar { get; set; }
        public string NamaProduk { get; set; }
         public decimal Subtotal { get; set; }
        public decimal HargaBarang { get; set; }
        public int quantity{ get; set; }


    }
}