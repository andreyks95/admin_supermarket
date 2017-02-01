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
    public partial class Stock : Form
    {

        Connection connection = new Connection();//для отображения, записи, изменения, удаления данных в таблице
        DataTable table = new DataTable();


        public Stock()
        {
            InitializeComponent();
        }


        private void Stock_Load(object sender, EventArgs e)
        {
            /*
            string query = "select products.name, stock.available, stock.entered, stock.sold, stock.quantity, products.measurement, stock.price "+
                           " From supermarket.products, supermarket.stock " +
                           " where supermarket.products.id_products = supermarket.stock.id_products; ";
            connection.ShowTable(dataGridView1, query);*/
            SortDG("id_stock");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SortDG(comboBox1.SelectedItem.ToString());
        }

        //можно сделать и с dataTable
        private void SortDG(string value)
        {
            //Инициализация
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand cmd;
            DataView dataView;
            
            //sql 
            string sql = "select * From supermarket.stock";
            cmd = new MySqlCommand(sql, connection.connection);
            adapter.SelectCommand = cmd;

            //Заполнение
            table.Clear();
            adapter.Fill(table);
            dataView = new DataView(table);
            dataView.Sort = value;

            dataGridView1.DataSource = dataView;

        }
    }
}
