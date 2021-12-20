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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            try
           
            {
                db.openConnection();
                MySqlCommand com = new MySqlCommand("SELECT * from `user` where `login` = @uP AND `password` = @uP", db.getConnection());
                com.Parameters.Add("@uL", MySqlDbType.String).Value = textBox1.Text;
                com.Parameters.Add("@uP", MySqlDbType.String).Value = textBox2.Text;
                adapter.SelectCommand = com;
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    MessageBox.Show("Authorization successful ");
                    Main main = new Main();
                    main.Show();
                    db.closeConnection();
                    this.Hide();

                }
                else
                    MessageBox.Show("Incorrect user login or password");
            }

            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
