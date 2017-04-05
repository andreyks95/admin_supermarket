using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Administrator_company.LogicProgram;
using iTextSharp.text;
using MySql.Data.MySqlClient;
//Дальше подключаемые 

namespace Administrator_company
{
    public partial class InfoForm : Form
    {

        public InfoForm()
        {
            InitializeComponent();
        }

        Connection connection = new Connection();
        Settings settings = new Settings();
        Checking checking = new Checking();
        Report report = new Report();
        BindingManagerBase managerBase; //для перемещения по таблице 


        private static string nameTable = "info", //таблица
                              id_field = "id_info"; //id поле таблицы
        private static string[] nameFieldsAll = { "id_info", "full_name", "passport_id", "age", "address", "phone", "photo" }, //все поля
                                 variables = { "@id", "@name", "@passport", "@age", "@address", "@phone", "@photo" }, //для переменных
                                 nameFieldsAS = { "ИД", "ФИО", "Серия и номер паспорта", "Возраст", "Адрес", "Телефон", "Фото" }, //как будут отображаться
                                 numericFields = { "id_info", "age" }; //для корректного поиска по числовым столбцам                                                    
        private static MySqlDbType[] mySqlDbTypes = { MySqlDbType.UInt32, MySqlDbType.VarChar, MySqlDbType.VarChar, MySqlDbType.UInt32,
                                           MySqlDbType.MediumText, MySqlDbType.VarChar, MySqlDbType.LongBlob };//для типов в (AddParameters)


        //При загрузке отобразить таблицу
        private void TestFormTwo_Load(object sender, EventArgs e)
        {
            FillDataGridView(""); //при загрузке отображаем таблицу          
        }


        //Заполняем DataGridView и корректируем её
        public void FillDataGridView(string valueToSearch)
        {
            //получаем запрос на отображение данных с поиском
            string query = connection.GetQueryShowSearch(nameTable, nameFieldsAll, nameFieldsAS, numericFields, valueToSearch);
            //заполняем данные таблицы на основе запроса
            //connection.FillDataGridView(dataGridView1, query);
            //настраиваем отображение таблицы
            //settings.GetSettingDisplayTable(dataGridView1, 100);
            //Для отображения картинки в DataGridView
            //settings.GetViewImageInCellTable(dataGridView1, 6);
            //((DataGridViewImageColumn)this.dataGridView1.Columns["info.photo"]).DefaultCellStyle.NullValue = null;
            DataTable table = connection.FillDataGridView(dataGridView1, 100, new[] {6}, query); //заполняем таблицу данными с запроса и настраиваем
            managerBase = this.BindingContext[table]; //подключаем таблицу для передвижения по ней
        }

        //для выбора изображения 
        private void BTN_CHOOSE_IMAGE_Click(object sender, EventArgs e)
        {
            settings.GetChooseImage(pictureBox1); //выбрать изображение из файла
        }


        //Когда пользователь будет кликать мышью по нужной строке, то в textBox-ах и pictuerBox будут отображаться данные текущей строки
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            //отображаем текущую картинку из DataGridView в pictureBox
            settings.CurrentColumnCellsIMG(6, pictureBox1, dataGridView1);

