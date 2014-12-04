using System;

namespace BPM.Data
{
    public class WorkAssignment
    {
        public string ID { get; set; }
        public string SheetID { get; set; }
        public string ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string WorkContent { get; set; }
        public DateTime? ReportDate { get; set; }
        public string Remarks { get; set; }
        public DateTime? FinancialFinishDate { get; set; }
        public string FinancialSituation { get; set; }
        public string FinancialDelayReason { get; set; }
        public string FinancialUserID { get; set; }
        public DateTime? TestFinishDate { get; set; }
        public string TestSituation { get; set; }
        public string TestDelayReason { get; set; }
        public string TestUserID { get; set; }
        public string ApplierID { get; set; }
        public DateTime ApplyDate { get; set; }
        public DateTime? CheckDate { get; set; }
        public int Status { get; set; }

        public virtual User Applier { get; set; }
        public virtual User FinancialUser { get; set; }
        public virtual User TestUser { get; set; }
    }
}