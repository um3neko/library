using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace library
{
    public partial class User : Form
    {
        public User()
        {
            InitializeComponent();
            sqlConnection = new MySqlConnection("server= localhost; port= 3306; username= root; password= 1234; database=library");
            try
            {
                sqlConnection.Open();
                FillDataGridView1();
            }
            catch (System.Exception exc)
            {
                MessageBox.Show(exc.Message, exc.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private MySqlConnection sqlConnection;
        private MySqlDataReader sqlDataReader;
        private MySqlCommand sqlCommand;
        private MySqlDataAdapter sqlAdapter;

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void FillDataGridView1()
        {

            dataGridView1.Rows.Clear();
            sqlCommand = new MySqlCommand(@"select iduser, name,surname,phone from user", sqlConnection);

            sqlAdapter = new MySqlDataAdapter();
            sqlAdapter.SelectCommand = sqlCommand;

            try
            {
                sqlDataReader = sqlCommand.ExecuteReader();
                UInt16 idx = 0;
                while (sqlDataReader.Read())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[idx].Cells[0].Value = sqlDataReader["iduser"].ToString();
                    dataGridView1.Rows[idx].Cells[1].Value = sqlDataReader["name"].ToString();
                    dataGridView1.Rows[idx].Cells[2].Value = sqlDataReader["surname"].ToString();
                    dataGridView1.Rows[idx].Cells[3].Value = sqlDataReader["phone"].ToString();
                    ++idx;
                }
            }
            catch (System.Exception exc)
            {
                MessageBox.Show(exc.Message, exc.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlDataReader != null)
                    sqlDataReader.Close();
            }
        }

        
    }
}
