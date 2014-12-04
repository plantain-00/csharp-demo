using System.Collections.Generic;

using BPM.Data;

namespace BPM.Service
{
    public static class Protocols
    {
        public const string NO_PERMISSION_CLASS = "no permisson class";
        public const string USER_ADMIN_NAME = "admin";
        public const string ROLE_ADMIN_NAME = "管理员角色";
        public const string SYSTEM_SETTINGS = "系统设置";
        public const string MY_DESKTOP = "我的工作台";
        public const string LOGIN_CLASS = "登入";
        public const string USER_MANAGEMENT = "用户管理";
        public const string DEPARTMENT_MANAGEMENT = "部门管理";
        public const string ROLE_MANAGEMENT = "角色管理";
        public const string PERSONAL_SETTINGS = "个人设置";
        public const string MODIFY_PASSWORD = "修改密码";
        public const string NEW_APPLICATION = "新增申请";
        public const string MY_TO_DO = "待办事项";
        public const string TO_BE_READ = "待阅事项";
        public const string HISTORY = "历史";
        public const string ALL_PERMISSIONS = "权限一览";
        public const string ALL_USERS_AND_DEPARTMENTS = "部门和用户一览";
        public const string LOGIN = "登入";
        public const string PROCESS = "流程";
        public const string WORK_ASSIGNMENT = "工作安排";
        public const char ID_SPLITTER = '|';
        public static Dictionary<Permission, string> DisplayedPermissions;
        public static Dictionary<string, List<Permission>> AllPermissions;

        static Protocols()
        {
            AllPermissions = new Dictionary<string, List<Permission>>
                             {
                                 {
                                     SYSTEM_SETTINGS, new List<Permission>
                                                      {
                                                          Permission.DepartmentManagement,
                                                          Permission.UserManagement,
                                                          Permission.RoleManagement,
                                                          Permission.AllPermissions,
                                                          Permission.AllUsers
                                                      }
                                 },
                                 {
                                     MY_DESKTOP, new List<Permission>
                                                 {
                                                     Permission.NewApplication,
                                                     Permission.ToDoList,
                                                     Permission.ToReadList,
                                                     Permission.History
                                                 }
                                 },
                                 {
                                     PERSONAL_SETTINGS, new List<Permission>
                                                        {
                                                            Permission.ModifyPassword
                                                        }
                                 },
                                 {
                                     LOGIN, new List<Permission>
                                            {
                                                Permission.Login
                                            }
                                 },
                                 {
                                     PROCESS, new List<Permission>
                                              {
                                                  Permission.WorkDistribution
                                              }
                                 }
                             };
            DisplayedPermissions = new Dictionary<Permission, string>
                                   {
                                       {
                                           Permission.DepartmentManagement, DEPARTMENT_MANAGEMENT
                                       },
                                       {
                                           Permission.UserManagement, USER_MANAGEMENT
                                       },
                                       {
                                           Permission.RoleManagement, ROLE_MANAGEMENT
                                       },
                                       {
                                           Permission.AllPermissions, ALL_PERMISSIONS
                                       },
                                       {
                                           Permission.AllUsers, ALL_USERS_AND_DEPARTMENTS
                                       },
                                       {
                                           Permission.NewApplication, NEW_APPLICATION
                                       },
                                       {
                                           Permission.ToDoList, MY_TO_DO
                                       },
                                       {
                                           Permission.ToReadList, TO_BE_READ
                                       },
                                       {
                                           Permission.History, HISTORY
                                       },
                                       {
                                           Permission.ModifyPassword, MODIFY_PASSWORD
                                       },
                                       {
                                           Permission.Login, LOGIN
                                       },
                                       {
                                           Permission.WorkDistribution, WORK_ASSIGNMENT
                                       }
                                   };
        }

        public static bool IsBuildInPermissionClass(string name)
        {
            return name == SYSTEM_SETTINGS || name == MY_DESKTOP || name == LOGIN_CLASS || name == PERSONAL_SETTINGS || name == PROCESS;
        }

        public static bool IsBuildInPermission(string name)
        {
            return name == USER_MANAGEMENT || name == DEPARTMENT_MANAGEMENT || name == ROLE_MANAGEMENT || name == MODIFY_PASSWORD || name == NEW_APPLICATION || name == MY_TO_DO || name == TO_BE_READ || name == HISTORY || name == ALL_PERMISSIONS || name == ALL_USERS_AND_DEPARTMENTS || name == LOGIN || name == WORK_ASSIGNMENT;
        }

        public static bool IsBuildInRole(string name)
        {
            return name == ROLE_ADMIN_NAME;
        }

        public static bool IsBuildInDepartment(string name)
        {
            return false;
        }

        public static bool IsBuildInUser(string name)
        {
            return name == USER_ADMIN_NAME;
        }

        public static class Cookie
        {
            public const string USER_ID = "UserID";
            public const string NAME = "Name";
            public const string REALNAME = "RealName";
        }

        public static class ReadType
        {
            public static string Unread = "未读";
            public static string Read = "已读";
        }
    }
}