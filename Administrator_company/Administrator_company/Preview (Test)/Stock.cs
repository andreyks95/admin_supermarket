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

namespace Administrator_company.Preview__Test_
{
    public partial class Stock : Form
    {
        public Stock()
        {
            InitializeComponent();
        }
        Connection connection = new Connection();//для отображения, записи, изменения, удаления данных в таблице
        Queries queries = new Queries();//для запросов выборки
        private void Stock_Load(object sender, EventArgs e)
        {

            string query = "select products.name, stock.available, stock.entered, stock.sold, stock.quantity, products.measurement, stock.price "+
                           " From supermarket.products, supermarket.stock " +
                           " where supermarket.products.id_products = supermarket.stock.id_products; ";
            connection.ShowTable(dataGridView1, query);
        }
    }
}
