﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Administrator_supermarket
{
    public partial class TableAdministrator : Form
    {
        public TableAdministrator()
        {
            InitializeComponent();
        }

        //создаём объект класса Connection, где будем иметь доступ ко всем функциям 
        private readonly Connection connect = new Connection();
        private readonly Checking checking = new Checking();


        // List<string> fieldsTable = new List<string> { "full_name", "passport_id", "experience", "address", "phone_number" }; 

        //public object DataGridViewDataSource => dataGridView1.DataSource;
        //Отображение записей в таблице при загрузке
        private void TableAdministrator_Load(object sender, EventArgs e)
        {
                connect.ShowTable("sql7150982", "administrator", dataGridView1);//grocery_supermarket_manager
        }

        //Вставка записей в таблицу
        private void InsertData_Click(object sender, EventArgs e)
        {
            //возвращаем результаты проверок всех полей
            bool resultSecurity = checking.SecurityAll(textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8),
                resultVoid = checking.VoidAll(textBox1, textBox2, textBox3); //Проверяем только обязательные для ввода поля
            //если результаты вернулись положительные, тогда можно добавить данные, иначе вывести ошибку
            if (resultSecurity == true && resultVoid == true)
            {
                //создаём массив из списка полей в таблице "administrator"
                string[] fieldsTable = { "id_department", "full_name", "passport_id", "experience", "address", "phone_number", "age", "photo" };
            connect.InsertDataTable("sql7150982", "administrator", fieldsTable, textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, textBox7, textBox8);
            }//grocery_supermarket_manager
            else
            {
                checking.ErrorMessage(this);
            }
        }   

        private void button1_Click(object sender, EventArgs e)
        {
            //возвращаем результаты проверок всех полей
            bool resultSecurity = checking.SecurityAll(textBox9, textBox10, textBox11, textBox12, textBox13, textBox14, textBox15, textBox16, textBox17),
                resultVoid = checking.VoidAll(textBox9, textBox10, textBox11, textBox17); //Проверяем только обязательные для ввода поля
            //если результаты вернулись положительные, тогда можно добавить данные, иначе вывести ошибку
            if (resultSecurity == true && resultVoid == true)
            {
                string[] fieldsTable = { "id_department", "full_name", "passport_id", "experience", "address", "phone_number", "age", "photo", "id_administrator" };
            connect.UpdateDataTable("sql7150982", "administrator", fieldsTable, textBox9, textBox10, textBox11, textBox12, textBox13, textBox14, textBox15, textBox16, textBox17);
            }//grocery_supermarket_manager
            else
            {
                checking.ErrorMessage(this);
            }
        }

        private void buttonDeleteData_Click(object sender, EventArgs e)
        {
            //возвращаем результаты проверок всех полей
            bool resultSecurity = checking.SecurityAll(textBoxDelete),
                resultVoid = checking.VoidAll(textBoxDelete); //Проверяем только обязательные для ввода поля
            //если результаты вернулись положительные, тогда можно добавить данные, иначе вывести ошибку
            if (resultSecurity == true && resultVoid == true)
            {
                string[] fieldsTable = {"id_administrator"};
            connect.DeleteDataTable("sql7150982", "administrator", fieldsTable, textBoxDelete);
            }//grocery_supermarket_manager
            else
            {
                checking.ErrorMessage(this);
            }
         }
    }
}

