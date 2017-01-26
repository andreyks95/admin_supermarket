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

                //для comboBox1
                MySqlDataReader reader = connect.command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox1.Items.Add(reader.GetValue(1)); //Получаем все значения products.name
                }

                connect.CloseConnection();
                

                //для comboBox2
                connect.OpenConnection();

                selectQuery = "select trim(trailing ')'                    " +
                              "    from trim(leading '('                  " +
                              "    from trim(leading 'enum'               " +
                              "    from column_type))) column_type        " +
                              "    from information_schema.COLUMNS        " +
                              "    where TABLE_SCHEMA = 'supermarket'     " +
                              "    AND TABLE_NAME = 'products'            " +
                              "    AND COLUMN_NAME = 'category';          ";
                connect.command = new MySqlCommand(selectQuery, connect.connection);

                string enumCategory  = connect.command.ExecuteScalar().ToString(); //вид перечислений будет таким 'text','text2'
                char delimiter = '\''; 
                string[] substrings = enumCategory.Split(delimiter); //массив строк будет таким "text" "," "text2"
                enumCategory = "";
                foreach (var str in substrings)
                    enumCategory += str; //объединяем массив в строку чтобы потом удалить , будет таким  "text,text2"
                delimiter = ','; 
                substrings = enumCategory.Split(delimiter); //массив строк будет таки "text" "text2"
                foreach(var x in substrings)
                    comboBox2.Items.Add(x); //Получаем все значения products.name

                


                connect.CloseConnection();

                //для textBox8 и textBox9 получаем значения из ячеек

                //НОВЫЙ СПОСОБ СДЕЛАТЬ ПОЛЕ CALCULATED
                //БЫСТРЕЕ!!!
                connect.OpenConnection();

                selectQuery = " SELECT price_for_one            " +
                              "   FROM supermarket.products     " +
                              "   where id_products = 2;        ";
                connect.command = new MySqlCommand(selectQuery, connect.connection);
                var valueCell_1 = connect.command.ExecuteScalar();
                textBox8.Text = valueCell_1.ToString();

                selectQuery = " SELECT quantity             "+
                              "   FROM supermarket.stock    "+
                              "  WHERE id_stock = 1;        ";
                connect.command = new MySqlCommand(selectQuery, connect.connection);
                var valueCell_2 = connect.command.ExecuteScalar();
                textBox9.Text = valueCell_2.ToString();

                float v1 = Convert.ToSingle(valueCell_1),
                    v2 = Convert.ToSingle(valueCell_2);

                float result = v1 * v2;
                label13.Text = result.ToString();

                selectQuery = " UPDATE supermarket.stock AS T1 " +
                              " SET T1.price = " + result.ToString() + " " +
                              " WHERE T1.id_stock = 1;         ";
                     
                connect.command = new MySqlCommand(selectQuery, connect.connection);
                connect.command.ExecuteScalar();
                
                selectQuery = " SELECT price            " +
                              "  FROM supermarket.stock " +
                              "  where id_stock = 1;    ";
                connect.command = new MySqlCommand(selectQuery, connect.connection);
                string valueShow = connect.command.ExecuteScalar().ToString();
                label14.Text = valueShow;

                


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

        private void button5_Click(object sender, EventArgs e)
        {
            Settings set = new Settings();
            //set.GetText(textBox10);

            
            //присвоить label текущий выбранные текст в comboBox
            //использовать try catch если пользователь выбрал
            //label16.Text = comboBox2.SelectedItem.ToString();
            //label17.Text = comboBox2.SelectedText.ToString();
        }
    }
}
