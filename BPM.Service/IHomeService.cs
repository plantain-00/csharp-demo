using System;
using System.Collections.Generic;
using System.Collections.Specialized;

using Bootstrap.Pagination;

using BPM.Data;
using BPM.Service.Models;

using Newtonsoft.Json.Linq;

namespace BPM.Service
{
    public interface IHomeService
    {
        Tuple<List<Permission>, List<string>> Index(string name);
        List<Permission> NewApplication(string name);
        Tuple<Pagination, List<WorkAssignmentDTO>> ToDoList(string userId, int page, int group);
        Tuple<Pagination, List<WorkAssignmentDTO>> History(string userId, int page, int group);
        JsonResultModel Save(string userId, NameValueCollection form);
        JObject Recover(string userId, IList<string> names);
        Tuple<Pagination, List<ReadDTO>> UnreadList(string userId, int page, int group);
        JsonResultModel Read(string id);
    }
}