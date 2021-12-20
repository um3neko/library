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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            dataGridView1.Visible = false;
            dataGridView2.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            

            sqlConnection = new MySqlConnection("server= localhost; port= 3306; username= root; password= 1234; database=library");
            try
            {
                sqlConnection.Open();
                FillDataGridView1();
                FillDataGridView2();
            }
            catch (System.Exception exc)
            {
                MessageBox.Show(exc.Message, exc.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private int a = 1;
        private int b = 1;
        private MySqlConnection sqlConnection;
        private MySqlDataReader sqlDataReader;
        private MySqlCommand sqlCommand;
        private MySqlDataAdapter sqlAdapter;
        private string users;

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Books books = new Books();
            books.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Customer c = new Customer();
            c.Show();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Zakazy z = new Zakazy();
            z.Show();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            label3.Visible = true;
            label2.Visible = false;

            dataGridView1.Visible = false;
            dataGridView2.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label3.Visible = false;
            label2.Visible = true;

            dataGridView1.Visible = true;
            dataGridView2.Visible = false;

        }
        public void FillDataGridView1()
        {
            dataGridView2.Rows.Clear();
            sqlCommand = new MySqlCommand(@"select idclaim, nameof, name,surname,dateofissue, returndate, phone,  countOfTaked, isReturned from claim
                                                                            inner join book on claim.idbook = book.idbook
                                                                            inner join novel on book.idnovel = novel.idnovel
                                                                            inner join customers on claim.idcustomer = customers.idcustomers", sqlConnection);
            sqlAdapter = new MySqlDataAdapter();
            sqlAdapter.SelectCommand = sqlCommand;
            try
            {
                sqlDataReader = sqlCommand.ExecuteReader();
                UInt16 idx = 0;
                while (sqlDataReader.Read())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[idx].Cells[0].Value = sqlDataReader["idclaim"].ToString();
                    dataGridView1.Rows[idx].Cells[1].Value = sqlDataReader["nameof"].ToString();
                    dataGridView1.Rows[idx].Cells[2].Value = sqlDataReader["name"].ToString();
                    dataGridView1.Rows[idx].Cells[3].Value = sqlDataReader["surname"].ToString();
                    dataGridView1.Rows[idx].Cells[4].Value = sqlDataReader["dateofissue"].ToString();
                    dataGridView1.Rows[idx].Cells[5].Value = sqlDataReader["returndate"].ToString();
                    dataGridView1.Rows[idx].Cells[6].Value = sqlDataReader["phone"].ToString();
                    dataGridView1.Rows[idx].Cells[7].Value = sqlDataReader["countOfTaked"].ToString();
                    dataGridView1.Rows[idx].Cells[8].Value = sqlDataReader["isReturned"].ToString();

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
            sqlCommand = new MySqlCommand(@"select idclaim, nameof, name,surname,dateofissue, returndate, phone,  countOfTaked, isReturned from claim
                                                                            inner join book on claim.idbook = book.idbook
                                                                            inner join novel on book.idnovel = novel.idnovel
                                                                            inner join customers on claim.idcustomer = customers.idcustomers 
                                                                            where returndate < now()", sqlConnection);
            sqlAdapter = new MySqlDataAdapter();
            sqlAdapter.SelectCommand = sqlCommand;
            try
            {
                sqlDataReader = sqlCommand.ExecuteReader();
                UInt16 idx = 0;
                while (sqlDataReader.Read())
                {
                    dataGridView2.Rows.Add();
                    dataGridView2.Rows[idx].Cells[0].Value = sqlDataReader["idclaim"].ToString();
                    dataGridView2.Rows[idx].Cells[1].Value = sqlDataReader["nameof"].ToString();
                    dataGridView2.Rows[idx].Cells[2].Value = sqlDataReader["name"].ToString();
                    dataGridView2.Rows[idx].Cells[3].Value = sqlDataReader["surname"].ToString();
                    dataGridView2.Rows[idx].Cells[4].Value = sqlDataReader["dateofissue"].ToString();
                    dataGridView2.Rows[idx].Cells[5].Value = sqlDataReader["returndate"].ToString();
                    dataGridView2.Rows[idx].Cells[6].Value = sqlDataReader["phone"].ToString();
                    dataGridView2.Rows[idx].Cells[7].Value = sqlDataReader["countOfTaked"].ToString();
                    dataGridView2.Rows[idx].Cells[8].Value = sqlDataReader["isReturned"].ToString();

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

        private void button6_Click(object sender, EventArgs e)
        {
            User u = new User();
            u.Show();
        }
    }
}
