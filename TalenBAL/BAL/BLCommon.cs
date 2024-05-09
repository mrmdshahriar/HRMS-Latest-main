using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TalenBAL.BALModel;

namespace TalenBAL.BAL
{
   public class BLCommon
    {
        BALHRMS db = new BALHRMS();
        LeaveRequest ObjLeaveRequest = new LeaveRequest();
        public dynamic GetDepartmentById(long Id)
        {
            var record = db.Departments.Where(x => x.Id == Id).FirstOrDefault();
            return record;
        }

        public dynamic GetDesignationId(long Id)
        {
            var record = db.Designations.Where(x => x.Id == Id).FirstOrDefault();
            return record;
        }

        public dynamic GetShiftId(long Id)
        {
            var record = db.ShiftMasters.Where(x => x.ShiftId == Id).FirstOrDefault();
            return record;
        }
        public dynamic GetJobTypeId(long Id)
        {
            var record = db.HrmJobTypes.Where(x => x.Id == Id).FirstOrDefault();
            return record;
        }

    }
}
