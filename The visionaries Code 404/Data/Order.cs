using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace The_visionaries_Code_404.Data
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Order Date/Time:")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime OrderDate { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual ICollection<OrderRow> OrderRows { get; set; } = [];
    }
}
