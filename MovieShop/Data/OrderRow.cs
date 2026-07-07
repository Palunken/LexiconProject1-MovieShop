using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace The_visionaries_Code_404.Data
{
    public class OrderRow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }
        [Required]
        public int MovieId { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required, Range(1, 100)]
        public int Quantities { get; set; }
        [Required]
        public virtual Order? Orders { get; set; }
        [Required]
        public virtual Movie? Movies { get; set; }
    }
}
