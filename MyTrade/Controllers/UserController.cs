using Mytrade.Controllers;
using MyTrade.Filter;
using MyTrade.Models;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
namespace MyTrade.Controllers
{
    public class UserController : UserBaseController
    {
        // GET: User
        public ActionResult UserDashBoard()
        {
            Dashboard obj = new Dashboard();
            List<Dashboard> lstinvestment = new List<Dashboard>();
            obj.FK_UserId = Session["Pk_UserId"].ToString();
            DataSet ds = obj.GetAssociateDashboard();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.TotalDownline = ds.Tables[0].Rows[0]["TotalDownline"].ToString();
                ViewBag.TotalBusiness = ds.Tables[0].Rows[0]["TotalBusiness"].ToString();
                ViewBag.TeamBusiness = ds.Tables[0].Rows[0]["TeamBusiness"].ToString();
                ViewBag.SelfBusiness = ds.Tables[0].Rows[0]["SelfBusiness"].ToString();
                ViewBag.TotalDirect = ds.Tables[0].Rows[0]["TotalDirect"].ToString();
                ViewBag.TotalActive = ds.Tables[0].Rows[0]["TotalActive"].ToString();
                ViewBag.TotalInActive = ds.Tables[0].Rows[0]["TotalInActive"].ToString();
                ViewBag.TPSId = ds.Tables[0].Rows[0]["TPSId"].ToString();
                ViewBag.TotalBlocked = ds.Tables[0].Rows[0]["TotalBlocked"].ToString();
                ViewBag.TotalROI = ds.Tables[0].Rows[0]["TotalROIWalletAmount"].ToString();
                ViewBag.TotalPayoutWallet = ds.Tables[0].Rows[0]["TotalPayoutWalletAmount"].ToString();
                ViewBag.TotalWalletAmount = ds.Tables[0].Rows[0]["TotalWalletAmount"].ToString();
                ViewBag.TotalTeam = ds.Tables[0].Rows[0]["TotalTeam"].ToString();
                ViewBag.TotalTeamActive = ds.Tables[0].Rows[0]["TotalTeamActive"].ToString();
                ViewBag.TotalTeamInActive = ds.Tables[0].Rows[0]["TotalTeamInActive"].ToString();
                ViewBag.TotalTeamTPSId = ds.Tables[0].Rows[0]["TotalTeamTPSId"].ToString();
                ViewBag.TotalIncome = Convert.ToDecimal(ds.Tables[0].Rows[0]["TotalLevelIncomeTTP"]) + Convert.ToDecimal(ds.Tables[0].Rows[0]["TotalLevelIncomeTPS"]) + Convert.ToDecimal(ds.Tables[0].Rows[0]["SponsorBonus"].ToString());
                ViewBag.LevelIncomeTr1 = ds.Tables[0].Rows[0]["TotalLevelIncomeTTP"].ToString();
                ViewBag.LevelIncomeTr2 = ds.Tables[0].Rows[0]["TotalLevelIncomeTPS"].ToString();
                ViewBag.LevelIncomeTR1ForPayout = ds.Tables[0].Rows[0]["LevelIncomeTR1ForPayout"].ToString();
                ViewBag.LevelIncomeTR2ForPayout = ds.Tables[0].Rows[0]["LevelIncomeTR2ForPayout"].ToString();
                ViewBag.TotalPayout = ds.Tables[0].Rows[0]["TotalPayout"].ToString();
                ViewBag.UsedPins = ds.Tables[0].Rows[0]["UsedPins"].ToString();
                ViewBag.AvailablePins = ds.Tables[0].Rows[0]["AvailablePins"].ToString();
                ViewBag.TotalPins = ds.Tables[0].Rows[0]["TotalPins"].ToString();
                ViewBag.Status = ds.Tables[2].Rows[0]["Status"].ToString();
                ViewBag.SponsorBonus = ds.Tables[0].Rows[0]["SponsorBonus"].ToString();
                ViewBag.TotalAmount = Convert.ToDecimal(ds.Tables[0].Rows[0]["TotalPayoutWalletAmount"]) + 0;
                
                ViewBag.TotalCrAmount = ds.Tables[7].Rows[0]["TotalCrAmount"].ToString();
                ViewBag.TotalDrAmount = ds.Tables[7].Rows[0]["TotalDrAmount"].ToString();

                
                //ViewBag.CustomerId = ds.Tables[2].Rows[0]["CustomerId"].ToString();
                //ViewBag.CustomerName = ds.Tables[2].Rows[0]["CustomerName"].ToString();



                if (ViewBag.Status == "InActive")
                {
                    Session["IdActivated"] = false;
                    return RedirectToAction("CompleteRegistration", "Home");
                  

                }
                else
                {
                    Session["IdActivated"] = true;
                }
            }
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                ViewBag.Tr1Business = ds.Tables[1].Rows[0]["Tr1Business"].ToString();
                if (ViewBag.Tr1Business == "")
                {
                    ViewBag.Tr1Business = 0;
                }
                ViewBag.Tr2Business = ds.Tables[1].Rows[0]["Tr2Business"].ToString();
            }
            List<Dashboard> lst = new List<Dashboard>();
            obj.AddedBy = Session["Pk_userId"].ToString();
            DataSet ds1 = obj.GetRewarDetails();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    Dashboard obj1 = new Dashboard();
                    obj1.PK_RewardId = r["PK_RewardId"].ToString();
                    obj1.Title = r["Title"].ToString();
                    obj1.Image = "/UploadReward/" + r["postedFile"].ToString();
                    lst.Add(obj1);
                }
                obj.lstReward = lst;
            }

            List<Dashboard> lst2 = new List<Dashboard>();
            obj.AddedBy = Session["Pk_userId"].ToString();
            DataSet ds2 = obj.GetCustomerList();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    Dashboard obj1 = new Dashboard();
                    obj1.ProfilePic = r["ProfilePic"].ToString();
                    obj1.CustomerId = r["CustomerId"].ToString();
                    obj1.CustomerName = r["CustomerName"].ToString();
                    lst2.Add(obj1);
                }
                obj.lstCustomer = lst2;
            }

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[3].Rows.Count > 0)
            {
                ViewBag.TotalTPSAmountTobeReceived = double.Parse(ds.Tables[3].Compute("sum(TopUpAmount)", "").ToString()).ToString("n2");
            }
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[4].Rows.Count > 0)
            {
                ViewBag.TotalTPSAmountReceived = double.Parse(ds.Tables[4].Compute("sum(TotalROI)", "").ToString()).ToString("n2");
                ViewBag.TotalTPSBalanceAmount = Convert.ToDecimal(ViewBag.TotalTPSAmountTobeReceived) - Convert.ToDecimal(ViewBag.TotalTPSAmountReceived);
            }
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[6].Rows.Count > 0)
            {
                Session["TopUp"]= ds.Tables[6].Rows[0]["IsActive"].ToString();
            }
            return View(obj);
        }

        public JsonResult GetchartBarRunning()
        {
            ProgressReport model = new ProgressReport();
            try
            {
                List<ProgressReport> lst = new List<ProgressReport>();
                model.FK_UserId = Session["Pk_UserId"].ToString();
                DataSet ds = model.GetAssociateDashboard();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        ProgressReport obj = new ProgressReport();
                        obj.Year = r["Year"].ToString();
                        obj.Cramount = r["Cramount"].ToString();
                        obj.Dramount = r["Dramount"].ToString();
                        lst.Add(obj);
                    }
                    model.lstCoin = lst;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Json(model.lstCoin, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetlineChart()
        {
            ProgressReport model = new ProgressReport();
            try
            {
                List<ProgressReport> lst = new List<ProgressReport>();
                model.FK_UserId = Session["Pk_UserId"].ToString();
                DataSet ds = model.GetlineChart();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        ProgressReport obj = new ProgressReport();
                        obj.Year = r["Year"].ToString();
                        obj.TotalBusiness = (r["TotalBusiness"].ToString());
                        lst.Add(obj);
                    }
                    model.lstCoin = lst;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Json(model.lstCoin, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult ActivateByPin(User model)
        {
            return View(model);
        }
        [HttpPost]
        [ActionName("ActivateByPin")]
        [OnAction(ButtonName = "btn_Pin")]
        public ActionResult ActivateUser(User obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.Fk_UserId = Session["Pk_UserId"].ToString();
                
                DataSet ds = obj.ActivateUser();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        string Email = ds.Tables[0].Rows[0]["Email"].ToString();
                        string Name = ds.Tables[0].Rows[0]["Name"].ToString()+","+ ds.Tables[0].Rows[0]["LoginId"].ToString();
                        string Product = ds.Tables[0].Rows[0]["Package"].ToString();
                        string Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        string TempId = "1707166036877932940";
                        string str = BLSMS.IdActivated(Name, Product);
                        try
                        {
                            BLSMS.SendSMS(Mobile, str, TempId);
                        }
                        catch
                        {
                        }
                        //if (Email != null && Email != "")
                        //{
                        //    try
                        //    {
                        //        BLMail.SendActivationMail(Session["FullName"].ToString(), Session["LoginId"].ToString(), Crypto.Decrypt(Session["Password"].ToString()), "Activation Successful", Email);
                        //    }
                        //    catch (Exception ex)
                        //    {

                        //    }
                        //}
                        TempData["Activated"] = "User Activated Successfully";
                        FormName = "ConfirmActivation";
                        Controller = "User";
                    }
                    else
                    {
                        TempData["error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ActivateByPin";
                        Controller = "User";
                    }

                }
                else
                {
                    FormName = "ActivateByPin";
                    Controller = "User";
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                FormName = "ActivateByPin";
                Controller = "User";
            }
            return RedirectToAction(FormName, Controller);
        }
        public ActionResult ValidatePin(string EPin)
        {
            User obj = new User();
            obj.EPin = EPin;
            obj.Fk_UserId = Session["Pk_UserId"].ToString();
            DataSet ds = obj.ValidateEpin();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                obj.PinStatus = ds.Tables[0].Rows[0]["PinStatus"].ToString();
                obj.Result = "Yes";
            }
            else { obj.Result = "Invalid Epin or Already Used"; }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TopUp()
        {
            Account model = new Account();
           //model.LoginId = Session["CustomerId"].ToString();
            model.LoginId = Session["LoginId"].ToString();
          
            if (Session["IdActivated"].ToString()=="true")
            {
                model.BankName = Session["Bank"].ToString();
                model.BankBranch = Session["Branch"].ToString();
              
            }

            #region PackageType Bind

            Common objcommpkg = new Common();
            List<SelectListItem> ddlPackageType = new List<SelectListItem>();
            DataSet dspkg = objcommpkg.BindPackageType();
            if (dspkg != null && dspkg.Tables.Count > 0 && dspkg.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in dspkg.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlPackageType.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlPackageType.Add(new SelectListItem { Text = r["PackageTypeName"].ToString(), Value = r["Pk_PackageTypeId"].ToString() });
                    count++;
                }
            }
            ViewBag.ddlPackageType = ddlPackageType;
            #endregion

            #region Product Bind
            Common objcomm = new Common();
            //List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindProductForTopUp();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
               // int count = 0;
                ViewBag.FromAmount = ds1.Tables[0].Rows[0]["FromAmount"].ToString();
                ViewBag.ToAmount = ds1.Tables[0].Rows[0]["ToAmount"].ToString();
                if (Session["UserActivationTopUp"].ToString() == "0")
                {
                    ViewBag.ActivationMTGToken = ds1.Tables[0].Rows[0]["ActivationMTGToken"].ToString();
                }
                else
                {
                    ViewBag.ActivationMTGToken = "0";
                }
               // ViewBag.ActivationMTGToken = ds1.Tables[0].Rows[0]["ActivationMTGToken"].ToString();
                ViewBag.InMultipleOf = ds1.Tables[0].Rows[0]["InMultipleOf"].ToString();
                ViewBag.ROIPercent = ds1.Tables[0].Rows[0]["ROIPercent"].ToString();
                ViewBag.Status = ds1.Tables[1].Rows[0]["Status"].ToString();
                ViewBag.Reason = ds1.Tables[1].Rows[0]["Reason"].ToString();
                //foreach (DataRow r in ds1.Tables[0].Rows)
                //{
                //    if (count == 0)
                //    {
                //        ddlProduct.Add(new SelectListItem { Text = "Select", Value = "0" });
                //    }
                //    ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });
                //    count++;
                //}
            }
           // ViewBag.ddlProduct = ddlProduct;
            #endregion

            #region Check Balance
            objcomm.Fk_UserId = Session["Pk_UserId"].ToString();
            DataSet ds = objcomm.GetWalletBalance();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.WalletBalance = ds.Tables[0].Rows[0]["amount"].ToString();
            }
            #endregion

            #region ddlpaymentType
            List<SelectListItem> ddlpaymentType = Common.BindPaymentType();
            ViewBag.ddlpaymentType = ddlpaymentType;
            #endregion

            #region ddlpaymentmode
            UserWallet obj = new UserWallet();
            int count1 = 0;
            List<SelectListItem> ddlpaymentmode = new List<SelectListItem>();
            DataSet ds2 = obj.GetPaymentMode();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlpaymentmode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "" });
                    }
                    ddlpaymentmode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlpaymentmode = ddlpaymentmode;

            #endregion


            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            ddlProduct.Add(new SelectListItem { Text = "Select Package type", Value = "0" });
            ViewBag.ddlProduct = ddlProduct;

            return View(model);
        }

        public ActionResult GetProductList(string PackageTypeId)
        {
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            Account model = new Account();
            model.PackageTypeId = PackageTypeId;
           
            DataSet ds = model.GetProductListForTopUp();
            
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    ddlProduct.Add(new SelectListItem { Text = dr["ProductName"].ToString(), Value = dr["Pk_ProductId"].ToString() });
                }
            }
            model.ddlProduct = ddlProduct;

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult GetActivationMTG(string PackageTypeId)
        //{
        //    List<SelectListItem> ActivationMTGToken = new List<SelectListItem>();
        //    Account obj = new Account();
        //    obj.PackageTypeId = PackageTypeId;
        //    DataSet ds = obj.GetActivationMTGForTopUp();
        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //            obj.ActivationMTGToken = ds.Tables[0].Rows[0]["ActivationMTGToken"].ToString();
        //    }
        //    ViewBag.ActivationMTGToken = ActivationMTGToken;
        //    return Json(obj, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public ActionResult TopUp(Account obj)
        {
            try
            {
                obj.LoginId = Session["LoginId"].ToString();
                obj.AddedBy = Session["Pk_userId"].ToString();
                //  obj.TopUpDate = string.IsNullOrEmpty(obj.TopUpDate) ? null : Common.ConvertToSystemDate(obj.TopUpDate, "dd/mm/yyyy");
                //obj.TransactionDate = string.IsNullOrEmpty(obj.TransactionDate) ? null : Common.ConvertToSystemDate(obj.TransactionDate, "dd/mm/yyyy");
                obj.FK_UserId = Session["Pk_userId"].ToString();
                DataSet ds = obj.TopUp();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.Name = ds.Tables[0].Rows[0]["Name"].ToString()+","+obj.LoginId+"";
                        obj.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                        string Mobile= ds.Tables[0].Rows[0]["Mobile"].ToString();
                        string Amount = ds.Tables[0].Rows[0]["Amount"].ToString();
                        string TempId = "1707166036857908702";
                        string str = BLSMS.Topup(obj.Name, Amount);
                        try
                        {
                            BLSMS.SendSMS(Mobile, str, TempId);
                        }
                        catch
                        {
                        }
                        //if (obj.Email != null)
                        //{
                        //    string mailbody = "";
                        //    try
                        //    {
                        //        mailbody = "Dear  " + obj.Name + ", <br/> Your Top-Up Done successfully..";
                        //        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                        //        {
                        //            Host = "smtp.gmail.com",
                        //            Port = 587,
                        //            EnableSsl = true,
                        //            DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                        //            UseDefaultCredentials = true,
                        //            Credentials = new NetworkCredential("coustomer.mytrade@gmail.com", "Mytrade@2022")
                        //        };
                        //        using (var message = new MailMessage("coustomer.mytrade@gmail.com", obj.Email)
                        //        {
                        //            IsBodyHtml = true,
                        //            Subject = "TopUp",
                        //            Body = mailbody
                        //        })
                        //            smtp.Send(message);
                        //    }
                        //    catch (Exception ex)
                        //    {

                        //    }
                        //}
                        TempData["msg"] = "Top-Up Done successfully";
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
            return RedirectToAction("Topup", "User");
        }

        public ActionResult FillAmount(string ProductId)
        {
            Admin obj = new Admin();
            obj.Package = ProductId;
            DataSet ds = obj.BindPriceByProduct();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                obj.Amount = ds.Tables[0].Rows[0]["ProductPrice"].ToString();
                obj.FinalAmount = ds.Tables[0].Rows[0]["FinalAmount"].ToString();
            }
            else { }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BinaryTree()
        {
            ViewBag.Fk_UserId = Session["Pk_UserId"].ToString();
            return View();
        }
        public ActionResult GetUserList()
        {
            Profile obj = new Profile();
            List<Profile> lst = new List<Profile>();
            obj.LoginId = Session["LoginId"].ToString();
            DataSet ds = obj.GettingUserProfile();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Profile objList = new Profile();
                    objList.UserName = dr["Fullname"].ToString();
                    objList.LoginIDD = dr["LoginId"].ToString();
                    lst.Add(objList);
                }
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PinList()
        {
            Pin model = new Pin();
            List<Pin> lst = new List<Pin>();
            model.PinStatus = (model.PinStatus = "T");
            model.FK_UserId = Session["Pk_userId"].ToString();
            DataSet ds = model.GetPinList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Pin obj = new Pin();
                    obj.ePinNo = r["ePinNo"].ToString();
                    obj.PinAmount = r["PinAmount"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.PinStatus = r["PinStatus"].ToString();
                    obj.RegisteredTo = r["RegisteredTo"].ToString();
                    obj.ActivationDate = r["ActivationDate"].ToString();
                    obj.Amount = r["TotalAmount"].ToString();
                    obj.BV = r["BV"].ToString();
                    //obj.IsRegistered = r["IsRegistered"].ToString();
                    obj.PinGenerationDate = r["PinGenerationDate"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.GST = r["IGST"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }
        [HttpPost]
        [OnAction(ButtonName = "btnSearch")]
        [ActionName("PinList")]
        public ActionResult PinList(Pin model)
        {
            List<Pin> lst = new List<Pin>();
            model.FK_UserId = Session["Pk_userId"].ToString();
            DataSet ds = model.GetPinList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Pin obj = new Pin();
                    obj.ePinNo = r["ePinNo"].ToString();
                    obj.PinAmount = r["PinAmount"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.PinStatus = r["PinStatus"].ToString();
                    obj.RegisteredTo = r["RegisteredTo"].ToString();
                    obj.Amount = r["TotalAmount"].ToString();
                    obj.ActivationDate = r["ActivationDate"].ToString();
                    obj.BV = r["BV"].ToString();
                    //obj.IsRegistered = r["IsRegistered"].ToString();
                    obj.PinGenerationDate = r["PinGenerationDate"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.GST = r["IGST"].ToString();

                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }
        [HttpPost]
        [OnAction(ButtonName = "btnMutipleTranser")]
        [ActionName("PinList")]
        public ActionResult PinListTransfer(Pin model)
        {
            model.ParentLoginId = Session["LoginId"].ToString();

            if (model.LoginId != model.ParentLoginId)
            {
                try
                {
                    string hdrows = Request["hdRows"].Count().ToString();
                    string chkselect = "";
                    for (int i = 1; i < int.Parse(hdrows); i++)
                    {
                        try
                        {
                            chkselect = Request["chkSelect_ " + i];
                            if (chkselect == "on")
                            {
                                model.ePinNo = Request["ePinNo_ " + i].ToString();
                                DataSet ds = model.ePinTransfer();
                                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                {
                                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                                    {
                                        TempData["msg"] = "Transferred Successfully";
                                    }
                                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                                    {
                                        TempData["error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                    }
                                }
                            }
                        }
                        catch { chkselect = "0"; }
                    }
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                }
            }
            else
            {
                TempData["error"] = "You Can't transfer on the same Id";
            }
            return RedirectToAction("PinList");
        }
        public ActionResult Tree(string LoginId, string Id)
        {
            Tree model = new Tree();
            if (LoginId != "" && LoginId != null)
            {
                model.RootAgentCode = Session["LoginId"].ToString();
                model.LoginId = LoginId;
                model.PK_UserId = Id;
            }
            else
            {
                model.RootAgentCode = Session["LoginId"].ToString();
                model.PK_UserId = Session["Pk_UserId"].ToString();
                model.LoginId = Session["LoginId"].ToString();
                model.DisplayName = Session["FullName"].ToString();
            }
            List<TreeMembers> lst = new List<TreeMembers>();
            List<MemberDetails> lstMember = new List<MemberDetails>();
            DataSet ds = model.GetLevelMembersCount();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    TreeMembers obj = new TreeMembers();
                    obj.LevelName = r["LevelNo"].ToString();
                    obj.NumberOfMembers = r["TotalAssociate"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                ViewBag.Level = ds.Tables[1].Rows[0]["Lvl"].ToString();
                ViewBag.Status = ds.Tables[1].Rows[0]["Status"].ToString();
                model.Color = ds.Tables[1].Rows[0]["Color"].ToString();
                model.DisplayName = ds.Tables[1].Rows[0]["Name"].ToString();
                model.PK_UserId = ds.Tables[1].Rows[0]["PK_UserId"].ToString();
                model.ProfilePic = ds.Tables[1].Rows[0]["ProfilePic"].ToString();
                model.TotalDirect = ds.Tables[1].Rows[0]["TotalDirect"].ToString();
                model.TotalActive = ds.Tables[1].Rows[0]["TotalActive"].ToString();
                model.TotalInactive = ds.Tables[1].Rows[0]["TotalInActive"].ToString();
                model.TotalTeam = ds.Tables[1].Rows[0]["TotalTeam"].ToString();
                model.TotalActiveTeam = ds.Tables[1].Rows[0]["TotalActiveTeam"].ToString();
                model.TotalInActiveTeam = ds.Tables[1].Rows[0]["TotalInActiveTeam"].ToString();
                model.SponsorName = ds.Tables[1].Rows[0]["SponsorName"].ToString();
                model.SelfBV = ds.Tables[1].Rows[0]["SelfBV"].ToString();
                model.TeamBV = ds.Tables[1].Rows[0]["TeamBV"].ToString();
                model.SelfBVDollar = Math.Round((Convert.ToDouble(ds.Tables[1].Rows[0]["SelfBV"]) / 76.805), 2).ToString();
                model.TeamBVDollar = Math.Round((Convert.ToDouble(ds.Tables[1].Rows[0]["TeamBV"]) / 76.805), 2).ToString();
                model.SponsorName = ds.Tables[1].Rows[0]["SponsorName"].ToString();
            }
            model.Level = "1";
            DataSet ds1 = model.GetLevelMembers();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    MemberDetails obj = new MemberDetails();
                    obj.PK_UserId = r["PK_UserId"].ToString();
                    obj.MemberName = r["MemberName"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Level = r["Lvl"].ToString();
                    obj.ProfilePic = r["ProfilePic"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.SelfBV = r["SelfBV"].ToString();
                    obj.TeamBV = r["TeamBV"].ToString();
                    //obj.SelfBVDollar = (Convert.ToDouble(r["SelfBV"]) / 76.805).ToString();
                    //obj.TeamBVDollar = (Convert.ToDouble(r["TeamBV"]) / 76.805).ToString();
                    obj.SponsorName = r["SponsorName"].ToString();
                    obj.Color = r["Color"].ToString();
                    lstMember.Add(obj);
                }
                model.lstMember = lstMember;
            }
            return View(model);
        }
        public ActionResult GetMemberName(string LoginId, string ePinNo)
        {
            Home model = new Home();
            try
            {
                model.ePinNo = ePinNo;
                model.LoginId = LoginId;
                DataSet ds = model.GetMemberName();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                    model.DisplayName = ds.Tables[0].Rows[0]["Name"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetInActiveUser(string LoginId)
        {
            Home model = new Home();
            try
            {
                model.LoginId = LoginId;
                DataSet ds = model.GetInActiveUser();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                    model.Fk_UserId = ds.Tables[0].Rows[0]["PK_UserId"].ToString();
                    model.DisplayName = ds.Tables[0].Rows[0]["Name"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PinTransferReport(Pin model)
        {
            List<Pin> list = new List<Pin>();
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds = model.GetTransferPinReport();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Pin obj = new Pin();
                    obj.ePinNo = r["EpinNo"].ToString();
                    obj.FromId = r["FromId"].ToString();
                    obj.FromName = r["FromName"].ToString();
                    obj.ToId = r["ToId"].ToString();
                    obj.ToName = r["ToName"].ToString();
                    obj.TransferDate = r["TransferDate"].ToString();
                    obj.PinAmount = r["PinAmount"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.BV = r["BV"].ToString();
                    list.Add(obj);
                }
                model.lst = list;
            }
            return View(model);
        }
        [HttpPost]
        [OnAction(ButtonName = "GetDetails")]
        [ActionName("PinTransferReport")]
        public ActionResult PinTransferReportDetails(Pin model)
        {
            List<Pin> list = new List<Pin>();
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds = model.GetTransferPinReport();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Pin obj = new Pin();
                    obj.ePinNo = r["EpinNo"].ToString();
                    obj.FromId = r["FromId"].ToString();
                    obj.FromName = r["FromName"].ToString();
                    obj.ToId = r["ToId"].ToString();
                    obj.ToName = r["ToName"].ToString();
                    obj.TransferDate = r["TransferDate"].ToString();
                    obj.PinAmount = r["PinAmount"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    list.Add(obj);
                }
                model.lst = list;
            }
            return View(model);
        }
        public ActionResult ConfirmActivation()
        {
            return View();
        }
        public ActionResult UserProfile()
        {
            Home model = new Home();
            List<SelectListItem> Gender = Common.BindGender();
            ViewBag.Gender = Gender;
            model.Fk_UserId = Session["Pk_userId"].ToString();
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds = model.UserProfile();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "1")
                {
                    model.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    model.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                    model.SponsorId = ds.Tables[0].Rows[0]["SponsorId"].ToString();
                    model.SponsorName = ds.Tables[0].Rows[0]["SponsorName"].ToString();
                    model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                    model.MobileNo = ds.Tables[0].Rows[0]["Mobile"].ToString();
                    model.PinCode = ds.Tables[0].Rows[0]["PinCode"].ToString();
                    model.Gender = ds.Tables[0].Rows[0]["Sex"].ToString();
                    model.State = ds.Tables[0].Rows[0]["State"].ToString();
                    model.City = ds.Tables[0].Rows[0]["City"].ToString();
                    model.AdharNo = ds.Tables[0].Rows[0]["AdharNumber"].ToString();
                    model.PanNo = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                    model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    model.ProfilePic = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
                }
            }
            return View(model);
        }
        public ActionResult ChangePasswordForUser()
        {
            return View();
        }
        [HttpPost]
        [ActionName("ChangePasswordForUser")]
        public ActionResult ChangePasswordForUser(User model)
        {
            try
            {
                model.UpdatedBy = Session["Pk_userId"].ToString();
                model.Password = Crypto.Encrypt(model.Password);
                model.NewPassword = Crypto.Encrypt(model.NewPassword);
                DataSet ds = model.ChangePassword();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["msg"] = "Password Changed  Successfully";
                    }
                    else
                    {
                        TempData["error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return RedirectToAction("ChangePasswordForUser", "User");
        }
        public ActionResult ActivatePin(string ePinNo, string Fk_UserId)
        {
            Pin model = new Pin();
            try
            {
                model.ePinNo = ePinNo;
                model.FK_UserId = Fk_UserId;
                DataSet ds = model.ActivatePin();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        model.Response = "1";
                        string Name = ds.Tables[0].Rows[0]["Name"].ToString() + "," + ds.Tables[0].Rows[0]["LoginId"].ToString();
                        string Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        string Package = ds.Tables[0].Rows[0]["Package"].ToString();
                        string TempId = "1707166036877932940";
                        string Message = "Dear "+Name+", Your Profile is activated successfully with package "+ Package + ". Kindly check your account for more details. MY TRADE";
                        try
                        {
                            BLSMS.SendSMS(Mobile, Message, TempId);
                        }
                        catch { }
                    }
                    else
                    {
                        model.Response = "0";
                        model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                model.Response = "0";
                model.Message = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BankDetailsUpdate()
        {
            User model = new User();
            model.Fk_UserId = Session["Pk_userId"].ToString();
            DataSet ds = model.UserProfile();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "1")
                {
                    model.IsVerified = ds.Tables[0].Rows[0]["IsVerified"].ToString();
                    model.AdharNo = ds.Tables[0].Rows[0]["AdharNumber"].ToString();
                    model.PanNumber = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                    model.BankName = ds.Tables[0].Rows[0]["MemberBankName"].ToString();
                    model.AccountNo = ds.Tables[0].Rows[0]["MemberAccNo"].ToString();
                    model.BranchName = ds.Tables[0].Rows[0]["MemberBranch"].ToString();
                    model.IFSCCode = ds.Tables[0].Rows[0]["IFSCCode"].ToString();
                    model.NomineeName = ds.Tables[0].Rows[0]["NomineeName"].ToString();
                    model.NomineeRelation = ds.Tables[0].Rows[0]["NomineeRelation"].ToString();
                    model.NomineeAge = ds.Tables[0].Rows[0]["NomineeAge"].ToString();
                    model.Image = ds.Tables[0].Rows[0]["PanImage"].ToString();
                    model.UPIID = ds.Tables[0].Rows[0]["UPIID"].ToString();
                }
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("BankDetailsUpdate")]
        public ActionResult BankDetailsUpdate(User model, HttpPostedFileBase Image)
        {
            try
            {
                Random rnd = new Random();
                if (Image != null)
                {
                    if (model.PanNumber != null && model.PanNumber != "")
                    {
                        model.Image = "/PanUpload/" + model.PanNumber + rnd.Next(1, 10) + Path.GetExtension(Image.FileName);
                    }
                    else
                    {
                        model.Image = "/PanUpload/" + Guid.NewGuid() + Path.GetExtension(Image.FileName);
                    }
                    Image.SaveAs(Path.Combine(Server.MapPath(model.Image)));
                }
                model.Fk_UserId = Session["Pk_userId"].ToString();
                DataSet ds = model.BankDetailsUpdate();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["msg"] = "KYC Details Updated Successfully";
                    }
                    else
                    {
                        TempData["error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return RedirectToAction("BankDetailsUpdate", "User");
        }
        public ActionResult ViewProfile()
        {
            Home model = new Home();
            List<SelectListItem> Gender = Common.BindGender();
            ViewBag.Gender = Gender;
            model.Fk_UserId = Session["Pk_userId"].ToString();
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds = model.UserProfile();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "1")
                {
                    model.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    model.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                    model.SponsorId = ds.Tables[0].Rows[0]["SponsorId"].ToString();
                    model.SponsorName = ds.Tables[0].Rows[0]["SponsorName"].ToString();
                    model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                    model.MobileNo = ds.Tables[0].Rows[0]["Mobile"].ToString();
                    model.PinCode = ds.Tables[0].Rows[0]["PinCode"].ToString();
                    model.Gender = ds.Tables[0].Rows[0]["Sex"].ToString();
                    model.State = ds.Tables[0].Rows[0]["State"].ToString();
                    model.Country = ds.Tables[0].Rows[0]["Country"].ToString();
                    model.City = ds.Tables[0].Rows[0]["City"].ToString();
                    model.AdharNo = ds.Tables[0].Rows[0]["AdharNumber"].ToString();
                    model.PanNo = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                    model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    model.ProfilePic = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
                }
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult ViewProfile(Home model)
        {
            try
            {
                List<SelectListItem> Gender = Common.BindGender();
                ViewBag.Gender = Gender;
                if (model.postedFile != null)
                {
                    model.ProfilePic = "/ProfilePicture/" + Guid.NewGuid() + Path.GetExtension(model.postedFile.FileName);
                    model.postedFile.SaveAs(Path.Combine(Server.MapPath(model.ProfilePic)));
                }
                model.Fk_UserId = Session["Pk_userId"].ToString();
                DataSet ds = model.UpdateProfile();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["msg"] = "Profile Updated Successfully";
                    }
                    else
                    {
                        TempData["error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return RedirectToAction("ViewProfile");
        }
        public ActionResult GetMemberDetails(string LoginId)
        {
            Common obj = new Common();
            obj.ReferBy = LoginId;
            DataSet ds = obj.GetMemberDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                obj.DisplayName = ds.Tables[0].Rows[0]["FullName"].ToString();
                obj.Result = "Yes";
            }
            else { obj.Result = "Invalid LoginId"; }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EPinRequest()
        {
            User model = new User();
            List<User> list = new List<User>();
            model.LoginId = Session["LoginId"].ToString();
            if (Session["IdActivated"].ToString() == "true")
            {
                model.BankName = Session["Bank"].ToString();
                model.BranchName = Session["Branch"].ToString();
           
            }
            DataSet dss = model.GetEPinRequestDetails();
            if (dss != null && dss.Tables.Count > 0 && dss.Tables[1].Rows.Count > 0)
            {
                ViewBag.Status = dss.Tables[1].Rows[0]["Status"].ToString();
                ViewBag.Reason = dss.Tables[1].Rows[0]["Reason"].ToString();
            }
            if (dss != null && dss.Tables.Count > 0 && dss.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dss.Tables[0].Rows)
                {
                    User obj = new User();
                    obj.NoofPins = r["NoOfPins"].ToString();
                    obj.PK_RequestID = r["PK_RequestID"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.Amount = Convert.ToDecimal(r["Amount"].ToString());
                    obj.Fk_Paymentid = r["PaymentMode"].ToString();
                    obj.BankName = r["BankName"].ToString();
                    obj.BankBranch = r["BankBranch"].ToString();
                    obj.TransactionNo = r["ChequeDDNo"].ToString();
                    obj.TransactionDate = r["ChequeDDDate"].ToString();
                    list.Add(obj);
                }
                model.lstEpinRequest = list;
            }


            #region Check Balance
            model.Fk_UserId = Session["Pk_UserId"].ToString();
            DataSet dsss = model.GetWalletBalance();
            if (dsss != null && dsss.Tables.Count > 0 && dsss.Tables[0].Rows.Count > 0)
            {
                ViewBag.WalletBalance = dsss.Tables[0].Rows[0]["amount"].ToString();
            }
            #endregion
            
            #region Product Bind
            Common objcomm = new Common();
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindProductForJoiningForUser();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlProduct.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlProduct = ddlProduct;

            #endregion

            #region PaymentMode
            Common com = new Common();
            List<SelectListItem> ddlPayment = new List<SelectListItem>();
            DataSet ds = com.PaymentList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int paycount = 0;
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (paycount == 0)
                    {
                        ddlPayment.Add(new SelectListItem { Text = "Select Payment", Value = "0" });
                    }
                    ddlPayment.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    paycount++;
                }
            }

            ViewBag.ddlPayment = ddlPayment;

            #endregion

            return View(model);
        }
        [HttpPost]
        [ActionName("EPinRequest")]
        [OnAction(ButtonName = "btnsave")]
        public ActionResult EPinRequest(User model)
        {
            try
            {
                model.AddedBy = Session["Pk_userId"].ToString();
                model.TransactionDate = string.IsNullOrEmpty(model.TransactionDate) ? null : Common.ConvertToSystemDate(model.TransactionDate, "dd/mm/yyyy");
                if (model.Fk_Paymentid == "12")
                {
                    model.BankName = null;
                    model.BranchName = null;
                }
                DataSet ds = model.SaveEpinRequest();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["msg"] = "E_pin generated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return RedirectToAction("EPinRequest", "User");
        }
        public ActionResult DeleteEPinRequest(string Id)
        {
            try
            {
                User model = new User();
                model.PK_RequestID = Id;
                DataSet ds = model.DeleteEPinRequest();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["msg"] = "E_pin request deleted successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return RedirectToAction("EPinRequest", "User");
        }
        public JsonResult GetTreeMembers(string Level, string PK_UserId)
        {
            Tree model = new Tree();
            model.PK_UserId = PK_UserId;
            model.Level = Level;
            List<MemberDetails> lst = new List<MemberDetails>();
            DataSet ds = model.GetLevelMembers();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    MemberDetails obj = new MemberDetails();
                    obj.PK_UserId = r["PK_UserId"].ToString();
                    obj.MemberName = r["MemberName"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Level = r["Lvl"].ToString();
                    obj.ProfilePic = r["ProfilePic"].ToString();
                    obj.SelfBV = r["SelfBV"].ToString();
                    obj.TeamBV = r["TeamBV"].ToString();
                    obj.SelfBVDollar = (Convert.ToDouble(r["SelfBV"]) / 76.805).ToString();
                    obj.TeamBVDollar = (Convert.ToDouble(r["TeamBV"]) / 76.805).ToString();
                    obj.SponsorName = r["SponsorName"].ToString();
                    obj.Color = r["Color"].ToString();
                    lst.Add(obj);
                }
                model.lstMember = lst;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TreeTTP(string LoginId, string Id)
        {
            Tree model = new Tree();
            if (LoginId != "" && LoginId != null)
            {
                model.RootAgentCode = Session["LoginId"].ToString();
                model.LoginId = LoginId;
                model.PK_UserId = Id;
            }
            else
            {
                model.RootAgentCode = Session["LoginId"].ToString();
                model.PK_UserId = Session["Pk_UserId"].ToString();
                model.LoginId = Session["LoginId"].ToString();
                model.DisplayName = Session["FullName"].ToString();
            }
            List<TreeMembers> lst = new List<TreeMembers>();
            List<MemberDetails> lstMember = new List<MemberDetails>();
            DataSet ds = model.GetLevelMembersCountTR1();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {

                }
                else
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        TreeMembers obj = new TreeMembers();
                        obj.LevelName = r["LevelNo"].ToString();
                        obj.NumberOfMembers = r["TotalAssociate"].ToString();
                        lst.Add(obj);
                    }
                    model.lst = lst;
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                    {
                        ViewBag.Level = ds.Tables[1].Rows[0]["Lvl"].ToString();
                        ViewBag.Status = ds.Tables[1].Rows[0]["Status"].ToString();
                        model.Color = ds.Tables[1].Rows[0]["Color"].ToString();
                        model.DisplayName = ds.Tables[1].Rows[0]["Name"].ToString();
                        model.PK_UserId = ds.Tables[1].Rows[0]["PK_UserId"].ToString();
                        model.ProfilePic = ds.Tables[1].Rows[0]["ProfilePic"].ToString();
                        model.TotalDirect = ds.Tables[1].Rows[0]["TotalDirect"].ToString();
                        model.TotalActive = ds.Tables[1].Rows[0]["TotalActive"].ToString();
                        model.TotalInactive = ds.Tables[1].Rows[0]["TotalInActive"].ToString();
                        model.TotalTeam = ds.Tables[1].Rows[0]["TotalTeam"].ToString();
                        model.TotalActiveTeam = ds.Tables[1].Rows[0]["TotalActiveTeam"].ToString();
                        model.TotalInActiveTeam = ds.Tables[1].Rows[0]["TotalInActiveTeam"].ToString();
                        model.SponsorName = ds.Tables[1].Rows[0]["SponsorName"].ToString();
                    }
                    model.Level = "1";
                    DataSet ds1 = model.GetLevelMembers();
                    if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow r in ds1.Tables[0].Rows)
                        {
                            MemberDetails obj = new MemberDetails();
                            obj.PK_UserId = r["PK_UserId"].ToString();
                            obj.MemberName = r["MemberName"].ToString();
                            obj.LoginId = r["LoginId"].ToString();
                            obj.Level = r["Lvl"].ToString();
                            obj.ProfilePic = r["ProfilePic"].ToString();
                            obj.Status = r["Status"].ToString();
                            obj.SelfBV = r["SelfBV"].ToString();
                            obj.TeamBV = r["TeamBV"].ToString();
                            obj.SponsorName = r["SponsorName"].ToString();
                            obj.Color = r["Color"].ToString();
                            lstMember.Add(obj);
                        }
                        model.lstMember = lstMember;
                    }
                }
            }
            return View(model);
        }
        public ActionResult TopUpList()
        {
            Account model = new Account();
            List<Account> lst = new List<Account>();
            model.Pk_userId = Session["PK_UserId"].ToString();
            model.LoginId = Session["LoginId"].ToString();
           
            DataSet ds1 = model.GetTopUpDetails();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    Account obj = new Account();
                    obj.InvestmentId = r["Pk_InvestmentId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.PinAmount = r["PinAmount"].ToString();
                    obj.UsedFor = r["UsedFor"].ToString();
                    //obj.BV = r["BV"].ToString();
                    //obj.Topupid = r["Topupid"].ToString();
                    obj.ActivationMTGToken = r["ActivationMTGToken"].ToString();
                    obj.IsCalculated = r["IsCalculated"].ToString();
                    obj.TransactionBy = r["TransactionBy"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.ROIPercentage = r["ROIPercentage"].ToString();
                    obj.TopUpDate = r["TopUpDate"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.PackageDays = r["PackageDays"].ToString();
                    lst.Add(obj);
                }
                model.lstTopUp = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("TopUpList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult TopUpList(Account model)
        {
            List<Account> lst = new List<Account>();
            model.Pk_userId = Session["PK_UserId"].ToString();
            //model.LoginId = Session["LoginId"].ToString();
            //model.LoginId = Session["CustomerId"].ToString();
            model.CustomerId = Session["LoginId"].ToString();
            model.FK_UserId = model.FK_UserId == "0" ? null : model.FK_UserId;
            model.LoginId = model.LoginId == "0" ? null : model.LoginId;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds1 = model.GetTopUpDetails();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    Account obj = new Account();
                    obj.InvestmentId = r["Pk_InvestmentId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.PinAmount = r["PinAmount"].ToString();
                    obj.UsedFor = r["UsedFor"].ToString();
                    //obj.BV = r["BV"].ToString();
                    obj.ActivationMTGToken = r["ActivationMTGToken"].ToString();
                    //obj.Topupid = r["Topupid"].ToString();
                    obj.IsCalculated = r["IsCalculated"].ToString();
                    obj.TransactionBy = r["TransactionBy"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.ROIPercentage = r["ROIPercentage"].ToString();
                    obj.TopUpDate = r["TopUpDate"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.PackageDays = r["PackageDays"].ToString();
                    lst.Add(obj);
                }
                model.lstTopUp = lst;
            }
            return View(model);
        }
        public ActionResult BusinessReportsForUser()
        {
            User model = new User();
            model.LoginId = model.LoginId == "" ? null : model.LoginId;
            if (model.IsDownline == "on")
            {
                model.IsDownline = "1";
            }
            else
            {
                model.IsDownline = "0";
            }
            List<User> lst = new List<User>();
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds = model.GetBusinessReports();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    User obj = new User();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["FirstName"].ToString();
                    obj.Amount = Convert.ToDecimal(r["Amount"].ToString());
                    obj.BV = r["BV"].ToString();
                    obj.Date = r["Date"].ToString();
                    obj.Level = r["Lvl"].ToString();
                    obj.PackageType = r["PackageType"].ToString();
                    lst.Add(obj);
                }
                model.lstBReports = lst;
                ViewBag.Amount = double.Parse(ds.Tables[0].Compute("sum(Amount)", "").ToString()).ToString("n2");
                ViewBag.BV = double.Parse(ds.Tables[0].Compute("sum(BV)", "").ToString()).ToString("n2");
            }

            #region ddlPlotSize
            int count = 0;
            List<SelectListItem> ddlProductName = new List<SelectListItem>();
            DataSet dss = model.GetProductName();
            if (dss != null && dss.Tables.Count > 0 && dss.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dss.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlProductName.Add(new SelectListItem { Text = "-Select-", Value = "" });
                    }
                    ddlProductName.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["PK_ProductID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlProductName = ddlProductName;
            #endregion
            return View(model);
        }
        [HttpPost]
        [ActionName("BusinessReportsForUser")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult BusinessReportsForUser(User model)
        {
            List<User> lst = new List<User>();
            model.LoginId = model.LoginId == "" ? null : model.LoginId;
            if (model.IsDownline == "on")
            {
                model.IsDownline = "1";
            }
            else
            {
                model.IsDownline = "0";
            }
            model.PK_ProductID = model.PK_ProductID == "0" ? null : model.PK_ProductID;
            model.Level = model.Level == "0" ? null : model.Level;
            model.IsDownline = model.IsDownline == "0" ? null : model.IsDownline;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds = model.GetBusinessReports();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    User obj = new User();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["FirstName"].ToString();
                    obj.Amount = Convert.ToDecimal(r["Amount"].ToString());
                    obj.BV = r["BV"].ToString();
                    obj.Date = r["Date"].ToString();
                    obj.Level = r["Lvl"].ToString();
                    obj.PackageType = r["PackageType"].ToString();
                    lst.Add(obj);
                }
                model.lstBReports = lst;

                ViewBag.Amount = double.Parse(ds.Tables[0].Compute("sum(Amount)", "").ToString()).ToString("n2");
                ViewBag.BV = double.Parse(ds.Tables[0].Compute("sum(BV)", "").ToString()).ToString("n2");

            }

            #region ddlPlotSize
            int count = 0;
            List<SelectListItem> ddlProductName = new List<SelectListItem>();
            DataSet dss = model.GetProductName();
            if (dss != null && dss.Tables.Count > 0 && dss.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dss.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlProductName.Add(new SelectListItem { Text = "-Select-", Value = "" });
                    }
                    ddlProductName.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["PK_ProductID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlProductName = ddlProductName;
            #endregion
            return View(model);
        }
        public ActionResult PayoutRequest()
        {
            string FormName = "";
            string Controller = "";
            User model = new User();
            model.LoginId = Session["LoginId"].ToString();
            model.Fk_UserId = Session["Pk_userId"].ToString();
            //DataSet ds = model.GetPayoutBalance();
            //model.PayoutBalance = ds.Tables[0].Rows[0]["Balance"].ToString();

            List<User> lst = new List<User>();
            model.State = model.State == "0" ? null : model.State;
            model.LoginId = model.LoginId == "" ? null : model.LoginId;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds1 = model.GetPayoutRequest();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    User obj = new User();
                    obj.PK_RequestID = r["Pk_RequestId"].ToString();
                    obj.Amount = Convert.ToDecimal(r["AMount"].ToString());
                    obj.Date = r["RequestedDate"].ToString();
                    obj.IFSCCode = r["IFSCCode"].ToString();
                    obj.AccountNo = r["MemberAccNo"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.ROIPercentage = r["BackColor"].ToString();
                    obj.TransactionNo = r["TransactionNo"].ToString();
                    obj.GrossAmount = r["GrossAmount"].ToString();
                    obj.ProcessingFee = r["DeductionCharges"].ToString();
                    obj.TDSAmount = r["TDS"].ToString();
                    lst.Add(obj);
                }
                model.lstPayoutRequest = lst;
            }
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[1].Rows.Count > 0)
            {
                model.Status = ds1.Tables[1].Rows[0]["PanStatus"].ToString();
            }
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[2].Rows.Count > 0)
            {
                ViewBag.MenuStatus = ds1.Tables[2].Rows[0]["MenuStatus"].ToString();
                ViewBag.Reason = ds1.Tables[2].Rows[0]["Reason"].ToString();
            }
            #region Check Balance
            DataSet ds11 = model.GetWalletBalance();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                model.PayoutBalance = ds11.Tables[0].Rows[0]["amount"].ToString();
            }
            #endregion
            return View(model);
        }
        [HttpPost]
        [ActionName("PayoutRequest")]
        [OnAction(ButtonName = "PayoutRequest")]
        public ActionResult PayoutRequest(User model)
        {
            try
            {
                model.AddedBy = Session["Pk_userId"].ToString();
                DataSet ds = model.PayoutRequest();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["msg"] = "Transfer To Account Initiated Successfully.";
                    }
                    else
                    {
                        TempData["msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else { }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("PayoutRequest", "User");
        }
        public ActionResult Download()
        {
            User model = new User();
            List<User> lst = new List<User>();
            model.AddedBy = Session["Pk_userId"].ToString();
            DataSet ds = model.GetFileDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    User obj = new User();
                    obj.PK_RewardId = r["PK_RewardId"].ToString();
                    obj.Title = r["Title"].ToString();
                    obj.Image = "/UploadReward/" + r["postedFile"].ToString();
                    lst.Add(obj);
                }
                model.lstReward = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("Download")]
        public ActionResult Download(User model)
        {
            List<User> lst = new List<User>();
            model.AddedBy = Session["Pk_userId"].ToString();
            DataSet ds = model.GetFileDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    User obj = new User();
                    obj.PK_RewardId = r["PK_RewardId"].ToString();
                    obj.Title = r["Title"].ToString();
                    obj.Image = "/UploadReward/" + r["postedFile"].ToString();
                    lst.Add(obj);
                }
                model.lstReward = lst;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult GetNameDetails(string LoginId)
        {
            Admin model = new Admin();
            model.LoginId = LoginId;
            DataSet ds = model.GetNameDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {
                    model.Result = "no";
                }
                else
                {
                    model.Result = "yes";
                    model.Fk_UserId = ds.Tables[0].Rows[0]["PK_UserId"].ToString();
                    model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                }
            }
            else
            {
                model.Result = "no";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult WalletRequest()
        {
            UserWallet obj = new UserWallet();
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
            obj.LoginId = Session["LoginId"].ToString();
            ViewBag.ddlpaymentmode = ddlpaymentmode;
            #endregion
            #region ddlpaymentType
            List<SelectListItem> ddlpaymentType = Common.BindPaymentTypeOnline();
            ViewBag.ddlpaymentType = ddlpaymentType;
            
            #endregion
            #region Check Balance
            obj.FK_UserId = Session["Pk_UserId"].ToString();
            DataSet ds = obj.GetWalletBalance();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.WalletBalance = ds.Tables[0].Rows[0]["amount"].ToString();
            }
            #endregion
            return View(obj);
        }
        //[HttpPost]
        //public ActionResult WalletRequest(UserWallet obj)
        //{
        //    OrderModel orderModel = new OrderModel();
        //    string random = Common.GenerateRandom();
        //    CreateOrderResponse obj1 = new CreateOrderResponse();
        //    try
        //    {
        //        Dictionary<string, object> options = new Dictionary<string, object>();
        //        options.Add("amount", 100); // amount in the smallest currency unit
        //        options.Add("receipt", random);
        //        options.Add("currency", "INR");
        //        options.Add("payment_capture", "1");

        //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //        RazorpayClient client = new RazorpayClient(PaymentGateWayDetails.KeyName, PaymentGateWayDetails.SecretKey);
        //        Razorpay.Api.Order order = client.Order.Create(options);
        //        obj1.OrderId = order["id"].ToString();
        //        obj1.Status = "0";
        //        obj.OrderId = order["id"].ToString();
        //        obj.LoginId = Session["LoginId"].ToString();
        //        obj.TransactionType = "Wallet";
        //        orderModel.orderId = order.Attributes["id"];
        //        orderModel.razorpayKey = "rzp_test_p7xC4ZuTyASYAM";
        //        orderModel.amount = 100;
        //        orderModel.currency = "INR";
        //        orderModel.description = "Testing description";
        //        // Return on PaymentPage with Order data
        //    }
        //    catch (Exception ex)
        //    {
        //        obj1.Status = "1";
        //        obj1.ErrorMessage = ex.Message;
        //    }
        //    return View("PaymentPage", orderModel);
        //}
        [HttpPost]
        public ActionResult WalletRequest(UserWallet obj)
        {
            OrderModel orderModel = new OrderModel();
            string random = Common.GenerateRandom();
            CreateOrderResponse obj1 = new CreateOrderResponse();
            try
            {
                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", Convert.ToInt32(obj.Amount) * 100); // amount in the smallest currency unit
                options.Add("receipt", random);
                options.Add("currency", "INR");
                options.Add("payment_capture", "1");

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                RazorpayClient client = new RazorpayClient(PaymentGateWayDetails.KeyName, PaymentGateWayDetails.SecretKey);
                Razorpay.Api.Order order = client.Order.Create(options);
                obj1.OrderId = order["id"].ToString();
                obj1.Status = "0";
                obj.OrderId = order["id"].ToString();
                obj.LoginId = Session["LoginId"].ToString();
                obj.AddedBy = Session["Pk_UserId"].ToString();
                obj.Amount = (Convert.ToInt32(obj.Amount) * 100).ToString();
                obj.PaymentMode = "12";
                orderModel.orderId = order.Attributes["id"];
                orderModel.razorpayKey = "rzp_live_k8z9ufVw0R0MLV";
                orderModel.amount = Convert.ToInt32(obj.Amount)*100;
                orderModel.currency = "INR";
                orderModel.description = "Recharge Wallet";
                orderModel.name = Session["FullName"].ToString();
                orderModel.contactNumber = Session["Contact"].ToString();
                orderModel.email = Session["Email"].ToString();
                orderModel.image = "http://mytrade.co.in/MyTradeWebsite/assets/img/logo.png";
                DataSet ds = obj.SaveEwalletRequestNew();
                return View("PaymentPage", orderModel);
                // Return on PaymentPage with Order data
            }
            catch (Exception ex)
            {
                obj1.Status = "1";
                TempData["error"] = ex.Message;
                return RedirectToAction("WalletRequest", "User");
            }
        }
        public HttpWebRequest GetCreateOrderURL()
        {
            var url = PaymentGateWayDetails.CreateOrder;
            HttpWebRequest webRequest =
            (HttpWebRequest)WebRequest.Create(@"" + url);
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";
            return webRequest;
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
                        if(ds!=null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count>0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString()=="1")
                            {
                                if(obj1.captured== "captured")
                                {
                                    TempData["Msg"] = "Amount credited successfully. Order Id : " + obj1.OrderId + " and PaymentId : " + obj1.PaymentId;
                                }
                                else
                                {
                                    TempData["error"] = "Payment Failed";
                                }

                                return RedirectToAction("WalletRequest", "User");
                            }
                        }
                    }
                }
                else
                {
                    obj1.OrderId = obj.OrderId;
                    obj1.captured = "Failed";
                    obj1.Pk_UserId = obj.Pk_UserId;
                    obj1.description = "Error";
                    DataSet ds = obj1.SaveFetchPaymentResponse();
                }
            }
            catch (Exception ex)
            {
                obj1.OrderId = obj.OrderId;
                obj1.captured = ex.Message;
                obj1.Pk_UserId = obj.Pk_UserId;
                obj1.description = ex.Message;
                DataSet ds = obj1.SaveFetchPaymentResponse();
            }
            return RedirectToAction("AddWallet", "Wallet");
        }
        public ActionResult GetPackageDetails(string PackageId)
        {
            Master obj = new Master();
            try
            {
                obj.Packageid = PackageId;
                DataSet ds = obj.ProductList();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    obj.Result = "yes";
                    obj.FromAmount = Convert.ToDecimal(ds.Tables[0].Rows[0]["FromAmount"]);
                    obj.ActivationMTGToken = Convert.ToDecimal(ds.Tables[0].Rows[0]["ActivationMTGToken"]);
                    obj.BasisOn = (ds.Tables[0].Rows[0]["BasisOn"]).ToString();
                    obj.ToAmount = Convert.ToDecimal(ds.Tables[0].Rows[0]["ToAmount"]);
                    obj.InMultipleOf = Convert.ToDecimal(ds.Tables[0].Rows[0]["InMultipleOf"]);
                    obj.ROIPercent = Convert.ToDecimal(ds.Tables[0].Rows[0]["ROIPercent"]);
                }
                else { }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult SponsorIncomeForUser()
        {
            List<UserReports> lst = new List<UserReports>();
            UserReports model = new UserReports();
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds = model.GetSponsorIncomeReport();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserReports obj = new UserReports();
                    obj.ToName = r["ToName"].ToString();
                    obj.ToLoginID = r["ToLoginId"].ToString();
                    //obj.PayoutNo = r["PayoutNo"].ToString();
                    obj.FromName = r["FromName"].ToString();
                    obj.FromLoginId = r["LoginId"].ToString();
                    obj.BusinessAmount = r["BusinessAmount"].ToString();
                    obj.Percentage = r["CommissionPercentage"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    //obj.Level = r["Lvl"].ToString();
                    //obj.TransactionDate = r["TransactionDate"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.Date = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lstSponsor = lst;
                ViewBag.BusinessAmount = double.Parse(ds.Tables[0].Compute("sum(BusinessAmount)", "").ToString()).ToString("n2");
                ViewBag.Amount = double.Parse(ds.Tables[0].Compute("sum(Amount)", "").ToString()).ToString("n2");
            }
            return View(model);
        }

        [ActionName("SponsorIncomeForUser")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult SponsorIncomeForUser(UserReports model)
        {
            List<UserReports> lst = new List<UserReports>();
            model.LoginId = Session["LoginId"].ToString();
            model.LoginId = model.LoginId == "" ? null : model.LoginId;
            model.Name = model.Name == "" ? null : model.Name;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetSponsorIncomeReport();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserReports obj = new UserReports();
                    obj.ToName = r["ToName"].ToString();
                    obj.ToLoginID = r["ToLoginId"].ToString();
                    //obj.PayoutNo = r["PayoutNo"].ToString();
                    obj.FromName = r["FromName"].ToString();
                    obj.FromLoginId = r["LoginId"].ToString();
                    obj.BusinessAmount = r["BusinessAmount"].ToString();
                    obj.Percentage = r["CommissionPercentage"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    //obj.Level = r["Lvl"].ToString();
                    //obj.TransactionDate = r["TransactionDate"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.Date = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lstSponsor = lst;
                ViewBag.BusinessAmount = double.Parse(ds.Tables[0].Compute("sum(BusinessAmount)", "").ToString()).ToString("n2");
                ViewBag.Amount = double.Parse(ds.Tables[0].Compute("sum(Amount)", "").ToString()).ToString("n2");
            }
            return View(model);
        }
        public ActionResult UserReward(AssociateBooking model)
        {

            model.UserID = Session["Pk_userId"].ToString();
            //model.RewardID = "1";

            List<AssociateBooking> lst = new List<AssociateBooking>();

            DataSet ds = model.RewardList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssociateBooking obj = new AssociateBooking();

                    obj.Status = r["Status"].ToString();
                    obj.QualifyDate = r["QualifyDate"].ToString();
                    obj.RewardImage = r["RewardImage"].ToString();
                    obj.RewardName = r["RewardName"].ToString();
                    //obj.Contact = r["BackColor"].ToString();
                    //obj.PK_RewardItemId = r["PK_RewardItemId"].ToString();
                    obj.Target = r["Target"].ToString();
                    lst.Add(obj);
                }
                model.lstPlot = lst;
            }

            return View(model);
        }
        public ActionResult ClaimReward(string id)
        {
            AssociateBooking obj = new AssociateBooking();
            try
            {
                obj.PK_RewardItemId = id;
                obj.Status = "Claim";
                obj.Fk_UserId = Session["Pk_UserId"].ToString();
                DataSet ds = obj.ClaimReward();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Rewardmsg"] = "Reward Claimed";
                    }
                    else
                    {
                        TempData["Rewardmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Rewardmsg"] = ex.Message;
            }
            return RedirectToAction("UserReward");
        }
        public ActionResult SkipReward(string id)
        {
            AssociateBooking obj = new AssociateBooking();
            try
            {
                obj.PK_RewardItemId = id;
                obj.Status = "Skip";
                obj.Fk_UserId = Session["Pk_UserId"].ToString();
                DataSet ds = obj.ClaimReward();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Rewardmsg"] = "Reward Skipped";
                    }
                    else
                    {
                        TempData["Rewardmsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();

                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Rewardmsg"] = ex.Message;
            }
            return RedirectToAction("UserReward");
        }

        public ActionResult WalletTransfer()
        {
            Common objcomm = new Common();
            #region Check Balance
            objcomm.Fk_UserId = Session["Pk_UserId"].ToString();
            DataSet ds = objcomm.GetWalletBalance();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.WalletBalance = ds.Tables[0].Rows[0]["Amount"].ToString();
            }
            #endregion


            #region GetDirectPayment
            DataSet ds1 = objcomm.GetWalletTransferCharge();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                Session["MemberTransferCharge"] = ds1.Tables[0].Rows[0]["MemberTransferCharge"].ToString();
            }
            #endregion
            return View();
        }

        [HttpPost]
        [ActionName("WalletTransfer")]
        [OnAction(ButtonName = "btnTransfer")]
        public ActionResult WalletTransfer(User model)
        {
            try
            {
                model.AddedBy = Session["PK_UserId"].ToString();
                DataSet ds = model.SaveWalletTransferBalance();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["wallettransfer"] = "Transferred  successfully";
                    }
                    else
                    {
                        TempData["wallettransfer"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["wallettransfer"] = ex.Message;
            }
            return RedirectToAction("WalletTransfer", "User");
        }

        [HttpPost]
        public ActionResult GetNameDetailsforUser(string LoginId, string CustomerId)
        {
            User model = new User();
            model.LoginId = LoginId;
            DataSet ds = model.GetNameDetailsforUserWalletTransfer();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {
                    model.Result = "no";
                }
                else
                {
                    model.Result = "yes";
                    model.Fk_UserId = ds.Tables[0].Rows[0]["PK_UserId"].ToString();
                    model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                    //model.Amount = ds.Tables[1].Rows[0]["Balance"].ToString();
                }
            }
            else
            {
                model.Result = "no";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RoyaltyClub()
        {
            return View();
        }

        }
}