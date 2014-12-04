using System.Collections.Generic;
using System.Linq;

using BPM.Data;

namespace BPM.Service
{
    public static class RoleExtension
    {
        public static List<string> GetPermissionClasses(this Role role)
        {
            var permissions = new List<Permission>();
            permissions.AddRange(role.RolePermissions.Select(rp => rp.Permission));
            var permissionClasses = new List<string>();
            foreach (var pair in Protocols.AllPermissions)
            {
                if (pair.Value.Any(permissions.Contains))
                {
                    permissionClasses.Add(pair.Key);
                }
            }
            return permissionClasses;
        }
    }
}