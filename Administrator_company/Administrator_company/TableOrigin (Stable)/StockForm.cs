using System;
using System.Data;
using System.Windows.Forms;
using Administrator_company.LogicProgram;
using MySql.Data.MySqlClient;

namespace Administrator_company
{
    public partial class StockForm : Form
    {
        public StockForm()
        {
            InitializeComponent();
        }

        Connection connection = new Connection();
        Settings settings = new Settings();
        Checking checking = new Checking();
        Сalculations calculations = new Сalculations();

        BindingManagerBase managerBase; //для перемещения по таблице

        private static string nameTable = "stock", //таблица
                             id_field = "id_stock"; //id поле таблицы
        private static string[] nameFieldsAll = { "id_stock", "id_products", "available", "entered", "sold", "quantity", "price"}, //все поля
                                variables = { "@id", "@id_products", "@available", "@entered", "@sold", "@quantity", "@price"}, //для переменных
                                nameFieldsAS = { "ИД", "Название продукта", "Наличие", "Доставлен", "Продан", "Количество", "Цена" }, //как будут отображаться
                                numericFields = { "id_stock", "id_products", "quantity", "price" }; //для корректного поиска по числовым столбцам                                                    

        private static MySqlDbType[] mySqlDbTypes =
        {
            MySqlDbType.UInt32, MySqlDbType.UInt32, MySqlDbType.Enum,
            MySqlDbType.Date, MySqlDbType.Date, MySqlDbType.UInt32, MySqlDbType.Float
        };//для типов в (AddParameters)

        private Tuple<Tuple<string[], string[][], string[], string[]>,
                  Tuple<string>,
                  Tuple<string, string, string, string>>
        SetTuple(TextBox textBoxsIdField, ComboBox comboBoxsIdField)
            {
                //Rest1
                //Таблицы которые принимают участвие в вычислении
                string[] nameTables = { "stock", "products" };
                //Название полей выше указанных таблиц. У них есть значения для нужного нам вычисления
                string[][] nameFields = new string[][]
                {
                    new string[] { "quantity"  },
                    new string[] { "price_for_one" }
                };
                //Название полей id-ов выше указанных таблиц
                string[] nameIdTables = { "id_stock", "id_products" };
                string[] ids = { textBoxsIdField.Text, //id_stock
                                 comboBoxsIdField.Text}; //id_products

                //Rest2
                string mathOperation = "*";

                //Rest3
                string resultTable = "stock";
                string resultField = "price";
                string resultIdFiedTable = "id_stock";
                string resultNumberId = ids[0];

                var dataCalculations = new Tuple<
                                                Tuple<string[], string[][], string[], string[]>,
                                                Tuple<string>,
                                                Tuple<string, string, string, string>
                                        >(
                                          new Tuple<string[], string[][], string[], string[]>(nameTables, nameFields, nameIdTables, ids),
                                          new Tuple<string>(mathOperation),
                                          new Tuple<string, string, string, string>(resultTable, resultField, resultIdFiedTable, resultNumberId)
                                        );
                return dataCalculations;
            }

        private void StockForm_Load(object sender, EventArgs e)
        {
            FillDataGridView(""); //при загрузке отображаем таблицу
        }

        public void FillDataGridView(string valueToSearch)
        {
            //получаем запрос на отображение данных с поиском
            string query = connection.GetQueryShowSearch(nameTable, nameFieldsAll, nameFieldsAS, numericFields, valueToSearch);
            DataTable table = connection.FillDataGridView(dataGridView1, 20, query: query); //заполняем таблицу данными с запроса и настраиваем
            managerBase = this.BindingContext[table]; //подключаем таблицу для передвижения по ней
        }

