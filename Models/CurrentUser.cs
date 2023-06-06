using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vaccine.Models
{
    internal class CurrentUser
    {
        private static CurrentUser instance;

        public int id;
        public string login;
        public string email;
        public string password;
        public int organizationId;
        public string fio;
        public string role;

        private CurrentUser()
        {

        }

        public static CurrentUser getInstance()
        {
            if (instance == null)
            {
                instance = new CurrentUser();
            }

            return instance;
        }
    }
}
