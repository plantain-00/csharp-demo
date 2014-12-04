using System;

using Newtonsoft.Json;

namespace BPM.Service.Models
{
    public class WorkAssignmentDTO
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("sheetId")]
        public string SheetID { get; set; }

        [JsonProperty("projectId")]
        public string ProjectID { get; set; }

        [JsonProperty("projectName")]
        public string ProjectName { get; set; }

        [JsonProperty("workContent")]
        public string WorkContent { get; set; }

        [JsonProperty("reportDate")]
        public DateTime? ReportDate { get; set; }

        [JsonProperty("remarks")]
        public string Remarks { get; set; }

        [JsonProperty("financialFinishDate")]
        public DateTime? FinancialFinishDate { get; set; }

        [JsonProperty("financialSituation")]
        public string FinancialSituation { get; set; }

        [JsonProperty("financialDelayReason")]
        public string FinancialDelayReason { get; set; }

        [JsonProperty("financialUserId")]
        public string FinancialUserID { get; set; }

        [JsonProperty("testFinishDate")]
        public DateTime? TestFinishDate { get; set; }

        [JsonProperty("testSituation")]
        public string TestSituation { get; set; }

        [JsonProperty("testDelayReason")]
        public string TestDelayReason { get; set; }

        [JsonProperty("testUserId")]
        public string TestUserID { get; set; }

        [JsonProperty("applierId")]
        public string ApplierID { get; set; }

        [JsonProperty("applyDate")]
        public DateTime ApplyDate { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("applierName")]
        public string ApplierName { get; set; }

        [JsonProperty("financialUserName")]
        public string FinancialUserName { get; set; }

        [JsonProperty("testUserName")]
        public string TestUserName { get; set; }

        public string GetStatus(string userId)
        {
            if (userId == FinancialUserID
                && !((WorkAssignmentStatus) Status).HasFlag(WorkAssignmentStatus.Financial))
            {
                return "财务部";
            }
            if (userId == TestUserID
                && !((WorkAssignmentStatus) Status).HasFlag(WorkAssignmentStatus.Test))
            {
                return "化验室";
            }
            if (userId == ApplierID
                && !((WorkAssignmentStatus) Status).HasFlag(WorkAssignmentStatus.Check))
            {
                return "查看完成情况";
            }
            return null;
        }
    }
}