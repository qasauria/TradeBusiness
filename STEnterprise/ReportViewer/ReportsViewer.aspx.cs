using System;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using STEnterprise.BLL;
using ReportObject = STEnterprise.CommonObject.ReportObject;

namespace STEnterprise.ReportViewer
{
    public partial class ReportsViewer : System.Web.UI.Page
    {
        readonly ReportBLL objReportBLL = new ReportBLL();
        private DataTable dtCommonDataTable;
        private string reportPath = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ShowCrystalReport();
        }

        private void ShowCrystalReport()
        {

            if (ReportObject.ReportType != null)
            {
                switch (ReportObject.ReportType)
                {
                    case "DailyBuyReport":
                        dtCommonDataTable = new DataTable();
                        dtCommonDataTable = objReportBLL.GetDailyBuyReport();
                        //dtCommonDataTable.TableName = "DailyBuyReport";
                        //dtCommonDataTable.WriteXmlSchema(Server.MapPath(".") + "/XMLFile.xml");
                        reportPath = "~/Rpt/DailyBuyReport.rpt";
                        break;
                    case "DailyLocalSaleReport":
                        dtCommonDataTable = new DataTable();
                        dtCommonDataTable = objReportBLL.GetDailyLocalSaleReport();
                        //dtCommonDataTable.TableName = "DailyLocalSaleReport";
                        //dtCommonDataTable.WriteXmlSchema(Server.MapPath(".") + "/XMLFile.xml");
                        reportPath = "~/Rpt/DailyLocalSaleReport.rpt";
                        break;
                    case "ConsignmentWiseSale":
                        dtCommonDataTable = new DataTable();
                        dtCommonDataTable = objReportBLL.GetConsignmentWiseSaleReport();
                        //dtCommonDataTable.TableName = "ConsignmentWiseSaleReport";
                        //dtCommonDataTable.WriteXmlSchema(Server.MapPath(".") + "/XMLFile.xml");
                        reportPath = "~/Rpt/ConsignmentWiseSale.rpt";
                        break;
                    case "DailyStockReport":
                        dtCommonDataTable = new DataTable();
                        dtCommonDataTable = objReportBLL.GetDailyStockReport();
                        //dtCommonDataTable.TableName = "DailyStockReport";
                        //dtCommonDataTable.WriteXmlSchema(Server.MapPath(".") + "/XMLFile2.xml");
                        reportPath = "~/Rpt/DailyStockReport.rpt";
                        break;
                    case "DailyDueStatement":
                        dtCommonDataTable = new DataTable();
                        dtCommonDataTable = objReportBLL.GetDailyDueStatementReport();
                        //dtCommonDataTable.TableName = "DailyBuyReport";
                        //dtCommonDataTable.WriteXmlSchema(Server.MapPath(".") + "/XMLFile.xml");
                        reportPath = "~/Rpt/DailyDueStatement.rpt";
                        break;
                    case "DailyPayStatement":
                        dtCommonDataTable = new DataTable();
                        dtCommonDataTable = objReportBLL.GetDailyPayStatementReport();
                        //dtCommonDataTable.TableName = "DailyBuyReport";
                        //dtCommonDataTable.WriteXmlSchema(Server.MapPath(".") + "/XMLFile.xml");
                        reportPath = "~/Rpt/DailyPayStatement.rpt";
                        break;
                    case "DailyForeignSaleReport":
                        dtCommonDataTable = new DataTable();
                        dtCommonDataTable = objReportBLL.GetDailyForeignSaleReport();
                        //dtCommonDataTable.TableName = "DailyForeignSalesReport";
                        //dtCommonDataTable.WriteXmlSchema(Server.MapPath(".") + "/XMLFile.xml");
                        reportPath = "~/Rpt/DailyForeignSaleReport.rpt";
                        break;
                    case "DailyPartlyLedger":
                        dtCommonDataTable = new DataTable();
                        dtCommonDataTable = objReportBLL.GetDailyPartlyLedgerReport();
                        //dtCommonDataTable.TableName = "DailyPartyLedgerReport";
                        //dtCommonDataTable.WriteXmlSchema(Server.MapPath(".") + "/XMLFile.xml");
                        reportPath = "~/Rpt/DailyPartlyLedger.rpt";
                        break;
                    case "DailyTotalProductionCost":
                        dtCommonDataTable = new DataTable();
                        dtCommonDataTable = objReportBLL.GetDailyTotalProductionCostReport();
                        //dtCommonDataTable.TableName = "DailyTotalProductionCost";
                        //dtCommonDataTable.WriteXmlSchema(Server.MapPath(".") + "/XMLFile7.xml");
                        reportPath = "~/Rpt/DailyTotalProductionCost.rpt";
                        break;
                    case "IncomeStatementReport":
                        dtCommonDataTable = new DataTable();
                        dtCommonDataTable = objReportBLL.GetIncomeStatementReport();
                        //dtCommonDataTable.TableName = "IncomeStatementReport";
                        //dtCommonDataTable.WriteXmlSchema(Server.MapPath(".") + "/XMLFileB.xml");
                        reportPath = "~/Rpt/IncomeStatementReport.rpt";
                        break;
                    case "SalesLedgerAccount":
                        dtCommonDataTable = new DataTable();
                        dtCommonDataTable = objReportBLL.GetSalesLedgerAccount();
                        //dtCommonDataTable.TableName = "SalesLedgerAccountReport";
                        //dtCommonDataTable.WriteXmlSchema(Server.MapPath(".") + "/XMLFileB.xml");
                        reportPath = "~/Rpt/SalesLedgerAccountReport.rpt";
                        break;
                    case "PurchaseLedgerAccount":
                        dtCommonDataTable = new DataTable();
                        dtCommonDataTable = objReportBLL.GetPurchaseLedgerAccount();
                        //dtCommonDataTable.TableName = "PurchaseLedgerAccountReport";
                        //dtCommonDataTable.WriteXmlSchema(Server.MapPath(".") + "/XMLFileB.xml");
                        reportPath = "~/Rpt/PurchaseLedgerAccountReport.rpt";
                        break;
                    case "ChequePayment":
                        dtCommonDataTable = new DataTable();
                        dtCommonDataTable = objReportBLL.GetChequePaymentForPurchase();
                        //dtCommonDataTable.TableName = "ChequePaymentForPurchaseReport";
                        //dtCommonDataTable.WriteXmlSchema(Server.MapPath(".") + "/ChequePayment.xml");
                        reportPath = "~/Rpt/ChequePaymentForPurchaseReport.rpt";
                        break;
                    case "ChequeReceived":
                        dtCommonDataTable = new DataTable();
                        dtCommonDataTable = objReportBLL.GetChequeReceivedForSale();
                        //dtCommonDataTable.TableName = "ChequeReceivedForSaleReport";
                        //dtCommonDataTable.WriteXmlSchema(Server.MapPath(".") + "/ChequeReceived.xml");
                        reportPath = "~/Rpt/ChequeReceivedForSaleReport.rpt";
                        break;
                    case "ConsignmentWiseTruckFare":
                        dtCommonDataTable = new DataTable();
                        dtCommonDataTable = objReportBLL.GetConsignmentWiseTruckFare();
                        //dtCommonDataTable.TableName = "ConsignmentWiseTruckFairReport";
                        //dtCommonDataTable.WriteXmlSchema(Server.MapPath(".") + "/ConsignmentWiseTruckFair.xml");
                        reportPath = "~/Rpt/ConsignmentWiseTruckFairReport.rpt";
                        break;
                    case "DailyTotalCostEverySubject":
                        dtCommonDataTable = new DataTable();
                        dtCommonDataTable = objReportBLL.GetDailyTotalCostEverySubject();
                        //dtCommonDataTable.TableName = "DailyTotalCostEverySubjectReport";
                        //dtCommonDataTable.WriteXmlSchema(Server.MapPath(".") + "/DailyTotalCostEverySubject.xml");
                        reportPath = "~/Rpt/DailyTotalCostEverySubjectReport.rpt";
                        break;
                    case "BuyerDetail":
                        dtCommonDataTable = new DataTable();
                        dtCommonDataTable = objReportBLL.GetBuyerDetail();
                        //dtCommonDataTable.TableName = "BuyerDetailReport";
                        //dtCommonDataTable.WriteXmlSchema(Server.MapPath(".") + "/BuyerDetail.xml");
                        reportPath = "~/Rpt/BuyerDetailReport.rpt";
                        break;
                    default: break;
                }
                
                var document = new ReportDocument();
                document.Load(Server.MapPath(reportPath));
                document.SetDataSource(dtCommonDataTable);
                document.SetParameterValue("@DateRange","Date From "+ 
                    Convert.ToDateTime(ReportObject.FromDate).ToString("dd/MM/yy") +" To " + Convert.ToDateTime(ReportObject.ToDate).ToString("dd/MM/yy"));
                CrystalReportViewer1.ReportSource = document;
                CrystalReportViewer1.DisplayToolbar = true;
                CrystalReportViewer1.ToolPanelView = ToolPanelViewType.None;
                CrystalReportViewer1.Zoom(125);
            }
        }
    }
}