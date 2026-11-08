
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ADMIN.Controllers
{
    public class AdminLoginController : Controller
    {
        // GET: AdminLogin
        public ActionResult Index(string ID)
        {
            //if (Session["logindetails"] != null)
            Session.RemoveAll();

            return View();
        }

        public dynamic GetForgotPassword(string EmailID)
        {
            try
            {
                //API_Login ObjProduct = new API_Login();
                //return JsonConvert.SerializeObject(ObjProduct.GetForgotPassword(EmailID));
                return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        public dynamic AddLogin(string ObjAdmin)
        {
            try
            {
                User obj = JsonConvert.DeserializeObject<User>(ObjAdmin);                
                using (FinanceDBEntities finContext = new FinanceDBEntities())
                {
                    var res = (from a in finContext.Users
                               where (a.UserName == obj.UserName && a.Password == obj.Password)
                               && a.IsActive == "true"
                               select new
                               {
                                   Id = a.UserId,
                                   UserName = a.UserName
                               }
                               ).FirstOrDefault();

                    if (res != null)
                    {
                        Session.RemoveAll();
                        var abc = res;
                        var dc = abc;
                        Session["logindetails"] = dc;
                    }
                    return JsonConvert.SerializeObject(res);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult ForgotPassword()
        {

            Session.RemoveAll();
            return View();

        }
    }
}