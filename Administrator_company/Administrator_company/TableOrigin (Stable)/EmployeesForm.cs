using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Administrator_company.LogicProgram;
using MySql.Data.MySqlClient;

namespace Administrator_company
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
        Сalculations calculations = new Сalculations();
        GetTextObjectsForm getText = new GetTextObjectsForm();


        BindingManagerBase managerBase; //для перемещения по таблице 

        private static string
            //таблица
            nameTable = "employees",
            id_field = "id_employee",
            //id поле таблицы
            secondaryTables = "employees";


        private static string[]
            nameTables =
            {
                "employees",
                "position",
                "info"
            },
            nameFields =
            {
                "employees.id_employee", "info.full_name", "position.position",
                "employees.department", "employees.experience", "employees.salary", "employees.started_work",
                "employees.fired"
            },
            nameFieldsAll =
            {
                "id_employee", "id_info", "id_position", "department", "experience", /*"salary",*/
                "started_work", "fired"
            },
            //все поля
            //для переменных
            variables =
            {
                "@id", "@id_info", "@id_position", "@department", "@experience", /*"@salary",*/ "@started_work",
                "@fired"
            },
            //как будут отображаться
            nameFieldsAS = { "ИД", "ФИО", "Должность", "Отдел", "Опыт", "Зарплата", "Принят", "Уволен" },
            //для корректного поиска по числовым столбцам 
            numericFields = { "employees.id_employee", "employees.experience", "employees.salary" },
            //Главные таблицы для части запроса where
            primaryTables = { "position", "info" },
            primaryIdField = { "id_position", "id_info" },
            secondaryIdField = { "id_position", "id_info" };

        //для типов в (AddParameters)
        private static MySqlDbType[] mySqlDbTypes = { MySqlDbType.UInt32, MySqlDbType.UInt32, MySqlDbType.UInt32,
                                MySqlDbType.Enum, MySqlDbType.UInt32, /*MySqlDbType.UInt32,*/ MySqlDbType.Date, MySqlDbType.Date };

        private Tuple<Tuple<string[], string[][], string[], string[]>,
                  Tuple<string, string, string, string>>
        SetTuple(string[] values = null, TextBox textBoxsIdField = null, ComboBox comboBoxsIdField = null)
        {
            //Rest1
            //Таблицы которые принимают участвие в вычислении
            string[] nameTables = { "employees", "position" };
            //Название полей выше указанных таблиц. У них есть значения для нужного нам вычисления
            string[][] nameFields = new string[][]
            {
                    new string[] { "experience"  },
                    new string[] { "salary" }
            };
            //Название полей id-ов выше указанных таблиц
            string[] nameIdTables = { "id_employee", "id_position" };
            string[] ids = new string[2];
            if (textBoxsIdField != null)
                ids[0] = textBoxsIdField.Text; //id_stock
            if (comboBoxsIdField != null)
                ids[1] = comboBoxsIdField.Text; //id_products
            if (values != null)
            {
                ids[0] = values[0];
                ids[1] = values[1];
            }


            //Rest2
            //Результат вычисление куда необходимо вставить
            string resultTable = "employees";
            string resultField = "salary";
            string resultIdFiedTable = "id_employee";
            string resultNumberId = ids[0];

            var dataCalculations = new Tuple<
                                            Tuple<string[], string[][], string[], string[]>,
                                            Tuple<string, string, string, string>
                                    >(
                                      new Tuple<string[], string[][], string[], string[]>(nameTables, nameFields, nameIdTables, ids),
                                      new Tuple<string, string, string, string>(resultTable, resultField, resultIdFiedTable, resultNumberId)
                                    );
            return dataCalculations;
        }

        private void ChangedInDataGridView()
        {
            int[] ColumnsTextForTextBox = { 0, 4, 5 },//столбцы таблицы с которых нужно взять данные и вставить в TextBox-ы
                  ColumnsTextForComboBox = { 1, 2, 3 },//столбцы таблицы с которых нужно взять данные и вставить в ComboBox-ы
                  ColumnsDateForDateDateTimePicker = { 6, 7 };//столбцы таблицы с которых нужно взять данные и вставить в DateTimePicker-ы

            //Массив куда будут (в textBox-ы) отображаться значения текущей строки из DataGridView
            TextBox[] textBoxs = { textBox1, textBox2 };
            //отображаем все текстовые записи из DataGridView в textBox-ы
            settings.CurrentColumnCellsTEXT(ColumnsTextForTextBox, textBoxs, dataGridView1);

            //Массив куда будут (в comboBoxs-ы) отображаться значения текущей строки из DataGridView
            ComboBox[] comboBoxs = { comboBox1, comboBox2, comboBox3 };
            settings.CurrentColumnCellsTEXT(ColumnsTextForComboBox, comboBoxs, dataGridView1);

            //Массив куда будут(в dateTimePickers - ы) отображаться значения текущей строки из DataGridView
            DateTimePicker[] dateTimePickers = { dateTimePicker1, dateTimePicker2 };
            settings.CurrentColumnCellsDate(ColumnsDateForDateDateTimePicker, dateTimePickers, dataGridView1);
        }

        private string[] GetAllValuesDataFromElementsForm()
        {
            //Получить значения id для вставки
            string[] idsFromComboBox = settings.GetIdFromComboBox(new[] { comboBox1, comboBox2 });

            object[] objects = { textBox1, comboBox3, textBox2, /*textBox3,*/ dateTimePicker1, dateTimePicker2 };
            //Получить значения всех объектов
            string[] allValuesFromObjects = getText.GetText(objects);

            string[] allValues = new string[idsFromComboBox.Length + allValuesFromObjects.Length];

            //вставляем первый элемент - это будет id
            Array.Copy(allValuesFromObjects, 0, allValues, 0, 1);
            //вставляем id подчинённых таблиц, которые получили
            Array.Copy(idsFromComboBox, 0, allValues, 1, 2);
            //вставляем оставшиеся элементы
            Array.Copy(allValuesFromObjects, 1, allValues, 3, 4);
            return allValues;
        }

        //Заполняем DataGridView и корректируем её
        public void FillDataGridView(string valueToSearch)
        {
            //получаем запрос на отображение данных с поиском
            string query = connection.GetQueryShowSearch(nameTables, nameFields, nameFieldsAS,
                primaryTables, secondaryTables, primaryIdField, secondaryIdField,
                numericFields, valueToSearch);
            DataTable table = connection.FillDataGridView(dataGridView1, 20, query: query); //заполняем таблицу данными с запроса и настраиваем
            managerBase = this.BindingContext[table]; //подключаем таблицу для передвижения по ней
        }

        private void EmployeesForm_Load(object sender, EventArgs e)
        {
            //Заполняем значениями все ComboBox-ы распаложены на форме
            List<string> values = new List<string>();

            //Заполняем ComboBox1 всеми значениями из двух столбцов id (для ориентировки) и нужные нам значения
            string[] nameTables = { "info" },
                nameFields = { "id_info", "full_name" };
            values = connection.GetValuesColumn(nameTables, nameFields);
            settings.FillComboBox(comboBox1, values);

            //Заполняем ComboBox2 как ComboBox1
            nameTables[0] = "position";
            nameFields[0] = "id_position";
            nameFields[1] = "position";
            values = connection.GetValuesColumn(nameTables, nameFields);
            settings.FillComboBox(comboBox2, values);

            //Заполняем ComboBox3 всеми значениями Enum которые могут принимать ячейки в столбце  
            values = connection.GetEnum("employees", "department");
            settings.FillComboBox(comboBox3, values);

            //при загрузке отображаем таблицу
            FillDataGridView("");
        }

        private void Insert_Click(object sender, EventArgs e)
        {
            TextBox[] textBoxs = { textBox1, textBox2 };
            ComboBox[] comboBoxs = { comboBox1, comboBox2, comboBox3 };
            DateTimePicker[] dateTimePickers = { dateTimePicker1, dateTimePicker2 };

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

                //Получить значения всех объектов формы для вставки
                var allValues = GetAllValuesDataFromElementsForm();

                //Добавляем данные 
                connection.AddParametersString(connection.command, variables, mySqlDbTypes, allValues);

                //попытаться выполнить запрос
                connection.ExecuteQuery(connection.command);

                query = calculations.GetUpdateQuerySalary(SetTuple(allValues));
                //cюда попытаться вставить запрос для выполнения вычисления
                connection.FieldDateTableCalculation(query);

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
            TextBox[] textBoxs = { textBox1, textBox2 };
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

                //Получить значения всех объектов формы для вставки
                var allValues = GetAllValuesDataFromElementsForm();

                //Добавляем данные 
                connection.AddParametersString(connection.command, variables, mySqlDbTypes, allValues);

                //попытаться выполнить запрос
                connection.ExecuteQuery(connection.command);

                query = calculations.GetUpdateQuerySalary(SetTuple(allValues));
                //cюда попытаться вставить запрос для выполнения вычисления
                connection.FieldDateTableCalculation(query);

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
                object[] objects = { textBox1, comboBox1, comboBox2, comboBox3, textBox2, dateTimePicker1, dateTimePicker2 };
                // Добавляем данные
                connection.AddParameters(connection.command, variables[0], mySqlDbTypes[0], objects[0]);
                //попытаться выполнить запрос
                connection.ExecuteQuery(connection.command);
                //отобразить новые данные 
                FillDataGridView("");
                //Что есть на форме
                TextBox[] textBoxs = { textBox1, textBox2 };
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

                TextBox[] textBoxs = { textBox1, textBox2 };
                ComboBox[] comboBoxs = { comboBox1, comboBox2, comboBox3 };
                DateTimePicker[] dateTimePickers = { dateTimePicker1, dateTimePicker2 };
                int[] ColumnsTextForTextBox = { 0, 4, 5 },//столбцы таблицы с которых нужно взять данные и вставить в TextBox-ы
                    ColumnsTextForComboBox = { 1, 2, 3 },//столбцы таблицы с которых нужно взять данные и вставить в ComboBox-ы
                    ColumnsDateForDateDateTimePicker = { 6, 7 };//столбцы таблицы с которых нужно взять данные и вставить в DateTimePicker-ы


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
            TextBox[] textBoxs = { textBox1, textBox2 };
            ComboBox[] comboBoxs = { comboBox1, comboBox2, comboBox3 };
            DateTimePicker[] dateTimePickers = { dateTimePicker1, dateTimePicker2 };
            settings.ClearFields(textBoxs, comboBoxs, dateTimePickers: dateTimePickers);
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            FillDataGridView(textBoxSearch.Text);
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            ChangedInDataGridView();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            ChangedInDataGridView();
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
