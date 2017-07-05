using System.ComponentModel.DataAnnotations;
using STEnterprise.Models;

namespace STEnterprise.Areas.Inventory.Models
{
    //created by ataur
    public class Product:CommonModel
    {
        [Key]
        public int ProductId { get; set; }



        [Required]
        [Display(Name = "Product Name")]
        [StringLength(300, MinimumLength = 3, ErrorMessage = "Product Name must between alteast 3 to 300 characters.")]
        public string ProductName { get; set; }




        [Display(Name = "Product Description")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "Description must between alteast 5 to 500 characters.")]
        public string ProductDescription { get; set; }




        //[Required]
        [Display(Name = "Total Bag")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Numbers Only.")]
        public short TotalBag { get; set; }




        //[Required]
        [Display(Name = "Stock")]
        [RegularExpression(@"^(?=.*\d)\d*(?:\.\d\d)?$", ErrorMessage = "Numbers Only.")]
        public decimal Stock { get; set; }
    }
}