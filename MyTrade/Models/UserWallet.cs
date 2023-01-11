using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyTrade.Models
{
    public class UserWallet
    {
        public string Country { get; set; }
        public string LoginId { get; set; }
        public string Amount { get; set; }
        public string DirectPayment { get; set; }
        public string CustomerId { get; set; }
        public string PaymentMode { get; set; }
        public string DDChequeNo { get; set; }
        public string DDChequeDate { get; set; }
        public string BankBranch { get; set; }
        public string BankName { get; set; }
        public string AddedBy { get; set; }
        public string ReferBy { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string CrAmount { get; set; }
        public string DrAmount { get; set; }
        public string Narration { get; set; }
        public string TransactionDate { get; set; }
        public string RoiWalletId { get; set; }
        public string Name { get; set; }
        public string TopUpAmount { get; set; }
        public string Date { get; set; }
        public string ROIId { get; set; }
        public string ROI { get; set; }
        public string FK_UserId { get; set; }
        public List<UserWallet> lstTps { get; set; }
        public List<UserWallet> lstROIIncome { get; set; }
        public List<UserWallet> lstROI { get; set; }
        public List<UserWallet> lstWalletLedger { get; set; }
        public string PaymentType { get; set; }
        public string Pk_EwalletId { get; set; }
        public string Remark { get; set; }
        public string Fk_Paymentid { get; set; }
        public string BranchName { get; set; }
        public string PK_RequestID { get; set; }
        public string Balance { get; set; }
        public string Pk_InvestmentId { get; set; }
        public dynamic OrderId { get; internal set; }
        public string Type { get; set; }
        public string TransactionType { get; set; }
        public string Fk_UserId { get; set; }
        public string Status { get; set; }
        public string TodaysCurrency { get; set; }
        public string IndianValue { get; set; }
        public string result { get; set; }
        public string ContactName { get; set; }
        public string Result { get; set; }
        
        public DataSet GetMemberDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", ReferBy),

                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetUserName", para);

            return ds;
        }

        public DataSet SaveEwalletRequest()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@Amount", Amount),
                                      new SqlParameter("@PaymentMode", PaymentMode) ,
                                      new SqlParameter("@DDChequeNo", DDChequeNo) ,
                                      new SqlParameter("@DDChequeDate", DDChequeDate) ,
                                      new SqlParameter("@BankBranch", BankBranch) ,
                                          new SqlParameter("@BankName", BankName),
                                            new SqlParameter("@Remarks", Remark),
                                            new SqlParameter("@AddedBy", AddedBy),
                                            new SqlParameter("@TodaysCurrency", IndianValue)
                                     };
            DataSet ds = DBHelper.ExecuteQuery("EwalletRequest", para);
            return ds;
        }
        public DataSet SaveEwalletRequestNew()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@Amount", Amount),
                                       new SqlParameter("@PaymentType", PaymentType) ,
                                      new SqlParameter("@PaymentMode", PaymentMode) ,
                                      new SqlParameter("@DDChequeNo", DDChequeNo) ,
                                      new SqlParameter("@DDChequeDate", DDChequeDate) ,
                                      new SqlParameter("@BankBranch", BankBranch) ,
                                          new SqlParameter("@BankName", BankName),
                                            new SqlParameter("@Remarks", Remark),
                                            new SqlParameter("@AddedBy", AddedBy),
                                             new SqlParameter("@OrderId", OrderId),
                                     };
            DataSet ds = DBHelper.ExecuteQuery("EwalletRequestNew", para);
            return ds;
        }
        public DataSet GetPaymentMode()
        {

            DataSet ds = DBHelper.ExecuteQuery("GetPaymentModeList");

            return ds;
        }
        public DataSet GetPaymentType()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetPaymentTypeForUser");
            return ds;
        }
        public DataSet GetROIWalletDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@FK_UserId", FK_UserId),
                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate)
                                     };

            DataSet ds = DBHelper.ExecuteQuery("GetROIWalletDetails", para);
            return ds;
        }
        public DataSet GetROIIncomeReportsDetails()
        {

            SqlParameter[] para = {
                  new SqlParameter("@Fk_UserId", FK_UserId),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate)
                                     };
            DataSet ds = DBHelper.ExecuteQuery("GetROIIncomeReportsDetails", para);
            return ds;
        }

        public DataSet GetROIDetails()
        {
            SqlParameter[] para = {
                  new SqlParameter("@Fk_UserId", FK_UserId),
                   new SqlParameter("@Pk_InvestmentId", Pk_InvestmentId)
                                     };


            DataSet ds = DBHelper.ExecuteQuery("GetROIDetails", para);
            return ds;
        }

        public DataSet GetEWalletDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@FK_UserId", FK_UserId),
                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate)
                                     };

            DataSet ds = DBHelper.ExecuteQuery("GetEWalletDetails", para);
            return ds;
        }

        public DataSet GetWalletBalance()
        {
            SqlParameter[] para = { new SqlParameter("@PK_USerID", FK_UserId)

            };
            DataSet ds = DBHelper.ExecuteQuery("GetWalletBalance", para);

            return ds;

        }
        public DataSet DeleteWallet()
        {
            SqlParameter[] para = {
                new SqlParameter("@PK_RequestID", PK_RequestID),
                  new SqlParameter("@DeletedBy", AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteWalletRequest", para);

            return ds;

        }
    }
    public class CreateOrderResponse
    {
        public string OrderId { get; set; }
        public string Status { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class FetchPaymentByOrder
    {
        public List<FetchPaymentByOrder> lstdetails { get; set; }

        public string Pk_UserId { get; set; }
        public string OrderId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Mobile { get; set; }
        public string LoginId { get; set; }
        public string Name { get; set; }
        public string Amount { get; set; }

        public DataSet GetDataForUpdateRazorPayStatus()
        {
            SqlParameter[] param = {
                                       new SqlParameter("@FromDate",FromDate),
                                       new SqlParameter("@ToDate",ToDate),
                                       new SqlParameter("@Mobile",Mobile),
                                   };
            DataSet ds = DBHelper.ExecuteQuery("GetDataForUpdateRazorPayStatus", param);
            return ds;
        }
    }
    public class OrderModel
    {
        public string orderId { get; set; }
        public string razorpayKey { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string contactNumber { get; set; }
        public string address { get; set; }
        public string description { get; set; }
        public string image { get; set; }
        public string FK_ProductId { get; set; }
    }
    public class FetchPaymentByOrderResponse
    {
        public string PaymentId { get; set; }
        public string entity { get; set; }
        public string amount { get; set; }
        public string currency { get; set; }
        public string status { get; set; }

        public string OrderId { get; set; }
        public string invoice_id { get; set; }
        public string international { get; set; }
        public string method { get; set; }
        public string amount_refunded { get; set; }
        public string refund_status { get; set; }
        public string captured { get; set; }
        public string description { get; set; }
        public string card_id { get; set; }
        public string bank { get; set; }
        public string wallet { get; set; }
        public string vpa { get; set; }
        public string email { get; set; }
        public string contact { get; set; }
        public notes notes { get; set; }
        public string fee { get; set; }
        public string tax { get; set; }
        public string error_code { get; set; }

        public string error_description { get; set; }
        public string error_source { get; set; }
        public string error_step { get; set; }
        public string error_reason { get; set; }
        public string created_at { get; set; }
        public string Pk_UserId { get; set; }
        public string FK_ProductId { get; set; }
        public DataSet GetDataForUpdateRazorPayStatus()
        {

            DataSet ds = DBHelper.ExecuteQuery("GetDataForUpdateRazorPayStatus");
            return ds;
        }
        public DataSet SaveFetchPaymentResponse()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@PaymentId", PaymentId),
                                      new SqlParameter("@entity", entity),
                                      new SqlParameter("@amount", amount),
                                      new SqlParameter("@currency", currency),
                                      new SqlParameter("@OrderId", OrderId),
                                      new SqlParameter("@status", status),
                                      new SqlParameter("@invoice_id", invoice_id),
                                      new SqlParameter("@international", international),
                                      new SqlParameter("@method", method),
                                      new SqlParameter("@amount_refunded", amount_refunded),
                                      new SqlParameter("@refund_status", refund_status),
                                      new SqlParameter("@captured", captured),
                                      new SqlParameter("@description", description),
                                      new SqlParameter("@card_id", card_id),
                                      new SqlParameter("@bank", bank),
                                      new SqlParameter("@wallet", wallet),
                                      new SqlParameter("@vpa", vpa),
                                      new SqlParameter("@email", email),
                                      new SqlParameter("@contact", contact),
                                      new SqlParameter("@fee", fee),
                                      new SqlParameter("@tax", tax),
                                      new SqlParameter("@error_code", error_code),
                                      new SqlParameter("@error_description", error_description),
                                      new SqlParameter("@error_source", error_source),
                                      new SqlParameter("@error_step", error_step),
                                      new SqlParameter("@error_reason", error_reason),
                                      new SqlParameter("@created_at", created_at),
                                      new SqlParameter("@Pk_UserId", Pk_UserId),

                                  };
            DataSet ds = DBHelper.ExecuteQuery("SaveFetchPaymentResponse", para);
            return ds;
        }

        public DataSet UpdateRazorpayStatus()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@PaymentId", PaymentId),
                                      new SqlParameter("@entity", entity),
                                      new SqlParameter("@amount", amount),
                                      new SqlParameter("@currency", currency),
                                      new SqlParameter("@OrderId", OrderId),
                                      new SqlParameter("@status", status),
                                      new SqlParameter("@invoice_id", invoice_id),
                                      new SqlParameter("@international", international),
                                      new SqlParameter("@method", method),
                                      new SqlParameter("@amount_refunded", amount_refunded),
                                      new SqlParameter("@refund_status", refund_status),
                                      new SqlParameter("@captured", captured),
                                      new SqlParameter("@description", description),
                                      new SqlParameter("@card_id", card_id),
                                      new SqlParameter("@bank", bank),
                                      new SqlParameter("@wallet", wallet),
                                      new SqlParameter("@vpa", vpa),
                                      new SqlParameter("@email", email),
                                      new SqlParameter("@contact", contact),
                                      new SqlParameter("@fee", fee),
                                      new SqlParameter("@tax", tax),
                                      new SqlParameter("@error_code", error_code),
                                      new SqlParameter("@error_description", error_description),
                                      new SqlParameter("@error_source", error_source),
                                      new SqlParameter("@error_step", error_step),
                                      new SqlParameter("@error_reason", error_reason),
                                      new SqlParameter("@created_at", created_at),
                                      new SqlParameter("@Pk_UserId", Pk_UserId),
                                        new SqlParameter("@FK_ProductId", FK_ProductId),
                                  };
            DataSet ds = DBHelper.ExecuteQuery("SavePaymentResponse", para);
            return ds;
        }
       
    }
    public class notes
    {
        public string notes_key_1 { get; set; }
        public string notes_key_2 { get; set; }
    }
}