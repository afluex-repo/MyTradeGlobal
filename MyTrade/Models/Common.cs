using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyTrade.Models
{
    public class Common
    {
        public string AddedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string ReferBy { get; set; }
        public string Result { get; set; }
        public string DisplayName { get; set; }
        public string AddedOn { get; set; }
        public string Fk_UserId { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Fk_Paymentid { get; set; }
        public string TransactionNo { get; set; }
        public string TransactionDate { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string Package { get; set; }
        public string Leg { get; set; }
        public string ProfilePic { get; set; }
        public string TransactionType { get; set; }
        public string Country { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public List<SelectListItem> ddlcountry { get; set; }

        public string MemberTransferCharge { get; set; }
        

        public static string GenerateRandom()
        {
            Random r = new Random();
            string s = "";
            for (int i = 0; i < 6; i++)
            {
                s = string.Concat(s, r.Next(10).ToString());
            }
            return s;
        }
        public static string GenerateAlphaNumericNumber()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }
        public static string ConvertToSystemDate(string InputDate, string InputFormat)
        {
            string DateString = "";
            DateTime Dt;

            string[] DatePart = (InputDate).Split(new string[] { "-", @"/" }, StringSplitOptions.None);

            if (InputFormat == "dd-MMM-yyyy" || InputFormat == "dd/MMM/yyyy" || InputFormat == "dd/MM/yyyy" || InputFormat == "dd-MM-yyyy" || InputFormat == "DD/MM/YYYY" || InputFormat == "dd/mm/yyyy")
            {
                string Day = DatePart[0];
                string Month = DatePart[1];
                string Year = DatePart[2];

                if (Month.Length > 2)
                    DateString = InputDate;
                else
                    DateString = Month + "/" + Day + "/" + Year;
            }
            else if (InputFormat == "MM/dd/yyyy" || InputFormat == "MM-dd-yyyy")
            {
                DateString = InputDate;
            }
            else
            {
                throw new Exception("Invalid Date");
            }

            try
            {
                //Dt = DateTime.Parse(DateString);
                //return Dt.ToString("MM/dd/yyyy");
                return DateString;
            }
            catch
            {
                throw new Exception("Invalid Date");
            }

        }
        public static List<SelectListItem> AssociateStatus()
        {
            List<SelectListItem> AssociateStatus = new List<SelectListItem>();
            AssociateStatus.Add(new SelectListItem { Text = "All", Value = null });
            AssociateStatus.Add(new SelectListItem { Text = "Active", Value = "O" });
            AssociateStatus.Add(new SelectListItem { Text = "Inactive", Value = "T" });
            AssociateStatus.Add(new SelectListItem { Text = "TopUp ID", Value = "P" });
            return AssociateStatus;
        }
        public static List<SelectListItem> LegType()
        {
            List<SelectListItem> LegType = new List<SelectListItem>();
            LegType.Add(new SelectListItem { Text = "All", Value = null });
            LegType.Add(new SelectListItem { Text = "Left", Value = "L" });
            LegType.Add(new SelectListItem { Text = "Right", Value = "R" });

            return LegType;
        }
        public static List<SelectListItem> BindTopupStatus()
        {
            List<SelectListItem> IncomeStatus = new List<SelectListItem>();
            IncomeStatus.Add(new SelectListItem { Text = "All", Value = null });
            IncomeStatus.Add(new SelectListItem { Text = "Calculated", Value = "1" });
            IncomeStatus.Add(new SelectListItem { Text = "Pending", Value = "0" });

            return IncomeStatus;
        }

        public static List<SelectListItem> BindGender()
        {
            List<SelectListItem> Gender = new List<SelectListItem>();
            Gender.Add(new SelectListItem { Text = "Male", Value = "M" });
            Gender.Add(new SelectListItem { Text = "Female", Value = "F" });
            return Gender;
        }

        public static List<SelectListItem> BindPaymentMode()
        {
            List<SelectListItem> PaymentMode = new List<SelectListItem>();
            PaymentMode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
            PaymentMode.Add(new SelectListItem { Text = "Cash", Value = "Cash" });
            PaymentMode.Add(new SelectListItem { Text = "Cheque", Value = "Cheque" });
            PaymentMode.Add(new SelectListItem { Text = "NEFT", Value = "NEFT" });
            PaymentMode.Add(new SelectListItem { Text = "RTGS", Value = "RTGS" });
            PaymentMode.Add(new SelectListItem { Text = "Demand Draft", Value = "DD" });
            return PaymentMode;
        }

        public static List<SelectListItem> BindPaymentType()
        {
            List<SelectListItem> PaymentType = new List<SelectListItem>();
            PaymentType.Add(new SelectListItem { Text = "Offline", Value = "Offline" });
            PaymentType.Add(new SelectListItem { Text = "Online", Value = "Online" });
            return PaymentType;
        }

        public static List<SelectListItem> BindPaymentTypeOnline()
        {
            List<SelectListItem> PaymentType = new List<SelectListItem>();
            PaymentType.Add(new SelectListItem { Text = "Online", Value = "Online" });
            return PaymentType;
        }
        public static List<SelectListItem> BindAllWallet()
        {
            List<SelectListItem> Wallet = new List<SelectListItem>();
            Wallet.Add(new SelectListItem { Text = "-Select-", Value = "0" });
            Wallet.Add(new SelectListItem { Text = "My Trade", Value = "1" });
            Wallet.Add(new SelectListItem { Text = "TPS", Value = "2" });
            Wallet.Add(new SelectListItem { Text = "Payout", Value = "3" });
            return Wallet;
        }
        public DataSet GetMemberDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", ReferBy),

                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetMemberName", para);

            return ds;
        }

        public string LoginId { get; set; }
        
        public DataSet GetStateCity()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@PinCode", PinCode),

                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetStateCity", para);

            return ds;
        }
        public int GenerateRandomNo()
        {
            int _min = 0000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }
        public DataSet BindProduct()
        {
            SqlParameter[] para =
            {
                  new SqlParameter("@ProductId", Package),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetProductList", para);
            return ds;
        }
        public DataSet BindPackageType()
        {
            SqlParameter[] para =
            {
                  new SqlParameter("@PackageTypeId",PackageTypeId),
            };

            DataSet ds = DBHelper.ExecuteQuery("GetPackageType",para);
            return ds;
        }

        public string PackageTypeId { get; set; }
        public DataSet PaymentList()
        {
            SqlParameter[] para =
            {
                  new SqlParameter("@FK_paymentID", Fk_Paymentid),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetPaymentModeList", para);
            return ds;
        }
        public DataSet BindProductForTopUp()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetProductListForTopUp");
            return ds;
        }
        public DataSet BindProductForJoining()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetProductListForJoining");
            return ds;
        }

        public DataSet BindProductForJoiningForUser()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetProductListForJoiningUser");
            return ds;
        }
        public DataSet BindUserTypeForRegistration()
        {

            DataSet ds = DBHelper.ExecuteQuery("GetUserTypeForRegistration");

            return ds;

        }
        public DataSet BindFormTypeMaster()
        {
            SqlParameter[] para = { new SqlParameter("@Parameter", 4) };
            DataSet ds = DBHelper.ExecuteQuery("FormTypeMasterManage", para);

            return ds;

        }
        public DataSet GetWalletBalance()
        {
            SqlParameter[] para = { new SqlParameter("@PK_USerID", Fk_UserId) };
            DataSet ds = DBHelper.ExecuteQuery("GetWalletBalance", para);

            return ds;

        }


        public DataSet GetWalletTransferCharge()
        {
            SqlParameter[] para = { /*new SqlParameter("@MemberTransferCharge", MemberTransferCharge)*/ };
            DataSet ds = DBHelper.ExecuteQuery("GetWalletTransferCharge", para);

            return ds;

        }

        public static List<SelectListItem> BindCountry()
        {
            List<SelectListItem> ddlcountry = new List<SelectListItem>();
            ddlcountry.Add(new SelectListItem { Text = "-Select Country-", Value = "-Select Country-" });
            ddlcountry.Add(new SelectListItem { Text = "Afghanistan", Value = "Afghanistan" });
            ddlcountry.Add(new SelectListItem { Text = "Albania", Value = "Albania" });
            ddlcountry.Add(new SelectListItem { Text = "Algeria", Value = "Algeria" });
            ddlcountry.Add(new SelectListItem { Text = "Andorra", Value = "Andorra" });
            ddlcountry.Add(new SelectListItem { Text = "Angola", Value = "Angola" });
            ddlcountry.Add(new SelectListItem { Text = "Antigua & Deps", Value = "Antigua & Deps" });
            ddlcountry.Add(new SelectListItem { Text = "Argentina", Value = "Argentina" });
            ddlcountry.Add(new SelectListItem { Text = "Armenia", Value = "Armenia" });
            ddlcountry.Add(new SelectListItem { Text = "Australia", Value = "Australia" });
            ddlcountry.Add(new SelectListItem { Text = "Austria", Value = "Austria" });
            ddlcountry.Add(new SelectListItem { Text = "Azerbaijan", Value = "Azerbaijan" });
            ddlcountry.Add(new SelectListItem { Text = "Bahamas", Value = "Bahamas" });
            ddlcountry.Add(new SelectListItem { Text = "Bahrain", Value = "Bahrain" });
            ddlcountry.Add(new SelectListItem { Text = "Bangladesh", Value = "Bangladesh" });
            ddlcountry.Add(new SelectListItem { Text = "Barbados", Value = "Barbados" });
            ddlcountry.Add(new SelectListItem { Text = "Belarus", Value = "Belarus" });
            ddlcountry.Add(new SelectListItem { Text = "Belgium", Value = "Belgium" });
            ddlcountry.Add(new SelectListItem { Text = "Belize", Value = "Belize" });
            ddlcountry.Add(new SelectListItem { Text = "Benin", Value = "Benin" });
            ddlcountry.Add(new SelectListItem { Text = "Bhutan", Value = "Bhutan" });
            ddlcountry.Add(new SelectListItem { Text = "Bolivia", Value = "Bolivia" });
            ddlcountry.Add(new SelectListItem { Text = "Bosnia Herzegovina", Value = "Bosnia Herzegovina" });
            ddlcountry.Add(new SelectListItem { Text = "Botswana", Value = "Botswana" });
            ddlcountry.Add(new SelectListItem { Text = "Brazil", Value = "Brazil" });
            ddlcountry.Add(new SelectListItem { Text = "Brunei", Value = "Brunei" });
            ddlcountry.Add(new SelectListItem { Text = "Bulgaria", Value = "Bulgaria" });
            ddlcountry.Add(new SelectListItem { Text = "Burkina", Value = "Burkina" });
            ddlcountry.Add(new SelectListItem { Text = "Burundi", Value = "Burundi" });
            ddlcountry.Add(new SelectListItem { Text = "Cambodia", Value = "Cambodia" });
            ddlcountry.Add(new SelectListItem { Text = "Cameroon", Value = "Cameroon" });
            ddlcountry.Add(new SelectListItem { Text = "Canada", Value = "Canada" });
            ddlcountry.Add(new SelectListItem { Text = "Cape Verde", Value = "Cape Verde" });
            ddlcountry.Add(new SelectListItem { Text = "Central African Rep", Value = "Central African Rep" });
            ddlcountry.Add(new SelectListItem { Text = "Chad", Value = "Chad" });
            ddlcountry.Add(new SelectListItem { Text = "Chile", Value = "Chile" });
            ddlcountry.Add(new SelectListItem { Text = "China", Value = "China" });
            ddlcountry.Add(new SelectListItem { Text = "Colombia", Value = "Colombia" });
            ddlcountry.Add(new SelectListItem { Text = "Comoros", Value = "Comoros" });
            ddlcountry.Add(new SelectListItem { Text = "Congo", Value = "Congo" });
            ddlcountry.Add(new SelectListItem { Text = "Congo {Democratic Rep}", Value = "Congo {Democratic Rep}" });
            ddlcountry.Add(new SelectListItem { Text = "Costa Rica", Value = "Costa Rica" });
            ddlcountry.Add(new SelectListItem { Text = "Croatia", Value = "Croatia" });
            ddlcountry.Add(new SelectListItem { Text = "Cuba", Value = "Cuba" });
            ddlcountry.Add(new SelectListItem { Text = "Cyprus", Value = "Cyprus" });
            ddlcountry.Add(new SelectListItem { Text = "Czech Republic", Value = "Czech Republic" });
            ddlcountry.Add(new SelectListItem { Text = "Denmark", Value = "Denmark" });
            ddlcountry.Add(new SelectListItem { Text = "Djibouti", Value = "Djibouti" });
            ddlcountry.Add(new SelectListItem { Text = "Dominica", Value = "Dominica" });
            ddlcountry.Add(new SelectListItem { Text = "Dominican Republic", Value = "Dominican Republic" });
            ddlcountry.Add(new SelectListItem { Text = "East Timor", Value = "East Timor" });
            ddlcountry.Add(new SelectListItem { Text = "Ecuador", Value = "Ecuador" });
            ddlcountry.Add(new SelectListItem { Text = "Egypt", Value = "Egypt" });
            ddlcountry.Add(new SelectListItem { Text = "El Salvador", Value = "El Salvador" });
            ddlcountry.Add(new SelectListItem { Text = "Equatorial Guinea", Value = "Equatorial Guinea" });
            ddlcountry.Add(new SelectListItem { Text = "Eritrea", Value = "Eritrea" });
            ddlcountry.Add(new SelectListItem { Text = "Estonia", Value = "Estonia" });
            ddlcountry.Add(new SelectListItem { Text = "Ethiopia", Value = "Ethiopia" });
            ddlcountry.Add(new SelectListItem { Text = "Fiji", Value = "Fiji" });
            ddlcountry.Add(new SelectListItem { Text = "Finland", Value = "Finland" });
            ddlcountry.Add(new SelectListItem { Text = "France", Value = "France" });
            ddlcountry.Add(new SelectListItem { Text = "Gabon", Value = "Gabon" });
            ddlcountry.Add(new SelectListItem { Text = "Gambia", Value = "Gambia" });
            ddlcountry.Add(new SelectListItem { Text = "Georgia", Value = "Georgia" });
            ddlcountry.Add(new SelectListItem { Text = "Germany", Value = "Germany" });
            ddlcountry.Add(new SelectListItem { Text = "Ghana", Value = "Ghana" });
            ddlcountry.Add(new SelectListItem { Text = "Greece", Value = "Greece" });
            ddlcountry.Add(new SelectListItem { Text = "Grenada", Value = "Grenada" });
            ddlcountry.Add(new SelectListItem { Text = "Guatemala", Value = "Guatemala" });
            ddlcountry.Add(new SelectListItem { Text = "Guinea", Value = "Guinea" });
            ddlcountry.Add(new SelectListItem { Text = "Guinea-Bissau", Value = "Guinea-Bissau" });
            ddlcountry.Add(new SelectListItem { Text = "Guyana", Value = "Guyana" });
            ddlcountry.Add(new SelectListItem { Text = "Haiti", Value = "Haiti" });
            ddlcountry.Add(new SelectListItem { Text = "Honduras", Value = "Honduras" });
            ddlcountry.Add(new SelectListItem { Text = "Hungary", Value = "Hungary" });
            ddlcountry.Add(new SelectListItem { Text = "Iceland", Value = "Iceland" });
            ddlcountry.Add(new SelectListItem { Text = "India", Value = "India" });
            ddlcountry.Add(new SelectListItem { Text = "Indonesia", Value = "Indonesia" });
            ddlcountry.Add(new SelectListItem { Text = "Iran", Value = "Iran" });
            ddlcountry.Add(new SelectListItem { Text = "Iraq", Value = "Iraq" });
            ddlcountry.Add(new SelectListItem { Text = "Ireland {Republic}", Value = "Ireland {Republic}" });
            ddlcountry.Add(new SelectListItem { Text = "Israel", Value = "Israel" });
            ddlcountry.Add(new SelectListItem { Text = "Italy", Value = "Italy" });
            ddlcountry.Add(new SelectListItem { Text = "Ivory Coast", Value = "Ivory Coast" });
            ddlcountry.Add(new SelectListItem { Text = "Jamaica", Value = "Jamaica" });
            ddlcountry.Add(new SelectListItem { Text = "Japan", Value = "Japan" });
            ddlcountry.Add(new SelectListItem { Text = "Jordan", Value = "Jordan" });
            ddlcountry.Add(new SelectListItem { Text = "Kazakhstan", Value = "Kazakhstan" });
            ddlcountry.Add(new SelectListItem { Text = "Kenya", Value = "Kenya" });
            ddlcountry.Add(new SelectListItem { Text = "Kiribati", Value = "Kiribati" });
            ddlcountry.Add(new SelectListItem { Text = "Korea North", Value = "Korea North" });
            ddlcountry.Add(new SelectListItem { Text = "Korea South", Value = "Korea South" });
            ddlcountry.Add(new SelectListItem { Text = "Kosovo", Value = "Kosovo" });
            ddlcountry.Add(new SelectListItem { Text = "Kuwait", Value = "Kuwait" });
            ddlcountry.Add(new SelectListItem { Text = "Kyrgyzstan", Value = "Kyrgyzstan" });
            ddlcountry.Add(new SelectListItem { Text = "Laos", Value = "Laos" });
            ddlcountry.Add(new SelectListItem { Text = "Latvia", Value = "Latvia" });
            ddlcountry.Add(new SelectListItem { Text = "Lebanon", Value = "Lebanon" });
            ddlcountry.Add(new SelectListItem { Text = "Lesotho", Value = "Lesotho" });
            ddlcountry.Add(new SelectListItem { Text = "Liberia", Value = "Liberia" });
            ddlcountry.Add(new SelectListItem { Text = "Libya", Value = "Libya" });
            ddlcountry.Add(new SelectListItem { Text = "Liechtenstein", Value = "Liechtenstein" });
            ddlcountry.Add(new SelectListItem { Text = "Lithuania", Value = "Lithuania" });
            ddlcountry.Add(new SelectListItem { Text = "Luxembourg", Value = "Luxembourg" });
            ddlcountry.Add(new SelectListItem { Text = "Macedonia", Value = "Macedonia" });
            ddlcountry.Add(new SelectListItem { Text = "Madagascar", Value = "Madagascar" });
            ddlcountry.Add(new SelectListItem { Text = "Malawi", Value = "Malawi" });
            ddlcountry.Add(new SelectListItem { Text = "Malaysia", Value = "Malaysia" });
            ddlcountry.Add(new SelectListItem { Text = "Maldives", Value = "Maldives" });
            ddlcountry.Add(new SelectListItem { Text = "Mali", Value = "Mali" });
            ddlcountry.Add(new SelectListItem { Text = "Malta", Value = "Malta" });
            ddlcountry.Add(new SelectListItem { Text = "Marshall Islands", Value = "Marshall Islands" });
            ddlcountry.Add(new SelectListItem { Text = "Mauritania", Value = "Mauritania" });
            ddlcountry.Add(new SelectListItem { Text = "Mauritius", Value = "Mauritius" });
            ddlcountry.Add(new SelectListItem { Text = "Mexico", Value = "Mexico" });
            ddlcountry.Add(new SelectListItem { Text = "Micronesia", Value = "Micronesia" });
            ddlcountry.Add(new SelectListItem { Text = "Moldova", Value = "Moldova" });
            ddlcountry.Add(new SelectListItem { Text = "Monaco", Value = "Monaco" });
            ddlcountry.Add(new SelectListItem { Text = "Mongolia", Value = "Mongolia" });
            ddlcountry.Add(new SelectListItem { Text = "Montenegro", Value = "Montenegro" });
            ddlcountry.Add(new SelectListItem { Text = "Morocco", Value = "Morocco" });
            ddlcountry.Add(new SelectListItem { Text = "Mozambique", Value = "Mozambique" });
            ddlcountry.Add(new SelectListItem { Text = "Myanmar, {Burma}", Value = "Myanmar, {Burma}" });
            ddlcountry.Add(new SelectListItem { Text = "Namibia", Value = "Namibia" });
            ddlcountry.Add(new SelectListItem { Text = "Nauru", Value = "Nauru" });
            ddlcountry.Add(new SelectListItem { Text = "Nepal", Value = "Nepal" });
            ddlcountry.Add(new SelectListItem { Text = "Netherlands", Value = "Netherlands" });
            ddlcountry.Add(new SelectListItem { Text = "New Zealand", Value = "New Zealand" });
            ddlcountry.Add(new SelectListItem { Text = "Nicaragua", Value = "Nicaragua" });
            ddlcountry.Add(new SelectListItem { Text = "Niger", Value = "Niger" });
            ddlcountry.Add(new SelectListItem { Text = "Nigeria", Value = "Nigeria" });
            ddlcountry.Add(new SelectListItem { Text = "Norway", Value = "Norway" });
            ddlcountry.Add(new SelectListItem { Text = "Oman", Value = "Oman" });
            ddlcountry.Add(new SelectListItem { Text = "Pakistan", Value = "Pakistan" });
            ddlcountry.Add(new SelectListItem { Text = "Palau", Value = "Palau" });
            ddlcountry.Add(new SelectListItem { Text = "Panama", Value = "Panama" });
            ddlcountry.Add(new SelectListItem { Text = "Papua New Guinea", Value = "Papua New Guinea" });
            ddlcountry.Add(new SelectListItem { Text = "Paraguay", Value = "Paraguay" });
            ddlcountry.Add(new SelectListItem { Text = "Peru", Value = "Peru" });
            ddlcountry.Add(new SelectListItem { Text = "Philippines", Value = "Philippines" });
            ddlcountry.Add(new SelectListItem { Text = "Poland", Value = "Poland" });
            ddlcountry.Add(new SelectListItem { Text = "Portugal", Value = "Portugal" });
            ddlcountry.Add(new SelectListItem { Text = "Qatar", Value = "Qatar" });
            ddlcountry.Add(new SelectListItem { Text = "Romania", Value = "Romania" });
            ddlcountry.Add(new SelectListItem { Text = "Russian Federation", Value = "Russian Federation" });
            ddlcountry.Add(new SelectListItem { Text = "Rwanda", Value = "Rwanda" });
            ddlcountry.Add(new SelectListItem { Text = "St Kitts & Nevis", Value = "St Kitts & Nevis" });
            ddlcountry.Add(new SelectListItem { Text = "St Lucia", Value = "St Lucia" });
            ddlcountry.Add(new SelectListItem { Text = "Saint Vincent & the Grenadines", Value = "Saint Vincent & the Grenadines" });
            ddlcountry.Add(new SelectListItem { Text = "Samoa", Value = "Samoa" });
            ddlcountry.Add(new SelectListItem { Text = "San Marino", Value = "San Marino" });
            ddlcountry.Add(new SelectListItem { Text = "Sao Tome & Principe", Value = "Sao Tome & Principe" });
            ddlcountry.Add(new SelectListItem { Text = "Saudi Arabia", Value = "Saudi Arabia" });
            ddlcountry.Add(new SelectListItem { Text = "Senegal", Value = "Senegal" });
            ddlcountry.Add(new SelectListItem { Text = "Serbia", Value = "Serbia" });
            ddlcountry.Add(new SelectListItem { Text = "Seychelles", Value = "Seychelles" });
            ddlcountry.Add(new SelectListItem { Text = "Sierra Leone", Value = "Sierra Leone" });
            ddlcountry.Add(new SelectListItem { Text = "Singapore", Value = "Singapore" });
            ddlcountry.Add(new SelectListItem { Text = "Slovakia", Value = "Slovakia" });
            ddlcountry.Add(new SelectListItem { Text = "Slovenia", Value = "Slovenia" });
            ddlcountry.Add(new SelectListItem { Text = "Solomon Islands", Value = "Solomon Islands" });
            ddlcountry.Add(new SelectListItem { Text = "Somalia", Value = "Somalia" });
            ddlcountry.Add(new SelectListItem { Text = "South Africa", Value = "South Africa" });
            ddlcountry.Add(new SelectListItem { Text = "South Sudan", Value = "South Sudan" });
            ddlcountry.Add(new SelectListItem { Text = "Spain", Value = "Spain" });
            ddlcountry.Add(new SelectListItem { Text = "Sri Lanka", Value = "Sri Lanka" });
            ddlcountry.Add(new SelectListItem { Text = "Sudan", Value = "Sudan" });
            ddlcountry.Add(new SelectListItem { Text = "Suriname", Value = "Suriname" });
            ddlcountry.Add(new SelectListItem { Text = "Swaziland", Value = "Swaziland" });
            ddlcountry.Add(new SelectListItem { Text = "Sweden", Value = "Sweden" });
            ddlcountry.Add(new SelectListItem { Text = "Switzerland", Value = "Switzerland" });
            ddlcountry.Add(new SelectListItem { Text = "Syria", Value = "Syria" });
            ddlcountry.Add(new SelectListItem { Text = "Taiwan", Value = "Taiwan" });
            ddlcountry.Add(new SelectListItem { Text = "Tajikistan", Value = "Tajikistan" });
            ddlcountry.Add(new SelectListItem { Text = "Tanzania", Value = "Tanzania" });
            ddlcountry.Add(new SelectListItem { Text = "Thailand", Value = "Thailand" });
            ddlcountry.Add(new SelectListItem { Text = "Togo", Value = "Togo" });
            ddlcountry.Add(new SelectListItem { Text = "Tonga", Value = "Tonga" });
            ddlcountry.Add(new SelectListItem { Text = "Trinidad & Tobago", Value = "Trinidad & Tobago" });
            ddlcountry.Add(new SelectListItem { Text = "Tunisia", Value = "Tunisia" });
            ddlcountry.Add(new SelectListItem { Text = "Turkey", Value = "Turkey" });
            ddlcountry.Add(new SelectListItem { Text = "Turkmenistan", Value = "Turkmenistan" });
            ddlcountry.Add(new SelectListItem { Text = "Tuvalu", Value = "Tuvalu" });
            ddlcountry.Add(new SelectListItem { Text = "Uganda", Value = "Uganda" });
            ddlcountry.Add(new SelectListItem { Text = "Ukraine", Value = "Ukraine" });
            ddlcountry.Add(new SelectListItem { Text = "United Arab Emirates", Value = "United Arab Emirates" });
            ddlcountry.Add(new SelectListItem { Text = "United Kingdom", Value = "United Kingdom" });
            ddlcountry.Add(new SelectListItem { Text = "United States", Value = "United States" });
            ddlcountry.Add(new SelectListItem { Text = "Uruguay", Value = "Uruguay" });
            ddlcountry.Add(new SelectListItem { Text = "Uzbekistan", Value = "Uzbekistan" });
            ddlcountry.Add(new SelectListItem { Text = "Vanuatu", Value = "Vanuatu" });
            ddlcountry.Add(new SelectListItem { Text = "Vatican City", Value = "Vatican City" });
            ddlcountry.Add(new SelectListItem { Text = "Venezuela", Value = "Venezuela" });
            ddlcountry.Add(new SelectListItem { Text = "Vietnam", Value = "Vietnam" });
            ddlcountry.Add(new SelectListItem { Text = "Yemen", Value = "Yemen" });
            ddlcountry.Add(new SelectListItem { Text = "Zambia", Value = "Zambia" });
            ddlcountry.Add(new SelectListItem { Text = "Zimbabwe", Value = "Zimbabwe" });

            return ddlcountry;
        }



    }
    public class PaymentGateWayDetails
    {
        public static string CreateOrder = "https://api.razorpay.com/v1/orders";
        public static string CapturePayment = "https://api.razorpay.com/v1/payments/";
        public static string FetchPaymentByOrderURL = "https://api.razorpay.com/v1/orders/";
        public static string KeyName = "rzp_live_k8z9ufVw0R0MLV";
        public static string SecretKey = "BxiVGly6SUkZkI6aJ5vjbSU6";
    }

}