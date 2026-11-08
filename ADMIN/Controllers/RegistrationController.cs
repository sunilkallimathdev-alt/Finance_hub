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
    public class RegistrationController : Controller
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


        public dynamic SaveRegistration(string ObjRegister)
        {
            #region
            try
            {
                Registration objReg = new Registration();
                objReg = JsonConvert.DeserializeObject<Registration>(ObjRegister);
                int iRes = 0;

                using (FinanceDBEntities finContext = new FinanceDBEntities())
                {
                    var Exist = finContext.Hirers.FirstOrDefault(x => x.HirerMobileNumber == objReg.HirerMobileNumber);
                    if (Exist != null)
                    {
                        iRes = -1;
                    }
                    else
                    {
                        Hirer objHirer = new Hirer();

                        objHirer.BranchID = objReg.BranchID;

                        objHirer.HirerName = objReg.HirerName;
                        objHirer.HirerFathername = objReg.HirerFathername;
                        objHirer.HirerAge = objReg.HirerAge;
                        objHirer.HirerOccupation = objReg.HirerOccupation;
                        objHirer.HirerMobileNumber = objReg.HirerMobileNumber;
                        objHirer.HirerAddress = objReg.HirerAddress;
                        objHirer.HirerGender = objReg.HirerGender;
                        objHirer.HirerAadharNumber = objReg.HirerAadharNumber;

                        objHirer.GuarantorName = objReg.GuarantorName;
                        objHirer.GuarantorFathername = objReg.GuarantorFathername;
                        objHirer.GuarantorAddress = objReg.GuarantorAddress;
                        objHirer.GuarantorGender = objReg.GuarantorGender;
                        objHirer.GuarantorAadharNumber = objReg.GuarantorAadharNumber;
                        objHirer.GuarantorMobileNumber = objReg.GuarantorMobileNumber;

                        objHirer.IsActive = "true";
                        objHirer.CreatedBy = "admin";
                        objHirer.CreatedDate = DateTime.UtcNow;
                        objHirer.Status = "active";

                        finContext.Hirers.Add(objHirer);
                        finContext.SaveChanges();
                        iRes = objHirer.HirerId;

                        if (iRes > 0)
                        {
                            // save to Loan table
                            LoanDetail objLoanDet = new LoanDetail();
                            objLoanDet.HirerId = iRes;
                            objLoanDet.LoanDate = objReg.LoanDate;
                            objLoanDet.FinanceRs = objReg.FinanceRs;
                            objLoanDet.Interest = objReg.Interest;
                            objLoanDet.Period = objReg.Period;
                            objLoanDet.Installment = objReg.Installment;
                            objLoanDet.RequiredLoanRs = objReg.RequiredLoanRs;
                            objLoanDet.LoanVisibility = objReg.LoanVisibility;
                            objLoanDet.CreatedBy = "admin";
                            objLoanDet.CreatedDate = DateTime.Now;
                            objLoanDet.IsActive = "true";
                            objLoanDet.LoanStatus = "active";
                            objLoanDet.PaidStatus = "Nodue";

                            finContext.LoanDetails.Add(objLoanDet);
                            finContext.SaveChanges();

                            // save to Vehicle table
                            VehicleDetail objVDet = new VehicleDetail();
                            objVDet.HirerId = iRes;
                            objVDet.EngineNo = objReg.EngineNo;
                            objVDet.ChassisNo = objReg.ChassisNo;
                            objVDet.Model = objReg.Model;
                            objVDet.KeyNo = objReg.KeyNo;
                            objVDet.VehicleNo = objReg.VehicleNo;
                            objVDet.VehicleOnRoadRs = objReg.VehicleOnRoadRs;
                            objVDet.VehicleName = objReg.VehicleName;
                            //objVDet.VehicleImage = objReg.VehicleImage;
                            objVDet.CreatedBy = "admin";
                            objVDet.CreatedDate = DateTime.Now;
                            objVDet.IsActive = "true";

                            finContext.VehicleDetails.Add(objVDet);
                            finContext.SaveChanges();

                            // save to Loan History table

                            // get Number of installments
                            int NOI = objReg.Period;                           

                            double Principal = Convert.ToDouble(objReg.RequiredLoanRs);
                            double interest = Convert.ToDouble(objReg.Interest);
                            int period = objReg.Period;
                            double EMIAmt = CalculateEMI(Principal, period, interest);
                            int EMIAmount = Convert.ToInt32(EMIAmt);

                            for (int i = 1; i <= NOI; i++)
                            {
                                LoanHistory objLoanHist = new LoanHistory();
                                objLoanHist.HirerID = iRes;
                                objLoanHist.EMIAmount = EMIAmount;
                                objLoanHist.PenaltyAmount = 0.0M;
                                objLoanHist.TotalAmount = EMIAmount;
                                objLoanHist.PaidAmount = 0.0M;
                                objLoanHist.PendingAmount = EMIAmount;
                                //objLoanHist.PaidDate = ;
                                objLoanHist.MonthYear = DateTime.Now.AddMonths(i - 1);
                                objLoanHist.CreatedBy = "admin";
                                objLoanHist.CreatedDate = DateTime.Now;
                                objLoanHist.IsActive = "true";
                                objLoanHist.IsEMIPaid = false;

                                finContext.LoanHistories.Add(objLoanHist);
                            }

                            finContext.SaveChanges();

                        }
                    }

                }
                return iRes;


            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
        }

        private double CalculateEMI(double LoanAmount, int PaymentPeriods, double InterestRate)
        {
            double EMIAmt = 0.00;
            try
            {
                //EMI = [(p * r / 12)(1 + r / 12) ^ n] /[(1 + r / 12) ^ n – 1]
                //        where p = principal amount(primary loan amount)
                //        r= rate of interest per year
                //        n= Total number of Years

                //double LoanAmount = Convert.ToDouble(loanAmt);
                ////double InterestRate = (Convert.ToDouble(interest) / 100) / 12;
                //double InterestRate = Convert.ToDouble(interest);
                //int PaymentPeriods = Convert.ToInt16(period);

                //double Part1 = (LoanAmount * InterestRate);
                //double numerator = (Math.Pow((1 + InterestRate), PaymentPeriods));
                //double denomitor = (numerator - 1);
                //double Part2 = numerator / denomitor;
                //EMIAmt = Part1 * Part2;

                double InterestAmount = (((LoanAmount * InterestRate) / 100) / 12) * PaymentPeriods;
                double TotalAmount = InterestAmount + LoanAmount;
                EMIAmt = TotalAmount / PaymentPeriods;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return EMIAmt;
        }
    }
}