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
    enum RowState
    {
        Exited,
        New,
        Modified,
        ModifiedNew,
        Deleted
    }
    public partial class Main : Form
    {
        DataBase database = new DataBase();
        int secelctRow;
        
        public Main()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
        }
        private void CreateColumn()
        {
            dataGridView1.Columns.Add("id", "id");
            dataGridView1.Columns.Add("full_name", "Полное название");
            dataGridView1.Columns.Add("inn", "ИНН");
            dataGridView1.Columns.Add("kpp", "КПП");
            dataGridView1.Columns.Add("org_address", "Адрес Орг");
            dataGridView1.Columns.Add("org_type", "Тип орг");
            dataGridView1.Columns.Add("locality_id", "locality_id");
            dataGridView1.Columns.Add("IsNew", string.Empty);
        }

        private void ReadSingleRow(DataGridView dgv, IDataRecord record)
        {
            dgv.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2), record.GetString(3),
                record.GetString(4), record.GetString(5), record.GetInt32(6), RowState.ModifiedNew);
        }

        private void RefreshDataGrid(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string queryString = $"SELECT * FROM organizaton";
            SqlCommand command = new SqlCommand(queryString, DataBase.getConnection());//?database
            DataBase.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                ReadSingleRow(dgv, reader);
            }
            reader.Close();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            CreateColumn();
            RefreshDataGrid(dataGridView1);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            secelctRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[secelctRow];
                label2.Text = row.Cells[0].Value.ToString();
                textBox1.Text = row.Cells[1].Value.ToString();
                textBox2.Text = row.Cells[2].Value.ToString();
                textBox3.Text = row.Cells[3].Value.ToString();
                textBox4.Text = row.Cells[4].Value.ToString();
                textBox5.Text = row.Cells[5].Value.ToString();
                textBox6.Text = row.Cells[6].Value.ToString();
            }
            button2.Visible = true;
            button6.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form fAdd = new FormAdd();
            fAdd.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
            ClearFields();
        }

        private void Search(DataGridView dgv)
        {
            dgv.Rows.Clear();
            string searchString = $"SELECT * FROM organizaton where concat (id, " +
                $"full_name, inn, kpp, org_address, org_type,locality_id) like '%"+textBox7.Text+"%'";
            SqlCommand com = new SqlCommand(searchString, DataBase.getConnection());
            DataBase.openConnection();
            SqlDataReader read = com.ExecuteReader();
            while (read.Read())
            {
                ReadSingleRow(dgv, read);
            }
            read.Close();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Search(dataGridView1);
        }

        private void deleteRow()
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.Rows[index].Visible = false;
            if (dataGridView1.Rows[index].Cells[0].Value.ToString() == string.Empty)
            {
                dataGridView1.Rows[index].Cells[7].Value = RowState.Deleted;
                return;
            }
            dataGridView1.Rows[index].Cells[7].Value = RowState.Deleted;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            deleteRow();
        }

        private void Update()
        {
            DataBase.openConnection();
            for( int index=0; index < dataGridView1.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView1.Rows[index].Cells[7].Value;
                if (rowState == RowState.Exited)
                    continue;
                if (rowState == RowState.Deleted)
                {
                    var id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                    var deleteQuery = $"DELETE FROM organizaton where id ={id}";
                    var command = new SqlCommand(deleteQuery, DataBase.getConnection());
                    command.ExecuteNonQuery();
                }
                if (rowState == RowState.Modified)
                {
                    var id = dataGridView1.Rows[index].Cells[0].Value.ToString();
                    var full_name = dataGridView1.Rows[index].Cells[1].Value.ToString();
                    var inn = dataGridView1.Rows[index].Cells[2].Value.ToString();
                    var kpp = dataGridView1.Rows[index].Cells[3].Value.ToString();
                    var org_address = dataGridView1.Rows[index].Cells[4].Value.ToString();
                    var org_type = dataGridView1.Rows[index].Cells[5].Value.ToString();
                    var locality_id = dataGridView1.Rows[index].Cells[6].Value.ToString();
                    var changeQuery = $"UPDATE organizaton set full_name = '{full_name}'," +
                        $"inn = '{inn}',kpp = '{kpp}'," +
                        $"org_address = '{org_address}',org_type = '{org_type}', locality_id = '{locality_id}' " +
                        $"where id='{id}'";
                    var command = new SqlCommand(changeQuery, DataBase.getConnection());
                    command.ExecuteNonQuery();
                }
            }
            DataBase.closeConnection();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            /*RefreshDataGrid(dataGridView1);
            ClearFields();*/
            Update();
            ClearFields();
        }

        private void Change()
        {
            var selectedRowIndex = dataGridView1.CurrentCell.RowIndex;
            var id = label2.Text;
            var full_name = textBox1.Text;
            var inn = textBox2.Text;
            var kpp = textBox3.Text;
            var org_address = textBox4.Text;
            var org_type = textBox5.Text;
            int locality_id;
            if (dataGridView1.Rows[selectedRowIndex].Cells[0].Value.ToString() != string.Empty)
            {
                if(int.TryParse(textBox6.Text,out locality_id))
                {
                    dataGridView1.Rows[selectedRowIndex].SetValues(id, full_name, inn, kpp, org_address, org_type,
                        locality_id);
                    dataGridView1.Rows[selectedRowIndex].Cells[7].Value = RowState.Modified;
                }
                else
                {
                    MessageBox.Show("locality_id должно иметь числовой формат");
                }
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Change();
            ClearFields();
        }
        private void ClearFields()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            label2.Text = "none";
            button2.Visible = false;
            button6.Visible = false;

        }
    }
}
