﻿using System;
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
    public partial class TableEmployees : Form
    {
        public TableEmployees()
        {
            InitializeComponent();
        }
        private readonly Connection connect = new Connection();//Для отображения, вставки, обновления, удаления данных в таблице
        private readonly Checking checking = new Checking();//Для проверки ячеек на вредные запросы и пустоту значений

        #region Загрузка формы и отображения таблицы 
        private void TableEmployees_Load(object sender, EventArgs e)
        {
            connect.ShowTable("sql7150982", "employees", dataGridView1);//grocery_supermarket_manager
        }
        #endregion

        #region Вставка данных в таблицу
        private void InsertData_Click(object sender, EventArgs e)
        {
            //возвращаем результаты проверок всех полей
            bool resultSecurity = checking.SecurityAll(textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, textBox9, textBox10, textBox11),
                resultVoid = checking.VoidAll(textBox1, textBox2, textBox3, textBox5, textBox8); //Проверяем только обязательные для ввода поля
            //если результаты вернулись положительные, тогда можно добавить данные, иначе вывести ошибку
            if (resultSecurity == true && resultVoid == true)
            {
                string[] fieldsTable = { "id_department", "full_name", "position", "experience", "passport_id", "address", "phone_number", "started_work", "fired", "age", "photo" };
            connect.InsertDataTable("sql7150982", "employees", fieldsTable, textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8, textBox9, textBox10, textBox11);
            }//grocery_supermarket_manager
            else
            {
                checking.ErrorMessage(this);
            }
        }
        #endregion

        #region Обновление данных в таблице
        private void UpdateData_Click(object sender, EventArgs e)
        {
            bool resultSecurity = checking.SecurityAll(textBox12, textBox13, textBox14, textBox15, textBox16, textBox17, textBox18, textBox19, textBox20, textBox21, textBox22, textBox23),
                resultVoid = checking.VoidAll(textBox12, textBox13, textBox14, textBox16, textBox19, textBox23); 
            if (resultSecurity == true && resultVoid == true)
            {
                string[] fieldsTable = { "id_department", "full_name", "position", "experience", "passport_id", "address", "phone_number", "started_work", "fired", "age", "photo", "id_employee" };
            connect.UpdateDataTable("sql7150982", "employees", fieldsTable, textBox12, textBox13, textBox14, textBox15, textBox16, textBox17, textBox18, textBox19, textBox20, textBox21, textBox22, textBox23);
            }//grocery_supermarket_manager
            else
            {
                checking.ErrorMessage(this);
            }
        }
        #endregion

        #region Удаление данных в таблице
        private void DeleteData_Click(object sender, EventArgs e)
        {
            bool resultSecurity = checking.SecurityAll(textBox24),
                resultVoid = checking.VoidAll(textBox1, textBox24); 

            if (resultSecurity == true && resultVoid == true)
            {
                string[] fieldsTable = { "id_employee" };
            connect.DeleteDataTable("sql7150982", "employees", fieldsTable, textBox24);
            }//grocery_supermarket_manager
            else
            {
                checking.ErrorMessage(this);
            }
        }
        #endregion

    }
}
