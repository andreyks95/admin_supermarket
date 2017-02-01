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

        private readonly Connection connect = new Connection(); //Для отображения, вставки, обновления, удаления данных в таблице
        private readonly Checking checking = new Checking(); //Для проверки ячеек на вредные запросы и пустоту значений

        #region Загрузка формы и отображения таблицы 
        private void TableDepartment_Load(object sender, EventArgs e)
        {
            connect.ShowTable("grocery_supermarket_manager", "department", dataGridView1);//grocery_supermarket_manager //sql7150982
        }
        #endregion

        #region Вставка данных в таблицу
        private void InsertData_Click(object sender, EventArgs e)
        {
            //возвращаем результаты проверок всех полей
            bool resultSecurity = checking.SecurityAll(textBox1),
                resultVoid = checking.VoidAll(textBox1); //Проверяем только обязательные для ввода поля
            //если результаты вернулись положительные, тогда можно добавить данные, иначе вывести ошибку
            if (resultSecurity == true && resultVoid == true)
            {
                string[] fieldsTable = { "department_name" };
            connect.InsertDataTable("grocery_supermarket_manager", "department", fieldsTable, textBox1);//grocery_supermarket_manager //sql7150982
            }
            else
            {
                checking.ErrorMessage(this);
            }
        }
        #endregion

        #region Обновление данных в таблице
        private void UpdateData_Click(object sender, EventArgs e)
        {
            //возвращаем результаты проверок всех полей
            bool resultSecurity = checking.SecurityAll(textBox2, textBox3),
                resultVoid = checking.VoidAll(textBox2, textBox3); //Проверяем только обязательные для ввода поля
            //если результаты вернулись положительные, тогда можно добавить данные, иначе вывести ошибку
            if (resultSecurity == true && resultVoid == true)
            {
                string[] fieldsTable = { "department_name", "id_department" };
            connect.UpdateDataTable("grocery_supermarket_manager", "department", fieldsTable, textBox2, textBox3);//grocery_supermarket_manager //sql7150982
            }
            else
            {
                checking.ErrorMessage(this);
            }
        }
        #endregion

        #region Удаление данных в таблице
        private void DeleteData_Click(object sender, EventArgs e)
        {
            //возвращаем результаты проверок всех полей
            bool resultSecurity = checking.SecurityAll(textBox4),
                resultVoid = checking.VoidAll(textBox4); //Проверяем только обязательные для ввода поля
            //если результаты вернулись положительные, тогда можно добавить данные, иначе вывести ошибку
            if (resultSecurity == true && resultVoid == true)
            {
                string[] fieldsTable = { "id_department" };
            connect.DeleteDataTable("grocery_supermarket_manager", "department", fieldsTable, textBox4);//grocery_supermarket_manager //sql7150982
            }
            else
            {
                checking.ErrorMessage(this);
            }
        }
        #endregion


    }
}
