using System;
using System.Linq;

using Models.demo.anotherSolution.DbModels;

namespace Models.demo.anotherSolution
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var entities = new Entities())
            {
                entities.Users.Add(new User
                                   {
                                       Id = 1,
                                       Name = "user",
                                       Age = 234,
                                       DepartmentId = 1
                                   });
                entities.Departments.Add(new Department
                                         {
                                             Id = 1,
                                             Name = "d"
                                         });
                entities.Roles.Add(new Role
                                   {
                                       Id = 1,
                                       Name = "r"
                                   });
                entities.UserRoles.Add(new UserRole
                                       {
                                           UserId = 1,
                                           RoleId = 1
                                       });
                entities.SaveChanges();

                var users = entities.Users.Join(entities.Departments.Where(d => d.Name == "d"),
                                                u => u.DepartmentId,
                                                d => d.Id,
                                                (u, d) => new Business1.User
                                                          {
                                                              Id = u.Id,
                                                              Name = u.Name,
                                                              Age = u.Age,
                                                              DepartmentId = u.DepartmentId,
                                                              Department = d
                                                          }).ToArray();
                var departments = entities.Departments.GroupJoin(entities.Users,
                                                                 d => d.Id,
                                                                 u => u.DepartmentId,
                                                                 (d, us) => new Business1.Department
                                                                            {
                                                                                Id = d.Id,
                                                                                Name = d.Name,
                                                                                FatherId = d.FatherId,
                                                                                Users = us
                                                                            }).ToArray();
                var usersWithRoles = entities.Users.GroupJoin(entities.UserRoles.Join(entities.Roles,
                                                                                      ur => ur.RoleId,
                                                                                      r => r.Id,
                                                                                      (ur, r) => new
                                                                                                 {
                                                                                                     ur.UserId,
                                                                                                     Roles = r
                                                                                                 }),
                                                              u => u.Id,
                                                              r => r.UserId,
                                                              (u, rs) => new Business2.User
                                                                         {
                                                                             Id = u.Id,
                                                                             Name = u.Name,
                                                                             Age = u.Age,
                                                                             Roles = rs.Select(r => r.Roles)
                                                                         }).ToArray();
                Console.Read();
            }
        }
    }
}