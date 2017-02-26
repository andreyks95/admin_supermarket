using System;
using System.Data;
using System.Windows.Forms;
using Administrator_supermarket;
using MySql.Data.MySqlClient;

namespace Administrator_company.Preview__Test_
{
    public partial class PositionForm : Form
    {
        public PositionForm()
        {
            InitializeComponent();
        }

        Connection connection = new Connection();
        Settings settings = new Settings();
        Checking checking = new Checking();
        BindingManagerBase managerBase; //для перемещения по таблице 

        private static string nameTable = "position", //таблица
                             id_field = "id_position"; //id поле таблицы
        private static string[] nameFieldsAll = { "id_position", "position", "salary" }, //все поля
                                 variables = { "@id", "@position", "@salary" }, //для переменных
                                 nameFieldsAS = { "ИД", "Должность", "Зарплата"}, //как будут отображаться
                                 numericFields = { "id_position", "salary" }; //для корректного поиска по числовым столбцам                                                    
        private static MySqlDbType[] mySqlDbTypes = { MySqlDbType.UInt32, MySqlDbType.VarChar,  MySqlDbType.UInt32 };//для типов в (AddParameters)

        //private object[] objects = new object[3];
        //private readonly TextBox[] textBoxs = new TextBox[3];
        //public object[] Objects
        //{
        //    get   { return objects;}
        //    set{
        //        objects[0] = textBox1;
        //        objects[1] = textBox2;
        //        objects[2] = textBox3;
        //    }
        //}
        //public TextBox[] TextBoxs
        //{
        //    get{return textBoxs;}
        //    set
        //    {
        //        textBoxs[0] = textBox1;
        //        textBoxs[1] = textBox2;
        //        textBoxs[2] = textBox3;
        //    }
        //}

        //При загрузке отобразить таблицу
        private void PositionForm_Load(object sender, EventArgs e)
        {
            FillDataGridView(""); //при загрузке отображаем таблицу
        }

        //Заполняем DataGridView и корректируем её
        public void FillDataGridView(string valueToSearch)
        {
            //получаем запрос на отображение данных с поиском
            string query = connection.GetQueryShowSearch(nameTable, nameFieldsAll, nameFieldsAS, numericFields, valueToSearch);
            DataTable table = connection.FillDataGridView(dataGridView1, 20, query:query); //заполняем таблицу данными с запроса и настраиваем
            managerBase = this.BindingContext[table]; //подключаем таблицу для передвижения по ней
        }

        //Вставить данные 
        private void Insert_Click(object sender, EventArgs e)
        {
                //возвращаем результаты проверок всех полей
                bool resultSecurity = checking.SecurityAll(textBox1, textBox2, textBox3),
                    resultVoid = checking.VoidAll(textBox1, textBox2, textBox3); //Проверяем только обязательные для ввода поля
                //если результаты вернулись положительные, тогда можно добавить данные, иначе вывести ошибку
                if (resultSecurity == true && resultVoid == true)
                {
                    //получить запрос для вставки Insert
                    string query = connection.GetQueryInsert(nameTable, nameFieldsAll, variables);
                    //выполняить команду с Insert
                    connection.command = new MySqlCommand(query, connection.connection);
                    //для объектов, у них есть данные которые нужно вставить
                    object[] objects = { textBox1, textBox2, textBox3};
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

        //Обновленяем нужные нам данные
        private void Update_Click(object sender, EventArgs e)
        {
            //возвращаем результаты проверок всех полей
            bool resultSecurity = checking.SecurityAll(textBox1, textBox2, textBox3),
                resultVoid = checking.VoidAll(textBox1, textBox2, textBox3); //Проверяем только обязательные для ввода поля
                                                                             //если результаты вернулись положительные, тогда можно добавить данные, иначе вывести ошибку
            if (resultSecurity == true && resultVoid == true)
            {
                //Получаем запрос для обновления Update
                string query = connection.GetQueryUpdate(nameTable, nameFieldsAll, variables, id_field);
                //выполнить команду
                connection.command = new MySqlCommand(query, connection.connection);
                //для объектов, у них есть данные которые нужно вставить
                object[] objects = { textBox1, textBox2, textBox3 };
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

        //удаляем данные
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
                object[] objects = { textBox1, textBox2, textBox3};
                // Добавляем данные
                connection.AddParameters(connection.command, variables[0], mySqlDbTypes[0], objects[0]);
                //попытаться выполнить запрос
                connection.ExecuteQuery(connection.command);
                //отобразить новые данные 
                FillDataGridView("");
                //Что есть на форме
                TextBox[] textBoxs = {textBox1, textBox2, textBox3};
                //Очищяем поля
                settings.ClearFields(textBoxs);
            }
            else
            {
                checking.ErrorMessage(this);
            }
        }

        //Нахождение записи по id
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
                object[] objects = { textBox1, textBox2, textBox3 };
                connection.AddParameters(connection.command, variables[0], mySqlDbTypes[0], objects[0]);

                TextBox[] textBoxs = { textBox1, textBox2, textBox3 };
                int[] ColumnsTextForTextBox = { 0, 1, 2 };//столбцы таблицы с которых нужно взять данные и вставить в TextBox-ы
                //Найти запись 
                DataTable table = connection.Find(connection.command, textBoxs: textBoxs, ColumnsTextForTextBox: ColumnsTextForTextBox,
                                                     dataGrid: dataGridView1);
                //для передвижения по таблице
                managerBase = this.BindingContext[table];
            }
            else
            {
                checking.ErrorMessage(this);
            }
        }

        //Очистить поля на форме для ввода
        private void Clear_Click(object sender, EventArgs e)
        {
            TextBox[] textBoxs = { textBox1, textBox2, textBox3};
            settings.ClearFields(textBoxs);
        }

        //Поиск похожих записей
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            FillDataGridView(textBoxSearch.Text);
        }

        //Когда пользователь будет кликать мышью по нужной строке, то в textBox-ах  будут отображаться данные текущей строки
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            //Массив куда будут (в textBox-ы) отображаться значения текущей строки из DataGridView
            TextBox[] textBoxs = { textBox1, textBox2, textBox3};
            //отображаем все текстовые записи из DataGridView в textBox-ы
            settings.CurrentColumnCellsTEXT(textBoxs, dataGridView1);
        }

        //Когда пользователь будет нажимать стрелками клавиш "вверх" и "вниз" то будут в textBox-ах  отображаться данные текущей строки
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        { 
            //Массив куда будут (в textBox-ы) отображаться значения текущей строки из DataGridView
            TextBox[] textBoxs = { textBox1, textBox2, textBox3};
            //отображаем все текстовые записи из DataGridView в textBox-ы
            settings.CurrentColumnCellsTEXT(textBoxs, dataGridView1);
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
