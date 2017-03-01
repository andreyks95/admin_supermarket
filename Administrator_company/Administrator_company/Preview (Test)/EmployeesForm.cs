﻿using System;
using System.Data;
using System.Windows.Forms;
using Administrator_supermarket;
using MySql.Data.MySqlClient;

namespace Administrator_company.Preview__Test_
{
    public partial class EmployeesForm : Form
    {
        public EmployeesForm()
        {
            InitializeComponent();
        }

        Connection connection = new Connection();
        Settings settings = new Settings();
        Checking checking = new Checking();
        BindingManagerBase managerBase; //для перемещения по таблице 

        private static string nameTable = "employees", //таблица
                             id_field = "id_employee"; //id поле таблицы
        private static string[] nameFieldsAll = { "id_employee", "id_info", "id_position", "department", "experience", "salary", "started_work", "fired" }, //все поля
                                variables =     { "@id", "@id_info", "@id_position", "@department", "@experience", "@salary", "@started_work", "@fired" }, //для переменных
                                nameFieldsAS =  { "ИД", "ФИО сотрудника", "Должность", "Отдел", "Опыт", "Зарплата", "Принят", "Уволен" }, //как будут отображаться
                                numericFields = { "id_employee", "id_info", "id_position", "experience", "salary" }; //для корректного поиска по числовым столбцам                                                    
        private static MySqlDbType[] mySqlDbTypes = { MySqlDbType.UInt32, MySqlDbType.UInt32, MySqlDbType.UInt32, MySqlDbType.Enum, MySqlDbType.UInt32, MySqlDbType.UInt32, MySqlDbType.Date, MySqlDbType.Date };//для типов в (AddParameters)

        private void EmployeesForm_Load(object sender, EventArgs e)
        {
            FillDataGridView(""); //при загрузке отображаем таблицу
        }

        //Заполняем DataGridView и корректируем её
        public void FillDataGridView(string valueToSearch)
        {
            //получаем запрос на отображение данных с поиском
            string query = connection.GetQueryShowSearch(nameTable, nameFieldsAll, nameFieldsAS, numericFields, valueToSearch);
            DataTable table = connection.FillDataGridView(dataGridView1, 20, query: query); //заполняем таблицу данными с запроса и настраиваем
            managerBase = this.BindingContext[table]; //подключаем таблицу для передвижения по ней
        }

        private void Insert_Click(object sender, EventArgs e)
        {
            TextBox[] textBoxs = {textBox1, textBox2, textBox3};
            ComboBox[] comboBoxs = {comboBox1, comboBox2, comboBox3 };
            DateTimePicker[] dateTimePickers = {dateTimePicker1, dateTimePicker2 };

            //возвращаем результаты проверок всех полей
            bool resultSecurity = checking.SecurityAll(textBoxs, comboBoxs, dateTimePickers),
                 resultVoid = checking.VoidAll(textBoxs, comboBoxs, dateTimePickers); //Проверяем только обязательные для ввода поля
                                                                                     //если результаты вернулись положительные, тогда можно добавить данные, иначе вывести ошибку
            if (resultSecurity == true && resultVoid == true)
            {
                //получить запрос для вставки Insert
                string query = connection.GetQueryInsert(nameTable, nameFieldsAll, variables);
                //выполняить команду с Insert
                connection.command = new MySqlCommand(query, connection.connection);
                //для объектов, у них есть данные которые нужно вставить
                //ОБЯЗАТЕЛЬНО!
                //Писать объекты подобно расположению на форме 
                //В такой же последовательности
                //Переменная должна соответствувать требуемому значению 
                object[] objects = { textBox1, comboBox1, comboBox2, comboBox3, textBox2, textBox3, dateTimePicker1, dateTimePicker2 };
                //Добавляем данные 
                connection.AddParameters(connection.command, variables, mySqlDbTypes, objects);
                //попытаться выполнить запрос
                connection.ExecuteQuery(connection.command);
                //отобразить новые данные 
                FillDataGridView("");
                
            }
            else
            {
                checking.ErrorMessage(this); //вывести ошибку
            }
        }

