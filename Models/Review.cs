using Bingol.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Bingol.Models
{
    public partial class Review
    {
        [Key]
        public int ReviewId { get; set; }
        public int? ReviewRating { get; set; }
        public int ReviewProductId { get; set; }
        public int ReviewOrderId { get; set; }
        public string ReviewUserId { get; set; }
        public virtual Product ReviewProduct { get; set; }
        public virtual BingolUser ReviewUser { get; set; }
        public virtual Orderdetail ReviewOrderdDeatil { get; set; }
    }
}
