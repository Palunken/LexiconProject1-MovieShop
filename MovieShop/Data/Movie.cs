using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace The_visionaries_Code_404.Data
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Title { get; set; }

        [Required]
        [StringLength(50)]
        public required string Director { get; set; }

        [Required]
        [Display(Name = "Release Year")]
        public required int ReleaseYear { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public required decimal Price { get; set; }

        [StringLength(2000)]
        public string? Image {  get; set; }
    }
}
