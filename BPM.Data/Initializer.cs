using System;
using System.Data.Entity;

namespace BPM.Data
{
    public class Initializer : DropCreateDatabaseAlways<Entities>
    {
        protected override void Seed(Entities context)
        {
            var user = new User
                       {
                           ID = Guid.NewGuid().ToString(),
                           Name = "admin",
                           Password = "019AFF2B745A435C72D00E476AF4EA9A",
                           RealName = "超级管理员",
                           Salt = "B3C20164-A56F-4994-8783-DB946F56FFA1"
                       };
            context.Users.Add(user);

            var role = new Role
                       {
                           ID = Guid.NewGuid().ToString(),
                           Name = "管理员角色"
                       };
            context.Roles.Add(role);
            role.Users.Add(user);

            context.RolePermissions.Add(new RolePermission(role.ID, Permission.DepartmentManagement));
            context.RolePermissions.Add(new RolePermission(role.ID, Permission.Login));
            context.RolePermissions.Add(new RolePermission(role.ID, Permission.ModifyPassword));
            context.RolePermissions.Add(new RolePermission(role.ID, Permission.RoleManagement));
            context.RolePermissions.Add(new RolePermission(role.ID, Permission.UserManagement));
            context.RolePermissions.Add(new RolePermission(role.ID, Permission.AllUsers));
            context.RolePermissions.Add(new RolePermission(role.ID, Permission.AllPermissions));

            var financial = new Department
                            {
                                Name = "财务部",
                                ID = Guid.NewGuid().ToString(),
                            };
            context.Departments.Add(financial);

            var test = new Department
                       {
                           Name = "化验室",
                           ID = Guid.NewGuid().ToString()
                       };
            context.Departments.Add(test);

            var user1 = new User
                        {
                            ID = Guid.NewGuid().ToString(),
                            Name = "user1",
                            Password = "019AFF2B745A435C72D00E476AF4EA9A",
                            RealName = "测试1",
                            Salt = "B3C20164-A56F-4994-8783-DB946F56FFA1"
                        };
            var user2 = new User
                        {
                            ID = Guid.NewGuid().ToString(),
                            Name = "user2",
                            Password = "019AFF2B745A435C72D00E476AF4EA9A",
                            RealName = "测试2",
                            Salt = "B3C20164-A56F-4994-8783-DB946F56FFA1"
                        };
            financial.Users.Add(user1);
            test.Users.Add(user2);

            var assignerRole = new Role
                               {
                                   ID = Guid.NewGuid().ToString(),
                                   Name = "派发人"
                               };
            context.Roles.Add(assignerRole);
            var assigner = new User
                           {
                               ID = Guid.NewGuid().ToString(),
                               Name = "assigner",
                               Password = "019AFF2B745A435C72D00E476AF4EA9A",
                               RealName = "派发人",
                               Salt = "B3C20164-A56F-4994-8783-DB946F56FFA1"
                           };
            assignerRole.Users.Add(assigner);
            context.RolePermissions.Add(new RolePermission(assignerRole.ID, Permission.WorkDistribution));
            context.RolePermissions.Add(new RolePermission(assignerRole.ID, Permission.Login));

            base.Seed(context);
        }
    }
}