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
    public partial class Books : Form
    {
        private MySqlConnection sqlConnection;
        private MySqlDataReader sqlDataReader;
        private MySqlCommand sqlCommand;
        private MySqlDataAdapter sqlAdapter;
        private string idauthor;
        private string idnovel;

        public Books()
        { 
            InitializeComponent();
            
            sqlConnection = new MySqlConnection(" server= localhost; port= 3306; username= root; password= 1234; database=library");
            try
            {
                sqlConnection.Open();
                FillDataGridView1();
                addb.Visible = true;
                button4.Visible = false;

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

        public void FillDataGridView1()
        {
            
            dataGridView1.Rows.Clear();
            sqlCommand = new MySqlCommand(@"select novel.nameof,genre,name,surname,country,nowTotalBooks,totalBooks from novel 
                inner join author on novel.idauthor = author.idauthor 
                inner join book on novel.idnovel = book.idnovel ", sqlConnection);

            sqlAdapter = new MySqlDataAdapter();
            sqlAdapter.SelectCommand = sqlCommand;

            try
            {
                
                sqlDataReader = sqlCommand.ExecuteReader();
                UInt16 idx = 0;
                while (sqlDataReader.Read())
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[idx].Cells[0].Value = sqlDataReader["nameof"].ToString();
                    dataGridView1.Rows[idx].Cells[1].Value = sqlDataReader["genre"].ToString();
                    dataGridView1.Rows[idx].Cells[2].Value = sqlDataReader["name"].ToString();
                    dataGridView1.Rows[idx].Cells[3].Value = sqlDataReader["surname"].ToString();
                    dataGridView1.Rows[idx].Cells[4].Value = sqlDataReader["country"].ToString();
                    dataGridView1.Rows[idx].Cells[5].Value = sqlDataReader["nowTotalBooks"].ToString();
                    dataGridView1.Rows[idx].Cells[6].Value = sqlDataReader["totalBooks"].ToString();
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

        private void button1_Click(object sender, EventArgs e)
        {
            Main main = new Main();
            this.Close();
            main.Show();
        }

        

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.genreTableAdapter1.FillBy(this.libraryDataSet1.genre);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void addb_Click(object sender, EventArgs e)
        {
            try
            {
               
                MySqlCommand c2 = new MySqlCommand("insert `author` (name,surname,country) values (@na,@su,@co)", sqlConnection);
                c2.Parameters.Add("@na", MySqlDbType.String).Value = textBox2.Text;
                c2.Parameters.Add("@su", MySqlDbType.String).Value = textBox4.Text;
                c2.Parameters.Add("@co", MySqlDbType.String).Value = textBox5.Text;
                c2.ExecuteNonQuery();
                    MySqlCommand c4 = new MySqlCommand("select idauthor from author where name = @na and surname = @su", sqlConnection);
                    c4.Parameters.Add("@na", MySqlDbType.String).Value = textBox2.Text;
                    c4.Parameters.Add("@su", MySqlDbType.String).Value = textBox4.Text;
                    c4.ExecuteNonQuery();
                    idauthor = c4.ExecuteScalar().ToString();
                        MySqlCommand c1 = new MySqlCommand("insert `novel` (nameof,idauthor,genre) values (@na,@id,@ee)", sqlConnection);
                        c1.Parameters.Add("@na", MySqlDbType.String).Value = textBox1.Text;
                        c1.Parameters.Add("@id", MySqlDbType.String).Value = idauthor;
                        c1.Parameters.Add("@ee", MySqlDbType.String).Value = textBox6.Text;
                        c1.ExecuteNonQuery();
                            MySqlCommand c5 = new MySqlCommand("select idnovel from novel where nameof = @na", sqlConnection);
                            c5.Parameters.Add("@na", MySqlDbType.String).Value = textBox1.Text;
                            c5.ExecuteNonQuery();
                            idnovel = c5.ExecuteScalar().ToString();
                                MySqlCommand c3 = new MySqlCommand("insert `book` (idnovel,totalBooks,nowTotalBooks) values (@id,@co,@cc)", sqlConnection);
                                c3.Parameters.Add("@id", MySqlDbType.String).Value = idnovel;
                                c3.Parameters.Add("@co", MySqlDbType.String).Value = textBox3.Text;
                                c3.Parameters.Add("@cc", MySqlDbType.String).Value = textBox3.Text;
                                c3.ExecuteNonQuery();

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

        

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlCommand c4 = new MySqlCommand("select idauthor from author where name = @na and surname = @su", sqlConnection);
                c4.Parameters.Add("@na", MySqlDbType.String).Value = textBox2.Text;
                c4.Parameters.Add("@su", MySqlDbType.String).Value = textBox4.Text;
                c4.ExecuteNonQuery();
                idauthor = c4.ExecuteScalar().ToString();
                    MySqlCommand c1 = new MySqlCommand("insert `novel` (nameof,idauthor,genre) values (@na,@id,@ee)", sqlConnection);
                    c1.Parameters.Add("@na", MySqlDbType.String).Value = textBox1.Text;
                    c1.Parameters.Add("@id", MySqlDbType.String).Value = idauthor;
                    c1.Parameters.Add("@ee", MySqlDbType.String).Value = textBox6.Text;
                    c1.ExecuteNonQuery();
                        MySqlCommand c5 = new MySqlCommand("select idnovel from novel where nameof = @na", sqlConnection);
                        c5.Parameters.Add("@na", MySqlDbType.String).Value = textBox1.Text;
                        c5.ExecuteNonQuery();
                        idnovel = c5.ExecuteScalar().ToString();
                            MySqlCommand c3 = new MySqlCommand("insert `book` (idnovel,totalBooks, nowTotalBooks) values (@id,@co, @cc)", sqlConnection);
                            c3.Parameters.Add("@id", MySqlDbType.String).Value = idnovel;
                            c3.Parameters.Add("@co", MySqlDbType.String).Value = textBox3.Text;
                            c3.Parameters.Add("@cc", MySqlDbType.String).Value = textBox3.Text;
                            c3.ExecuteNonQuery();

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
        

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                addb.Visible = false;
                button4.Visible = true;
            }
            else
            {
                addb.Visible = true;
                button4.Visible = false;
            }
        }
    }
}