        private void Update_Click(object sender, EventArgs e)
        {
            TextBox[] textBoxs = { textBox1, textBox2, textBox3 };
            ComboBox[] comboBoxs = { comboBox1, comboBox2, comboBox3 };
            DateTimePicker[] dateTimePickers = { dateTimePicker1, dateTimePicker2 };

            //возвращаем результаты проверок всех полей
            bool resultSecurity = checking.SecurityAll(textBoxs, comboBoxs, dateTimePickers),
                resultVoid = checking.VoidAll(textBoxs, comboBoxs, dateTimePickers); //Проверяем только обязательные для ввода поля
                                                                             //если результаты вернулись положительные, тогда можно добавить данные, иначе вывести ошибку
            if (resultSecurity == true && resultVoid == true)
            {
                
                //Получаем запрос для обновления Update
                string query = connection.GetQueryUpdate(nameTable, nameFieldsAll, variables, id_field);
                //выполнить команду
                connection.command = new MySqlCommand(query, connection.connection);
                //для объектов, у них есть данные которые нужно вставить
                object[] objects = { textBox1, comboBox1, comboBox2, comboBox3, textBox2, textBox3, dateTimePicker1, dateTimePicker2 };
                //Добавляем данные 
                connection.AddParameters(connection.command, variables, mySqlDbTypes, objects);
                //попытаться выполнить запрос
                connection.ExecuteQuery(connection.command);
                //отобразить новые данные 
                FillDataGridView("");
            }
            else
            {
                checking.ErrorMessage(this);
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            //возвращаем результаты проверок всех полей
            bool resultSecurity = checking.SecurityAll(textBox1),
                resultVoid = checking.VoidAll(textBox1);//Проверяем только обязательные для ввода поля
            //если результаты вернулись положительные, тогда можно добавить данные, иначе вывести ошибку
            if (resultSecurity == true && resultVoid == true)
            {
                //Получаем запрос для удаления данных Delete
                string query = connection.GetQueryDelete(nameTable, id_field);
                //выполняем запрос
                connection.command = new MySqlCommand(query, connection.connection);
                //для объектов, у них есть данные которые нужно вставить
                object[] objects = { textBox1, comboBox1, comboBox2, comboBox3, textBox2, textBox3, dateTimePicker1, dateTimePicker2 };
                // Добавляем данные
                connection.AddParameters(connection.command, variables[0], mySqlDbTypes[0], objects[0]);
                //попытаться выполнить запрос
                connection.ExecuteQuery(connection.command);
                //отобразить новые данные 
                FillDataGridView("");
                //Что есть на форме
                TextBox[] textBoxs = { textBox1, textBox2, textBox3 };
                ComboBox[] comboBoxs = { comboBox1, comboBox2, comboBox3 };
                DateTimePicker[] dateTimePickers = { dateTimePicker1, dateTimePicker2 };
                //Очищяем поля
                settings.ClearFields(textBoxs, comboBoxs, dateTimePickers: dateTimePickers);
            }
            else
            {
                checking.ErrorMessage(this);
            }
        }
        private void Find_Click(object sender, EventArgs e)
        {
            bool resultSecurity = checking.SecurityAll(textBox1),
                   resultVoid = checking.VoidAll(textBox1);
            if (resultSecurity == true && resultVoid == true)
            {
                //получаем запрос для нахождения искомого значения
                string query = connection.GetQueryFindSelect(nameTable, nameFieldsAll, nameFieldsAS, id_field);
                //выполнить запрос
                connection.command = new MySqlCommand(query, connection.connection); //Создаём запрос для поиска
                //для объектов, у них есть данные которые нужно вставить
                object[] objects = { textBox1 };
                connection.AddParameters(connection.command, variables[0], mySqlDbTypes[0], objects[0]);

                TextBox[] textBoxs = { textBox1, textBox2, textBox3 };
                ComboBox[] comboBoxs = { comboBox1, comboBox2, comboBox3 };
                DateTimePicker[] dateTimePickers = { dateTimePicker1, dateTimePicker2 };
                int[] ColumnsTextForTextBox = {0, 4, 5},//столбцы таблицы с которых нужно взять данные и вставить в TextBox-ы
                    ColumnsTextForComboBox = {1,2,3},//столбцы таблицы с которых нужно взять данные и вставить в ComboBox-ы
                    ColumnsDateForDateDateTimePicker = {6,7};//столбцы таблицы с которых нужно взять данные и вставить в DateTimePicker-ы
                
                
                //Найти запись 
                DataTable table = connection.Find(connection.command, dataGrid: dataGridView1, 
                                                    textBoxs: textBoxs, ColumnsTextForTextBox: ColumnsTextForTextBox,
                                                    comboBoxs: comboBoxs, ColumnsTextForComboBox: ColumnsTextForComboBox,
                                                    dateTimePickers: dateTimePickers, ColumnsDateForDateDateTimePicker: ColumnsDateForDateDateTimePicker
                                                     );
                //для передвижения по таблице
                managerBase = this.BindingContext[table];
            }
            else
            {
                checking.ErrorMessage(this);
            }
        }
        private void Clear_Click(object sender, EventArgs e)
        {
            TextBox[] textBoxs = { textBox1, textBox2, textBox3 };
            ComboBox[] comboBoxs = { comboBox1, comboBox2, comboBox3 };
            DateTimePicker[] dateTimePickers = { dateTimePicker1, dateTimePicker2 };
            settings.ClearFields(textBoxs, comboBoxs, dateTimePickers:dateTimePickers);
        }
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            FillDataGridView(textBoxSearch.Text);
        }
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            int[] ColumnsTextForTextBox = { 0, 4, 5 },//столбцы таблицы с которых нужно взять данные и вставить в TextBox-ы
                    ColumnsTextForComboBox = { 1, 2, 3 },//столбцы таблицы с которых нужно взять данные и вставить в ComboBox-ы
                    ColumnsDateForDateDateTimePicker = { 6, 7 };//столбцы таблицы с которых нужно взять данные и вставить в DateTimePicker-ы

