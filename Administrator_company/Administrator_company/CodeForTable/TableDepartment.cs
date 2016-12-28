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

namespace Administrator_company
{
    public partial class TableDepartment : Form
    {
        public TableDepartment()
        {
            InitializeComponent();
        }
        private readonly Connection connect = new Connection();
        private readonly Checking checking = new Checking();
        private void TableDepartment_Load(object sender, EventArgs e)
        {
            connect.ShowTable("sql7150982", "department", dataGridView1);//grocery_supermarket_manager
        }

        private void InsertData_Click(object sender, EventArgs e)
        {
            //возвращаем результаты проверок всех полей
            bool resultSecurity = checking.SecurityAll(textBox1),
                resultVoid = checking.VoidAll(textBox1); //Проверяем только обязательные для ввода поля
            //если результаты вернулись положительные, тогда можно добавить данные, иначе вывести ошибку
            if (resultSecurity == true && resultVoid == true)
            {
                string[] fieldsTable = { "department_name" };
            connect.InsertDataTable("sql7150982", "department", fieldsTable, textBox1);
            }//grocery_supermarket_manager
            else
            {
                checking.ErrorMessage(this);
            }
        }

        private void UpdateData_Click(object sender, EventArgs e)
        {
            //возвращаем результаты проверок всех полей
            bool resultSecurity = checking.SecurityAll(textBox2, textBox3),
                resultVoid = checking.VoidAll(textBox2, textBox3); //Проверяем только обязательные для ввода поля
            //если результаты вернулись положительные, тогда можно добавить данные, иначе вывести ошибку
            if (resultSecurity == true && resultVoid == true)
            {
                string[] fieldsTable = { "department_name", "id_department" };
            connect.UpdateDataTable("sql7150982", "department", fieldsTable, textBox2, textBox3);
            }//grocery_supermarket_manager
            else
            {
                checking.ErrorMessage(this);
            }
        }

        private void DeleteData_Click(object sender, EventArgs e)
        {
            //возвращаем результаты проверок всех полей
            bool resultSecurity = checking.SecurityAll(textBox4),
                resultVoid = checking.VoidAll(textBox4); //Проверяем только обязательные для ввода поля
            //если результаты вернулись положительные, тогда можно добавить данные, иначе вывести ошибку
            if (resultSecurity == true && resultVoid == true)
            {
                string[] fieldsTable = { "id_department" };
            connect.DeleteDataTable("sql7150982", "department", fieldsTable, textBox4);
            }//grocery_supermarket_manager
            else
            {
                checking.ErrorMessage(this);
            }
        }


    }
}
