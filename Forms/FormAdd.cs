using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vaccine.DB;
using System.Data.SqlClient;
using Vaccine.Models;

namespace Vaccine
{
    
    public partial class FormAdd : Form
    {
        DataBase database = new DataBase();
        int secelctRow;

        public FormAdd()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void FormAdd_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataBase.openConnection();
            var full_name = textBox1.Text;
            var inn = textBox2.Text;
            var kpp = textBox3.Text;
            var org_address = textBox4.Text;
            var org_type = textBox5.Text;
            int  locality_id;
            if(int.TryParse(textBox6.Text,out locality_id))
            {
                var addQuery = $"INSERT into organizaton (full_name, inn, kpp, org_address, org_type,locality_id) " +
                    $"values ('{full_name}', '{inn}','{kpp}', '{org_address}', " +
                    $"'{org_type}','{locality_id}')";
                var command = new SqlCommand(addQuery, DataBase.getConnection());
                command.ExecuteNonQuery();
                MessageBox.Show("Запись создана", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Не удалось создать запись", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            DataBase.closeConnection();
        }
    }
}
