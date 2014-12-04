using System;
using System.Collections.Generic;

using BPM.Service.Models;

namespace BPM.Service
{
    public interface IProcessService
    {
        List<DepartmentDTO> WorkAssignmentStart();
        JsonResultModel WorkAssignmentStartSubmit(string financialUserID, string testUserID, string applierId, string projectID, string projectName, DateTime reportDate, string workContent, string remarks);
        WorkAssignmentDTO WorkAssignmentHandle(string id);
        JsonResultModel WorkAssignmentFinancialSubmit(string id, string situation, string delayReason, DateTime finishDate);
        JsonResultModel WorkAssignmentTestSubmit(string id, string situation, string delayReason, DateTime finishDate);
        JsonResultModel WorkAssignmentCheckSubmit(string id);
        WorkAssignmentDTO WorkAssignmentDetail(string id);
    }
}