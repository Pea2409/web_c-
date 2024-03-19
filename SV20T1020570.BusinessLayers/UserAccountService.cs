using SV20T1020570.DataLayers;
using SV20T1020570.DataLayers.SQL_Server;
using SV20T1020570.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV20T1020570.BusinessLayers
{
    public class UserAccountService
    {
        private static readonly IUserAccountDAL employeeAccountDB;
        static UserAccountService()
        {
            employeeAccountDB = new EmployeeAccountDAL(Configuration.ConnectString);
        }

        public static UserAccount? Authorize(string userName, string password)
        {
            
            return employeeAccountDB.Authorize(userName, password);
        }

        public static bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            
            return employeeAccountDB.ChangePassword(userName, oldPassword, newPassword);
        }
        public static bool CheckPassword(string userName, string oldPassword)
        {

            return employeeAccountDB.CheckPassword(userName, oldPassword);
        }
    }
}
