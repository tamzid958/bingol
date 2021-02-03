using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bingol.Models
{
    public class TempCart
    {
        [Key]
        public int TempCartID { get; set; }
        
        [Required]
        public int ProductID { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(450)")]
        public string CustomerID { get; set; }
        
        [Required]
        public int Quantity { get; set; }

        [Required]
        public int Color { get; set; }

        [Required]
        public int Size { get; set; }

        public virtual Product Product { get; set; }

    }
}
