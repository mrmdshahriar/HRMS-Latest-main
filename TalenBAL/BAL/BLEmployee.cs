using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalenBAL.BALModel;

namespace TalenBAL.BAL
{
   
    public class BLEmployee
    {
        BALHRMS db = new BALHRMS();
        IList<HrmEmployee> IList = new List<HrmEmployee>();
        EmplouyeeView EmployeeView = new EmplouyeeView();
        public dynamic GetEmployeeById(long EmployeeId)
        {
            List<HrmEmployee> records = db.HrmEmployees.Where(x => x.Id == EmployeeId).ToList();

            var record = (from ep in db.HrmEmployees
                          join dp in db.Departments on ep.DepartmentId equals dp.Id
                          join ds in db.Designations on ep.DesignationId equals ds.Id
                          join cu in db.Countries on ep.NationalCountryId equals cu.Id
                          where ep.Id == EmployeeId
                          select new
                          {

                              EmployeeCode = ep.EmployeeCode,
                              EmployeeName = ep.FirstName + "" + ep.LastName,
                              Department = dp.Name,
                              Designation = ds.Name,
                              DateofJoining = ep.dteJoiningDate,
                              BirthDate = ep.DateOfBirth,
                              ProfilePicture = ep.ProfilePicture,
                              MaritalStatus = ep.MaritalStatus,
                              Nationality = cu.Name,
                              ContacNo = ep.ContacNo,
                              Email = ep.Email,
                              CurrentAddress = ep.ZipCode,
                              CurrentZipCode = ep.CurrentZipCode,
                              IdentityExpiryDate = ep.IdentityExpiryDate,
                              PassportExpiryDate = ep.PassportExpiryDate,
                          }).ToList();
            //foreach (var item in record)
            //{
            //    EmployeeView.Id = item.Id;
            //    EmployeeView.EmployeeCode = item.EmployeeCode;
            //    EmployeeView.EmployeeName = item.FirstName + "" + item.LastName;
            //    EmployeeView.Department = item.Department;
            //    EmployeeView.Designation = item.Designation;
            //    EmployeeView.DateofJoining = item.JoiningDate;
            //    EmployeeView.BirthDate = item.DateOfBirth;
            //    EmployeeView.Picture = item.ProfilePicture;
            //    EmployeeView.MaritalStatus = item.MaritalStatus;
            //    EmployeeView.Nationality = item.Nationality;



            //    EmployeeView.ContacNo = item.ContacNo;
            //    EmployeeView.Email = item.Email;
            //    EmployeeView.CurrentAddress = item.CurrentAddress;
            //    EmployeeView.CurrentZipCode = item.CurrentAddress;
            //    EmployeeView.CurrentZipCode = item.CurrentZipCode;
            //    EmployeeView.IdentityExpiryDate = item.IdentityExpiryDate;
            //    EmployeeView.PassportExpiryDate = item.PassportExpiryDate;



            //}
            return record;
        }

        public dynamic UpdateEmployeeinfo(long EmployeeId, string ContacNo, string Email, string CurrentAddress, string CurrentZipCode, DateTime IdentityExpiryDate, DateTime PassportExpiryDate)
        {

           var record = db.HrmEmployees.Where(x => x.Id == EmployeeId).FirstOrDefault();

            if(record != null)
            {
                record.ContacNo = ContacNo;
                record.Email = Email;
                record.CurrentAddress = CurrentAddress;
                record.CurrentZipCode = CurrentZipCode;
                record.IdentityExpiryDate = IdentityExpiryDate;
                record.PassportExpiryDate = PassportExpiryDate;
                db.SaveChanges();

                EmployeeView.Id = Convert.ToInt32(EmployeeId);
                EmployeeView.EmployeeCode = record.EmployeeCode;
                EmployeeView.ContacNo = ContacNo;
                EmployeeView.Email = Email;
                EmployeeView.CurrentAddress = CurrentAddress;
                EmployeeView.CurrentZipCode = CurrentAddress;
                EmployeeView.CurrentZipCode = CurrentZipCode;
                EmployeeView.IdentityExpiryDate = IdentityExpiryDate;
                EmployeeView.PassportExpiryDate = PassportExpiryDate;
            }
         
            return EmployeeView;
        }
    }
}
