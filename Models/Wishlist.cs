using Bingol.Areas.Identity.Data;
using System;
using System.Collections.Generic;

#nullable disable

namespace Bingol.Models
{
    public partial class Wishlist
    {
        public int WishlistId { get; set; }
        public short? Wishlist1 { get; set; }
        public int WishlistProductId { get; set; }
        public string WishlistUserId { get; set; }

        public virtual Product WishlistProduct { get; set; }
        public virtual BingolUser WishlistUser { get; set; }       
    }
}
