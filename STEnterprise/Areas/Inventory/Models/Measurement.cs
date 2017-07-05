using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using STEnterprise.Models;

namespace STEnterprise.Areas.Inventory.Models
{
    public class Measurement : CommonModel
    {
        [Key]
        public short MeasurementId { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="Measurement name is required")]
        [DataType(DataType.Text)]
        [Display(Name ="Measurement Name")]
        [StringLength(maximumLength:100,ErrorMessage = "Measurement Name cannot be longer than 100 characters.",MinimumLength =2)]
        [RegularExpression("([a-zA-Z .&'-]+)", ErrorMessage = "Enter Valid Measurement Name !")]
        public string MeasurementName { get; set; }       

    }
}