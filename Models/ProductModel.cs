using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bingol.Models
{
    public class ProductModel
    {

        [Key]
        public int ProductID { get; set; }
        public string ProductSKU { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public double ProductWeight { get; set; }
        public string ProductCartDesc { get; set; }
        public string ProductShortDesc { get; set; }
        public string ProductLongDesc { get; set; }
        public string ProductThumb { get; set; }
        public string ProductImage { get; set; }
        public int ProductCategoryID { get; set; }
        public string ProductUpdateDate { get; set; }
        public int ProductStock { get; set; }
        public bool ProductLive { get; set; }
        public bool ProductUnlimited { get; set; }
        public string ProductLocation { get; set; }
    }
}
