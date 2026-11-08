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
    public class LoanHistoryController : Controller
    {
        // GET: AdminLogin
        public ActionResult Index(string ID)
        {
            if (Session["logindetails"] == null)
            {
                Response.Redirect("/AdminLogin/Index");
            }

            return View();
        }

        // POST: LoanHistory/Create
        [HttpPost]
        public dynamic SavePayment(string objPmt)
        {
            int res = 0;
            try
            {
                var objPayment = JsonConvert.DeserializeObject<PaymentModel>(objPmt);

                using (FinanceDBEntities finContext = new FinanceDBEntities())
                {
                    Payment objSavePayment = new Payment();

                    objSavePayment.HirerID = objPayment.HirerID;
                    objSavePayment.Amount = objPayment.Amount;
                    objSavePayment.LoanID = objPayment.LoanID;
                    objSavePayment.ReceivedBy = objPayment.ReceivedBy;
                    objSavePayment.IsActive = true;
                    objSavePayment.CreatedBy = "admin";
                    objSavePayment.CreatedDate = DateTime.UtcNow;

                    finContext.Payments.Add(objSavePayment);
                    finContext.SaveChanges();
                    res = objSavePayment.PaymentID;

                    if (res > 0)
                    {
                        // update Loan History Table

                        if (objPayment.EMIAmount == objPayment.Amount)
                        {
                            var ObjLoanHist = (from p in finContext.LoanHistories.Where(l => l.Id == objPayment.LoanID && l.HirerID == objPayment.HirerID && l.IsEMIPaid == false)
                                               select p).OrderBy(o => o.MonthYear).FirstOrDefault();
                            if (ObjLoanHist != null)
                            {
                                ObjLoanHist.PaidAmount = objPayment.Amount;
                                ObjLoanHist.PendingAmount = 0.0M;
                                ObjLoanHist.PaidDate = DateTime.UtcNow;
                                ObjLoanHist.ReceivedBy = objPayment.ReceivedBy;
                                ObjLoanHist.IsEMIPaid = true;
                                ObjLoanHist.ModifiedBy = objPayment.ReceivedBy;
                                //ObjLoanHist.ModifiedDate = DateTime.UtcNow;
                                finContext.SaveChanges();
                            }
                        }
                        else if (objPayment.EMIAmount > objPayment.Amount)
                        {
                            // pay the amount for the month and update the pending amount
                            var ObjLoanHist = (from p in finContext.LoanHistories.Where(l => l.Id == objPayment.LoanID && l.HirerID == objPayment.HirerID && l.IsEMIPaid == false)
                                               select p).OrderBy(o => o.MonthYear).FirstOrDefault();
                            if (ObjLoanHist != null)
                            {
                                ObjLoanHist.PaidAmount = objPayment.Amount;
                                ObjLoanHist.PendingAmount = (objPayment.EMIAmount - objPayment.Amount);
                                ObjLoanHist.PaidDate = DateTime.UtcNow;
                                ObjLoanHist.ReceivedBy = objPayment.ReceivedBy;
                                ObjLoanHist.IsEMIPaid = true;
                                ObjLoanHist.ModifiedBy = objPayment.ReceivedBy;
                                //ObjLoanHist.ModifiedDate = DateTime.UtcNow;
                                finContext.SaveChanges();
                            }
                        }
                        else
                        {
                            var loanHist = from p in finContext.LoanHistories.Where(l => l.Id == objPayment.LoanID && l.HirerID == objPayment.HirerID && l.IsEMIPaid == false)
                                           select p;

                            foreach (var lh in loanHist)
                            {

                            }
                        }


                    }
                }

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public dynamic GetLoanHistory(string ObjLoanHistory)
        {
            #region

            var objLoanHist = JsonConvert.DeserializeObject<LoanHistoryed>(ObjLoanHistory);

            List<LoanHistoryed> lstLoanHistryDetails = new List<LoanHistoryed>();
            try
            {
                using (FinanceDBEntities finContext = new FinanceDBEntities())
                {
                    if (objLoanHist.HirerID != 0)
                    {
                        var res = (from a in finContext.LoanHistories
                                   where a.IsActive == "true"
                                   && a.HirerID == objLoanHist.HirerID

                                   select new LoanHistoryed
                                   {
                                       HirerID = a.HirerID,
                                       Amount = a.Amount,
                                       EMIAmount = a.EMIAmount,
                                       PenaltyAmount = a.PenaltyAmount,
                                       TotalAmount = a.TotalAmount,
                                       PaidAmount = a.PaidAmount,
                                       PendingAmount = a.PendingAmount,
                                       PaidDate = a.PaidDate,
                                       MonthYear = a.MonthYear,
                                       ReceivedBy = a.ReceivedBy,
                                   }).ToList();


                        lstLoanHistryDetails = res.ToList().Select(q => new LoanHistoryed
                        {
                            HirerID = q.HirerID,
                            Amount = q.Amount,
                            EMIAmount = q.EMIAmount,
                            PenaltyAmount = q.PenaltyAmount,
                            TotalAmount = q.TotalAmount,
                            PaidAmount = q.PaidAmount,
                            PendingAmount = q.PendingAmount,
                            PaidDate = q.PaidDate,
                            Month_Year = q.MonthYear.Value.ToString("MMMM yyyy"),
                            ReceivedBy = q.ReceivedBy,
                        }).ToList();
                    }
                    else
                    {
                        return null;
                    }
                }

                return JsonConvert.SerializeObject(lstLoanHistryDetails);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            #endregion
        }


        [HttpPost]
        public dynamic GetHirerDetails(string objHirer)
        {
            #region

            var objSearchContent = JsonConvert.DeserializeObject<Registration>(objHirer);

            Registration objReg = new Registration();
            try
            {
                using (FinanceDBEntities finContext = new FinanceDBEntities())
                {

                    if (!string.IsNullOrEmpty(objSearchContent.SearchKey))
                    {
                        string strSearch = objSearchContent.SearchKey;
                        long no = 0;
                        if (long.TryParse(strSearch, out no))
                        {
                            // If it is number
                            objReg = (from p in finContext.Hirers
                                      where p.HirerMobileNumber.Contains(strSearch) || p.HirerAadharNumber.Contains(strSearch)
                                      select new Registration
                                      {
                                          HirerId = p.HirerId,
                                          HirerName = p.HirerName,
                                          HirerMobileNumber = p.HirerMobileNumber,
                                          HirerPhoto = p.HirerPhoto,
                                          VehicleName = p.VehicleDetails.FirstOrDefault(l => l.HirerId == p.HirerId && l.IsActive == "true").VehicleName,
                                          VehicleNo = p.VehicleDetails.FirstOrDefault(l => l.HirerId == p.HirerId && l.IsActive == "true").VehicleNo,
                                          TotalLoanAmount = p.LoanDetails.FirstOrDefault(l => l.HirerId == p.HirerId && l.IsActive == "true" && l.LoanStatus == "active").FinanceRs,
                                          TotalPendingAmount = p.LoanDetails.FirstOrDefault(l => l.HirerId == p.HirerId && l.IsActive == "true" && l.LoanStatus == "active").TotalPendingAmount,
                                          PaidStatus = p.LoanDetails.FirstOrDefault(l => l.HirerId == p.HirerId && l.IsActive == "true" && l.LoanStatus == "active").PaidStatus,
                                          EMIAmount = p.LoanDetails.FirstOrDefault(l => l.HirerId == p.HirerId && l.IsActive == "true" && l.LoanStatus == "active").Installment,
                                      }).FirstOrDefault();
                        }
                        else
                        {
                            // if it is not a number ( Vehicle number )
                            objReg = (from p in finContext.VehicleDetails
                                      where p.VehicleNo.Contains(strSearch)
                                      select new Registration
                                      {
                                          HirerId = p.HirerId,
                                          HirerName = p.Hirer.HirerName,
                                          HirerMobileNumber = p.Hirer.HirerMobileNumber,
                                          HirerPhoto = p.Hirer.HirerPhoto,
                                          VehicleName = p.VehicleName,
                                          VehicleNo = p.VehicleNo,
                                          TotalLoanAmount = finContext.LoanDetails.FirstOrDefault(l => l.HirerId == p.HirerId && l.IsActive == "true" && l.LoanStatus == "active").FinanceRs,
                                          TotalPendingAmount = finContext.LoanDetails.FirstOrDefault(l => l.HirerId == p.HirerId && l.IsActive == "true" && l.LoanStatus == "active").TotalPendingAmount,
                                          PaidStatus = finContext.LoanDetails.FirstOrDefault(l => l.HirerId == p.HirerId && l.IsActive == "true" && l.LoanStatus == "active").PaidStatus,
                                          EMIAmount = finContext.LoanDetails.FirstOrDefault(l => l.HirerId == p.HirerId && l.IsActive == "true" && l.LoanStatus == "active").Installment,

                                      }).FirstOrDefault();
                        }
                    }
                    else
                    {
                        return null;
                    }
                }

                return JsonConvert.SerializeObject(objReg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            #endregion
        }


        [HttpPost]
        public dynamic GetHirerDetailsByHirerID(string objHirer)
        {
            #region

            var objContent = JsonConvert.DeserializeObject<Registration>(objHirer);

            Registration objReg = new Registration();
            try
            {
                using (FinanceDBEntities finContext = new FinanceDBEntities())
                {
                    objReg = (from p in finContext.Hirers
                              where p.HirerId == objContent.HirerId
                              select new Registration
                              {
                                  HirerId = p.HirerId,
                                  HirerName = p.HirerName,
                                  HirerMobileNumber = p.HirerMobileNumber,
                                  HirerPhoto = p.HirerPhoto,
                                  VehicleName = p.VehicleDetails.FirstOrDefault(l => l.HirerId == p.HirerId && l.IsActive == "true").VehicleName,
                                  VehicleNo = p.VehicleDetails.FirstOrDefault(l => l.HirerId == p.HirerId && l.IsActive == "true").VehicleNo,
                                  TotalLoanAmount = p.LoanDetails.FirstOrDefault(l => l.HirerId == p.HirerId && l.IsActive == "true" && l.LoanStatus == "active").FinanceRs,
                                  TotalPendingAmount = p.LoanDetails.FirstOrDefault(l => l.HirerId == p.HirerId && l.IsActive == "true" && l.LoanStatus == "active").TotalPendingAmount,
                                  PaidStatus = p.LoanDetails.FirstOrDefault(l => l.HirerId == p.HirerId && l.IsActive == "true" && l.LoanStatus == "active").PaidStatus,
                                  EMIAmount = p.LoanDetails.FirstOrDefault(l => l.HirerId == p.HirerId && l.IsActive == "true" && l.LoanStatus == "active").Installment,
                              }).FirstOrDefault();

                }

                return JsonConvert.SerializeObject(objReg);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
        }




    }
}