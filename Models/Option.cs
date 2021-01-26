using System;
using System.Collections.Generic;

#nullable disable

namespace Bingol.Models
{
    public partial class Option
    {
        public Option()
        {
            Productoptions = new HashSet<Productoption>();
        }

        public int OptionId { get; set; }
        public string OptionName { get; set; }
        public int? OptionsGroupId { get; set; }

        public virtual Optiongroup OptionsGroup { get; set; }
        public virtual ICollection<Productoption> Productoptions { get; set; }
    }
}
