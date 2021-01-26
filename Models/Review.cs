using Bingol.Areas.Identity.Data;
using System;
using System.Collections.Generic;

#nullable disable

namespace Bingol.Models
{
    public partial class Review
    {
        public int ReviewId { get; set; }
        public int? ReviewRating { get; set; }
        public int ReviewProductId { get; set; }
        public string ReviewUserId { get; set; }

        public virtual Product ReviewProduct { get; set; }
        //public virtual User ReviewUser { get; set; }
        public virtual BingolUser ReviewUser { get; set; }
    }
}
