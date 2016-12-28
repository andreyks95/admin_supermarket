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

namespace Administrator_company
{
    public partial class TableProducts : Form
    {
        public TableProducts()
        {
            InitializeComponent();
        }
        private readonly Connection connect = new Connection();
        private readonly Checking checking = new Checking();
        private void TableProducts_Load(object sender, EventArgs e)
        {
            connect.ShowTable("sql7150982", "products", dataGridView1);//grocery_supermarket_manager
        }

        private void InsertData_Click(object sender, EventArgs e)
        {
            //возвращаем результаты проверок всех полей
            bool resultSecurity = checking.SecurityAll(textBox1, textBox2, textBox3),
                resultVoid = checking.VoidAll(textBox1, textBox2, textBox3); //Проверяем только обязательные для ввода поля
            //если результаты вернулись положительные, тогда можно добавить данные, иначе вывести ошибку
            if (resultSecurity == true && resultVoid == true)
            {
                string[] fieldsTable = { "name", "category", "price_for_one" };
                connect.InsertDataTable("sql7150982", "products", fieldsTable, textBox1, textBox2, textBox3);
            }//grocery_supermarket_manager
            else
            {
                checking.ErrorMessage(this);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            bool resultSecurity = checking.SecurityAll(textBox4, textBox5, textBox6, textBox7),
                resultVoid = checking.VoidAll(textBox4, textBox5, textBox6, textBox7);
            
            if (resultSecurity == true && resultVoid == true)
            {
                string[] fieldsTable = { "name", "category", "price_for_one", "id_products" };
            connect.UpdateDataTable("sql7150982", "products", fieldsTable, textBox4, textBox5, textBox6, textBox7);
            }//grocery_supermarket_manager
            else
            {
                checking.ErrorMessage(this);
            }
        }

        private void DeleteData_Click(object sender, EventArgs e)
        {
            bool resultSecurity = checking.SecurityAll(textBox8),
                resultVoid = checking.VoidAll(textBox8);
            if (resultSecurity == true && resultVoid == true)
            {
                string[] fieldsTable = { "id_products" };
            connect.DeleteDataTable("sql7150982", "products", fieldsTable, textBox8);
            }//grocery_supermarket_manager
            else
            {
                checking.ErrorMessage(this);
            }
        }
    }
}
