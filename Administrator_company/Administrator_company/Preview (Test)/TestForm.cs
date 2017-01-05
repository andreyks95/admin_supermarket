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
        BindingManagerBase managerBase;

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
                //для comboBox-а и для DataGridView
                connect.command = new MySqlCommand(selectQuery, connect.connection);

                MySqlDataAdapter adapter = new MySqlDataAdapter(connect.command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;

                textBox1.DataBindings.Add("text", table, "id_products");
                textBox2.DataBindings.Add("text", table, "name");
                textBox3.DataBindings.Add("text", table, "category");
                textBox4.DataBindings.Add("text", table, "kind");
                textBox5.DataBindings.Add("text", table, "subspecies");
                textBox6.DataBindings.Add("text", table, "price_for_one");
                textBox7.DataBindings.Add("text", table, "measurement");

                
                managerBase = this.BindingContext[table];

                //для comboBox-а
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

        //Начало
        private void button1_Click(object sender, EventArgs e)
        {
            managerBase.Position = 0;
        }
        //Следующий
        private void button2_Click(object sender, EventArgs e)
        {
            managerBase.Position += 1;
        }
        //Предыдущий
        private void button3_Click(object sender, EventArgs e)
        {
            managerBase.Position -= 1;
        }
        //Конец
        private void button4_Click(object sender, EventArgs e)
        {
            managerBase.Position = managerBase.Count;
        }


    }
}
