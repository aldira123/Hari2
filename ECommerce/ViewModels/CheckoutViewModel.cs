using eCommerce.Datas.Entities;

namespace eCommerce.ViewModels
{
    public partial class CheckoutViewModel
    {
        public CheckoutViewModel()
        {
           
        }

    public int[] Id { get; set; }
    public int[] Qty { get; set; }
    public int Alamat { get; set; }
    public string Action { get; set; }
    public string? Note { get; set; }
    public string[] harga { get; set; }
    }
}
