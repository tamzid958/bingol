using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Bingol.Models;
using Microsoft.AspNetCore.Identity;

namespace Bingol.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the BingolUser class
    public class BingolUser : IdentityUser
    {
        public BingolUser()
        {
            Orders = new HashSet<Order>();
            Reviews = new HashSet<Review>();
            Wishlists = new HashSet<Wishlist>();
        }
        [PersonalData]
        [Column(TypeName = "varchar(50)")]
        public string UserFirstName { get; set; }
        
        [PersonalData]
        [Column(TypeName = "varchar(50)")]
        public string UserLastName { get; set; }

        [PersonalData]
        [Column(TypeName = "varchar(90)")]
        public string UserCity { get; set; }

        [PersonalData]
        [Column(TypeName = "varchar(20)")]
        public string UserState { get; set; }

        [PersonalData]
        [Column(TypeName = "varchar(12)")]
        public string UserZip { get; set; }

        [PersonalData]
        [Column(TypeName = "datetime")]
        public DateTime? UserRegistrationDate { get; set; }

        [PersonalData]
        [Column(TypeName = "varchar(50)")]
        public string UserIp { get; set; }

        [PersonalData]
        [Column(TypeName = "varchar(20)")]
        public string UserPhone { get; set; }

        [PersonalData]
        [Column(TypeName = "varchar(50)")]
        public string UserFax { get; set; }

        [PersonalData]
        [Column(TypeName = "varchar(20)")]
        public string UserCountry { get; set; }

        [PersonalData]
        [Column(TypeName = "varchar(100)")]
        public string UserAddress { get; set; }

        [PersonalData]
        [Column(TypeName = "varchar(50)")]
        public string UserAddress2 { get; set; }

        [PersonalData]
        [Column(TypeName = "smallint")]
        public short UserType { get; set; }

        [ForeignKey("OrderUserId")]
        public virtual ICollection<Order> Orders { get; set; }
        
        [ForeignKey("ReviewUserId")]
        public virtual ICollection<Review> Reviews { get; set; }

        [ForeignKey("WishlistUserId")]
        public virtual ICollection<Wishlist> Wishlists { get; set; }
    }
}
