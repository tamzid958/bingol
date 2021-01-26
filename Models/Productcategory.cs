using System.Collections.Generic;

#nullable disable

namespace Bingol.Models
{
    public partial class Productcategory
    {
        public Productcategory()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
