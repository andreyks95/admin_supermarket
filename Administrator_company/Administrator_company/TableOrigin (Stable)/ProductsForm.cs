using System;
using System.Data;
using System.Windows.Forms;
using Administrator_company.LogicProgram;
using MySql.Data.MySqlClient;

namespace Administrator_company
{
    public partial class ProductsForm : Form
    {
        public ProductsForm()
        {
            InitializeComponent();
        }

        Connection connection = new Connection();
        Settings settings = new Settings();
        Checking checking = new Checking();
        BindingManagerBase managerBase; //для перемещения по таблице 

        private static string nameTable = "products", //таблица
                     id_field = "id_products"; //id поле таблицы
        private static string[] nameFieldsAll = { "id_products", "name", "category", "kind", "subspecies", "price_for_one", "measurement" }, //все поля
                                variables = { "@id", "@name", "@category", "@kind", "@subspecies", "@price_for_one", "@measurement" }, //для переменных
                                nameFieldsAS = { "ИД", "Название", "Категория", "Вид", "Подвид", "Цена", "Система мер" }, //как будут отображаться
                                numericFields = { "id_products", "price_for_one" }; //для корректного поиска по числовым столбцам                                                    
        private static MySqlDbType[] mySqlDbTypes = { MySqlDbType.UInt32, MySqlDbType.VarChar,  MySqlDbType.Enum, MySqlDbType.VarChar, MySqlDbType.VarChar, MySqlDbType.Float, MySqlDbType.Enum };//для типов в (AddParameters) MySqlDbType.Float

        //попробовать nameFieldsAS набрать заново на русском

        private void ProductsForm_Load(object sender, EventArgs e)
        {
            FillDataGridView("");
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
            TextBox[] textBoxs = { textBox1, textBox2, textBox3, textBox4, textBox5 };
            ComboBox[] comboBoxs = { comboBox1, comboBox2 };

            //возвращаем результаты проверок всех полей
            bool resultSecurity = checking.SecurityAll(textBoxs, comboBoxs),
                 resultVoid = checking.VoidAll(textBoxs, comboBoxs); //Проверяем только обязательные для ввода поля
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
                object[] objects = { textBox1, textBox2, comboBox1, textBox3, textBox4, textBox5, comboBox2 };
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
            TextBox[] textBoxs = { textBox1, textBox2, textBox3, textBox4, textBox5 };
            ComboBox[] comboBoxs = { comboBox1, comboBox2 };

            //возвращаем результаты проверок всех полей
            bool resultSecurity = checking.SecurityAll(textBoxs, comboBoxs),
                resultVoid = checking.VoidAll(textBoxs, comboBoxs); //Проверяем только обязательные для ввода поля
                                                                                     //если результаты вернулись положительные, тогда можно добавить данные, иначе вывести ошибку
            if (resultSecurity == true && resultVoid == true)
            {

                //Получаем запрос для обновления Update
                string query = connection.GetQueryUpdate(nameTable, nameFieldsAll, variables, id_field);
                //выполнить команду
                connection.command = new MySqlCommand(query, connection.connection);
                //для объектов, у них есть данные которые нужно вставить
                object[] objects = { textBox1, textBox2, comboBox1, textBox3, textBox4, textBox5, comboBox2 };
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
                object[] objects = { textBox1, textBox2, comboBox1, textBox3, textBox4, textBox5, comboBox2 }; 
                // Добавляем данные
                connection.AddParameters(connection.command, variables[0], mySqlDbTypes[0], objects[0]);
                //попытаться выполнить запрос
                connection.ExecuteQuery(connection.command);
                //отобразить новые данные 
                FillDataGridView("");
                //Что есть на форме
                TextBox[] textBoxs = { textBox1, textBox2, textBox3, textBox4, textBox5 };
                ComboBox[] comboBoxs = { comboBox1, comboBox2 };
                //Очищяем поля
                settings.ClearFields(textBoxs, comboBoxs);
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

                TextBox[] textBoxs = { textBox1, textBox2, textBox3, textBox4, textBox5 };
                ComboBox[] comboBoxs = { comboBox1, comboBox2 };
                int[] ColumnsTextForTextBox = {0, 1, 3, 4, 5},//столбцы таблицы с которых нужно взять данные и вставить в TextBox-ы
                    ColumnsTextForComboBox = {2, 6};//столбцы таблицы с которых нужно взять данные и вставить в ComboBox-ы

                //Найти запись 
                DataTable table = connection.Find(connection.command, dataGrid: dataGridView1,
                                                    textBoxs: textBoxs, ColumnsTextForTextBox: ColumnsTextForTextBox,
                                                    comboBoxs: comboBoxs, ColumnsTextForComboBox: ColumnsTextForComboBox);
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
            TextBox[] textBoxs = { textBox1, textBox2, textBox3, textBox4, textBox5 };
            ComboBox[] comboBoxs = { comboBox1, comboBox2 };
            settings.ClearFields(textBoxs, comboBoxs);
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            FillDataGridView(textBoxSearch.Text);
        }
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            int[] ColumnsTextForTextBox = { 0, 1, 3, 4, 5 },//столбцы таблицы с которых нужно взять данные и вставить в TextBox-ы
                  ColumnsTextForComboBox = { 2, 6 };//столбцы таблицы с которых нужно взять данные и вставить в ComboBox-ы

            //Массив куда будут (в textBox-ы) отображаться значения текущей строки из DataGridView
            TextBox[] textBoxs = { textBox1, textBox2, textBox3, textBox4, textBox5 };
            //отображаем все текстовые записи из DataGridView в textBox-ы
            settings.CurrentColumnCellsTEXT(ColumnsTextForTextBox, textBoxs, dataGridView1);

            //Массив куда будут (в comboBoxs-ы) отображаться значения текущей строки из DataGridView
            ComboBox[] comboBoxs = { comboBox1, comboBox2};
            settings.CurrentColumnCellsTEXT(ColumnsTextForComboBox, comboBoxs, dataGridView1);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int[] ColumnsTextForTextBox = { 0, 1, 3, 4, 5 },//столбцы таблицы с которых нужно взять данные и вставить в TextBox-ы
                  ColumnsTextForComboBox = { 2, 6 };//столбцы таблицы с которых нужно взять данные и вставить в ComboBox-ы

            //Массив куда будут (в textBox-ы) отображаться значения текущей строки из DataGridView
            TextBox[] textBoxs = { textBox1, textBox2, textBox3, textBox4, textBox5 };
            //отображаем все текстовые записи из DataGridView в textBox-ы
            settings.CurrentColumnCellsTEXT(ColumnsTextForTextBox, textBoxs, dataGridView1);

            //Массив куда будут (в comboBoxs-ы) отображаться значения текущей строки из DataGridView
            ComboBox[] comboBoxs = { comboBox1, comboBox2 };
            settings.CurrentColumnCellsTEXT(ColumnsTextForComboBox, comboBoxs, dataGridView1);
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
