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
    public class ChangePasswordController : Controller
    {
        public ActionResult Index(string ID)
        {
            if (Session["logindetails"] == null)
            {
                Response.Redirect("/AdminLogin/Index");
            }

            return View();
        }

    }
}