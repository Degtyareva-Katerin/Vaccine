using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Vaccine.DB;
using Vaccine.Models;
using Vaccine.Repository;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Vaccine
{
    public partial class AuthForm : Form
    {
        DataBase dataBase = new DataBase();
        UserRepository userRepository = new UserRepository();

        public AuthForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        public void Stop()
        {
            this.Close();
        }
        private void auth_load(object sender, EventArgs e)
        {
            password_user.PasswordChar = '*';
            password_user.MaxLength = 50;
            login_user.MaxLength = 40;
        }

        private void login_user_TextChanged(object sender, EventArgs e)
        {

        }

        private void password_user_TextChanged(object sender, EventArgs e)
        {

        }

        private void auth_button_click(object sender, EventArgs e)
        {
            string login = login_user.Text;
            string password = password_user.Text;

            DataTable tableUser = UserRepository.queryAuthDB(login, password);

            if (tableUser.Rows.Count == 1)
            {
              
                CurrentUser user = CurrentUser.getInstance();

                foreach (DataRow row in tableUser.Rows)
                {
                    user.id = Convert.ToInt32(row[0]);
                    user.fio = row[1].ToString();
                    user.login = row[2].ToString();
                    user.password = row[3].ToString();
                    user.email = row[4].ToString();
                    user.role = row[5].ToString() ?? "";
                    user.organizationId = row[5] is object ? 0 : 0;

                    break;
                }

                MessageBox.Show("Вы успешно вошли!", "Успешно!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //
                this.Visible = false;
                Form fMain = new Main();
                fMain.ShowDialog();
                this.Close();
                //
            }
            else
            {
                MessageBox.Show("Такого аккаунта не существует", "Аккаунта не существует", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    }
}
