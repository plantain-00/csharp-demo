using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;

using BCL.Extension;

using Bootstrap.Pagination;

using BPM.Data;
using BPM.Service.Models;

using Newtonsoft.Json.Linq;

namespace BPM.Service
{
    public class HomeService : IHomeService
    {
        public Tuple<List<Permission>, List<string>> Index(string name)
        {
            using (var entities = new Entities())
            {
                var user = entities.Users.FirstOrDefault(u => u.Name == name);
                var permissions = user.GetPermissions();
                var permissionClasses = user.GetPermissionClasses();
                return Tuple.Create(permissions, permissionClasses);
            }
        }

        public List<Permission> NewApplication(string name)
        {
            using (var entities = new Entities())
            {
                var user = entities.Users.FirstOrDefault(u => u.Name == name);
                return user.GetPermissions();
            }
        }

        public Tuple<Pagination, List<WorkAssignmentDTO>> ToDoList(string userId, int page, int group)
        {
            using (var entities = new Entities())
            {
                Expression<Func<WorkAssignment, bool>> workAssignmentCondition = w => (w.FinancialUserID == userId && (w.Status & (int) WorkAssignmentStatus.Financial) == 0) || (w.TestUserID == userId && (w.Status & (int) WorkAssignmentStatus.Test) == 0) || (w.ApplierID == userId && (w.Status & (int) WorkAssignmentStatus.Check) == 0);
                var totalCount = entities.WorkAssignments.Count(workAssignmentCondition);
                var pagination = new Pagination(totalCount, page, group, 5, 10);
                var workAssignments = entities.WorkAssignments.Where(workAssignmentCondition).OrderBy(p => p.ApplyDate).Skip(pagination.ItemIndex).Take(10).MapToDTO();
                return Tuple.Create(pagination, workAssignments);
            }
        }

        public Tuple<Pagination, List<WorkAssignmentDTO>> History(string userId, int page, int group)
        {
            using (var entities = new Entities())
            {
                Expression<Func<WorkAssignment, bool>> workAssignmentCondition = w => (w.FinancialUserID == userId && (w.Status & (int) WorkAssignmentStatus.Financial) != 0) || (w.TestUserID == userId && (w.Status & (int) WorkAssignmentStatus.Test) != 0) || (w.ApplierID == userId && (w.Status & (int) WorkAssignmentStatus.Check) != 0);
                var totalCount = entities.WorkAssignments.Count(workAssignmentCondition);
                var pagination = new Pagination(totalCount, page, group, 5, 10);
                var workAssignments = entities.WorkAssignments.Where(workAssignmentCondition).OrderBy(p => p.ApplyDate).Skip(pagination.ItemIndex).Take(10).MapToDTO();
                return Tuple.Create(pagination, workAssignments);
            }
        }

        public JsonResultModel Save(string userId, NameValueCollection form)
        {
            JsonResultModel json;
            try
            {
                using (var entities = new Entities())
                {
                    var user = entities.Users.FirstOrDefault(u => u.ID == userId);
                    for (var i = 0; i < form.Count; i++)
                    {
                        var name = form.GetKey(i);
                        var content = form[i];
                        if (user.Drafts.Any(d => d.Name == name))
                        {
                            user.Drafts.First(d => d.Name == name).Content = content;
                        }
                        else
                        {
                            var id = Guid.NewGuid().ToString();
                            entities.Drafts.Add(new Draft
                                                {
                                                    ID = id,
                                                    Name = name,
                                                    Content = content,
                                                    UserID = userId
                                                });
                        }
                    }
                    entities.SaveChanges();
                    json = new JsonResultModel(true, "保存成功");
                }
            }
            catch (Exception exception)
            {
                json = new JsonResultModel(false, "保存失败", exception.FullMessage());
            }
            return json;
        }

        public JObject Recover(string userId, IList<string> names)
        {
            var result = new JObject();
            using (var entities = new Entities())
            {
                var user = entities.Users.FirstOrDefault(u => u.ID == userId);
                foreach (var name in names)
                {
                    if (user.Drafts.Any(d => d.Name == name))
                    {
                        result.Add(new JProperty(name, user.Drafts.First(d => d.Name == name).Content));
                    }
                }
                entities.SaveChanges();
            }
            return result;
        }

        public Tuple<Pagination, List<ReadDTO>> UnreadList(string userId, int page, int group)
        {
            using (var entities = new Entities())
            {
                Expression<Func<Read, bool>> unreadCondition = w => w.ReaderID == userId && w.Status == Protocols.ReadType.Unread;
                var totalCount = entities.Reads.Count(unreadCondition);
                var pagination = new Pagination(totalCount, page, group, 5, 10);
                var reads = entities.Reads.Where(unreadCondition).OrderByDescending(p => p.SendDate).Skip(pagination.ItemIndex).Take(10).MapToDTO();
                return Tuple.Create(pagination, reads);
            }
        }

        public JsonResultModel Read(string id)
        {
            JsonResultModel json;
            try
            {
                using (var entities = new Entities())
                {
                    entities.Reads.FirstOrDefault(r => r.ID == id).Status = Protocols.ReadType.Read;
                    entities.SaveChanges();
                    json = new JsonResultModel(true, "已读");
                }
            }
            catch (Exception exception)
            {
                json = new JsonResultModel(false, "操作失败", exception.FullMessage());
            }
            return json;
        }
    }
}