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
using Vaccine.Models;

namespace Vaccine
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            string sql = $"SELECT * FROM organizaton";
            DataTable tableOrg = DataBase.selectFromDB(sql);
            /*int k_Org = tableOrg.Rows.Count;//хранение информации о кол-ве
            for (int k = 0; k < k_Org; k++) //заполнение DGV
            {
                dataGridView1.Rows.Add(tableOrg.Rows[k].ToString(), tableOrg.Rows[k].ToString(), 
                    tableOrg.Rows[k].ToString(), tableOrg.Rows[k].ToString(),
                    tableOrg.Rows[k].ToString(), tableOrg.Rows[k].ToString(), tableOrg.Rows[k].ToString());
            }*/
            foreach (DataRow row in tableOrg.Rows)
            {
                dataGridView1.Rows.Add(row[0].ToString(), row[1].ToString(), row[2].ToString(), row[3].ToString(),
                    row[4].ToString(), row[5].ToString(), row[6].ToString());
            }
           

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int cr = dataGridView1.CurrentRow.Index;
            button6.Show();
            button7.Show();
            string sql = $"SELECT * FROM organizaton WHERE id= '{cr}'";
            DataTable tableCurOrg = DataBase.selectFromDB(sql);
            //label2.Text = tableCurOrg.Rows[0].ToString();
            foreach (DataRow row in tableCurOrg.Rows)
            {
                label2.Text = row[1].ToString();
                MessageBox.Show("kolyan_учит");
            }
            

        }
    }
}
