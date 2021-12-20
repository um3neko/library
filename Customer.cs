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
    public partial class Customer : Form
    {
        private MySqlConnection sqlConnection;
        private MySqlDataReader sqlDataReader;
        private MySqlCommand sqlCommand;
        private MySqlDataAdapter sqlAdapter;

        public Customer()
        {
            InitializeComponent();
            textBox1.Visible = false;
            textBox2.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            maskedTextBox1.Visible = false;
            addb.Visible = false;
            deleteb.Visible = false;

            label5.Visible = false;
            textBox3.Visible = false;

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

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Visible = true;
            textBox2.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            maskedTextBox1.Visible = true;
            addb.Visible = true;
            deleteb.Visible = false;
            label5.Visible = false;
            textBox3.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label5.Visible = true;
            textBox3.Visible = true;
            textBox1.Visible = false;
            textBox2.Visible = false;
            maskedTextBox1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            addb.Visible = false;
            deleteb.Visible = true;
        }

        public void FillDataGridView1()
        {
            
            dataGridView1.Rows.Clear();
            sqlCommand = new MySqlCommand(@"select idcustomers, name,surname,phone from customers", sqlConnection);

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

        private void addb_Click(object sender, EventArgs e)
        {
            try
            {
                sqlCommand = new MySqlCommand("insert `customers` (name,surname,phone,countOfTaked, isReturned) values (@na, @su, @ph,@aa,@ab)", sqlConnection);
                sqlCommand.Parameters.Add("@na", MySqlDbType.String).Value = textBox1.Text;
                sqlCommand.Parameters.Add("@su", MySqlDbType.String).Value = textBox2.Text;
                sqlCommand.Parameters.Add("@ph", MySqlDbType.String).Value = maskedTextBox1.Text;
                sqlCommand.Parameters.Add("@aa", MySqlDbType.Int32).Value = 0;
                sqlCommand.Parameters.Add("@ab", MySqlDbType.Int32).Value = 0;
                sqlAdapter = new MySqlDataAdapter();
                sqlAdapter.SelectCommand = sqlCommand;
                sqlCommand.ExecuteNonQuery();
            }
            catch (System.Exception exc)
            {
                MessageBox.Show(exc.Message, exc.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlDataReader != null)
                    sqlDataReader.Close();
                FillDataGridView1();

            }

        }

        private void deleteb_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Sure", "Some Title", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    sqlCommand = new MySqlCommand("delete from customers where idcustomers = @id", sqlConnection);
                    sqlCommand.Parameters.Add("@id", MySqlDbType.String).Value = textBox3.Text;
                    sqlAdapter = new MySqlDataAdapter();
                    sqlAdapter.SelectCommand = sqlCommand;
                    sqlCommand.ExecuteNonQuery();
                    
                }
                else if (dialogResult == DialogResult.No)
                {
                    MessageBox.Show("=)");
                    
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
                FillDataGridView1();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Main m = new Main();
            m.Show();
            this.Close();
        }

        
    }
}
