using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace The_visionaries_Code_404.Data
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public required string LastName { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Billing Address")]
        public required string BillingAddress { get; set; }

        [Required]
        [StringLength(6)]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Zip")]
        public required string BillingZip { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Billing City")]
        public required string BillingCity { get; set; }


        [Required]
        [StringLength(50)]
        [Display(Name = "First Name for Delivery")]
        public required string DeliveryFirstName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name for Delivery")]
        public required string DeliveryLastName { get; set; }


        [Required]
        [StringLength(20)]
        [Display(Name = "Delivery Address")]
        public required string DeliveryAddress { get; set; }

        [Required]
        [StringLength(6)]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Delivery Zip")]
        public required string DeliveryZip { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Delivery City")]
        public required string DeliveryCity { get; set; }

        [Required]
        [StringLength(320)]
        [DataType (DataType.EmailAddress)]
        public required string Email { get; set; }

        [Required]
        [StringLength(13)]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public required string Phone { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = [];
    }
}
