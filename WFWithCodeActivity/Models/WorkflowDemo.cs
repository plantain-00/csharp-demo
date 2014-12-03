using System;

namespace WFWithCodeActivity.Models
{
    public class WorkflowDemo
    {
        public Guid ID { get; set; }
        public int Money { get; set; }
        public string Reason { get; set; }
        public string Submiter { get; set; }
        public DateTime SubmitDate { get; set; }
        public string FirstAuditer { get; set; }
        public string FirstAuditerComments { get; set; }
        public DateTime? FirstAuditerDate { get; set; }
        public string SecondAuditer { get; set; }
        public string SeconfAuditerComments { get; set; }
        public DateTime? SecondAuditerDate { get; set; }
        public string Status { get; set; }
    }
}