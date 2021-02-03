using Bingol.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Bingol.Models
{
    public partial class Wishlist
    {
        [Key]
        public int WishlistId { get; set; }
        public short? WishlistCondition { get; set; }
        public int WishlistProductId { get; set; }
        public string WishlistUserId { get; set; }
        public string BingolUser { get; set; }

        public virtual Product WishlistProduct { get; set; }
        public virtual BingolUser WishlistUser { get; set; }
    }
}
