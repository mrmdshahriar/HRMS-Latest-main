using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using TalenBAL.BALModel;

namespace TalenBAL.BAL
{
    public class BLRecruitment
    {
        BALHRMS db = new BALHRMS();
        LeaveRequest ObjLeaveRequest = new LeaveRequest();
        ApplyCandidate ObjApplyCandidate = new ApplyCandidate();
        IList<RecruitmentView> IListRecruitmentView = new List<RecruitmentView>();
        string DocPhysicalPath = WebConfigurationManager.AppSettings["DocPhysicalPath"];
        string DocPhysicalPathImage = "E:\\Upwork Task 2022\\Complete Project\\Project Repository\\HRMS\\Files";// WebConfigurationManager.AppSettings["DocPhysicalPathImage"];
        string DocLivePath = WebConfigurationManager.AppSettings["DocLivePath"];
        //public dynamic GetDefaultjobs()
        //{
        //    var record = db.JobRequisitions.Select(s => s).ToList();



        //    return record;
        //}
        //public dynamic GetAllJobs()
        //{
        //    var record = from rq in db.JobRequisitions
        //                 join jt in db.HrmJobTypes on rq.JobType equals jt.Id
        //                 join sf in db.ShiftMasters on rq.ShiftId equals sf.ShiftId
        //                 join dg in db.Designations on rq.DesignationId equals dg.Id
        //                 join dp in db.Departments on rq.DepartmentId equals dp.Id
        //                 select new
        //                 {

        //                     Id = rq.ReqId,
        //                     AddvertiseNo = rq.AddvertiseNo,
        //                     JobTypeId = rq.JobType,
        //                     JobTypeName = jt.Name,
        //                     JobTitle = rq.JobTitle,
        //                     DesignationId = rq.DesignationId,
        //                     DesignationName = dg.Name,
        //                     DepartmentId = rq.DepartmentId,
        //                     DepartmentName = dp.Name,
        //                     ShiftId = rq.ShiftId,
        //                     ShiftCode = sf.Code,
        //                     ShiftName = sf.ShiftName,
        //                     MinExpereince = rq.MinExpereince,
        //                     MaxExpereince = rq.MaxExpereince,
        //                     MInQualification = rq.MInQualification,
        //                     Location = rq.Location,
        //                     Gender = rq.Gender,
        //                     Age = rq.Age,
        //                     Skills = rq.Skills,
        //                     LastDate = rq.LastDate,
        //                     ExpectedSalary = rq.ExpectedSalary,
        //                     Currency = rq.Currency,
        //                     Status = rq.Status
        //                 };

        //    foreach (var item in record)
        //    {
        //        RecruitmentView Obj = new RecruitmentView();
        //        Obj.Id = item.Id;
        //        Obj.AddvertiseNo = item.AddvertiseNo;
        //        Obj.JobTypeId = Convert.ToInt64(item.JobTypeId);
        //        Obj.JobTypeName = item.JobTypeName;
        //        Obj.JobTitle = item.JobTitle;
        //        Obj.DesignationId = Convert.ToInt64(item.DesignationId);
        //        Obj.DesignationName = item.DesignationName;
        //        Obj.DepartmentId = Convert.ToInt64(item.DepartmentId);
        //        Obj.DepartmentName = item.DepartmentName;
        //        Obj.ShiftId = Convert.ToInt64(item.ShiftId);
        //        Obj.ShiftCode = item.ShiftCode;
        //        Obj.ShiftName = item.ShiftName;
        //        Obj.MinExpereince = Convert.ToInt32(item.MinExpereince);
        //        Obj.MaxExpereince = Convert.ToInt32(item.MaxExpereince);
        //        Obj.MInQualification = item.MInQualification;
        //        Obj.Location = item.Location;
        //        Obj.Gender = item.Gender;
        //        Obj.Age = Convert.ToInt32(item.Age);
        //        Obj.SkillId = Convert.ToInt64(item.Skills);
        //        Obj.LastDate = Convert.ToDateTime(item.LastDate);
        //        Obj.ExpectedSalary = Convert.ToDecimal(item.ExpectedSalary);
        //        Obj.Currency = item.Currency;
        //        Obj.Status = Convert.ToInt32(item.Status);
        //        IListRecruitmentView.Add(Obj);