            //Массив куда будут (в textBox-ы) отображаться значения текущей строки из DataGridView
            TextBox[] textBoxs = { textBox1, textBox2, textBox3 };
            //отображаем все текстовые записи из DataGridView в textBox-ы
            settings.CurrentColumnCellsTEXT(ColumnsTextForTextBox, textBoxs, dataGridView1);

            //Массив куда будут (в comboBoxs-ы) отображаться значения текущей строки из DataGridView
            ComboBox[] comboBoxs = { comboBox1, comboBox2, comboBox3 };
            settings.CurrentColumnCellsTEXT(ColumnsTextForComboBox, comboBoxs, dataGridView1);

            //Массив куда будут(в dateTimePickers - ы) отображаться значения текущей строки из DataGridView
            DateTimePicker[] dateTimePickers = { dateTimePicker1, dateTimePicker2 };
            settings.CurrentColumnCellsDate(ColumnsDateForDateDateTimePicker, dateTimePickers, dataGridView1);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int[] ColumnsTextForTextBox = { 0, 4, 5 },//столбцы таблицы с которых нужно взять данные и вставить в TextBox-ы
                    ColumnsTextForComboBox = { 1, 2, 3 },//столбцы таблицы с которых нужно взять данные и вставить в ComboBox-ы
                    ColumnsDateForDateDateTimePicker = { 6, 7 };//столбцы таблицы с которых нужно взять данные и вставить в DateTimePicker-ы

            //Массив куда будут (в textBox-ы) отображаться значения текущей строки из DataGridView
            TextBox[] textBoxs = { textBox1, textBox2, textBox3 };
            //отображаем все текстовые записи из DataGridView в textBox-ы
            settings.CurrentColumnCellsTEXT(ColumnsTextForTextBox, textBoxs, dataGridView1);

            //Массив куда будут (в comboBoxs-ы) отображаться значения текущей строки из DataGridView
            ComboBox[] comboBoxs = { comboBox1, comboBox2, comboBox3 };
            settings.CurrentColumnCellsTEXT(ColumnsTextForComboBox, comboBoxs, dataGridView1);

            //Массив куда будут(в dateTimePickers - ы) отображаться значения текущей строки из DataGridView
            DateTimePicker[] dateTimePickers = { dateTimePicker1, dateTimePicker2 };
            settings.CurrentColumnCellsDate(ColumnsDateForDateDateTimePicker, dateTimePickers, dataGridView1);
        }

        private void FirstRecordButton_Click(object sender, EventArgs e)
        {
            managerBase.Position = 0;
        }

        private void NextRecordButton_Click(object sender, EventArgs e)
        {
            managerBase.Position += 1;
        }

        private void PreviousRecordButton_Click(object sender, EventArgs e)
        {
            managerBase.Position -= 1;
        }

        private void LastRecordButton_Click(object sender, EventArgs e)
        {
            managerBase.Position = managerBase.Count;
        }
    }
}