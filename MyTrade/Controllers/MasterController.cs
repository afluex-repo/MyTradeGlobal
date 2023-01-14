using MyTrade.Filter;
using MyTrade.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyTrade.Controllers
{
    public class MasterController : AdminBaseController
    {

        #region PackageMaster
        public ActionResult PackageList()
        {
            Master model = new Master();
            #region pacakgeTpe Bind
            Common objcomm = new Common();
            List<SelectListItem> ddlPackageType = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindPackageType();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
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

            #region pacakge Bind
            List<SelectListItem> ddlPackage = new List<SelectListItem>();
            DataSet ds2 = objcomm.BindProduct();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlPackage.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlPackage.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });
                    count++;
                }
            }
            ViewBag.ddlPackage = ddlPackage;
            #endregion

            return View(model);
        }

        public ActionResult GetProductList(string PackageTypeId)
        {
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            Master model = new Master();
            model.PackageTypeId = PackageTypeId;

            DataSet ds = model.GetProductListForPackageList();

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

        [HttpPost]
        public ActionResult PackageList(Master model)
        {
            List<Master> lst = new List<Master>();
            if(model.Packageid=="0")
            {
                model.Packageid = null;
            }
            if(model.PackageTypeId=="0")
            {
                model.PackageTypeId = null;
            }

            DataSet ds = model.ProductList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.Packageid = r["Pk_ProductId"].ToString();
                    obj.IscomboPackage = r["IsComboPackage"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.ProductPrice = Convert.ToDecimal(r["ProductPrice"]);
                    //obj.IGST = Convert.ToDecimal(r["IGST"]);
                    //obj.CGST = Convert.ToDecimal(r["CGST"]);
                    //obj.SGST = Convert.ToDecimal(r["SGST"]);
                    obj.BinaryPercent = Convert.ToDecimal(r["BinaryPercent"]);
                    obj.DirectPercent = Convert.ToDecimal(r["DirectPercent"]);
                    obj.ROIPercent = Convert.ToDecimal(r["ROIPercent"]);
                    obj.Days = r["PackageDays"].ToString();
                    //obj.BV = Convert.ToDecimal(r["BV"]);
                    obj.ActivationMTGToken = Convert.ToDecimal(r["ActivationMTGToken"]);
                    obj.PackageTypeId = r["PackageTypeId"].ToString();
                    obj.PackageTypeId = obj.PackageTypeId;
                    obj.PackageTypeName = r["PackageTypeName"].ToString();
                    obj.FromAmount = Convert.ToDecimal(r["FromAmount"]);
                    obj.ToAmount = Convert.ToDecimal(r["ToAmount"]);
                    obj.Status = r["Status"].ToString();
                    obj.InMultipleOf = Convert.ToDecimal(r["InMultipleOf"]);
                    //obj.IGST = Convert.ToDecimal(r["IGST"]);
                    //obj.HSNCode = r["HSNCode"].ToString();
                    //obj.FinalAmount = Convert.ToDecimal(r["FinalAmount"]);
                    obj.SponsorIncome = Convert.ToDecimal(r["SponsorIncome"]);
                    lst.Add(obj);
                }
                model.lstpackage = lst;
            }


            #region pacakgeTpe Bind
            Common objcomm = new Common();
            List<SelectListItem> ddlPackageType = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindPackageType();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
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
            #region pacakge Bind
            List<SelectListItem> ddlPackage = new List<SelectListItem>();
            DataSet ds2 = objcomm.BindProduct();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlPackage.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlPackage.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });
                    count++;
                }
            }
            ViewBag.ddlPackage = ddlPackage;
            #endregion
            return View(model);
        }
        public ActionResult DeletePackage(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.Packageid = id;
                obj.AddedBy = Session["PK_AdminId"].ToString();
                DataSet ds = obj.DeleteProduct();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        TempData["Package"] = "Product deleted successfully";
                        FormName = "PackageMaster";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["Package"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "PackageMaster";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Package"] = ex.Message;
                FormName = "PackageMaster";
                Controller = "Master";
            }

            return RedirectToAction(FormName, Controller);
        }
        public ActionResult ActivateDeactivatePackage(string id, string IsActive)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.Packageid = id;
                obj.IsActive = Convert.ToBoolean(IsActive);
                obj.AddedBy = Session["PK_AdminId"].ToString();
                DataSet ds = obj.ActivateDeactivatePackage();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        TempData["Package"] = "Product status updated successfully";
                        FormName = "PackageMaster";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["Package"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "PackageMaster";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Package"] = ex.Message;
                FormName = "PackageMaster";
                Controller = "Master";
            }

            return RedirectToAction(FormName, Controller);
        }
        public ActionResult PackageMaster(string PackageID)
        {
            Master obj = new Master();
            #region pacakgeTpe Bind
            Common objcomm = new Common();
            List<SelectListItem> ddlPackageType = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindPackageType();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
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
            if (PackageID != null)
            {
                try
                {
                    obj.Packageid = PackageID;
                    DataSet ds = obj.ProductList();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        obj.IscomboPackage = ds.Tables[0].Rows[0]["IscomboPackage"].ToString();
                        obj.PackageTypeId = ds.Tables[0].Rows[0]["PackageTypeId"].ToString();
                        obj.Packageid = ds.Tables[0].Rows[0]["Pk_ProductId"].ToString();
                        obj.ProductName = ds.Tables[0].Rows[0]["ProductName"].ToString();
                        obj.ProductPrice = Convert.ToDecimal(ds.Tables[0].Rows[0]["ProductPrice"]);
                        //obj.IGST = Convert.ToDecimal(ds.Tables[0].Rows[0]["IGST"]);
                        //obj.CGST = Convert.ToDecimal(ds.Tables[0].Rows[0]["CGST"]);
                        //obj.SGST = Convert.ToDecimal(ds.Tables[0].Rows[0]["SGST"]);
                        obj.BinaryPercent = Convert.ToDecimal(ds.Tables[0].Rows[0]["BinaryPercent"]);
                        obj.DirectPercent = Convert.ToDecimal(ds.Tables[0].Rows[0]["DirectPercent"]);
                        obj.Days = ds.Tables[0].Rows[0]["PackageDays"].ToString();
                        obj.ROIPercent = Convert.ToDecimal(ds.Tables[0].Rows[0]["ROIPercent"]);
                        //obj.BV = Convert.ToDecimal(ds.Tables[0].Rows[0]["BV"]);
                        obj.ActivationMTGToken = Convert.ToDecimal(ds.Tables[0].Rows[0]["ActivationMTGToken"]);
                        obj.PackageTypeId = (ds.Tables[0].Rows[0]["PackageTypeId"].ToString());
                        obj.PackageTypeName = (ds.Tables[0].Rows[0]["PackageTypeName"].ToString());
                        obj.FromAmount = Convert.ToDecimal(ds.Tables[0].Rows[0]["FromAmount"]);
                        obj.ToAmount = Convert.ToDecimal(ds.Tables[0].Rows[0]["ToAmount"]);
                        obj.InMultipleOf = Convert.ToDecimal(ds.Tables[0].Rows[0]["InMultipleOf"]);
                        //obj.IGST = Convert.ToDecimal(ds.Tables[0].Rows[0]["IGST"]);
                        //obj.HSNCode = ds.Tables[0].Rows[0]["HSNCode"].ToString();
                        //obj.FinalAmount = Convert.ToDecimal(ds.Tables[0].Rows[0]["FinalAmount"]);
                        obj.SponsorIncome = Convert.ToDecimal(ds.Tables[0].Rows[0]["SponsorIncome"]);
                    }
                }
                catch (Exception ex)
                {
                    TempData["Package"] = ex.Message;
                }
            }
            else { 
            
            }
            return View(obj);
        }

        public ActionResult SaveProduct(string PackageType, string ProductName, string ProductPrice, string IGST, string ROIPercent, string BV, string FromAmount, string ToAmount, string Days, string InMultipleOf, string HSNCode, string FinalAmount, string SponsorIncome, string IscomboPackage,string ActivationMTGToken,string BasisOn,string DrAmount1,string DrAmount2,string DrAmount3,string ReturnPercent1,string ReturnPercent2,string ReturnPercent3)
        {
            Master obj = new Master();
            try
            {
                obj.PackageTypeId = PackageType;
                obj.ProductName = ProductName;
                obj.BasisOn = BasisOn;
                obj.ProductPrice = Convert.ToDecimal(ProductPrice);
                //obj.IGST = Convert.ToDecimal(IGST);
                obj.Days = Days;
                obj.ROIPercent = Convert.ToDecimal(ROIPercent);
                obj.DrAmount1 = Convert.ToDecimal(DrAmount1);
                obj.DrAmount2 = Convert.ToDecimal(DrAmount2);
                obj.DrAmount3 = Convert.ToDecimal(DrAmount3);
                obj.ReturnPercent1 = Convert.ToDecimal(ReturnPercent1);
                obj.ReturnPercent2 = Convert.ToDecimal(ReturnPercent2);
                obj.ReturnPercent3 = Convert.ToDecimal(ReturnPercent3);
                //obj.HSNCode = HSNCode == null ? "" : HSNCode;
                //obj.FinalAmount = Convert.ToDecimal(FinalAmount);
                //obj.BV = Convert.ToDecimal(BV);
                obj.ActivationMTGToken = Convert.ToDecimal(ActivationMTGToken);
                obj.AddedBy = Session["PK_AdminId"].ToString();
                obj.FromAmount = Convert.ToDecimal(FromAmount);
                obj.ToAmount = Convert.ToDecimal(ToAmount);
                obj.InMultipleOf = Convert.ToDecimal(InMultipleOf);
                obj.SponsorIncome = Convert.ToDecimal(SponsorIncome);
                obj.IscomboPackage = IscomboPackage;
                DataSet ds = obj.SaveProduct();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        obj.Result = "Product saved successfully";
                    }
                    else
                    {
                        obj.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Result = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateProduct(string PackageType, string Packageid, string ProductName, string ProductPrice, string IGST, string ROIPercent, string BV, string FromAmount, string ToAmount, string Days, string InMultipleOf, string HSNCode, string FinalAmount,string SponsorIncome, string IscomboPackage,string ActivationMTGToken,string BasisOn, string DrAmount1, string DrAmount2, string DrAmount3, string ReturnPercent1, string ReturnPercent2, string ReturnPercent3)
        {
            Master obj = new Master();
            try
            {
                obj.PackageTypeId = PackageType;
                obj.Packageid = Packageid;
                obj.ProductName = ProductName;
                obj.BasisOn = BasisOn;
                obj.ProductPrice = Convert.ToDecimal(ProductPrice);
                //obj.IGST = Convert.ToDecimal(IGST);
                obj.Days = Days;
                obj.SponsorIncome = Convert.ToDecimal(SponsorIncome);
                obj.ROIPercent = Convert.ToDecimal(ROIPercent);
                obj.DrAmount1 = Convert.ToDecimal(DrAmount1);
                obj.DrAmount2 = Convert.ToDecimal(DrAmount2);
                obj.DrAmount3 = Convert.ToDecimal(DrAmount3);
                obj.ReturnPercent1 = Convert.ToDecimal(ReturnPercent1);
                obj.ReturnPercent2 = Convert.ToDecimal(ReturnPercent2);
                obj.ReturnPercent3 = Convert.ToDecimal(ReturnPercent3);
                //obj.HSNCode = HSNCode;
                if (obj.IGST != 0)
                {
                    obj.FinalAmount = (obj.ProductPrice * (obj.IGST / 100)) + obj.ProductPrice;
                }

                //obj.BV = Convert.ToDecimal(BV);
                obj.ActivationMTGToken = Convert.ToDecimal(ActivationMTGToken);
                obj.UpdatedBy = Session["PK_AdminId"].ToString();
                obj.FromAmount = Convert.ToDecimal(FromAmount);
                obj.ToAmount = Convert.ToDecimal(ToAmount);
                obj.InMultipleOf = Convert.ToDecimal(InMultipleOf);
                obj.IscomboPackage = IscomboPackage;
                //obj.FinalAmount = Convert.ToDecimal(FinalAmount);
                DataSet ds = obj.UpdateProduct();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        obj.Result = "Product updated successfully";
                    }
                    else
                    {
                        obj.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                obj.Result = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        #endregion
        public ActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Upload")]
        public ActionResult Upload(Master model, HttpPostedFileBase postedFile)
        {
            try
            {
                if (postedFile != null)
                {
                    model.Image = "../UploadReward/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(Path.Combine(Server.MapPath(model.Image)));
                }
                model.AddedBy = Session["PK_AdminId"].ToString();
                DataSet ds = model.Upload();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Upload"] = "File upload successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Upload"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Upload"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                TempData["Upload"] = ex.Message;
            }
            return RedirectToAction("Upload", "Master");

        }
        public ActionResult UploadList()
        {
            Master model = new Master();
            List<Master> lst = new List<Master>();
            DataSet ds = model.GetRewarDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
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
        [ActionName("UploadList")]
        public ActionResult UploadList(Master model)
        {
            List<Master> lst = new List<Master>();
            DataSet ds = model.GetRewarDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PK_RewardId = r["PK_RewardId"].ToString();
                    obj.Title = r["Title"].ToString();
                    obj.Image = "/UploadReward/" + r["postedFile"].ToString();
                    lst.Add(obj);
                }
                model.lstReward = lst;
            }
            return View(model);
        }
        public ActionResult DeleteRewards(string Id)
        {
            try
            {
                Master model = new Master();
                model.PK_RewardId = Id;
                model.AddedBy = Session["PK_AdminId"].ToString();
                DataSet ds = model.DeleteReward();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Reward"] = "Reward deleted successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Reward"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Reward"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                TempData["Reward"] = ex.Message;
            }
            return RedirectToAction("UploadList", "Master");

        }
        public ActionResult UploadFile()
        {
            return View();
        }
        [HttpPost]
        [ActionName("UploadFile")]
        public ActionResult UploadFile(Master model, HttpPostedFileBase postedFile)
        {
            try
            {
                if (postedFile != null)
                {
                    model.Image = "../UploadFile/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(Path.Combine(Server.MapPath(model.Image)));
                }
                model.AddedBy = Session["PK_AdminId"].ToString();
                DataSet ds = model.UploadFile();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Upload"] = "File upload successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Upload"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Upload"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                TempData["Upload"] = ex.Message;
            }
            return RedirectToAction("UploadFile", "Master");
        }
        public ActionResult UploadFileList()
        {
            Master model = new Master();
            List<Master> lst = new List<Master>();
            DataSet ds = model.GetFilesDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PK_RewardId = r["PK_RewardId"].ToString();
                    obj.Title = r["Title"].ToString();
                    obj.Image = "/UploadFile/" + r["postedFile"].ToString();
                    lst.Add(obj);
                }
                model.lstReward = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("UploadFileList")]
        public ActionResult UploadFileList(Master model)
        {
            List<Master> lst = new List<Master>();
            DataSet ds = model.GetFilesDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PK_RewardId = r["PK_RewardId"].ToString();
                    obj.Title = r["Title"].ToString();
                    obj.Image = "/UploadFile/" + r["postedFile"].ToString();
                    lst.Add(obj);
                }
                model.lstReward = lst;
            }
            return View(model);
        }
        public ActionResult DeleteUploadFile(string Id)
        {
            try
            {
                Master model = new Master();
                model.PK_RewardId = Id;
                model.AddedBy = Session["PK_AdminId"].ToString();
                DataSet ds = model.DeleteFile();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Reward"] = "File deleted successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Reward"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Reward"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                TempData["Reward"] = ex.Message;
            }
            return RedirectToAction("UploadFileList", "Master");
        }

        public ActionResult RewardMaster(string RewardId)
        {
            Master model = new Master();
            if (RewardId !="" && RewardId !=null)
            {
                model.PK_RewardId = RewardId;
                DataSet ds = model.GetRewardList();
                if (ds !=null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count>0)
                {
                    model.RewardName = ds.Tables[0].Rows[0]["RewardName"].ToString();
                    model.FromDate = ds.Tables[0].Rows[0]["FromDate"].ToString();
                    model.ToDate = ds.Tables[0].Rows[0]["ToDate"].ToString();
                    model.PK_RewardId = ds.Tables[0].Rows[0]["Pk_BonazaRewardId"].ToString();
                }
                
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("RewardMaster")]
        [OnAction(ButtonName = "save")]
        public ActionResult RewardMasterAction(Master model)
        {
            try
            {
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.SaveRewardMaster();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Reward"] = "Reward Save Successfully !!";
                    }
                    else
                    {
                        TempData["Reward"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Reward"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                TempData["Reward"] = ex.Message;
            }
            return RedirectToAction("RewardMaster", "Master");
        }

        [HttpPost]
        [ActionName("RewardMaster")]
        [OnAction(ButtonName = "Update")]
        public ActionResult updateReward(Master model)
        {
            try
            {
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.UpdateReward();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Reward"] = "Reward Updated Successfully !!";
                    }
                    else
                    {
                        TempData["Reward"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Reward"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                TempData["Reward"] = ex.Message;
            }
            return RedirectToAction("RewardMaster", "Master");
        }
        public ActionResult RewardMasterList()
        {
            Master model = new Master();
            List<Master> lst = new List<Master>();
            DataSet ds = model.GetRewardList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PK_RewardId = r["Pk_BonazaRewardId"].ToString();
                    obj.RewardName = r["RewardName"].ToString();
                    obj.FromDate = r["FromDate"].ToString();
                    obj.ToDate = r["ToDate"].ToString();
                    lst.Add(obj);
                }
                model.lstBonazaReward = lst;
            }
            return View(model);
        }
        public ActionResult DeleteReward(string RewardId)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.PK_RewardId = RewardId;
                obj.AddedBy = Session["PK_AdminId"].ToString();
                DataSet ds = obj.deleteReward();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        TempData["Msg"] = "Reward Details deleted successfully";
                        FormName = "RewardMasterList";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["Msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "RewardMasterList";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Msg"] = ex.Message;
                FormName = "RewardMasterList";
                Controller = "Master";
            }

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult BalanceTransfer(Master model,string Pk_BalanceTransferId)
        {
            List<Master> lst = new List<Master>();
            DataSet ds = model.GetBalanceTransferList();
            if (ds != null && ds.Tables.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.Pk_BalanceTransferId = r["Pk_BalanceTransferId"].ToString();
                    obj.MemberTransferCharge = r["MemberTransferCharge"].ToString();
                    obj.BrokerTransferCharge = r["BrokerTransferCharge"].ToString();
                    obj.Status = r["Status"].ToString();
                    lst.Add(obj);
                }
                model.lstbalancetransfer = lst;
            }
            
            if (Pk_BalanceTransferId != null)
            {
                model.Pk_BalanceTransferId = Pk_BalanceTransferId;

                DataSet ds2 = model.GetBalanceTransferList();
                if (ds2 != null && ds2.Tables.Count > 0)
                {
                    model.Pk_BalanceTransferId= ds2.Tables[0].Rows[0]["Pk_BalanceTransferId"].ToString();
                    model.MemberTransferCharge = ds2.Tables[0].Rows[0]["MemberTransferCharge"].ToString();
                    model.BrokerTransferCharge = ds2.Tables[0].Rows[0]["BrokerTransferCharge"].ToString();
                    model.Status = ds2.Tables[0].Rows[0]["Status"].ToString();
                }
                
            }
            return View(model);
            
        }

        public ActionResult BalanceTransferList(Master model)
        {

            return View();
        }

        [HttpPost]
        [ActionName("BalanceTransfer")]
        [OnAction(ButtonName = "Save")]
        public ActionResult SaveBalanceTransfer(Master model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                model.Fk_UserId = Session["Pk_AdminId"].ToString();
                DataSet ds = model.SaveBalanceTransfer();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Transfermsg"] = "Brokerage Deduction Saved Successfully";
                    }
                    else
                    {
                        TempData["Transfermsg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Transfermsg"] = ex.Message;
            }
            return RedirectToAction("BalanceTransfer", "Master");
        }


        [HttpPost]
        [ActionName("BalanceTransfer")]
        [OnAction(ButtonName = "btnUpdate")]
        public ActionResult UpdateBalanceTransfer(Master model,Master modelinsrtupdt)
        {
            try
            {
                modelinsrtupdt.AddedBy = Session["Pk_AdminId"].ToString();
                modelinsrtupdt.Fk_UserId = Session["Pk_AdminId"].ToString();
                DataSet ds = model.UpdateBalanceTransfer();
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                            {

                                TempData["UpdateBalancetransfer"] = "Brokerage Deduction updated Successfully";
                            }
                            else
                            {
                                TempData["UpdateBalancetransfer"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            }
                        }
            }
            catch (Exception ex)
            {
                TempData["UpdateBalancetransfer"] = ex.Message;
            }

            try
            {
                //modelinsrtupdt.AddedBy = Session["Pk_AdminId"].ToString();
                //modelinsrtupdt.Fk_UserId = Session["Pk_AdminId"].ToString();
                DataSet ds2 = modelinsrtupdt.UpdateBalanceTransfer();
                if (ds2 != null && ds2.Tables.Count > 0)
                {
                    if (ds2.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["UpdateBalancetransfer"] = "Brokerage Deduction updated Successfully";
                    }
                    else
                    {
                        TempData["UpdateBalancetransfer"] = ds2.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["UpdateBalancetransfer"] = ex.Message;
            }



            return RedirectToAction("BalanceTransfer");
        }
        
    }
}