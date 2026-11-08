using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADMIN.Models
{
    public class Dashboard : Base_Model
    {
        public int TotalCustomers { get; set; }
        public int Nodue { get; set; }
        public int Due { get; set; }
        public int Defaulter { get; set; }
    }
}