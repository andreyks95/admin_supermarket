using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Administrator_supermarket;
using MySql.Data.MySqlClient;

namespace Administrator_company.Preview__Test_
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            try
            {
                Connection connect = new Connection();
                //MySqlConnection connection = new MySqlConnection("datasource=localhost; port=3306; username = root; password = andrey_1a6c2b");
                string selectQuery = "SELECT * FROM supermarket.products";
                connect.connection.Open();
                connect.command = new MySqlCommand(selectQuery, connect.connection);
                MySqlDataReader reader = connect.command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetValue(1)); //Получаем все значения products.name
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
