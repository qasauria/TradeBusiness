using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace STEnterprise.Areas.Sale.Models
{
    public class SaleDetail
    {
        [Key]
        public int SaleDetailId { get; set; }

        public int? SaleId { get; set; }
        public virtual SaleModel Sales { get; set; }
        public int? ProductId { get; set; }
        public string TruckNumber { get; set; }
        public decimal? SaleUnitBag { get; set; }
        public decimal? SaleUnitKG { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? ReturnSaleUnitBag { get; set; }
        public decimal? ReturnSaleUnitKG { get; set; }
        public decimal? ReturnAmount { get; set; }

        public SaleDetail()
        {
            Sales = new SaleModel();
        }

    }
}