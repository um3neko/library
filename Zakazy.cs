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
    public partial class Zakazy : Form
    {
        public Zakazy()
        {
            InitializeComponent();
            
            addb.Visible = false;
            button6.Visible = false;

            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            

            sqlConnection = new MySqlConnection("server= localhost; port= 3306; username= root; password= 1234; database=library");
            try
            {
                sqlConnection.Open();
                
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
      

        private void button2_Click(object sender, EventArgs e)
        {
            addb.Visible = true;
            button6.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            addb.Visible = false;
            button6.Visible = true;
        }
        public void FillDataGridView1()
        {

            dataGridView1.Rows.Clear();
            sqlCommand = new MySqlCommand(@"select * from customers", sqlConnection);

            sqlAdapter = new MySqlDataAdapter();
            sqlAdapter.SelectCommand = sqlCommand;

            try
            {
                sqlDataReader = sqlCommand.ExecuteReader();
                UInt16 idx = 0;
                while (sqlDataReader.Read())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[idx].Cells[0].Value = sqlDataReader["idcustomers"].ToString();
                    dataGridView1.Rows[idx].Cells[1].Value = sqlDataReader["name"].ToString();
                    dataGridView1.Rows[idx].Cells[2].Value = sqlDataReader["surname"].ToString();
                    ++idx;
                }
            }
            catch (System.Exception exc)
            {
                MessageBox.Show(exc.Message, exc.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlDataReader.Close();
            }
        }
        public void FillDataGridView2()
        {
            dataGridView2.Rows.Clear();
            sqlCommand = new MySqlCommand(@"SELECT idbook, nameof, nowTotalBooks FROM library.book inner join novel on book.idnovel = novel.idnovel", sqlConnection);
            sqlAdapter = new MySqlDataAdapter();
            sqlAdapter.SelectCommand = sqlCommand;
            try
            {
                sqlDataReader = sqlCommand.ExecuteReader();
                UInt16 idx = 0;
                while (sqlDataReader.Read())
                {
                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[idx].Cells[0].Value = sqlDataReader["idbook"].ToString();
                    dataGridView2.Rows[idx].Cells[1].Value = sqlDataReader["nameof"].ToString();
                    dataGridView2.Rows[idx].Cells[2].Value = sqlDataReader["nowTotalBooks"].ToString();

                    ++idx;
                }
            }
            catch (System.Exception exc)
            {
                MessageBox.Show(exc.Message, exc.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlDataReader.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FillDataGridView1();
            dataGridView1.Visible = true;
            dataGridView2.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FillDataGridView2();
            dataGridView1.Visible = false;
            dataGridView2.Visible = true;
        }

        private void addb_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlCommand sqlCommand = new MySqlCommand("insert `claim` (dateofissue,returndate, idbook, idcustomer, iduser) values (@na, @su, @ph, @iu, @ib)", sqlConnection);
                sqlCommand.Parameters.Add("@na", MySqlDbType.DateTime).Value = DateTime.Now;
                sqlCommand.Parameters.Add("@su", MySqlDbType.DateTime).Value = DateTime.Now.AddMonths(1);
                sqlCommand.Parameters.Add("@ph", MySqlDbType.String).Value = textBox1.Text;
                sqlCommand.Parameters.Add("@iu", MySqlDbType.String).Value = textBox2.Text;
                sqlCommand.Parameters.Add("@ib", MySqlDbType.String).Value = textBox3.Text;
                sqlCommand.ExecuteNonQuery();

                MySqlCommand sqlCommand1 = new MySqlCommand("update customers set countOfTaked = countOfTaked + 1  where idCustomers = @df", sqlConnection);
                sqlCommand1.Parameters.Add("@df", MySqlDbType.String).Value = textBox2.Text;
                sqlCommand1.ExecuteNonQuery();
                MessageBox.Show("Ok");

            }
            catch (System.Exception exc)
            {
                MessageBox.Show(exc.Message, exc.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                FillDataGridView2();
                if (sqlDataReader != null)
                sqlDataReader.Close();
                

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlCommand sqlCommand1 = new MySqlCommand("update book set nowTotalBooks = nowTotalBooks + 1  where idbook = @ph", sqlConnection);
                sqlCommand1.Parameters.Add("@ph", MySqlDbType.String).Value = textBox1.Text;
                sqlCommand1.ExecuteNonQuery();

                MySqlCommand sqlCommand2 = new MySqlCommand("update customers set isReturned = isReturned + 1  where idCustomers = @sd", sqlConnection);
                sqlCommand2.Parameters.Add("@sd", MySqlDbType.String).Value = textBox2.Text;
                sqlCommand2.ExecuteNonQuery();

                MessageBox.Show("Ok");
            }
            catch (System.Exception exc)
            {
                MessageBox.Show(exc.Message, exc.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                FillDataGridView2();
                if (sqlDataReader != null)
                sqlDataReader.Close();

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            this.Close();
            main.Show();
        }
    }
}
