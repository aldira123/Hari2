using System;
using System.Collections.Generic;

namespace eCommerce.Datas.Entities
{
    public partial class DetailOrder
    {
       
            public int IdDetail { get; set; }
            public int IdOrder { get; set; }
            public int IdProduk { get; set; }
            public decimal Harga { get; set; }
            public int JumlahBarang { get; set; }
            public decimal SubTotal { get; set; }

            public virtual Order Order { get; set; } = null!;
        
    }
}




