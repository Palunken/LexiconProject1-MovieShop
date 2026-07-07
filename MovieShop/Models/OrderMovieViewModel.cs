using System.ComponentModel.DataAnnotations;

namespace The_visionaries_Code_404.Models
{
    public class OrderMovieViewModel
    {
        public string Title { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal TotalPrice { get; set; }
    }
}
