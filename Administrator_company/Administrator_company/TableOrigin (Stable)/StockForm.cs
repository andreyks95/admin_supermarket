using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Administrator_company.LogicProgram;
using iTextSharp.text;
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
        GetTextObjectsForm getText = new GetTextObjectsForm();
        Report report = new Report();

        BindingManagerBase managerBase; //для перемещения по таблице

        private static string nameTable = "stock", //таблица
                             id_field = "id_stock", //id поле таблицы
                             secondaryTable = "stock";

        private static string[]
            nameTables = {"stock", "products"},
            nameFields =
            {
                "stock.id_stock", "products.name", "stock.available", "stock.entered", "stock.sold",
                "stock.quantity", "stock.price"
            },
            nameFieldsAll = {"id_stock", "id_products", "available", "entered", "sold", "quantity"},
            //, "price"}, //все поля
            variables = {"@id", "@id_products", "@available", "@entered", "@sold", "@quantity"},
            //, "@price"}, //для переменных
            nameFieldsAS = {"ИД", "Название продукта", "Наличие", "Доставлен", "Продан", "Количество", "Цена"},
            //как будут отображаться
            numericFields = {"stock.id_stock", "stock.quantity", "stock.price"},
            //для корректного поиска по числовым столбцам                                                    
            //Главные таблицы для части запроса where
            primaryTables = {"products"},
            primaryIdField = {"id_products"},
            secondaryIdField = { "id_products" };

        private static MySqlDbType[] mySqlDbTypes =
        {
            MySqlDbType.UInt32, MySqlDbType.UInt32, MySqlDbType.Enum,
            MySqlDbType.Date, MySqlDbType.Date, MySqlDbType.UInt32 //, MySqlDbType.Float
        };//для типов в (AddParameters)

        private Tuple<Tuple<string[], string[][], string[], string[]>,
                  Tuple<string>,
                  Tuple<string, string, string, string>>
        SetTuple(string[] values = null, TextBox textBoxsIdField = null, ComboBox comboBoxsIdField = null)
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

        private void ChangedInDataGridView()
        {
            int[] ColumnsTextForTextBox = { 0, 5 },//столбцы таблицы с которых нужно взять данные и вставить в TextBox-ы
                ColumnsTextForComboBox = { 1, 2 },//столбцы таблицы с которых нужно взять данные и вставить в ComboBox-ы
                ColumnsDateForDateDateTimePicker = { 3, 4 };//столбцы таблицы с которых нужно взять данные и вставить в DateTimePicker-ы
                                            //Массив куда будут (в textBox-ы) отображаться значения текущей строки из DataGridView
            TextBox[] textBoxs = { textBox1, textBox2 };
            //отображаем все текстовые записи из DataGridView в textBox-ы
            settings.CurrentColumnCellsTEXT(ColumnsTextForTextBox, textBoxs, dataGridView1);

            //Массив куда будут (в comboBoxs-ы) отображаться значения текущей строки из DataGridView
            ComboBox[] comboBoxs = { comboBox1, comboBox2 };
            settings.CurrentColumnCellsTEXT(ColumnsTextForComboBox, comboBoxs, dataGridView1);

            //Массив куда будут(в dateTimePickers - ы) отображаться значения текущей строки из DataGridView
            DateTimePicker[] dateTimePickers = { dateTimePicker1, dateTimePicker2 };
            settings.CurrentColumnCellsDate(ColumnsDateForDateDateTimePicker, dateTimePickers, dataGridView1);
        }

        public void FillDataGridView(string valueToSearch)
        {
            //получаем запрос на отображение данных с поиском
            string query = connection.GetQueryShowSearch(nameTables, nameFields, nameFieldsAS,
                primaryTables, secondaryTable, primaryIdField, secondaryIdField,
                numericFields, valueToSearch);
            DataTable table = connection.FillDataGridView(dataGridView1, 20, query: query); //заполняем таблицу данными с запроса и настраиваем
            managerBase = this.BindingContext[table]; //подключаем таблицу для передвижения по ней
        }

        private string[] GetAllValuesDataFromElementsForm()
        {
            //Получить значения id для вставки
            string[] idsFromComboBox = settings.GetIdFromComboBox(new[] { comboBox1 });

            object[] objects = { textBox1, comboBox2, dateTimePicker1, dateTimePicker2, textBox2 };
            //Получить значения всех объектов
            string[] allValuesFromObjects = getText.GetText(objects);

            string[] allValues = new string[idsFromComboBox.Length + allValuesFromObjects.Length];

            //вставляем первый элемент - это будет id
            Array.Copy(allValuesFromObjects, 0, allValues, 0, 1);
            //вставляем id подчинённых таблиц, которые получили
            Array.Copy(idsFromComboBox, 0, allValues, 1, 1);
            //вставляем оставшиеся элементы
            Array.Copy(allValuesFromObjects, 1, allValues, 2, 4);
            return allValues;
        }

        private void StockForm_Load(object sender, EventArgs e)
        {
            //Заполняем значениями все ComboBox-ы распаложены на форме
            List<string> values = new List<string>();

            //Заполняем ComboBox1 всеми значениями из двух столбцов id (для ориентировки) и нужные нам значения
            string[] nameTables = { "products" },
                nameFields = { "id_products", "name" };
            values = connection.GetValuesColumn(nameTables, nameFields);
            settings.FillComboBox(comboBox1, values);

            //Заполняем ComboBox2 всеми значениями Enum которые могут принимать ячейки в столбце  
            values = connection.GetEnum("stock", "available");
            settings.FillComboBox(comboBox2, values);

            FillDataGridView(""); //при загрузке отображаем таблицу
        }

        #region Работа с данными (вставка, обновление, удаление) 

        private void Insert_Click(object sender, EventArgs e)
        {
            TextBox[] textBoxs = {textBox1, textBox2};
            ComboBox[] comboBoxs = {comboBox1, comboBox2};
            DateTimePicker[] dateTimePickers = {dateTimePicker1, dateTimePicker2};

            //возвращаем результаты проверок всех полей
            bool resultSecurity = checking.SecurityAll(textBoxs, comboBoxs, dateTimePickers),
                 resultVoid = checking.VoidAll(textBoxs, comboBoxs, dateTimePickers), //Проверяем только обязательные для ввода поля
                 resultDate = checking.CheckDate(dateTimePicker1, dateTimePicker2 );

             //если результаты вернулись положительные, тогда можно добавить данные, иначе вывести ошибку
            if (resultSecurity && resultVoid  && resultDate)
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
                query = calculations.GetUpdateQuery(SetTuple(allValues));
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
            ComboBox[] comboBoxs = { comboBox1, comboBox2 };
            DateTimePicker[] dateTimePickers = { dateTimePicker1, dateTimePicker2 };

            //возвращаем результаты проверок всех полей
            bool resultSecurity = checking.SecurityAll(textBoxs, comboBoxs, dateTimePickers),
                 resultVoid = checking.VoidAll(textBoxs, comboBoxs, dateTimePickers), //Проверяем только обязательные для ввода поля
                 resultDate = checking.CheckDate(dateTimePicker1, dateTimePicker2);

            //если результаты вернулись положительные, тогда можно добавить данные, иначе вывести ошибку
            if (resultSecurity && resultVoid && resultDate)
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
                //Получаем запрос для обновления
                query = calculations.GetUpdateQuery(SetTuple(allValues));
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
                object[] objects = { textBox1, comboBox1, comboBox2, dateTimePicker1, dateTimePicker2, textBox2 };
                // Добавляем данные
                connection.AddParameters(connection.command, variables[0], mySqlDbTypes[0], objects[0]);
                //попытаться выполнить запрос
                connection.ExecuteQuery(connection.command);
                //отобразить новые данные 
                FillDataGridView("");
                //Что есть на форме
                TextBox[] textBoxs = { textBox1, textBox2 };
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
        #endregion

        private void Find_Click(object sender, EventArgs e)
        {
            bool resultSecurity = checking.SecurityAll(textBox1),
                   resultVoid = checking.VoidAll(textBox1);
            if (resultSecurity == true && resultVoid == true)
            {
                //получаем запрос для нахождения искомого значения
                string query = connection.GetQueryFindSelect(nameTables, nameFields, nameFieldsAS,
                            primaryTables, secondaryTable, primaryIdField, secondaryIdField, id_field);
                //выполнить запрос
                connection.command = new MySqlCommand(query, connection.connection); //Создаём запрос для поиска
                //для объектов, у них есть данные которые нужно вставить
                object[] objects = { textBox1 };
                connection.AddParameters(connection.command, variables[0], mySqlDbTypes[0], objects[0]);

                TextBox[] textBoxs = { textBox1, textBox2 };
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
            TextBox[] textBoxs = { textBox1, textBox2 };
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
            ChangedInDataGridView();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            ChangedInDataGridView();
        }

        #region Навигация по таблице

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

        #endregion

        #region создание отчёта
        private void ReportButton_Click(object sender, EventArgs e)
        {
            report.CreateBasicReport(dataGridView1,saveFileDialog1, "Склад", GetValues());
        }
        
        private string[] GetValues()
        {
            //Результаты 
            List<string> allResult = new List<string>();

            //Для получения количества товаров в наличии/не наличии и их сумму
            string partQuery =
                "select concat(products.name, '   ', sum(stock.quantity), '   ', sum(stock.price), ' грн.') as result " +
                " from " + connection.NAME_DATABASE + ".products, " + connection.NAME_DATABASE + ".stock " +
                " where stock.id_products = products.id_products " +
                " AND stock.available = ",
                groupBy = " Group by products.name ",
                query = default(string);
            string[] result = null;

            allResult.Add("Количество товаров в наличии и их сумма");
            query = partQuery + "\"Нету\"" + groupBy;
            result = connection.GetAllResult(query);
            allResult.AddRange(result);

            allResult.Add("Количество товаров которых нету в наличии и их сумма");
            query = partQuery + "\"Есть\"" + groupBy;
            result = connection.GetAllResult(query);
            allResult.AddRange(result);

            return allResult.ToArray();
        }

        #endregion
    }
}
