using Bingol.Areas.Identity.Data;
using System;
using System.Collections.Generic;

#nullable disable

namespace Bingol.Models
{
    public partial class Order
    {
        public Order()
        {
            Orderdetails = new HashSet<Orderdetail>();
        }

        public int OrderId { get; set; }
        public string OrderUserId { get; set; }
        public float OrderAmount { get; set; }
        public string OrderShipName { get; set; }
        public string OrderShipAddress { get; set; }
        public string OrderShipAddress2 { get; set; }
        public string OrderCity { get; set; }
        public string OrderState { get; set; }
        public string OrderZip { get; set; }
        public string OrderCountry { get; set; }
        public string OrderPhone { get; set; }
        public string OrderFax { get; set; }
        public float OrderShipping { get; set; }
        public float OrderTax { get; set; }
        public string OrderEmail { get; set; }
        public DateTime OrderDate { get; set; }
        public short OrderShipped { get; set; }
        public string OrderTrackingNumber { get; set; }

        //public virtual User OrderUser { get; set; }
        public virtual BingolUser OrderUser { get; set; }
        public virtual ICollection<Orderdetail> Orderdetails { get; set; }
    }
}
