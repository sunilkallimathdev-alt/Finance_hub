using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADMIN.Models
{
    public class PaymentModel : Base_Model
    {
        public int PaymentID { get; set; }
        public Nullable<int> HirerID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<int> LoanID { get; set; }
        public string ReceivedBy { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifirdBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> IsActive { get; set; }

        public Nullable<decimal> EMIAmount { get; set; }
    }
}