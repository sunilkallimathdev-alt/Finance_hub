using ADMIN.API_Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ADMIN.Models;


namespace ADMIN.Controllers
{
    public class DashBoardController : Controller
    {


        public ActionResult Welcome()
        {
            // if (Session["Builderlogindetails"] != null || Session["Ownerlogindetails"] != null || Session["Vendorlogindetails"] != null)
            // Response.Redirect("/AdminLogin/Index");
            return View();
        }

        [HttpPost]
        public dynamic GetDashoboardData()
        {
            #region
            Dashboard objDashboard = new Dashboard();
            try
            {
                using (FinanceDBEntities finContext = new FinanceDBEntities())
                {

                    objDashboard.TotalCustomers = (from a in finContext.Hirers
                                                   where a.IsActive == "true"
                                                   && a.Status == "active"
                                                   select a
                                                   ).Count();

                    objDashboard.Nodue = (from a in finContext.LoanDetails
                                          where a.IsActive == "true"
                                          && a.LoanStatus == "active"
                                          && a.PaidStatus == "Nodue"
                                          select a
                                                  ).Count();

                    objDashboard.Due = (from a in finContext.LoanDetails
                                        where a.IsActive == "true"
                                        && a.LoanStatus == "active"
                                        && a.PaidStatus == "due"
                                        select a
                                                  ).Count();

                    objDashboard.Defaulter = (from a in finContext.LoanDetails
                                              where a.IsActive == "true"
                                              && a.LoanStatus == "active"
                                              && a.PaidStatus == "defaulter"
                                              select a
                                                  ).Count();

                }

                return JsonConvert.SerializeObject(objDashboard);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            #endregion
        }

        //public ActionResult pdf()
        //{

        //    return View();
        //}

        //public ActionResult WelcomeCustomerRep()
        //{

        //    //if (Session["CustomerReplogindetails"] == null)
        //    //    Response.Redirect("/CustomerRepLogin/Index");
        //    //else
        //    //{
        //    //    var UserDetails = Session["CustomerReplogindetails"];
        //    //    var user = (JObject)JsonConvert.DeserializeObject(UserDetails.ToString());

        //    //    ViewBag.CustRepName = user.SelectToken("Name");

        //    //}

        //    return View();
        //}

        //public ActionResult WelcomePreOwner()
        //{

        //    //if (Session["PreOwnerlogindetails"] == null)
        //    //    Response.Redirect("/PreOwnerLogin/Index");
        //    //else
        //    //{
        //    //    var UserDetails = Session["PreOwnerlogindetails"];
        //    //    var user = (JObject)JsonConvert.DeserializeObject(UserDetails.ToString());

        //    //    ViewBag.PreOwnerName = user.SelectToken("Owner_Name");

        //    //}

        //    return View();
        //}




        //public ActionResult WelcomeBuilder()
        //{
        //    //if (Session["Builderlogindetails"] == null)
        //    //    Response.Redirect("/BuilderLogin/Index");
        //    //else
        //    //{
        //    //    var UserDetails = Session["Builderlogindetails"];
        //    //    var user = (JObject)JsonConvert.DeserializeObject(UserDetails.ToString());

        //    //    ViewBag.builderName = user.SelectToken("Builder_Name");

        //    //}
        //    return View();
        //}
    }
}