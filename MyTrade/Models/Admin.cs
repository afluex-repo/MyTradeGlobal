using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyTrade.Models
{
    public class Admin : Common
    {
        #region property
        public string LoginId { get; set; }
        public string CustomerId { get; set; }
        public string Amount { get; set; }
        public string NoofPins { get; set; }
        public string FinalAmount { get; set; }
        public List<Admin> lstunusedpins { get; set; }
        public List<Admin> lstView { get; set; }
        public List<Admin> lstForUserPermission { get; set; }
        public List<Admin> lstgeneratepin { get; set; }
        public string Fk_InvestmentId { get; set; }
      
        public string ePinNo { get; set; }

        public string RegisteredTo { get; set; }
        public string RegisteredToName { get; set; }
        public string OwnerID { get; set; }
        public string OwnerName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Status { get; set; }
        public string ActivationDate { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }

        public string RequestID { get; set; }
        public string UserId { get; set; }
        public string RequestCode { get; set; }
        public string PaymentMode { get; set; }
        public string BankBranch { get; set; }
        public string ChequeDDNo { get; set; }
        public string ChequeDDDate { get; set; }

        public string PaymentTypeId { get; set; }
        public string PaymentType { get; set; }
        public List<Admin> lstWallet { get; set; }
        public string WalletId { get; set; }
        public string Name { get; set; }
        public string SponsorBonus { get; set; }
        public string PK_RequestID { get; set; }
        public List<Admin> lstEpinRequest { get; set; }
        public string ProductName { get; set; }
        public string RewardName { get; set; }
        public string RewardAmount { get; set; }
        public string Reward { get; set; }
        public List<Admin> lstTps { get; set; }
        public string CrAmount { get; set; }
        public decimal CrDrAmount { get; set; }
        public string DrAmount { get; set; }
        public string Narration { get; set; }
        public string RoiWalletId { get; set; }

        public string Date { get; set; }
        public string TopUpAmount { get; set; }
        public string ROIId { get; set; }
        public string ROI { get; set; }
        public List<Admin> lstROIIncome { get; set; }
        public List<Admin> lstROI { get; set; }
        public List<Admin> lst { get; set; }
        public string PK_PayoutWalletId { get; set; }
        public List<Admin> lstlevelIncome { get; set; }
        public List<Admin> lstlevel { get; set; }
        public List<Admin> lstPayout { get; set; }

        public string FromName { get; set; }
        public string FromLoginId { get; set; }
        public string BusinessAmount { get; set; }
        public string Percentage { get; set; }
        public string PayoutNo { get; set; }
        public string Level { get; set; }

        public string LevelIncomeTR1 { get; set; }
        public string LevelIncomeTR2 { get; set; }
        public string ClosingDate { get; set; }
        public string GrossAmount { get; set; }
        public string ProcessingFee { get; set; }
        public string TDSAmount { get; set; }
        public string NetAmount { get; set; }
        public string Remark { get; set; }
        public List<Admin> lstDistributePayment { get; set; }
        public List<Admin> lstDistributePaymentTPP { get; set; }
        public string DirectIncome { get; set; }
        public string LastClosingDate { get; set; }
        public string TPSLevelIncome { get; set; }
        public string TPPLevelIncome { get; set; }
        public string TPS { get; set; }
        public string Pk_UserId { get; set; }
        public List<Admin> lstBReports { get; set; }
        public string IsDownline { get; set; }
        public List<SelectListItem> ddlProductName { get; set; }
        public string PK_ProductID { get; set; }
        public string BV { get; set; }
        public string PackageType { get; set; }
        public string IFSCCode { get; set; }
        public string MemberAccNo { get; set; }
        public string Deduction { get; set; }
        public string Pk_AdvanceId { get; set; }
        public List<Admin> lstdeduction { get; set; }
        public List<Admin> lstKycUpdate { get; set; }
        public List<Admin> lstViewLedger { get; set; }
        public List<Admin> lstPaymentMode { get; set; }
        
        public string NomineeName { get; set; }
        public string NomineeAge { get; set; }
        public string NomineeRelation { get; set; }
        public string UPIID { get; set; }
        public string AdharNo { get; set; }
        public string PanNo { get; set; }
        public string PanImage { get; set; }
        public string IsVerified { get; set; }
        public string Response { get; set; }
        public string Message { get; set; }
        public string DeductionType { get; set; }
        public string Pk_investmentId { get; set; }
        public string CommissionPercentage { get; set; }
        public string ToName { get; set; }
        public string ToLoginID { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string TotalAmount { get; set; }
        public string Pk_EwalletId { get; set; }
        public string TransactionBy { get; set; }

        public string AvailablePin { get; set; }
        public string UsedPin { get; set; }
        public string TransferPin { get; set; }
        public string TotalPin { get; set; }
        public string PackageName { get; set; }
        public string PK_BannerId { get; set; }
        public string BannerImage { get; set; }
        public string FormId { get; set; }
        public string FormName { get; set; }
        public string Permission { get; set; }
        public string GST { get; set; }
        public string ToId { get; set; }
        public string TransferDate { get; set; }
        public string Balance { get; set; }
        public string Reason { get; set; }
        public string PK_ePinDetailsId { get; set; }
        public string PinGenerationDate { get; set; }
        public string IGST { get; set; }
        public string Pk_PinId { get; set; }

        public string PinUser { get; set; }
        public string UsedFor { get; set; }
        public string UsedDate { get; set; }
        public string ProductPrice { get; set; }
        public string GenerateVia { get; set; }
        public string PinAmount { get; set; }
        public string PinStaus { get; set; }
        public string Fk_ProductId { get; set; }
        public string AvailableBalance { get; set; }
        public List<Admin> lstWalletLedger { get; set; }
        public HttpPostedFileBase rewardImage { get; set; }
        public List<Admin> lstSponsor { get; set; }
        public DataTable bonazalist { get; set; }
        #endregion
        #region PinGenerated
        public DataSet CreatePin()
        {
            SqlParameter[] para = {


                                        new SqlParameter("@NoofPins", NoofPins),
                                         new SqlParameter("@LoginId", LoginId),
                                         new SqlParameter("@CreatedBy", AddedBy),
                                         new SqlParameter("@Amount", Amount),
                                         new SqlParameter("@FK_PaymentId",Fk_Paymentid),
                                         new SqlParameter("@TransactionDate",TransactionDate),
                                         new SqlParameter("@TransactionNo",TransactionNo),
                                         new SqlParameter("@bankname",BankName),
                                         new SqlParameter("@branchname",BranchName),
                                         new SqlParameter("@Fk_PackageId",Package)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("GenerateEPinByAdmin", para);
            return ds;
        }
        public DataSet BindPriceByProduct()
        {
            SqlParameter[] para = { new SqlParameter("@ProductId",Package) };
            DataSet ds = DBHelper.ExecuteQuery("GetProductList", para);
            return ds;
        }
        public DataSet GetUsedUnUsedPins()
        {
            SqlParameter[] para = {

                                        new SqlParameter("@Status", Status),
                                        new SqlParameter("@EPinNo", ePinNo),
                                        new SqlParameter("@Package", Fk_ProductId),
                                        new SqlParameter("@OwnerID", OwnerID ),
                                        new SqlParameter("@RegToId", RegisteredTo ),
                                        new SqlParameter("@Fk_UserId", Fk_UserId )
                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetUnusedUsedPins", para);
            return ds;
        }
        #endregion

        public string OtpVerify { get; set; }

        public DataSet ChangePassword()
        {
            SqlParameter[] para = {new SqlParameter("@OldPassword",Password),
                                   new SqlParameter("@NewPassword",NewPassword),
                                   new SqlParameter("@UpdatedBy",UpdatedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("AdminChangePassword", para);
            return ds;

        }

        public DataSet GetEwalletRequestDetailsForAdmin()
        {
            SqlParameter[] para = {
                new SqlParameter("@Fk_UserId",Fk_UserId),
                                   new SqlParameter("@FromDate",FromDate),
                                   new SqlParameter("@ToDate",ToDate),
                                    new SqlParameter("@Status",Status),
                                     new SqlParameter("@PaymentMode",PaymentMode)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetEwalletRequestDetailsForAdmin", para);

            return ds;
        }
        public DataSet WalletLedger()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@Name",Name)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("[PayoutWalletLedgerForAdmin]", para);
            return ds;
        }
        public DataSet GetEwalletRequestDetails()
        {
            SqlParameter[] para = {
                new SqlParameter("@Fk_UserId",Fk_UserId),
                                   new SqlParameter("@FromDate",FromDate),
                                   new SqlParameter("@ToDate",ToDate)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetEwalletRequestDetails", para);

            return ds;
        }
        public DataSet ApproveDeclineEwalletRequest()
        {
            SqlParameter[] para = {
                new SqlParameter("@Pk_RequestId",RequestID),
                 new SqlParameter("@Status",Status),
                new SqlParameter("@AddedBy",UpdatedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("ApproveDeclineEwalletRequest", para);
            return ds;
        }

        public DataSet UpdatePaymentType()
        {
            SqlParameter[] para = {

                  new SqlParameter("@PaymentTypeId",PaymentTypeId),
                 new SqlParameter("@IsActive",Status),
                new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("SavePaymentType", para);
            return ds;
        }

        public DataSet GetPaymentType()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetPaymentType");
            return ds;
        }

        public DataSet GetEPinRequestDetails()
        {
            SqlParameter[] para = {
                new SqlParameter("@Name",Name),
                 new SqlParameter("@LoginId",LoginId),
                new SqlParameter("@FromDate",FromDate),
                new SqlParameter("@Todate",ToDate)
            };

            DataSet ds = DBHelper.ExecuteQuery("GetEPinRequestDetails", para);
            return ds;
        }


        public DataSet AcceptRejectEPinRequest()
        {
            SqlParameter[] para = {
                new SqlParameter("@Pk_RequestId",RequestID),
                 new SqlParameter("@Status",Status),
                new SqlParameter("@AddedBy",UpdatedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("AcceptRejectEPinRequest", para);
            return ds;
        }

        public DataSet GetROIWalletDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@FK_UserId", Fk_UserId),
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
                  new SqlParameter("@Fk_UserId", Fk_UserId),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate)
                                     };
            DataSet ds = DBHelper.ExecuteQuery("GetROIIncomeReportsDetails", para);
            return ds;
        }
        public DataSet GetROIDetails()
        {
            SqlParameter[] para = {
                    new SqlParameter("@Fk_UserId", Fk_UserId),
                  new SqlParameter("@Pk_InvestmentId", Pk_investmentId)
                                     };
            DataSet ds = DBHelper.ExecuteQuery("GetROIDetails", para);
            return ds;
        }
        public DataSet PayoutWalletLedger()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
            };
            DataSet ds = DBHelper.ExecuteQuery("PayoutWalletLedger", para);
            return ds;
        }
        public DataSet LevelIncomeTr1()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                 new SqlParameter("@Name", Name),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetLevelIncomeTr1", para);
            return ds;
        }
        public DataSet LevelIncomeTr2()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                  new SqlParameter("@Name", Name),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetLevelIncomeTr2", para);
            return ds;
        }
        public DataSet PayoutDetail()
        {
            SqlParameter[] para = {
                new SqlParameter("@Fk_Userid", Fk_UserId),
                new SqlParameter("@PayoutNo", PayoutNo),
                new SqlParameter("@FromDate", FromDate),
                    new SqlParameter("@ToDate", ToDate),
                new SqlParameter("@LoginId", LoginId)
            };
            DataSet ds = DBHelper.ExecuteQuery("PayoutDetails", para);
            return ds;
        }
        public DataSet DistributePayment()
        {
            SqlParameter[] para = {
                new SqlParameter("@ClosingDate",ClosingDate)

            };
            DataSet ds = DBHelper.ExecuteQuery("MakePaymentList", para);
            return ds;
        }
        public DataSet SaveDistributePayment()
        {
            SqlParameter[] para = {
                new SqlParameter("@ClosingDate", ClosingDate)
            };
            DataSet ds = DBHelper.ExecuteQuery("AutoDistributePayment", para);
            return ds;
        }
        public DataSet DistributePaymentTPS()
        {
            SqlParameter[] para = {
                new SqlParameter("@ClosingDate",ClosingDate)

            };
            DataSet ds = DBHelper.ExecuteQuery("MakePaymentListTPS", para);
            return ds;
        }
        public DataSet ListForDistributePaymentTPS()
        {
            SqlParameter[] para = {
                new SqlParameter("@ClosingDate",ClosingDate)

            };
            DataSet ds = DBHelper.ExecuteQuery("ListForDistributePaymentTPSNew", para);
            return ds;
        }
        public DataSet SaveDistributePaymentTPS()
        {
            SqlParameter[] para = {
                new SqlParameter("@ClosingDate", ClosingDate)
            };
            DataSet ds = DBHelper.ExecuteQuery("AutoDistributePaymentTPS", para);
            return ds;
        }
        public DataSet GetBusinessReports()
        {
            SqlParameter[] para = {
                new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
                 new SqlParameter("@IsDownline", IsDownline),
                    new SqlParameter("@PackageType", PK_ProductID),
                       new SqlParameter("@Lvl", Level)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetBusiness", para);
            return ds;
        }
        public DataSet GetProductName()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetProductName");
            return ds;
        }
        public DataSet GetPayoutRequest()
        {
            SqlParameter[] para = {
                new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
                 new SqlParameter("@Status", Status)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetPayoutRequest", para);
            return ds;
        }
        public DataSet ApprovePayoutRequest()
        {
            SqlParameter[] para = {
                new SqlParameter("@Pk_RequestId",PK_RequestID),
                 new SqlParameter("@Status",Status),
                  new SqlParameter("@TransactionNo",TransactionNo),
                new SqlParameter("@ApprovedBy",UpdatedBy),
                new SqlParameter("@TransactionDate",TransactionDate)
            };
            DataSet ds = DBHelper.ExecuteQuery("ApprovePayoutRequest", para);
            return ds;
        }

        public DataSet DeclinePayoutRequest()
        {
            SqlParameter[] para = {
                new SqlParameter("@Pk_RequestId",PK_RequestID),
                 new SqlParameter("@Status",Status),
                  new SqlParameter("@TransactionNo",TransactionNo),
                new SqlParameter("@DeclinedBy",UpdatedBy),
                 new SqlParameter("@TransactionDate",TransactionDate),
                 new SqlParameter("@Narration",Narration)
            };
            DataSet ds = DBHelper.ExecuteQuery("DeclinePayoutRequest", para);
            return ds;
        }

        public DataSet GetNameDetails()
        {
            SqlParameter[] para = {
                new SqlParameter("@LoginId",LoginId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetNameDetails", para);
            return ds;
        }

        
        
        public DataSet SaveTransferWallet()
        {
            SqlParameter[] para = {
                 new SqlParameter("@Fk_UserId",Fk_UserId),
                new SqlParameter("@Amount",CrAmount),
                 new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("SaveTransferWallet", para);
            return ds;
        }
        public DataSet SaveDeduction()
        {
            SqlParameter[] para = {
                   new SqlParameter("@Fk_UserId",Fk_UserId),
               new SqlParameter("@Amount",CrAmount),
              new SqlParameter("@Narration",Narration),
               new SqlParameter("@Type",DeductionType),
                new SqlParameter("@Remarks",Remark),
                  new SqlParameter("@TransactionDate",TransactionDate),
                 new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("SaveDeduction", para);
            return ds;
        }
        public DataSet GetAdvanceDeductionReports()
        {
            SqlParameter[] para = {
               new SqlParameter("@LoginId",LoginId),
                new SqlParameter("@Type",DeductionType)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetAdvanceDeductionReports", para);
            return ds;
        }
        public DataSet GetKYCUpdateDetailsOfUser()
        {
            SqlParameter[] para = {
               new SqlParameter("@IsVerified",IsVerified)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetKYCUpdateDetailsOfUser", para);
            return ds;
        }
        public DataSet PaidIncome()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@PayoutNo", PayoutNo)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetPaidIncomes", para);
            return ds;
        }

        public DataSet GetTotalCrDrAmount()
        {
            SqlParameter[] para = { new SqlParameter("@FK_UserId", Fk_UserId)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetTotalCrDrAmount", para);
            return ds;
        }

        public DataSet GetWalletLedgerDetails()
        {
            SqlParameter[] para = { new SqlParameter("@FK_UserId", Fk_UserId)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetWalletLedgerDetails", para);
            return ds;
        }

        public DataSet GetUnusedUsedPinsForAdmin()
        {
            SqlParameter[] para = {
                new SqlParameter("@LoginId", LoginId),
                 new SqlParameter("@ProductName", Package)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetUnusedUsedPinsForAdmin", para);
            return ds;
        }
        public DataSet SaveBannerImage()
        {
            SqlParameter[] para = {
                new SqlParameter("@BannerImage", BannerImage),
                 new SqlParameter("@AddedBy", AddedBy)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("SaveBannerImage", para);
            return ds;
        }
        public DataSet GetBannerImage()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetBanneImage");
            return ds;
        }
        public DataSet DeleteBanner()
        {
            SqlParameter[] para = {
                new SqlParameter("@PK_BannerId", PK_BannerId),
                 new SqlParameter("@DeletedBy", AddedBy)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("DeleteBanner", para);
            return ds;
        }
        public DataSet GetFormMasterList()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetFormMasterList");
            return ds;
        }


        public DataSet ActiveUser()
        {
            SqlParameter[] para = {
                new SqlParameter("@PK_FormId", FormId),
                 new SqlParameter("@AddedBy", AddedBy)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("ActiveUser", para);
            return ds;
        }

        public DataSet InActiveUser()
        {
            SqlParameter[] para = {
                new SqlParameter("@PK_FormId", FormId),
                 new SqlParameter("@AddedBy", AddedBy),
                 new SqlParameter("@Reason",Reason)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("InActiveUser", para);
            return ds;
        }
        public DataSet CreateTransaction()
        {
            SqlParameter[] para = {
                   new SqlParameter("@WalletId",WalletId),
                   new SqlParameter("@Fk_UserId",Fk_UserId),
                   new SqlParameter("@Amount",CrDrAmount),
                   new SqlParameter("@Narration",Narration),
                   new SqlParameter("@Type",DeductionType),
                   new SqlParameter("@Remarks",Remark),
                   new SqlParameter("@TransactionDate",TransactionDate),
                   new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("CreateTransaction", para);
            return ds;
        }
        public DataSet GetPinGeneratedByAdmin()
        {
            SqlParameter[] para = {
            };
            DataSet ds = DBHelper.ExecuteQuery("GetPinsGeneratedByAdmin"); 
            return ds;
        }
        
        public DataSet GetDistributedTPSList()
        {
            SqlParameter[] para = {
                new SqlParameter("@LoginId",LoginId),
                new SqlParameter("@PayoutNo", PayoutNo),
                new SqlParameter("@FK_InvestmentId", Fk_InvestmentId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetPaidIncomesForTPS", para);
            return ds;
        }
        //public DataSet SaveBonazaList()
        //{
        //    SqlParameter[] para = {
        //        new SqlParameter("@RewardName",RewardName),
        //        new SqlParameter("@FromDate", FromDate),
        //        new SqlParameter("@ToDate", ToDate),
        //        new SqlParameter("@AddedBy",AddedBy),
        //        new SqlParameter("@BonazaDetails",bonazalist)
        //    };
        //    DataSet ds = DBHelper.ExecuteQuery("SaveBonaza", para);
        //    return ds;
        //}
        public DataSet GetGeneratedEpinDetails()
        {
            SqlParameter[] para = {
                new SqlParameter("@Fk_UserId", Fk_UserId),
                new SqlParameter("@EPinNo", ePinNo),
                    new SqlParameter("@Status", Status),
                new SqlParameter("@FK_ProductId", Fk_ProductId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetPinDetailsGeneratedByAdmin", para);
            return ds;
        }

        #region
        public DataSet TPSPayoutDetail()
        {
            SqlParameter[] para = {
                new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@PayoutNo", PayoutNo),
                //new SqlParameter("@FromDate", FromDate),
                //    new SqlParameter("@ToDate", ToDate),
               // new SqlParameter("@LoginId", LoginId)
            };
            DataSet ds = DBHelper.ExecuteQuery("TPSPayoutDetails", para);
            return ds;
        }
        #endregion


        public DataSet GetNameDetailsforTransferwallet()
        {
            SqlParameter[] para = {
                new SqlParameter("@CustomerId",CustomerId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetNameDetailsforTransferwallet", para);
            return ds;
        }


        public DataSet GetSponsorIncomeReport()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                  new SqlParameter("@Name", Name),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetSponsorIncomeReport", para);
            return ds;
        }
        
    }
}