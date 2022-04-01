namespace eCommerce.ViewModels
{
    public partial class KeranjangViewModel
    {
        public KeranjangViewModel()
        {
        
        }

        public int IdKeranjang { get; set; }
        public int IdProduk { get; set; }
        public string NamaProduk{get; set;}
        public string Image{get; set;}
        public int IdCustomer { get; set; }
        public int JumlahBarang { get; set; }
        public decimal Subtotal { get; set; }

    }
}