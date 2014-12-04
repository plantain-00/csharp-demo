using System;
using System.Collections.Generic;
using System.Linq;

using BCL.Extension;

using BPM.Data;
using BPM.Service.Models;

namespace BPM.Service
{
    public class ProcessService : IProcessService
    {
        public List<DepartmentDTO> WorkAssignmentStart()
        {
            using (var entities = new Entities())
            {
                var departments = entities.Departments.Where(d => d.Name == "财务部" || d.Name == "化验室").MapToDTO();
                return departments;
            }
        }

        public JsonResultModel WorkAssignmentStartSubmit(string financialUserID, string testUserID, string applierId, string projectID, string projectName, DateTime reportDate, string workContent, string remarks)
        {
            JsonResultModel json;
            try
            {
                using (var entities = new Entities())
                {
                    var tmp = DateTime.Now;
                    var today = new DateTime(tmp.Year, tmp.Month, tmp.Day);
                    tmp = tmp.AddDays(1);
                    var tomorrow = new DateTime(tmp.Year, tmp.Month, tmp.Day);
                    var sheetID = DateTime.Now.ToString("yyyyMMdd") + (entities.WorkAssignments.Count(w => w.ApplyDate >= today && w.ApplyDate < tomorrow) + 1).ToString("0000");
                    if (string.IsNullOrEmpty(financialUserID)
                        && string.IsNullOrEmpty(testUserID))
                    {
                        json = new JsonResultModel(false, "至少需要选择一个处理人");
                    }
                    else
                    {
                        var status = WorkAssignmentStatus.Zero;
                        if (string.IsNullOrEmpty(financialUserID))
                        {
                            status |= WorkAssignmentStatus.Financial;
                        }
                        else if (string.IsNullOrEmpty(testUserID))
                        {
                            status |= WorkAssignmentStatus.Test;
                        }
                        entities.WorkAssignments.Add(new WorkAssignment
                                                     {
                                                         ID = Guid.NewGuid().ToString(),
                                                         ApplierID = applierId,
                                                         ProjectID = projectID,
                                                         ProjectName = projectName,
                                                         WorkContent = workContent,
                                                         ReportDate = reportDate,
                                                         Remarks = remarks,
                                                         FinancialUserID = financialUserID,
                                                         TestUserID = testUserID,
                                                         ApplyDate = DateTime.Now,
                                                         SheetID = sheetID,
                                                         Status = (int) status
                                                     });
                        entities.SaveChanges();
                        json = new JsonResultModel(true, "提交成功");
                    }
                }
            }
            catch (Exception exception)
            {
                json = new JsonResultModel(false, "提交失败", exception.FullMessage());
            }
            return json;
        }

        public WorkAssignmentDTO WorkAssignmentHandle(string id)
        {
            using (var entities = new Entities())
            {
                var workAssignment = entities.WorkAssignments.FirstOrDefault(w => w.ID == id);
                return workAssignment.MapToDTO();
            }
        }

        public JsonResultModel WorkAssignmentFinancialSubmit(string id, string situation, string delayReason, DateTime finishDate)
        {
            JsonResultModel json;
            if (string.IsNullOrEmpty(situation))
            {
                json = new JsonResultModel(false, "完成情况不能为空");
            }
            else
            {
                try
                {
                    using (var entities = new Entities())
                    {
                        var toBeModified = entities.WorkAssignments.FirstOrDefault(w => w.ID == id);
                        toBeModified.FinancialDelayReason = delayReason;
                        toBeModified.FinancialFinishDate = finishDate;
                        toBeModified.FinancialSituation = situation;
                        toBeModified.Status = (int) ((WorkAssignmentStatus) toBeModified.Status | WorkAssignmentStatus.Financial);
                        entities.SaveChanges();
                        json = new JsonResultModel(true, "提交成功");
                    }
                }
                catch (Exception exception)
                {
                    json = new JsonResultModel(false, "提交失败", exception.FullMessage());
                }
            }
            return json;
        }

        public JsonResultModel WorkAssignmentTestSubmit(string id, string situation, string delayReason, DateTime finishDate)
        {
            JsonResultModel json;
            if (string.IsNullOrEmpty(situation))
            {
                json = new JsonResultModel(false, "完成情况不能为空");
            }
            else
            {
                try
                {
                    using (var entities = new Entities())
                    {
                        var toBeModified = entities.WorkAssignments.FirstOrDefault(w => w.ID == id);
                        toBeModified.TestDelayReason = delayReason;
                        toBeModified.TestFinishDate = finishDate;
                        toBeModified.TestSituation = situation;
                        toBeModified.Status = (int) ((WorkAssignmentStatus) toBeModified.Status | WorkAssignmentStatus.Test);
                        entities.SaveChanges();
                        json = new JsonResultModel(true, "提交成功");
                    }
                }
                catch (Exception exception)
                {
                    json = new JsonResultModel(false, "提交失败", exception.FullMessage());
                }
            }
            return json;
        }

        public JsonResultModel WorkAssignmentCheckSubmit(string id)
        {
            JsonResultModel json;
            try
            {
                using (var entities = new Entities())
                {
                    var toBeModified = entities.WorkAssignments.FirstOrDefault(w => w.ID == id);
                    toBeModified.CheckDate = DateTime.Now;
                    toBeModified.Status = (int) ((WorkAssignmentStatus) toBeModified.Status | WorkAssignmentStatus.Check);
                    if (!string.IsNullOrEmpty(toBeModified.FinancialUserID))
                    {
                        entities.Reads.Add(new Read
                                           {
                                               ID = Guid.NewGuid().ToString(),
                                               ProcessType = Protocols.WORK_ASSIGNMENT,
                                               ProcessID = id,
                                               ReaderID = toBeModified.FinancialUserID,
                                               SenderID = toBeModified.ApplierID,
                                               SendDate = DateTime.Now,
                                               Status = Protocols.ReadType.Unread
                                           });
                    }
                    if (!string.IsNullOrEmpty(toBeModified.TestUserID))
                    {
                        entities.Reads.Add(new Read
                                           {
                                               ID = Guid.NewGuid().ToString(),
                                               ProcessType = Protocols.WORK_ASSIGNMENT,
                                               ProcessID = id,
                                               ReaderID = toBeModified.TestUserID,
                                               SenderID = toBeModified.ApplierID,
                                               SendDate = DateTime.Now,
                                               Status = Protocols.ReadType.Unread
                                           });
                    }
                    entities.SaveChanges();
                    json = new JsonResultModel(true, "提交成功");
                }
            }
            catch (Exception exception)
            {
                json = new JsonResultModel(false, "提交失败", exception.FullMessage());
            }
            return json;
        }

        public WorkAssignmentDTO WorkAssignmentDetail(string id)
        {
            using (var entities = new Entities())
            {
                var workAssignment = entities.WorkAssignments.FirstOrDefault(w => w.ID == id);
                return workAssignment.MapToDTO();
            }
        }
    }
}