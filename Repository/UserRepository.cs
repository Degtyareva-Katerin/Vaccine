using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaccine.DB;
using Vaccine.Models;

namespace Vaccine.Repository
{
    internal class UserRepository
    {
        private CurrentUser user;

        public UserRepository ()
        {

        }

        public CurrentUser setCurrentUser(CurrentUser user)
        {

            return user;
        }

        public static DataTable queryAuthDB(string loginUser, string passUser)
        {
            string sql = $"SELECT * FROM all_users WHERE user_login = '{loginUser}' AND user_password = '{passUser}'";

            DataTable table = DataBase.selectFromDB(sql);

            return table;
        }
    }
}
