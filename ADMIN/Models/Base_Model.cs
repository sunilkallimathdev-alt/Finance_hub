using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADMIN.Models
{
    public class Base_Model
    {
        public bool? IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string draw { get; set; }
        public int recordsTotal { get; set; }
        public dynamic data { get; set; }
        public string search { get; set; }
        public int? ID { get; set; }
        public bool IsLogin { get; set; }

        public dynamic length { get; set; }
        public dynamic start { get; set; }
        public dynamic sortColumn { get; set; }
        public dynamic sortColumnDir { get; set; }

    }
    public class DataTablesResponse
    {
        public dynamic data { get; set; }
        public dynamic draw { get; set; }
        public dynamic recordsFiltered { get; set; }
        public dynamic recordsTotal { get; set; }
    }
}