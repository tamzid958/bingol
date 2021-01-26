using System;
using System.Collections.Generic;

#nullable disable

namespace Bingol.Models
{
    public partial class Productoption
    {
        public long ProductOptionId { get; set; }
        public int ProductId { get; set; }
        public int OptionId { get; set; }
        public double? OptionPriceIncrement { get; set; }
        public int OptionGroupId { get; set; }

        public virtual Option Option { get; set; }
        public virtual Optiongroup OptionGroup { get; set; }
        public virtual Product Product { get; set; }
    }
}
