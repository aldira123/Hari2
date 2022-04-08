namespace eCommerce.Datas.Entities{
    public class Ulasan{
        public Ulasan(){

        }

        public int IdUlasan {get; set; }
        public int IdOrder {get; set; }
        public int IdCustomer{get; set;}
        public string Komentar {get; set;}
        public string Gambar {get; set;}
        public int Rating {get; set;}

        public virtual Order Order {get; set; }
        public virtual Customer Customer {get; set; }
    }
}