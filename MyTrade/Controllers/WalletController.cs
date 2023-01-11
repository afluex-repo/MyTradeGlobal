using Mytrade.Controllers;
using MyTrade.Filter;
using MyTrade.Models;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyTrade.Controllers
{
    public class WalletController : UserBaseController
    {
        // GET: Wallet
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetMemberName(string LoginId)
        {
            Common obj = new Common();
            obj.ReferBy = LoginId;
            DataSet ds = obj.GetMemberDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                obj.DisplayName = ds.Tables[0].Rows[0]["FullName"].ToString();
                obj.Result = "Yes";
            }
            else { obj.Result = "No"; }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddWallet()
        {
            UserWallet obj = new UserWallet();
            #region ddlpaymentType
            List<SelectListItem> ddlpaymentType = new List<SelectListItem>();
            DataSet dsp = obj.GetPaymentType();
            int count1 = 0;
            if (dsp != null && dsp.Tables.Count > 0 && dsp.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsp.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlpaymentType.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlpaymentType.Add(new SelectListItem { Text = r["PaymentType"].ToString(), Value = r["PK_PaymentTypeId"].ToString() });
                    count1 = count1 + 1;
                }
            }
            ViewBag.ddlpaymentType = ddlpaymentType;
            #endregion

            obj.Country = Session["Country"].ToString();
            obj.LoginId = Session["LoginId"].ToString();
            if (Session["IdActivated"].ToString() == "true")
            {
                obj.BankBranch = Session["Branch"].ToString();
                obj.BankName = Session["Bank"].ToString();
            }

            #region Check Balance
            obj.FK_UserId = Session["Pk_UserId"].ToString();
            DataSet ds = obj.GetWalletBalance();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.WalletBalance = ds.Tables[0].Rows[0]["amount"].ToString();
            }
            #endregion

            #region ddlpaymentmode
            int count = 0;
            List<SelectListItem> ddlpaymentmode = new List<SelectListItem>();
            DataSet ds1 = obj.GetPaymentMode();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlpaymentmode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "" });
                    }
                    ddlpaymentmode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlpaymentmode = ddlpaymentmode;


            obj.PaymentType = "Offline";
            #endregion

            return View(obj);
        }


        [HttpPost]
        [ActionName("AddWallet")]
        [OnAction(ButtonName = "Save")]
        public ActionResult AddWallet(UserWallet model)
        {
            try
            {
                model.DDChequeDate = string.IsNullOrEmpty(model.DDChequeDate) ? null : Common.ConvertToSystemDate(model.DDChequeDate, "dd/mm/yyyy");
                model.AddedBy = Session["Pk_userId"].ToString();
                if (model.PaymentMode == "1")
                {
                    model.BankName = null;
                    model.BankBranch = null;
                }
                if (model.PaymentType == "2")
                {
                    DataSet ds = model.SaveEwalletRequest();
                    if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            TempData["msg"] = "Requested Successfully.";
                        }
                        else
                        {
                            TempData["error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                    else { }
                }
                else if (model.PaymentType == "1")
                {
                    OrderModel orderModel = new OrderModel();
                    string random = Common.GenerateRandom();
                    CreateOrderResponse obj1 = new CreateOrderResponse();
                    try
                    {
                        decimal amount = Convert.ToDecimal(model.Amount) * 100;
                        Dictionary<string, object> options = new Dictionary<string, object>();
                        options.Add("amount", Convert.ToInt32(amount)); // amount in the smallest currency unit
                        options.Add("receipt", random);
                        options.Add("currency", "INR");
                        options.Add("payment_capture", "1");

                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        RazorpayClient client = new RazorpayClient(PaymentGateWayDetails.KeyName, PaymentGateWayDetails.SecretKey);
                        Razorpay.Api.Order order = client.Order.Create(options);
                        obj1.OrderId = order["id"].ToString();
                        obj1.Status = "0";
                        model.OrderId = order["id"].ToString();
                        model.LoginId = Session["LoginId"].ToString();
                        model.AddedBy = Session["Pk_UserId"].ToString();
                        model.PaymentType = "Online";
                        model.PaymentMode = "12";
                        model.Amount = amount.ToString();
                        orderModel.orderId = order.Attributes["id"];
                        orderModel.razorpayKey = "rzp_live_k8z9ufVw0R0MLV";
                        orderModel.amount = Convert.ToInt32(amount);
                        orderModel.currency = "INR";
                        orderModel.description = "Recharge Wallet";
                        orderModel.name = Session["FullName"].ToString();
                        orderModel.contactNumber = Session["Contact"].ToString();
                        orderModel.email = Session["Email"].ToString();
                        orderModel.image = "http://mytrade.co.in/MyTradeWebsite/assets/img/logo.png";
                        DataSet ds = model.SaveEwalletRequestNew();
                        return View("PaymentPage", orderModel);
                        // Return on PaymentPage with Order data
                    }
                    catch (Exception ex)
                    {
                        obj1.Status = "1";
                        TempData["error"] = ex.Message;
                        return RedirectToAction("AddWallet", "Wallet");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return RedirectToAction("AddWallet", "Wallet");
        }

        
        public ActionResult FetchPaymentByOrder(OrderModel model)
        {
            FetchPaymentByOrder obj = new FetchPaymentByOrder();
            FetchPaymentByOrderResponse obj1 = new FetchPaymentByOrderResponse();
            string random = Common.GenerateRandom();
            try
            {
                obj.OrderId = model.orderId;
                obj1.Pk_UserId = Session["Pk_UserId"].ToString();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                RazorpayClient client = new RazorpayClient(PaymentGateWayDetails.KeyName, PaymentGateWayDetails.SecretKey);
                List<Razorpay.Api.Payment> orderdetails = client.Order.Payments(obj.OrderId);
                if (orderdetails.Count > 0)
                {
                    for (int i = 0; i <= orderdetails.Count - 1; i++)
                    {
                        dynamic rr = orderdetails[i].Attributes;
                        obj1.PaymentId = rr["id"];
                        obj1.entity = rr["entity"];
                        obj1.amount = rr["amount"];
                        obj1.currency = rr["currency"];
                        obj1.status = rr["status"];
                        obj1.OrderId = rr["order_id"];
                        obj1.invoice_id = rr["invoice_id"];
                        obj1.international = rr["international"];
                        obj1.method = rr["method"];
                        obj1.amount_refunded = rr["amount_refunded"];
                        obj1.refund_status = rr["refund_status"];
                        obj1.captured = rr["captured"];
                        obj1.description = rr["description"];
                        obj1.card_id = rr["card_id"];
                        obj1.bank = rr["bank"];
                        obj1.wallet = rr["wallet"];
                        obj1.vpa = rr["vpa"];
                        obj1.email = rr["email"];
                        obj1.contact = rr["contact"];
                        obj1.fee = rr["fee"];
                        obj1.tax = rr["tax"];
                        obj1.error_code = rr["error_code"];
                        obj1.error_description = rr["error_description"];
                        obj1.error_source = rr["error_source"];
                        obj1.error_step = rr["error_step"];
                        obj1.error_reason = rr["error_reason"];
                        obj1.created_at = rr["created_at"];

                        DataSet ds = obj1.SaveFetchPaymentResponse();
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                            {
                                if (obj1.captured == "True")
                                {
                                    TempData["Msg"] = "Amount credited successfully. Order Id : " + obj1.OrderId + " and PaymentId : " + obj1.PaymentId;
                                }
                                else
                                {
                                    TempData["error"] = "Payment Failed";
                                }

                                return RedirectToAction("AddWallet", "Wallet");
                            }
                        }
                    }
                }
                else
                {
                    obj1.OrderId = obj.OrderId;
                    obj1.captured = "Failed";
                    TempData["error"] = "Payment Failed";
                    obj1.Pk_UserId = obj.Pk_UserId;
                    DataSet ds = obj1.SaveFetchPaymentResponse();
                    return RedirectToAction("AddWallet", "Wallet");
                }
            }
            catch (Exception ex)
            {
                obj1.OrderId = obj.OrderId;
                obj1.captured = ex.Message;
                TempData["error"] = ex.Message;
                obj1.Pk_UserId = obj.Pk_UserId;
                DataSet ds = obj1.SaveFetchPaymentResponse();
                return RedirectToAction("AddWallet", "Wallet");
            }
            return Json(obj1, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ROIWallet()
        {
            UserWallet model = new UserWallet();
            List<UserWallet> lst = new List<UserWallet>();
            model.FK_UserId = Session["Pk_UserId"].ToString();
            DataSet ds = model.GetROIWalletDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserWallet obj = new UserWallet();
                    obj.RoiWalletId = r["Pk_ROIWalletId"].ToString();
                    obj.CrAmount = r["CrAmount"].ToString();
                    obj.DrAmount = r["DrAmount"].ToString();
                    obj.Narration = r["Narration"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lstTps = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("ROIWallet")]
        [OnAction(ButtonName = "Search")]
        public ActionResult ROIWallet(UserWallet model)
        {
            List<UserWallet> lst = new List<UserWallet>();
            model.FK_UserId = Session["Pk_UserId"].ToString();
            model.FK_UserId = model.FK_UserId == "0" ? null : model.FK_UserId;
            model.LoginId = model.LoginId == "0" ? null : model.LoginId;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetROIWalletDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserWallet obj = new UserWallet();
                    obj.RoiWalletId = r["Pk_ROIWalletId"].ToString();
                    obj.CrAmount = r["CrAmount"].ToString();
                    obj.DrAmount = r["DrAmount"].ToString();
                    obj.Narration = r["Narration"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lstTps = lst;
            }
            return View(model);

        }
        public ActionResult ROIIncomeReports()
        {
            UserWallet model = new UserWallet();
            List<UserWallet> lst = new List<UserWallet>();
            model.FK_UserId = Session["Pk_UserId"].ToString();
            DataSet ds = model.GetROIIncomeReportsDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserWallet obj = new UserWallet();
                    obj.ROIId = r["Pk_ROIId"].ToString();
                    obj.Pk_InvestmentId = r["Pk_InvestmentId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.TopUpAmount = r["TopUpAmount"].ToString();
                    obj.Date = r["TopUpDate"].ToString();
                    lst.Add(obj);
                }
                model.lstROIIncome = lst;
                ViewBag.TopUpAmount = double.Parse(ds.Tables[0].Compute("sum(TopUpAmount)", "").ToString()).ToString("n2");
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("ROIIncomeReports")]
        [OnAction(ButtonName = "Search")]
        public ActionResult ROIIncomeReports(UserWallet model)
        {
            List<UserWallet> lst = new List<UserWallet>();
            model.FK_UserId = Session["Pk_UserId"].ToString();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetROIIncomeReportsDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserWallet obj = new UserWallet();
                    obj.ROIId = r["Pk_ROIId"].ToString();
                    obj.Pk_InvestmentId = r["Pk_InvestmentId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.TopUpAmount = r["TopUpAmount"].ToString();
                    obj.Date = r["TopUpDate"].ToString();
                    lst.Add(obj);
                }
                model.lstROIIncome = lst;
                ViewBag.TopUpAmount = double.Parse(ds.Tables[0].Compute("sum(TopUpAmount)", "").ToString()).ToString("n2");
            }
            return View(model);
        }
        public ActionResult ViewROI(string InvId)
        {
            UserWallet model = new UserWallet();
            model.Pk_InvestmentId = InvId;
            List<UserWallet> lst = new List<UserWallet>();
            model.FK_UserId = Session["Pk_UserId"].ToString();
            DataSet ds = model.GetROIDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserWallet obj = new UserWallet();
                    obj.ROI = r["Pk_ROIId"].ToString();
                    obj.ROI = r["ROI"].ToString();
                    obj.Date = r["ROIDate"].ToString();
                    obj.Status = r["Status"].ToString();
                    lst.Add(obj);
                }
                model.lstROI = lst;
            }
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                ViewBag.ReceivedAmount = ds.Tables[1].Rows[0]["ReceivedAmount"].ToString();
                ViewBag.TotalAmount = ds.Tables[1].Rows[0]["TotalAmount"].ToString();
                ViewBag.BalanceAmount = ds.Tables[1].Rows[0]["BalanceAmount"].ToString();
            }
            return View(model);
        }
        public ActionResult ViewTPS(string InvId)
        {
            UserWallet model = new UserWallet();
            model.Pk_InvestmentId = InvId;
            List<UserWallet> lst = new List<UserWallet>();
            model.FK_UserId = Session["Pk_UserId"].ToString();
            DataSet ds = model.GetROIDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserWallet obj = new UserWallet();
                    obj.ROI = r["Pk_ROIId"].ToString();
                    obj.ROI = r["ROI"].ToString();
                    obj.Date = r["ROIDate"].ToString();
                    obj.Status = r["Status"].ToString();
                    lst.Add(obj);
                }
                model.lstROI = lst;
            }
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                ViewBag.ReceivedAmount = ds.Tables[1].Rows[0]["ReceivedAmount"].ToString();
                ViewBag.TotalAmount = ds.Tables[1].Rows[0]["TotalAmount"].ToString();
                ViewBag.BalanceAmount = ds.Tables[1].Rows[0]["BalanceAmount"].ToString();
            }
            return View(model);
        }
        public ActionResult WalletList()
        {
            Admin model = new Admin();
            List<Admin> lst = new List<Admin>();
            model.Fk_UserId = Session["Pk_UserId"].ToString();
            DataSet ds = model.GetEwalletRequestDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.RequestID = r["PK_RequestID"].ToString();
                    obj.UserId = r["FK_UserId"].ToString();
                    obj.RequestCode = r["RequestCode"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.BankName = r["BankName"].ToString();
                    obj.TransactionDate = r["RequestedDate"].ToString();
                    obj.BankBranch = r["BankBranch"].ToString();
                    obj.ChequeDDNo = r["ChequeDDNo"].ToString();
                    obj.ChequeDDDate = r["ChequeDDDate"].ToString();
                    obj.WalletId = r["WalletId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.DisplayName = r["Name"].ToString();
                    obj.Remark = r["Remark"].ToString();
                    lst.Add(obj);
                }
                model.lstWallet = lst;
            }
            return View(model);
        }
        public ActionResult WalletLedgerList()
        {
            UserWallet model = new UserWallet();
            List<UserWallet> lst = new List<UserWallet>();
            model.FK_UserId = Session["Pk_UserId"].ToString();
            DataSet ds = model.GetEWalletDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserWallet obj = new UserWallet();
                    obj.Pk_EwalletId = r["Pk_EwalletId"].ToString();
                    obj.CrAmount = r["CrAmount"].ToString();
                    obj.DrAmount = r["DrAmount"].ToString();
                    obj.Narration = r["Narration"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    obj.Balance = r["Balance"].ToString();
                    lst.Add(obj);
                }
                model.lstWalletLedger = lst;
                ViewBag.TotalCrAmount = double.Parse(ds.Tables[0].Compute("sum(CrAmount)", "").ToString()).ToString("n2");
                ViewBag.TotalDrAmount = double.Parse(ds.Tables[0].Compute("sum(DrAmount)", "").ToString()).ToString("n2");
                ViewBag.Available = double.Parse(ds.Tables[0].Compute("sum(CrAmount)-sum(DrAmount)", "").ToString()).ToString("n2");
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("WalletLedgerList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult WalletLedgerList(UserWallet model)
        {
            List<UserWallet> lst = new List<UserWallet>();
            model.FK_UserId = Session["Pk_UserId"].ToString();
            model.FK_UserId = model.FK_UserId == "0" ? null : model.FK_UserId;
            model.LoginId = model.LoginId == "0" ? null : model.LoginId;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetEWalletDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserWallet obj = new UserWallet();
                    obj.Pk_EwalletId = r["Pk_EwalletId"].ToString();
                    obj.CrAmount = r["CrAmount"].ToString();
                    obj.DrAmount = r["DrAmount"].ToString();
                    obj.Narration = r["Narration"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    //obj.Balance = r["Balance"].ToString();
                    lst.Add(obj);
                }
                model.lstWalletLedger = lst;
                ViewBag.TotalCrAmount = double.Parse(ds.Tables[0].Compute("sum(CrAmount)", "").ToString()).ToString("n2");
                ViewBag.TotalDrAmount = double.Parse(ds.Tables[0].Compute("sum(DrAmount)", "").ToString()).ToString("n2");
                ViewBag.Available = double.Parse(ds.Tables[0].Compute("sum(CrAmount)-sum(DrAmount)", "").ToString()).ToString("n2");
            }
            return View(model);
        }
        public ActionResult DeleteWallet(string id)
        {
            try
            {
                UserWallet model = new UserWallet();
                model.AddedBy = Session["Pk_UserId"].ToString();
                model.PK_RequestID = id;
                DataSet ds = model.DeleteWallet();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["msg"] = "Wallet requested deleted successfully";
                    }
                    else
                    {
                        TempData["error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else { }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return RedirectToAction("WalletList", "Wallet");
        }

        //public ActionResult WalletTransfer()
        //{
        //    Common objcomm = new Common();
        //    #region Check Balance
        //    objcomm.Fk_UserId = Session["Pk_UserId"].ToString();
        //    DataSet ds = objcomm.GetWalletBalance();
        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        ViewBag.WalletBalance = ds.Tables[0].Rows[0]["Amount"].ToString();
        //    }
        //    #endregion


        //    #region GetDirectPayment
        //    DataSet ds1 = objcomm.GetWalletTransferCharge();
        //    if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
        //    {
        //        ViewBag.MemberTransferCharge = ds1.Tables[0].Rows[0]["MemberTransferCharge"].ToString();
        //    }
        //    #endregion
        //    return View();
        //}
        
        //[HttpPost]
        //[ActionName("WalletTransfer")]
        //[OnAction(ButtonName = "btnTransfer")]
        //public ActionResult WalletTransfer(UserWallet model)
        //{
        //    try
        //    {
        //        model.AddedBy = Session["PK_UserId"].ToString();
        //        DataSet ds = model.SaveWalletTransferBalance();
        //        if (ds != null && ds.Tables.Count > 0)
        //        {
        //            if (ds.Tables[0].Rows[0][0].ToString() == "1")
        //            {
        //                TempData["wallettransfer"] = "Transferred  successfully";
        //            }
        //            else
        //            {
        //                TempData["wallettransfer"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["wallettransfer"] = ex.Message;
        //    }
        //    return RedirectToAction("WalletTransfer","Wallet");
        //}

        //[HttpPost]
        //public ActionResult GetNameDetailsforUser(string LoginId,string CustomerId)
        //{
        //    UserWallet model = new UserWallet();
        //    model.LoginId = LoginId;
        //    DataSet ds = model.GetNameDetailsforUserWalletTransfer();
        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        if (ds.Tables[0].Rows[0][0].ToString() == "0")
        //        {
        //            model.Result = "no";
        //        }
        //        else
        //        {
        //            model.Result = "yes";
        //            model.Fk_UserId = ds.Tables[0].Rows[0]["PK_UserId"].ToString();
        //            model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
        //            model.Amount = ds.Tables[1].Rows[0]["Balance"].ToString();
        //        }
        //    }
        //    else
        //    {
        //        model.Result = "no";
        //    }
        //    return Json(model, JsonRequestBehavior.AllowGet);
        //}

    }
}