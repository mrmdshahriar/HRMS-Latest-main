using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalenBAL.BALModel;

namespace TalenBAL.User
{
    public class BLUserLogin
    {
        BALHRMS db = new BALHRMS();
        public dynamic LoginUser(string UserLoginId , string Password)
        {
            var record = db.Users.Where(x => x.LoginId == UserLoginId && x.Password == Password && x.IsActive == true).FirstOrDefault();
            return record;
        }

        public dynamic GetUserTypeById(long UserTypeId)
        {
            var record = db.UserTypes.Where(x => x.Id == UserTypeId).FirstOrDefault();
            return record;
        }
    }
}
