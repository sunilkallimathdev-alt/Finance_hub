using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
//using System.Web;

namespace ADMIN.API_Repository
{
    public class Static_Data
    {
        //static string _name = "http://localhost:62277/api/";
        //static string _name = "http://homeappliancesapi.myprelaunch.in/api/";
        static string _name = System.Configuration.ConfigurationManager.AppSettings["BaseURL"];
        public static string BaseURL { get { return _name; } }

        static string _server = @"C:\Inetpub\vhosts\myprelaunch.in\";
        public static string ServerDirectory { get { return _server; } }
    }


   
}
