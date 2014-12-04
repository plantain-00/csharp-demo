using System.Collections.Generic;
using System.Linq;

using BPM.Data;

namespace BPM.Service
{
    public static class UserExtension
    {
        public static bool Can(this User user, Permission permission)
        {
            return user.Roles.Any(r => r.RolePermissions.Any(p => p.Permission == permission)) || (user.Department != null && user.Department.Roles.Any(r => r.RolePermissions.Any(p => p.Permission == permission)));
        }

        public static List<Permission> GetPermissions(this User user)
        {
            var permissions = new List<Permission>();
            foreach (var role in user.Roles)
            {
                permissions.AddRange(role.RolePermissions.Select(rp => rp.Permission));
            }
            if (user.Department != null)
            {
                foreach (var role in user.Department.Roles)
                {
                    permissions.AddRange(role.RolePermissions.Select(rp => rp.Permission));
                }
            }
            return permissions.Distinct().ToList();
        }

        public static List<string> GetPermissionClasses(this User user)
        {
            var permissions = new List<Permission>();
            foreach (var role in user.Roles)
            {
                permissions.AddRange(role.RolePermissions.Select(rp => rp.Permission));
            }
            if (user.Department != null)
            {
                foreach (var role in user.Department.Roles)
                {
                    permissions.AddRange(role.RolePermissions.Select(rp => rp.Permission));
                }
            }
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

        public static bool HasAny(this string userName, Permission[] permission)
        {
            using (var entities = new Entities())
            {
                var user = entities.Users.FirstOrDefault(u => u.Name == userName);
                return !permission.Any(user.Can);
            }
        }
    }
}