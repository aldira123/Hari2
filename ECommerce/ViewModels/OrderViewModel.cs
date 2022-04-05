using eCommerce.Datas.Entities;

namespace eCommerce.ViewModels
{
    public partial class OrderViewModel
    {
        public OrderViewModel()
        {
            Details = new List<OrderDetailViewModel>();
        }
        public int IdOrder { get; set; }
        public DateTime TglTransaksi { get; set; }
        public int TotalQty
        {
            get
            {
                return (Details == null || !Details.Any()) ? 0 : Details.Sum(x => x.Quantity);
            }
        }
        public decimal JumlahBayar { get; set; }
        public string Status { get; set; }

        public List<OrderDetailViewModel> Details { get; set; }
    
    }
}