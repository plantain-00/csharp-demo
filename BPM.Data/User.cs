using System.Collections.Generic;

namespace BPM.Data
{
    public class User
    {
        public User()
        {
            Roles = new HashSet<Role>();
            WorkAssignmentsAsApplier = new HashSet<WorkAssignment>();
            WorkAssignmentsAsFinancialUser = new HashSet<WorkAssignment>();
            WorkAssignmentsAsTestUser = new HashSet<WorkAssignment>();
            ReadsAsReader = new HashSet<Read>();
            ReadsAsSender = new HashSet<Read>();
            Drafts = new HashSet<Draft>();
        }

        public string ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string DepartmentId { get; set; }
        public string RealName { get; set; }
        public string Salt { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<WorkAssignment> WorkAssignmentsAsApplier { get; set; }
        public virtual ICollection<WorkAssignment> WorkAssignmentsAsFinancialUser { get; set; }
        public virtual ICollection<WorkAssignment> WorkAssignmentsAsTestUser { get; set; }
        public virtual ICollection<Read> ReadsAsReader { get; set; }
        public virtual ICollection<Read> ReadsAsSender { get; set; }
        public virtual ICollection<Draft> Drafts { get; set; }
    }
}