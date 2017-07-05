using STEnterprise.Models;
using System.ComponentModel.DataAnnotations;

namespace STEnterprise.Areas.Sale.Models
{
    public class Country : CommonModel
    {
        [Key]
        public byte CountryId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Country name is required")]
        [DataType(DataType.Text)]
        [Display(Name = "Country Name")]
        [StringLength(maximumLength: 100, ErrorMessage = "Country Name cannot be longer than 100 characters.", MinimumLength = 2)]
        [RegularExpression("([a-zA-Z .&'-]+)", ErrorMessage = "Enter Valid Country Name !")]
        public string CountryName { get; set; }

    }
}