        //    }
        //    return IListRecruitmentView;
        //}
        //public dynamic GetJobsById(long JobId)
        //{
        //    var record = from rq in db.JobRequisitions
        //                 join jt in db.HrmJobTypes on rq.JobType equals jt.Id into table1
        //                 from jt in table1.ToList()
        //                 join sf in db.ShiftMasters on rq.ShiftId equals sf.ShiftId into table2
        //                 from sf in table2.ToList()
        //                 join dg in db.Designations on rq.DesignationId equals dg.Id into table3
        //                 from dg in table3.ToList()
        //                 join dp in db.Departments on rq.DepartmentId equals dp.Id into table4
        //                 from dp in table4.ToList()
        //                 where rq.ReqId == JobId
        //                 select new
        //                 {
        //                     Id = rq.ReqId,
        //                     AddvertiseNo = rq.AddvertiseNo,
        //                     JobTypeId = rq.JobType,
        //                     JobTypeName = jt.Name,
        //                     JobTitle = rq.JobTitle,
        //                     DesignationId = rq.DesignationId,
        //                     DesignationName = dg.Name,
        //                     DepartmentId = rq.DepartmentId,
        //                     DepartmentName = dp.Name,
        //                     ShiftId = rq.ShiftId,
        //                     ShiftCode = sf.Code,
        //                     MinExpereince = rq.MinExpereince,
        //                     MaxExpereince = rq.MaxExpereince,
        //                     MInQualification = rq.MInQualification,
        //                     Location = rq.Location,
        //                     Gender = rq.Gender,
        //                     Age = rq.Age,
        //                     Skills = rq.Skills,
        //                     LastDate = rq.LastDate,
        //                     ExpectedSalary = rq.ExpectedSalary,
        //                     Currency = rq.Currency,
        //                     Status = rq.Status
        //                 };
        //    return record;
        //}

        public dynamic GetAllJobType()
        {
            var record = db.HrmJobTypes.Select(x => x).ToList();
            return record;
        }
        public dynamic GetAllDepartment()
        {
            var record = db.Departments.Select(x => x).ToList();
            return record;
        }

        public dynamic GetAllDesignation()
        {
            var record = db.Designations.Select(x => x).ToList();
            return record;
        }

        public dynamic GetAllSkills()
        {
            var record = db.HrmSkills.Select(x => x).ToList();
            return record;
        }
        public dynamic GetAllShifts()
        {
            var record = db.ShiftMasters.Select(x => x).ToList();
            return record;
        }

