using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADMIN.Models
{
    public class Registration : Base_Model
    {
        public Nullable<int> BranchID { get; set; }
        public string BranchName { get; set; }
        public int HirerId { get; set; }
        public string HirerName { get; set; }
        public string HirerFathername { get; set; }
        public Nullable<int> HirerAge { get; set; }
        public string HirerOccupation { get; set; }
        public string HirerMobileNumber { get; set; }
        public string HirerAddress { get; set; }
        public string HirerPhoto { get; set; }
        public string HirerSignature { get; set; }
        public string HirerGender { get; set; }
        public string HirerAadharNumber { get; set; }
        public string Status { get; set; }

        public string GuarantorName { get; set; }
        public string GuarantorFathername { get; set; }
        public Nullable<int> GuarantorAge { get; set; }
        public string GuarantorOccupation { get; set; }
        public string GuarantorMobileNumber { get; set; }
        public string GuarantorAddress { get; set; }
        public string GuarantorPhoto { get; set; }
        public string GuarantorSignature { get; set; }
        public string GuarantorGender { get; set; }
        public string GuarantorAadharNumber { get; set; }

        public int VehicleId { get; set; }
        public string EngineNo { get; set; }
        public string ChassisNo { get; set; }
        public string Model { get; set; }
        public string KeyNo { get; set; }
        public string VehicleNo { get; set; }
        public Nullable<decimal> VehicleOnRoadRs { get; set; }
        public string VehicleName { get; set; }
        public string VehicleImage { get; set; }


        public int LoanId { get; set; }
        public System.DateTime LoanDate { get; set; }
        public decimal FinanceRs { get; set; }
        public decimal Interest { get; set; }
        public int Period { get; set; }
        public int Installment { get; set; }
        public decimal RequiredLoanRs { get; set; }
        public Nullable<bool> LoanVisibility { get; set; }
        public string LoanStatus { get; set; }

        public decimal? TotalLoanAmount { get; set; }
        public decimal? TotalPendingAmount { get; set; }
        public string PaidStatus { get; set; }

        public string SearchKey { get; set; }

        public decimal EMIAmount { get; set; }



    }
}