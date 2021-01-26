using System.Collections.Generic;

#nullable disable

namespace Bingol.Models
{
    public partial class Optiongroup
    {
        public Optiongroup()
        {
            Productoptions = new HashSet<Productoption>();
        }

        public int OptionGroupId { get; set; }
        public string OptionGroupName { get; set; }

        public virtual ICollection<Productoption> Productoptions { get; set; }
    }
}
