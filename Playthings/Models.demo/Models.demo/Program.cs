using System;
using System.Data.Entity;
using System.Linq;

using Models.demo.BusinessBase;
using Models.demo.DbModels;

namespace Models.demo
{
    internal class Program
    {
        private static void Main()
        {
            // initialize: 
            // department1
            //   user1
            //   department2
            //     user2
            using (var entities = new Entities())
            {
                var user1 = new User
                            {
                                Name = "user1",
                                Age = 1
                            };
                entities.Users.Add(user1);
                var user2 = new User
                            {
                                Name = "user2",
                                Age = 2
                            };
                entities.Users.Add(user2);
                var department1 = new Department
                                  {
                                      Name = "department1"
                                  };
                entities.Departments.Add(department1);
                var department2 = new Department
                                  {
                                      Name = "department2"
                                  };
                entities.Departments.Add(department2);
                user1.Department = department1;
                user2.Department = department2;
                department2.Father = department1;
                entities.SaveChanges();
            }
            // DbModel:使用Entity Framework的Model
            // 默认只有非virtual的成员才有实际值
            // 如果需要带virtual的成员也有实际值，使用Include
            // 不要使用此Model，因为如果Include所有导航属性，则性能太低；反之，太容易用错。

            // Business Base:不包括任何导航属性的Model，是其它Business Model的基类
            using (var entities = new Entities())
            {
                var departments = entities.Departments.AsEnumerable().Select(d => new DepartmentBase(d)).ToArray();
                var users = entities.Users.AsEnumerable().Select(u => new UserBase(u)).ToArray();
            }

            // 基于具体业务逻辑的Model 1:
            using (var entities = new Entities())
            {
                var departments = entities.Departments.Include(d => d.Users).Include(d => d.Departments).AsEnumerable().Select(d => new Business1.Department(d)).ToArray();
            }

            // 基于具体业务逻辑的Model 2:
            using (var entities = new Entities())
            {
                var users = entities.Users.Include(u => u.Department).Include(u => u.Department.Father).AsEnumerable().Select(u => new Business2.User(u)).ToArray();
            }
            Console.Read();
        }
    }
}