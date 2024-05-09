using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TalenBAL.BALModel;

namespace TalenBAL.BAL
{
    public class BLEmployeeProfile
    {
        BALHRMS db = new BALHRMS();
        IList<HrmEmployee> IListHrmEmployeeView = new List<HrmEmployee>();
        public dynamic GetEmployees()
        {
            var record = db.HrmEmployees.Select(s => s).ToList();
            return record;
        }
        public dynamic GetEmployeeById(long EmpId)
        {
            var record = db.HrmEmployees.Where(x => x.Id == EmpId).FirstOrDefault();
            return record;
        }




        public dynamic GetDataByJobChange(long JobId)
        {
            var record = (from rq in db.HrmEmployees
                          join dg in db.Designations on rq.DesignationId equals dg.Id
                          join dp in db.Departments on rq.DepartmentId equals dp.Id
                          where rq.Id == JobId
                          select new
                          {
                              Id = rq.Id,
                              DesignationId = rq.DesignationId,
                              DesigId = dg.Id,
                              Name = dg.Name,
                              DepartId = dp.Id,
                              DepartmentId = rq.DepartmentId,
                              dName = dp.Name,
                          }).ToList();
            return record;
        }

        public dynamic GetDataByEmployee_Sepration(long JobId)
        {
            var record = (from rq in db.HrmEmployees
                          join dg in db.Designations on rq.DesignationId equals dg.Id
                          join dp in db.Departments on rq.DepartmentId equals dp.Id
                          where rq.Id == JobId
                          select new
                          {
                              Id = rq.Id,
                              // DesignationId = rq.DesignationId,
                              DesigId = dg.Id,
                              Name = dg.Name,
                              // DepartmentId = rq.DepartmentId,
                              DepartId = dp.Id,
                              dName = dp.Name,
                          }).ToList();
            return record;
        }
    }
}