        public dynamic GetAppliedCAndiateList()
        {
            try
            {

                var record = (from mt in db.ApplyCandidates
                              join jb in db.JobRequisitions on mt.JobId equals jb.ReqId 
                              join jp in db.Jobs on jb.JobId equals jp.JobId
                              join dp in db.Departments on jp.DepartmentId equals dp.Id
                              join dg in db.Designations on jp.DesignationId equals dg.Id 

                              where mt.Status == "applied"
                              select new
                              {
                                  Id = mt.AppliedId,
                                  Name = mt.CandidateName,
                                  JobTitle = jp.JobTitle,
                                  ApplyDate = mt.ApplyDate.ToString(),
                                  AvailableDate = mt.AvailableDate.ToString(),
                                  Active = mt.IsActive,
                                  DepertmentName = dp.Name,
                                  DesignationName = dg.Name,
                                  Status = mt.Status,
                                  Attachment = mt.Attachment

                              }).ToList();



                return record;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dynamic AddAppliedCandidate(string CandidateName, string FatherName, string CNIC, string ContactNumber, string Email, long JobId, string AppliedFrom, DateTime AvailableDate, string Photo, string Attachment)
        {
            ObjApplyCandidate.CandidateName = CandidateName;
            ObjApplyCandidate.FatherName = FatherName;
            ObjApplyCandidate.CNIC = CNIC;
            ObjApplyCandidate.ContactNumber = ContactNumber;
            ObjApplyCandidate.Email = Email;
            ObjApplyCandidate.JobId = JobId;
            ObjApplyCandidate.AppliedFrom = AppliedFrom;
            ObjApplyCandidate.ApplyDate = DateTime.Now;
            ObjApplyCandidate.Status = "Applied";
            ObjApplyCandidate.AvailableDate = AvailableDate;
            ObjApplyCandidate.Attachment = Attachment;
            ObjApplyCandidate.Photo = Photo;
            ObjApplyCandidate.IsActive = true;
            ObjApplyCandidate.CreatedBy = 1;
            ObjApplyCandidate.CreatedOn = DateTime.Now;
            db.ApplyCandidates.Add(ObjApplyCandidate);
            db.SaveChanges();
            return ObjApplyCandidate;
        }

        public dynamic GetAllJobMaster()
        {
            var record = db.Jobs.Select(s => s).ToList();

            return record;
        }

        public dynamic GetAllJobsMasterbyId(long JobId)
        {
            var record = from mt in db.JobRequisitions
                         join rq in db.Jobs on mt.JobId equals rq.JobId
                         join jt in db.HrmJobTypes on rq.JobType equals jt.Id
                         join sf in db.ShiftMasters on rq.ShiftId equals sf.ShiftId
                         join dg in db.Designations on rq.DesignationId equals dg.Id
                         join dp in db.Departments on rq.DepartmentId equals dp.Id
                         join sk in db.HrmSkills on rq.Skills equals sk.Id
                         where mt.JobId == JobId
                         select new
                         {

                             Id = rq.JobId,
                             RequestId = mt.ReqId,
                             AddvertiseNo = mt.AddvertiseNo,
                             JobTypeId = rq.JobType,
                             JobTypeName = jt.Name,
                             JobTitle = rq.JobTitle,
                             DesignationId = rq.DesignationId,
                             DesignationName = dg.Name,
                             DepartmentId = rq.DepartmentId,
                             DepartmentName = dp.Name,
                             ShiftId = rq.ShiftId,
                             ShiftCode = sf.Code,
                             ShiftName = sf.ShiftName,
                             MinExpereince = rq.MinExpereince,
                             MaxExpereince = rq.MaxExpereince,
                             MInQualification = rq.MInQualification,
                             JobDescription = rq.jobDescription,
                             Location = rq.Location,
                             Gender = rq.Gender,
                             Age = rq.Age,
                             SkillId = rq.Skills,
                             SkillName = sk.Name,
                             LastDate = mt.LastDate,
                             ExpectedSalary = rq.SalaryRange,
                             Currency = mt.Currency,
                             Status = mt.Status
                         };

            foreach (var item in record)
            {
                RecruitmentView Obj = new RecruitmentView();
                Obj.Id = item.Id;
                Obj.RequestId = item.RequestId;
                Obj.AddvertiseNo = item.AddvertiseNo;
                Obj.JobTypeId = Convert.ToInt64(item.JobTypeId);
                Obj.JobTypeName = item.JobTypeName;
                Obj.JobTitle = item.JobTitle;
                Obj.DesignationId = Convert.ToInt64(item.DesignationId);
                Obj.DesignationName = item.DesignationName;
                Obj.DepartmentId = Convert.ToInt64(item.DepartmentId);
                Obj.DepartmentName = item.DepartmentName;
                Obj.ShiftId = Convert.ToInt64(item.ShiftId);
                Obj.ShiftCode = item.ShiftCode;
                Obj.ShiftName = item.ShiftName;
                Obj.MinExpereince = Convert.ToInt32(item.MinExpereince);
                Obj.MaxExpereince = Convert.ToInt32(item.MaxExpereince);
                Obj.MInQualification = item.MInQualification;
                Obj.Location = item.Location;
                Obj.JobDescription = item.JobDescription;
                Obj.LastDate = Convert.ToDateTime(item.LastDate);
                Obj.Currency = item.Currency;
                Obj.Status = Convert.ToInt32(item.Status);

                if (item.Gender == "1")
                {
                    Obj.Gender = "Male";
                }
                if (item.Gender == "2")
                {
                    Obj.Gender = "FeMale";
                }
                if (item.Gender == "3")
                {
                    Obj.Gender = "Any";
                }

                Obj.Age = Convert.ToInt32(item.Age);
                Obj.SkillId = Convert.ToInt64(item.SkillId);
                Obj.SkillsName = item.SkillName;
                //Obj.LastDate = Convert.ToDateTime(item.LastDate);
                Obj.ExpectedSalary = Convert.ToDecimal(item.ExpectedSalary);
                //Obj.Currency = item.Currency;
                // Obj.Status = Convert.ToInt32(item.Status);
                IListRecruitmentView.Add(Obj);

            }

            
            return IListRecruitmentView;
        }
        public dynamic GetAllJobsMasterApproved()
        {

            var record = from mt in db.JobRequisitions
                         join rq in db.Jobs on mt.JobId equals rq.JobId
                         join jt in db.HrmJobTypes on rq.JobType equals jt.Id
                         join sf in db.ShiftMasters on rq.ShiftId equals sf.ShiftId
                         join dg in db.Designations on rq.DesignationId equals dg.Id
                         join dp in db.Departments on rq.DepartmentId equals dp.Id
                         join sk in db.HrmSkills on rq.Skills equals sk.Id
                         where mt.Status ==1
                         select new
                         {

                             Id = rq.JobId,
                             RequestId = mt.ReqId,
                             AddvertiseNo = mt.AddvertiseNo,
                             JobTypeId = rq.JobType,
                             JobTypeName = jt.Name,
                             JobTitle = rq.JobTitle,
                             DesignationId = rq.DesignationId,
                             DesignationName = dg.Name,
                             DepartmentId = rq.DepartmentId,
                             DepartmentName = dp.Name,
                             ShiftId = rq.ShiftId,
                             ShiftCode = sf.Code,
                             ShiftName = sf.ShiftName,
                             MinExpereince = rq.MinExpereince,
                             MaxExpereince = rq.MaxExpereince,
                             MInQualification = rq.MInQualification,
                             JobDescription = rq.jobDescription,
                             Location = rq.Location,
                             Gender = rq.Gender,
                             Age = rq.Age,
                             SkillId = rq.Skills,
                             SkillName = sk.Name,
                             LastDate = mt.LastDate,
                             ExpectedSalary = rq.SalaryRange,
                             Currency = mt.Currency,
                             Status = mt.Status
                         };

            foreach (var item in record)
            {
                RecruitmentView Obj = new RecruitmentView();
                Obj.Id = item.Id;
                Obj.RequestId = item.RequestId;
                Obj.AddvertiseNo = item.AddvertiseNo;
                Obj.JobTypeId = Convert.ToInt64(item.JobTypeId);
                Obj.JobTypeName = item.JobTypeName;
                Obj.JobTitle = item.JobTitle;
                Obj.DesignationId = Convert.ToInt64(item.DesignationId);
                Obj.DesignationName = item.DesignationName;
                Obj.DepartmentId = Convert.ToInt64(item.DepartmentId);
                Obj.DepartmentName = item.DepartmentName;
                Obj.ShiftId = Convert.ToInt64(item.ShiftId);
                Obj.ShiftCode = item.ShiftCode;
                Obj.ShiftName = item.ShiftName;
                Obj.MinExpereince = Convert.ToInt32(item.MinExpereince);
                Obj.MaxExpereince = Convert.ToInt32(item.MaxExpereince);
                Obj.MInQualification = item.MInQualification;
                Obj.Location = item.Location;
                Obj.JobDescription = item.JobDescription;
                Obj.LastDate = Convert.ToDateTime(item.LastDate);
                Obj.Currency = item.Currency;
                Obj.Status = Convert.ToInt32(item.Status);
                if (Obj.Status == 1)
                {
                    Obj.JobStatus = "Post";
                }
                else
                {
                    Obj.JobStatus = "Pending";
                }

                if (item.Gender == "1")
                {
                    Obj.Gender = "Male";
                }
                if (item.Gender == "2")
                {
                    Obj.Gender = "FeMale";
                }
                if (item.Gender == "3")
                {
                    Obj.Gender = "Any";
                }

                Obj.Age = Convert.ToInt32(item.Age);
                Obj.SkillId = Convert.ToInt64(item.SkillId);
                Obj.SkillsName = item.SkillName;
                //Obj.LastDate = Convert.ToDateTime(item.LastDate);
                Obj.ExpectedSalary = Convert.ToDecimal(item.ExpectedSalary);
                //Obj.Currency = item.Currency;
                // Obj.Status = Convert.ToInt32(item.Status);
                IListRecruitmentView.Add(Obj);


            }
            return IListRecruitmentView;
        }
        public dynamic GetAllJobsMaster()
        {
            
            var record = from mt in db.JobRequisitions
                         join rq in db.Jobs on mt.JobId equals rq.JobId
                         join jt in db.HrmJobTypes on rq.JobType equals jt.Id
                         join sf in db.ShiftMasters on rq.ShiftId equals sf.ShiftId
                         join dg in db.Designations on rq.DesignationId equals dg.Id
                         join dp in db.Departments on rq.DepartmentId equals dp.Id
                         join sk in db.HrmSkills on rq.Skills equals sk.Id
                         orderby mt.ReqId descending
                         select new
                         {

                             Id = rq.JobId,
                             RequestId = mt.ReqId,
                             AddvertiseNo = mt.AddvertiseNo,
                             JobTypeId = rq.JobType,
                             JobTypeName = jt.Name,
                             JobTitle = rq.JobTitle,
                             DesignationId = rq.DesignationId,
                             DesignationName = dg.Name,
                             DepartmentId = rq.DepartmentId,
                             DepartmentName = dp.Name,
                             ShiftId = rq.ShiftId,
                             ShiftCode = sf.Code,
                             ShiftName = sf.ShiftName,
                             MinExpereince = rq.MinExpereince,
                             MaxExpereince = rq.MaxExpereince,
                             MInQualification = rq.MInQualification,
                             JobDescription = rq.jobDescription,
                             Location = rq.Location,
                             Gender = rq.Gender,
                             Age = rq.Age,
                             SkillId = rq.Skills,
                             SkillName = sk.Name,
                             LastDate = mt.LastDate,
                             ExpectedSalary = rq.SalaryRange,
                             Currency = mt.Currency,
                             Status = mt.Status
                         };

            foreach (var item in record)
            {
                RecruitmentView Obj = new RecruitmentView();
                Obj.Id = item.Id;
                Obj.RequestId = item.RequestId;
                Obj.AddvertiseNo = item.AddvertiseNo;
                Obj.JobTypeId = Convert.ToInt64(item.JobTypeId);
                Obj.JobTypeName = item.JobTypeName;
                Obj.JobTitle = item.JobTitle;
                Obj.DesignationId = Convert.ToInt64(item.DesignationId);
                Obj.DesignationName = item.DesignationName;
                Obj.DepartmentId = Convert.ToInt64(item.DepartmentId);
                Obj.DepartmentName = item.DepartmentName;
                Obj.ShiftId = Convert.ToInt64(item.ShiftId);
                Obj.ShiftCode = item.ShiftCode;
                Obj.ShiftName = item.ShiftName;
                Obj.MinExpereince = Convert.ToInt32(item.MinExpereince);
                Obj.MaxExpereince = Convert.ToInt32(item.MaxExpereince);
                Obj.MInQualification = item.MInQualification;
                Obj.Location = item.Location;
                Obj.JobDescription = item.JobDescription;
                Obj.LastDate = Convert.ToDateTime(item.LastDate);
                Obj.Currency = item.Currency;
                Obj.Status = Convert.ToInt32(item.Status);
                if(Obj.Status ==1)
                {
                    Obj.JobStatus = "Post";
                }
                else
                {
                    Obj.JobStatus = "Pending";
                }

                if (item.Gender == "1")
                {
                    Obj.Gender = "Male";
                }
                if (item.Gender == "2")
                {
                    Obj.Gender = "FeMale";
                }
                if (item.Gender == "3")
                {
                    Obj.Gender = "Any";
                }

                Obj.Age = Convert.ToInt32(item.Age);
                Obj.SkillId = Convert.ToInt64(item.SkillId);
                Obj.SkillsName = item.SkillName;
                //Obj.LastDate = Convert.ToDateTime(item.LastDate);
                Obj.ExpectedSalary = Convert.ToDecimal(item.ExpectedSalary);
                //Obj.Currency = item.Currency;
                // Obj.Status = Convert.ToInt32(item.Status);
                IListRecruitmentView.Add(Obj);


            }
            return IListRecruitmentView;
        }

        public dynamic GetSelectedJobById(long JobId)
        {
            var record = from mt in db.JobRequisitions
                         join rq in db.Jobs on mt.JobId equals rq.JobId
                         join jt in db.HrmJobTypes on rq.JobType equals jt.Id
                         join sf in db.ShiftMasters on rq.ShiftId equals sf.ShiftId
                         join dg in db.Designations on rq.DesignationId equals dg.Id
                         join dp in db.Departments on rq.DepartmentId equals dp.Id
                         join sk in db.HrmSkills on rq.Skills equals sk.Id
                         where mt.ReqId == JobId
                         select new
                         {

                             Id = rq.JobId,
                             RequestId = mt.ReqId,
                             AddvertiseNo = mt.AddvertiseNo,
                             JobTypeId = rq.JobType,
                             JobTypeName = jt.Name,
                             JobTitle = rq.JobTitle,
                             DesignationId = rq.DesignationId,
                             DesignationName = dg.Name,
                             DepartmentId = rq.DepartmentId,
                             DepartmentName = dp.Name,
                             ShiftId = rq.ShiftId,
                             ShiftCode = sf.Code,
                             ShiftName = sf.ShiftName,
                             MinExpereince = rq.MinExpereince,
                             MaxExpereince = rq.MaxExpereince,
                             MInQualification = rq.MInQualification,
                             JobDescription = rq.jobDescription,
                             Location = rq.Location,
                             Gender = rq.Gender,
                             Age = rq.Age,
                             SkillId = rq.Skills,
                             SkillName = sk.Name,
                             LastDate = mt.LastDate,
                             ExpectedSalary = rq.SalaryRange,
                             Currency = mt.Currency,
                             Status = mt.Status
                         };

            foreach (var item in record)
            {
                RecruitmentView Obj = new RecruitmentView();
                Obj.Id = item.Id;
                Obj.RequestId = item.RequestId;
                Obj.AddvertiseNo = item.AddvertiseNo;
                Obj.JobTypeId = Convert.ToInt64(item.JobTypeId);
                Obj.JobTypeName = item.JobTypeName;
                Obj.JobTitle = item.JobTitle;
                Obj.DesignationId = Convert.ToInt64(item.DesignationId);
                Obj.DesignationName = item.DesignationName;
                Obj.DepartmentId = Convert.ToInt64(item.DepartmentId);
                Obj.DepartmentName = item.DepartmentName;
                Obj.ShiftId = Convert.ToInt64(item.ShiftId);
                Obj.ShiftCode = item.ShiftCode;
                Obj.ShiftName = item.ShiftName;
                Obj.MinExpereince = Convert.ToInt32(item.MinExpereince);
                Obj.MaxExpereince = Convert.ToInt32(item.MaxExpereince);
                Obj.MInQualification = item.MInQualification;
                Obj.Location = item.Location;
                Obj.JobDescription = item.JobDescription;
                Obj.LastDate = Convert.ToDateTime(item.LastDate);
                Obj.Currency = item.Currency;
                Obj.Status = Convert.ToInt32(item.Status);
                if (Obj.Status == 1)
                {
                    Obj.JobStatus = "Post";
                }
                else
                {
                    Obj.JobStatus = "Pending";
                }

                if (item.Gender == "1")
                {
                    Obj.Gender = "Male";
                }
                if (item.Gender == "2")
                {
                    Obj.Gender = "FeMale";
                }
                if (item.Gender == "3")
                {
                    Obj.Gender = "Any";
                }

                Obj.Age = Convert.ToInt32(item.Age);
                Obj.SkillId = Convert.ToInt64(item.SkillId);
                Obj.SkillsName = item.SkillName;
                //Obj.LastDate = Convert.ToDateTime(item.LastDate);
                Obj.ExpectedSalary = Convert.ToDecimal(item.ExpectedSalary);
                //Obj.Currency = item.Currency;
                // Obj.Status = Convert.ToInt32(item.Status);
                IListRecruitmentView.Add(Obj);


            }
            return IListRecruitmentView;
        }

        public dynamic GetDataByJobChange(long JobId)
        {
            var record = from rq in db.Jobs                         
                         join jt in db.HrmJobTypes on rq.JobType equals jt.Id
                         join sf in db.ShiftMasters on rq.ShiftId equals sf.ShiftId
                         join dg in db.Designations on rq.DesignationId equals dg.Id
                         join dp in db.Departments on rq.DepartmentId equals dp.Id
                         join sk in db.HrmSkills on rq.Skills equals sk.Id
                         where rq.JobId == JobId
                         select new
                         {

                             Id = rq.JobId,
                         
                             JobTypeId = rq.JobType,
                             JobTypeName = jt.Name,
                             JobTitle = rq.JobTitle,
                             DesignationId = rq.DesignationId,
                             DesignationName = dg.Name,
                             DepartmentId = rq.DepartmentId,
                             DepartmentName = dp.Name,
                             ShiftId = rq.ShiftId,
                             ShiftCode = sf.Code,
                             ShiftName = sf.ShiftName,
                             MinExpereince = rq.MinExpereince,
                             MaxExpereince = rq.MaxExpereince,
                             MInQualification = rq.MInQualification,
                             JobDescription = rq.jobDescription,
                             Location = rq.Location,
                             Gender = rq.Gender,
                             Age = rq.Age,
                             SkillId = rq.Skills,
                             SkillName = sk.Name,
                        
                             ExpectedSalary = rq.SalaryRange,
                           
                         };

            foreach (var item in record)
            {
                RecruitmentView Obj = new RecruitmentView();
                Obj.Id = item.Id;
               
                Obj.JobTypeId = Convert.ToInt64(item.JobTypeId);
                Obj.JobTypeName = item.JobTypeName;
                Obj.JobTitle = item.JobTitle;
                Obj.DesignationId = Convert.ToInt64(item.DesignationId);
                Obj.DesignationName = item.DesignationName;
                Obj.DepartmentId = Convert.ToInt64(item.DepartmentId);
                Obj.DepartmentName = item.DepartmentName;
                Obj.ShiftId = Convert.ToInt64(item.ShiftId);
                Obj.ShiftCode = item.ShiftCode;
                Obj.ShiftName = item.ShiftName;
                Obj.MinExpereince = Convert.ToInt32(item.MinExpereince);
                Obj.MaxExpereince = Convert.ToInt32(item.MaxExpereince);
                Obj.MInQualification = item.MInQualification;
                Obj.Location = item.Location;
                Obj.JobDescription = item.JobDescription;
             
             
                if (Obj.Status == 1)
                {
                    Obj.JobStatus = "Post";
                }
                else
                {
                    Obj.JobStatus = "Pending";
                }

                if (item.Gender == "1")
                {
                    Obj.Gender = "Male";
                }
                if (item.Gender == "2")
                {
                    Obj.Gender = "FeMale";
                }
                if (item.Gender == "3")
                {
                    Obj.Gender = "Any";
                }

                Obj.Age = Convert.ToInt32(item.Age);
                Obj.SkillId = Convert.ToInt64(item.SkillId);
                Obj.SkillsName = item.SkillName;
                //Obj.LastDate = Convert.ToDateTime(item.LastDate);
                Obj.ExpectedSalary = Convert.ToDecimal(item.ExpectedSalary);
                //Obj.Currency = item.Currency;
                // Obj.Status = Convert.ToInt32(item.Status);
                IListRecruitmentView.Add(Obj);


            }
            return IListRecruitmentView;
        }

        public dynamic GetApplyCandidatebyId(long Id)
        {
            var result = (from mt in db.ApplyCandidates
                          join jb in db.JobRequisitions on mt.JobId equals jb.ReqId 
                          join jp in db.Jobs on jb.JobId equals jp.JobId
                          join dp in db.Departments on jp.DepartmentId equals dp.Id 
                          join dg in db.Designations on jp.DesignationId equals dg.Id 
                          join jt in db.HrmJobTypes on jp.JobType equals jt.Id 
                          join sf in db.ShiftMasters on jp.ShiftId equals sf.ShiftId 
                          where mt.AppliedId == Id
                          //where mt.Status == "applied" 
                          select new
                          {
                              Id = mt.AppliedId,
                              AddvertiseNo = jb.AddvertiseNo,
                              CandidateName = mt.CandidateName,
                              FatherName = mt.FatherName,
                              CNIC = mt.CNIC,
                              ContactNumber = mt.ContactNumber,
                              Email = mt.Email,
                              JobType = jt.Name,
                              JobTitle = jp.JobTitle,
                              ShiftName = sf.ShiftName,
                              MinExpereince = jp.MinExpereince,
                              MaxExpereince = jp.MaxExpereince,
                              MInQualification = jp.MInQualification,
                              Location = jp.Location,
                              Gender = jp.Gender,
                              Age = jp.Age,
                              Skills = jp.Skills,
                              ExpectedSalary = jp.SalaryRange,
                              Currency = jb.Currency,
                              Photo = DocPhysicalPath + mt.Photo,
                              ApplyDate = mt.ApplyDate.ToString(),
                              AvailableDate = mt.AvailableDate.ToString(),
                              Active = mt.IsActive,
                              DepartmentId = jp.DepartmentId,
                              DepertmentName = dp.Name,
                              DesignationId = jp.DesignationId,
                              DesignationName = dg.Name,
                              AppliedFrom = mt.AppliedFrom,
                              Status = mt.Status,
                              Attachment = DocPhysicalPath + mt.Attachment


                          }).ToList();
            return result;
        }

        public dynamic GetApplyCandidatebyDataByStatus(string Status)
        {
            var result = (from mt in db.ApplyCandidates
                          join jb in db.JobRequisitions on mt.JobId equals jb.ReqId
                          join jp in db.Jobs on jb.JobId equals jp.JobId
                          join dp in db.Departments on jp.DepartmentId equals dp.Id
                          join dg in db.Designations on jp.DesignationId equals dg.Id
                          join jt in db.HrmJobTypes on jp.JobType equals jt.Id
                          join sf in db.ShiftMasters on jp.ShiftId equals sf.ShiftId
                          
                          where mt.Status == Status
                          select new
                          {
                              Id = mt.AppliedId,
                              AddvertiseNo = jb.AddvertiseNo,
                              CandidateName = mt.CandidateName,
                              FatherName = mt.FatherName,
                              CNIC = mt.CNIC,
                              ContactNumber = mt.ContactNumber,
                              Email = mt.Email,
                              JobType = jt.Name,
                              JobTitle = jp.JobTitle,
                              ShiftName = sf.ShiftName,
                              MinExpereince = jp.MinExpereince,
                              MaxExpereince = jp.MaxExpereince,
                              MInQualification = jp.MInQualification,
                              Location = jp.Location,
                              Gender = jp.Gender,
                              Age = jp.Age,
                              Skills = jp.Skills,
                              ExpectedSalary = jp.SalaryRange,
                              Currency = jb.Currency,
                              Photo = DocPhysicalPath + mt.Photo,
                              ApplyDate = mt.ApplyDate.ToString(),
                              AvailableDate = mt.AvailableDate.ToString(),
                              Active = mt.IsActive,
                              DepartmentId = jp.DepartmentId,
                              DepertmentName = dp.Name,
                              DesignationId = jp.DesignationId,
                              DesignationName = dg.Name,
                              AppliedFrom = mt.AppliedFrom,
                              Status = mt.Status,
                              Attachment = DocPhysicalPath + mt.Attachment


                          }).ToList();
            return result;
        }

   

        public dynamic GetShiftAll()
        {
            var record = db.ShiftMasters.Select(s => s).ToList();
            return record;
        }

    }
}