            //Массив куда будут (в textBox-ы) отображаться значения текущей строки из DataGridView
            TextBox[] textBoxs = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6 };
            //отображаем все текстовые записи из DataGridView в textBox-ы
            settings.CurrentColumnCellsTEXT(textBoxs, dataGridView1);
        }

        //Когда пользователь будет нажимать стрелками клавиш "вверх" и "вниз" то будут в textBox-ах и pictuerBox отображаться данные текущей строки
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //отображаем текущую картинку из DataGridView в pictureBox
            settings.CurrentColumnCellsIMG(6, pictureBox1, dataGridView1);
            //Массив куда будут (в textBox-ы) отображаться значения текущей строки из DataGridView
            TextBox[] textBoxs = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6 };
            //отображаем все текстовые записи из DataGridView в textBox-ы
            settings.CurrentColumnCellsTEXT(textBoxs, dataGridView1);
        }

        #region работа с данными (вставка, обновление, удаление)
        //Вставить данные 
        private void Insert_Click(object sender, EventArgs e)
        {
            //Если есть изображение, тогда добавляем
            if (pictureBox1.Image != null)
            {
                //возвращаем результаты проверок всех полей
                bool resultSecurity = checking.SecurityAll(textBox1, textBox2, textBox3, textBox5, textBox6),
                    resultVoid = checking.VoidAll(textBox1, textBox2, textBox3); //Проверяем только обязательные для ввода поля
                //если результаты вернулись положительные, тогда можно добавить данные, иначе вывести ошибку
                if (resultSecurity == true && resultVoid == true)
                {
                    //Сохранить изображение из pictureBox в массив byte[]
                    byte[] img = settings.SaveImagesToBytes(pictureBox1);
                    //получить запрос для вставки Insert
                    string query = connection.GetQueryInsert(nameTable, nameFieldsAll, variables);
                    //выполняить команду с Insert
                    connection.command = new MySqlCommand(query, connection.connection);
                    //для объектов, у них есть данные которые нужно вставить
                    object[] objects = {textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, img};
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
            else
            {
                MessageBox.Show("Выберите изображение!");
            }
        }

        //Обновленяем нужные нам данные
        private void Update_Click(object sender, EventArgs e)
        {
            //возвращаем результаты проверок всех полей
            bool resultSecurity = checking.SecurityAll(textBox1, textBox2, textBox3, textBox5, textBox6),
                resultVoid = checking.VoidAll(textBox1, textBox2, textBox3); //Проверяем только обязательные для ввода поля
           //если результаты вернулись положительные, тогда можно добавить данные, иначе вывести ошибку
            if (resultSecurity == true && resultVoid == true)
            {
                //Сохранить изображение из pictureBox в массив byte[]
                byte[] img = settings.SaveImagesToBytes(pictureBox1);
                //Получаем запрос для обновления Update
                string query = connection.GetQueryUpdate(nameTable, nameFieldsAll, variables, id_field);
                //выполнить команду
                connection.command = new MySqlCommand(query, connection.connection);
                //для объектов, у них есть данные которые нужно вставить
                object[] objects = {textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, img};
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
                object[] objects = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6 };
                // Добавляем данные
                connection.AddParameters(connection.command, variables[0], mySqlDbTypes[0], objects[0]);
                //попытаться выполнить запрос
                connection.ExecuteQuery(connection.command);
                //отобразить новые данные 
                FillDataGridView("");
                //Что есть на форме
                TextBox[] textBoxs = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6 };
                PictureBox[] pictureBoxs = { pictureBox1 };
                //Очищяем поля
                settings.ClearFields(textBoxs, pictureBoxs: pictureBoxs);
            }
            else
            {
                checking.ErrorMessage(this);
            }

        }
        #endregion

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
                object[] objects = { textBox1};

                connection.AddParameters(connection.command, variables[0], mySqlDbTypes[0], objects);

                TextBox[] textBoxs = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6 };
                PictureBox[] pictureBoxs = { pictureBox1 };
                int[] ColumnsTextForTextBox = { 0, 1, 2, 3, 4, 5 };//столбцы таблицы с которых нужно взять данные и вставить в TextBox-ы
                //Найти запись 
                DataTable table = connection.Find(connection.command, textBoxs: textBoxs, ColumnsTextForTextBox: ColumnsTextForTextBox,
                                                    pictureBoxs: pictureBoxs, ColumnsPictureForPictureBox: new[] { 6 }, dataGrid:dataGridView1);
                //для передвижения по таблице
                managerBase = this.BindingContext[table];
            }
            else
            {
                checking.ErrorMessage(this);
            }
        }

        //Для поиска
        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            FillDataGridView(textBoxSearch.Text);
        }

        //Очисть поля
        private void Clear_Click(object sender, EventArgs e)
        {
            TextBox[] textBoxs = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6 };
            PictureBox[] pictureBoxs = { pictureBox1 };
            settings.ClearFields(textBoxs, pictureBoxs: pictureBoxs);
        }

        #region навигация по таблице
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

        #region создание отчета
        private void ReportButton_Click(object sender, EventArgs e)
        {
            report.CreateBasicReport(dataGridView1,saveFileDialog1, "Информация о сотрудниках",GetValues(), 6);
        }

        private string[] GetValues()
        {
            //Результаты 
            List<string> allResult = new List<string>();

            //Общее данные
            string partQuery =
                " select concat(coalesce(info.full_name, ''), \"; \", coalesce(info.passport_id, ''), \"; \"," +
                " coalesce(info.age, ''), \" лет; \", coalesce(info.address, ''), \"; \"," +
                " coalesce(info.phone, '')) AS result " +
                " from " + connection.NAME_DATABASE + ".info " +
                " where info.age = (SELECT ",
                otherPart = "(info.age) from " + connection.NAME_DATABASE + ".info);",
                query = default(string),
                result = null;

            //Максимальный возраст сотрудника
            query = partQuery + " max" + otherPart;
            result = "Максимальный возрасто сотрудника: " + connection.GetOneResult(query);
            allResult.Add(result);

            //Минимальный возраст сотрудника
            query = partQuery + " min" + otherPart;
            result = "Минимальный возрасто сотрудника: " + connection.GetOneResult(query);
            allResult.Add(result);

            //Общее кол. человек в супермаркете
            query = connection.GetSelectFunc("info", "id_info", "count");
            result = "Общее кол. сотрудников супермаркета: " + connection.GetOneResult(query);
            allResult.Add(result);

            //Средний возраст в супермаркете
            query = connection.GetSelectFunc("info", "age");
            result = "Средний возраст сотрудников супермаркета: " + connection.GetOneResult(query);
            allResult.Add(result);

            return allResult.ToArray();
        }
        #endregion

    }
}
