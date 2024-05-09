using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TalenBAL.BALModel;

namespace TalenBAL.BAL
{
    public class BLMaster
    {
        BALHRMS db = new BALHRMS();
        LeaveRequest ObjLeaveRequest = new LeaveRequest();
        public dynamic GetAllCountries()
        {
            var record = db.Countries.Select(s => s).ToList();
            return record;
        }

        public dynamic GetAllCountriesById(long Id)
        {
            var record = db.Countries.Where(s => s.Id == Id).FirstOrDefault();
            return record;
        }

        public dynamic GetAllStates()
        {
            var record = db.States.Select(s => s).ToList();
            return record;
        }

        public dynamic GetAllStatesById(long Id)
        {
            var record = db.States.Where(s => s.Id == Id).FirstOrDefault();
            return record;
        }

        public dynamic GetAllCities()
        {
            var record = db.Cities.Select(s => s).ToList();
            return record;
        }

        public dynamic GetAllCitiesById(long Id)
        {
            var record = db.Cities.Where(s => s.Id == Id).FirstOrDefault();
            return record;
        }
    }
}
