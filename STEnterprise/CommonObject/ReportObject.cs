using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace STEnterprise.CommonObject
{
    public static class ReportObject
    {
        public static string FromDate { get; set; }
        public static string ToDate { get; set; }
        public static string ReportType { get; set; }
        public static string ConsignmentNumber { get; set; }
        public static int? SupplierId { get; set; }
        public static int? CustomerId { get; set; }
        public static int? ProductId { get; set; }
        public static int? TruckDetailId { get; set; }
        public static int? CountryId { get; set; }
    }
}