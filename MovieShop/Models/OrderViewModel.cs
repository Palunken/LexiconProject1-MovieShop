using System.ComponentModel.DataAnnotations;
using The_visionaries_Code_404.Data;

namespace The_visionaries_Code_404.Models
{
    public class OrderViewModel
    {
        public DateTime OrderDate { get; set; }
        public string? CustomerName { get; set; }
        public List<OrderMovieViewModel> Movies { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal TotalCost { get; set; }
        public bool EveryCustomer { get; set; }
    }
}
