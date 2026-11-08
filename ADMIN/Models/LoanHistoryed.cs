using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADMIN.Models
{
    public class LoanHistoryed : Base_Model
    {
        public int Id { get; set; }
        public int HirerID { get; set; }
        public int Amount { get; set; }       
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }      
        public Nullable<decimal> EMIAmount { get; set; }
        public Nullable<decimal> PenaltyAmount { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public Nullable<decimal> PaidAmount { get; set; }
        public Nullable<decimal> PendingAmount { get; set; }
        public Nullable<System.DateTime> PaidDate { get; set; }
        public Nullable<System.DateTime> MonthYear { get; set; }
        public string ReceivedBy { get; set; }
        public string Month_Year { get; set; }
    }
}