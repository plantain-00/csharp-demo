using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BPM.Data
{
    public class RolePermission
    {
        public RolePermission()
        {
        }

        public RolePermission(string roleId, Permission permission)
        {
            RoleID = roleId;
            Permission = permission;
        }

        [Key]
        [Column(Order = 0)]
        public string RoleID { get; set; }

        [Key]
        [Column(Order = 1)]
        public Permission Permission { get; set; }

        public virtual Role Role { get; set; }
    }
}