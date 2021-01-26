using System;
using System.Collections.Generic;

#nullable disable

namespace Bingol.Models
{
    public partial class Orderdetail
    {
        public int DetailId { get; set; }
        public int DetailOrderId { get; set; }
        public int DetailProductId { get; set; }
        public string DetailName { get; set; }
        public float DetailPrice { get; set; }
        public string DetailSku { get; set; }
        public int DetailQuantity { get; set; }

        public virtual Order DetailOrder { get; set; }
        public virtual Product DetailProduct { get; set; }
    }
}
