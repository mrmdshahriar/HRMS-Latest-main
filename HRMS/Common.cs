using HRMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRMS
{
    public class Common
    { 
        
        //Contact Information
        public List<Region> RegionsList = new List<Region>();
        public List<Country> CountriesList = new List<Country>();
        public List<State> StatesList = new List<State>();
        public List<City> CitiesList = new List<City>();

        //Position
        public List<EmployeeGroup> EmployeeGroupList = new List<EmployeeGroup>();
        public List<Designation> DesignationList = new List<Designation>();
        public List<Department> DepartmentList = new List<Department>();
        public List<HrmJobType> JobTypeList = new List<HrmJobType>();
        public List<ShiftMaster> ShiftMasterList = new List<ShiftMaster>();
        //Employee Information
        public List<HrmEmployee> EmployeeList { get; set; } = new List<HrmEmployee>();
        public Terminations Termination { get; set; } = new Terminations();
        //Employee Profile
        public string EmployeeCode { get; set; }

        //Job Requestion
        public List<JobRequisitions> JobRequisitions { get; set; } = new List<JobRequisitions>();

  
    }

}