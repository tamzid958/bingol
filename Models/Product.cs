using System;
using System.Collections.Generic;

#nullable disable

namespace Bingol.Models
{
    public partial class Product
    {
        public Product()
        {
            Orderdetails = new HashSet<Orderdetail>();
            Productoptions = new HashSet<Productoption>();
            Reviews = new HashSet<Review>();
            Wishlists = new HashSet<Wishlist>();
        }

        public int ProductId { get; set; }
        public string ProductSku { get; set; }
        public string ProductName { get; set; }
        public float ProductPrice { get; set; }
        public float ProductWeight { get; set; }
        public string ProductCartDesc { get; set; }
        public string ProductShortDesc { get; set; }
        public string ProductLongDesc { get; set; }
        public string ProductThumb { get; set; }
        public string ProductImage { get; set; }
        public int? ProductCategoryId { get; set; }
        public DateTime ProductUpdateDate { get; set; }
        public float? ProductStock { get; set; }
        public short? ProductLive { get; set; }
        public short? ProductUnlimited { get; set; }
        public string ProductLocation { get; set; }

        public virtual Productcategory ProductCategory { get; set; }
        public virtual ICollection<Orderdetail> Orderdetails { get; set; }
        public virtual ICollection<Productoption> Productoptions { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Wishlist> Wishlists { get; set; }
    }
}
