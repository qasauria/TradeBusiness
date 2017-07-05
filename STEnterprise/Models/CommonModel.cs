using System;
using System.ComponentModel.DataAnnotations;

namespace STEnterprise.Models
{
    public class CommonModel
    {
        [Display(Name = "User Status")]
        public bool IsActive { get; set; }
        [Display(Name = "User Status")]
        public string UserStatus { get; set; }
        public short CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public short? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public byte? SortedBy { get; set; }
        public string Remarks { get; set; }
    }
}