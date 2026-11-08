using ADMIN.API_Repository;
using ADMIN.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace ADMIN.Controllers
{
    public class UserDetailsController : Controller
    {
        public ActionResult Index(string ID)
        {
            if (Session["logindetails"] == null)
            {
                Response.Redirect("/AdminLogin/Index");
            }

            return View();
        }


        public dynamic GetUserDetails(string ObjReg)
        {
            #region

            var ObjRegister = JsonConvert.DeserializeObject<Registration>(ObjReg);

            List<Registration> lstUserDetails = new List<Registration>();
            try
            {
                using (FinanceDBEntities finContext = new FinanceDBEntities())
                {
                    if (ObjRegister.BranchID == 0)
                    {
                        lstUserDetails = (from a in finContext.Hirers
                                          where a.IsActive == "true"
                                          && a.Status.ToLower() == "active"
                                          select new Registration
                                          {
                                              HirerId = a.HirerId,
                                              BranchName = finContext.Branches.FirstOrDefault(b => b.BranchId == a.BranchID && b.IsActive == "true").BrannchName,
                                              HirerName = a.HirerName,
                                              HirerMobileNumber = a.HirerMobileNumber,
                                              VehicleNo = a.VehicleDetails.FirstOrDefault(l => l.HirerId == a.HirerId && l.IsActive == "true").VehicleNo,
                                              TotalLoanAmount = a.LoanDetails.FirstOrDefault(l => l.HirerId == a.HirerId && l.IsActive == "true" && l.LoanStatus == "active").FinanceRs,
                                              TotalPendingAmount = a.LoanDetails.FirstOrDefault(l => l.HirerId == a.HirerId && l.IsActive == "true" && l.LoanStatus == "active").TotalPendingAmount,
                                              PaidStatus = a.LoanDetails.FirstOrDefault(l => l.HirerId == a.HirerId && l.IsActive == "true" && l.LoanStatus == "active").PaidStatus,
                                          }).ToList();
                    }
                    else
                    {
                        lstUserDetails = (from a in finContext.Hirers
                                          where a.IsActive == "true"
                                          && a.Status.ToLower() == "active"
                                          && a.BranchID == ObjRegister.BranchID
                                          select new Registration
                                          {
                                              HirerId = a.HirerId,
                                              BranchName = finContext.Branches.FirstOrDefault(b => b.BranchId == a.BranchID && b.IsActive == "true").BrannchName,
                                              HirerName = a.HirerName,
                                              HirerMobileNumber = a.HirerMobileNumber,
                                              VehicleNo = a.VehicleDetails.FirstOrDefault(l => l.HirerId == a.HirerId && l.IsActive == "true").VehicleNo,
                                              TotalLoanAmount = a.LoanDetails.FirstOrDefault(l => l.HirerId == a.HirerId && l.IsActive == "true" && l.LoanStatus == "active").FinanceRs,
                                              TotalPendingAmount = a.LoanDetails.FirstOrDefault(l => l.HirerId == a.HirerId && l.IsActive == "true" && l.LoanStatus == "active").TotalPendingAmount,
                                              PaidStatus = a.LoanDetails.FirstOrDefault(l => l.HirerId == a.HirerId && l.IsActive == "true" && l.LoanStatus == "active").PaidStatus,
                                          }).ToList();
                    }
                }

                return JsonConvert.SerializeObject(lstUserDetails);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            #endregion
        }

    }
}