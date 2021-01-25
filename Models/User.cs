using System;
using System.Collections.Generic;

#nullable disable

namespace Bingol.Model
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
            Reviews = new HashSet<Review>();
            Wishlists = new HashSet<Wishlist>();
        }

        public int UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string UserCity { get; set; }
        public string UserState { get; set; }
        public string UserZip { get; set; }
        public short? UserEmailVerified { get; set; }
        public DateTime? UserRegistrationDate { get; set; }
        public string UserVerificationCode { get; set; }
        public string UserIp { get; set; }
        public string UserPhone { get; set; }
        public string UserFax { get; set; }
        public string UserCountry { get; set; }
        public string UserAddress { get; set; }
        public string UserAddress2 { get; set; }
        public short UserType { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Wishlist> Wishlists { get; set; }
    }
}