        private void Insert_Click(object sender, EventArgs e)
        {
            TextBox[] textBoxs = {textBox1, textBox2, textBox3 };
            ComboBox[] comboBoxs = {comboBox1, comboBox2};
            DateTimePicker[] dateTimePickers = {dateTimePicker1, dateTimePicker2};

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
                object[] objects = { textBox1, comboBox1, comboBox2, dateTimePicker1, dateTimePicker2, textBox2, textBox3 };
                //Добавляем данные 
                connection.AddParameters(connection.command, variables, mySqlDbTypes, objects);
                //попытаться выполнить запрос
                connection.ExecuteQuery(connection.command);

                //ТЕСТ ФУНКЦИИ
                query = calculations.GetUpdateQuery(SetTuple(textBox1, comboBox1));
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
            TextBox[] textBoxs = { textBox1, textBox2, textBox3 };
            ComboBox[] comboBoxs = { comboBox1, comboBox2 };
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
                object[] objects = { textBox1, comboBox1, comboBox2, dateTimePicker1, dateTimePicker2, textBox2, textBox3 };
                //Добавляем данные 
                connection.AddParameters(connection.command, variables, mySqlDbTypes, objects);
                //попытаться выполнить запрос
                connection.ExecuteQuery(connection.command);

                //ТЕСТ ФУНКЦИИ
                query = calculations.GetUpdateQuery(SetTuple(textBox1, comboBox1));
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
                object[] objects = { textBox1, comboBox1, comboBox2, dateTimePicker1, dateTimePicker2, textBox2, textBox3 };
                // Добавляем данные
                connection.AddParameters(connection.command, variables[0], mySqlDbTypes[0], objects[0]);
                //попытаться выполнить запрос
                connection.ExecuteQuery(connection.command);
                //отобразить новые данные 
                FillDataGridView("");
                //Что есть на форме
                TextBox[] textBoxs = { textBox1, textBox2, textBox3 };
                ComboBox[] comboBoxs = { comboBox1, comboBox2 };
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
                ComboBox[] comboBoxs = { comboBox1, comboBox2 };
                DateTimePicker[] dateTimePickers = { dateTimePicker1, dateTimePicker2 };
                int[] ColumnsTextForTextBox = { 0, 5, 6 },//столбцы таблицы с которых нужно взять данные и вставить в TextBox-ы
                    ColumnsTextForComboBox = { 1, 2 },//столбцы таблицы с которых нужно взять данные и вставить в ComboBox-ы
                    ColumnsDateForDateDateTimePicker = { 3, 4 };//столбцы таблицы с которых нужно взять данные и вставить в DateTimePicker-ы


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
            ComboBox[] comboBoxs = { comboBox1, comboBox2 };
            DateTimePicker[] dateTimePickers = { dateTimePicker1, dateTimePicker2 };
            settings.ClearFields(textBoxs, comboBoxs, dateTimePickers: dateTimePickers);
        }
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            FillDataGridView(textBoxSearch.Text);
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            int[] ColumnsTextForTextBox = { 0, 5, 6 },//столбцы таблицы с которых нужно взять данные и вставить в TextBox-ы
                    ColumnsTextForComboBox = { 1, 2 },//столбцы таблицы с которых нужно взять данные и вставить в ComboBox-ы
                    ColumnsDateForDateDateTimePicker = { 3, 4 };//столбцы таблицы с которых нужно взять данные и вставить в DateTimePicker-ы
                                                                //Массив куда будут (в textBox-ы) отображаться значения текущей строки из DataGridView
            TextBox[] textBoxs = { textBox1, textBox2, textBox3 };
            //отображаем все текстовые записи из DataGridView в textBox-ы
            settings.CurrentColumnCellsTEXT(ColumnsTextForTextBox, textBoxs, dataGridView1);

            //Массив куда будут (в comboBoxs-ы) отображаться значения текущей строки из DataGridView
            ComboBox[] comboBoxs = { comboBox1, comboBox2 };
            settings.CurrentColumnCellsTEXT(ColumnsTextForComboBox, comboBoxs, dataGridView1);

            //Массив куда будут(в dateTimePickers - ы) отображаться значения текущей строки из DataGridView
            DateTimePicker[] dateTimePickers = { dateTimePicker1, dateTimePicker2 };
            settings.CurrentColumnCellsDate(ColumnsDateForDateDateTimePicker, dateTimePickers, dataGridView1);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int[] ColumnsTextForTextBox = { 0, 5, 6 },//столбцы таблицы с которых нужно взять данные и вставить в TextBox-ы
        ColumnsTextForComboBox = { 1, 2 },//столбцы таблицы с которых нужно взять данные и вставить в ComboBox-ы
        ColumnsDateForDateDateTimePicker = { 3, 4 };//столбцы таблицы с которых нужно взять данные и вставить в DateTimePicker-ы
                                                    //Массив куда будут (в textBox-ы) отображаться значения текущей строки из DataGridView
            TextBox[] textBoxs = { textBox1, textBox2, textBox3 };
            //отображаем все текстовые записи из DataGridView в textBox-ы
            settings.CurrentColumnCellsTEXT(ColumnsTextForTextBox, textBoxs, dataGridView1);

            //Массив куда будут (в comboBoxs-ы) отображаться значения текущей строки из DataGridView
            ComboBox[] comboBoxs = { comboBox1, comboBox2 };
            settings.CurrentColumnCellsTEXT(ColumnsTextForComboBox, comboBoxs, dataGridView1);

            //Массив куда будут(в dateTimePickers - ы) отображаться значения текущей строки из DataGridView
            DateTimePicker[] dateTimePickers = { dateTimePicker1, dateTimePicker2 };
            settings.CurrentColumnCellsDate(ColumnsDateForDateDateTimePicker, dateTimePickers, dataGridView1);
            //dataGridView1_Click(this, e);
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
