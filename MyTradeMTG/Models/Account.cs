using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace MyTradeMTG.Models
{
    public class Account
    {
        public string LoginId { get; set; }
        public string PackageId { get; set; }
        public string PackageTypeId { get; set; }
        public string TopUpDate { get; set; }
        public string Amount { get; set; }
        public string Remarks { get; set; }
        public string FK_UserId { get; set; }
        public string PaymentMode { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string TransactionNo { get; set; }
        public string TransactionDate { get; set; }
        public string AddedBy { get; set; }
        public string TotalAmount { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public List<Account> lstTopUp { get; set; }
        public string InvestmentId { get; set; }
        public string Name { get; set; }
        public string PinAmount { get; set; }
        public string UsedFor { get; set; }
        public string BV { get; set; }
        public string IsCalculated { get; set; }
        public string TransactionBy { get; set; }
        public string Status { get; set; }
        public string ROIPercentage { get; set; }
        public string Pk_userId { get; set; }
        public string PaymentType { get; set; }
        public string ProductName { get; set; }
        public string Remark { get; set; }
        public string PackageDays { get; set; }
        public string Email { get; set; }
        public string ActivationMyTradeMTG { get; set; }
        public string CustomerId { get; set; }
        public string ROI { get; set; }
        public string Topupid { get; set; }

        public List<SelectListItem> ddlProduct { get; set; }



        



        public DataSet TopUp()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginId", LoginId),
                                        new SqlParameter("@AddedBy", FK_UserId),
                                        new SqlParameter("@Fk_ProductId",PackageId),
                                        new SqlParameter("@Amount", Amount),
                                        new SqlParameter("@ActivationMyTradeMTGToken", ActivationMyTradeMTGToken)
                                 };
            DataSet ds = DBHelper.ExecuteQuery("TopUp", para);
            return ds;
        }
        public DataSet TopUpByAdmin()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginId", LoginId),
                                        new SqlParameter("@AddedBy",AddedBy),
                                        new SqlParameter("@Fk_ProductId",PackageId),
                                        new SqlParameter("@Amount", Amount)
                                 };
            DataSet ds = DBHelper.ExecuteQuery("TopUpByAdmin", para);
            return ds;
        }
        public DataSet GetTopUpDetails()
        {
            SqlParameter[] para = {
                                       new SqlParameter("@FK_UserId", FK_UserId),
                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate)
                                 };
            DataSet ds = DBHelper.ExecuteQuery("GetTopUpDetails", para);
            return ds;
        }

        public DataSet GetAssociateDashboard()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", FK_UserId), };
            DataSet ds = DBHelper.ExecuteQuery("GetDashBoardDetailsForAssociate", para);
            return ds;
        }

        public DataSet GetTotalWalletAmount()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetTotalWalletAmountOfMyTradeMTG");
            return ds;
        }
        

        public DataSet GetProductListForTopUp()
        {
            SqlParameter[] para = {

                new SqlParameter("@PackageTypeId", PackageTypeId),

            };
            DataSet ds = DBHelper.ExecuteQuery("GetProductListForTopUp", para);
            return ds;
        }

        public DataSet GetActivationMyTradeMTGForTopUp()
        {
            SqlParameter[] para = {

                new SqlParameter("@PackageTypeId", PackageTypeId),

            };
            DataSet ds = DBHelper.ExecuteQuery("GetProductListForTopUp", para);
            return ds;
        }

        public string ActivationMyTradeMTGToken { get; set; }

    }
}