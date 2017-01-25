using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Дальше подключаемые 
using MySql.Data.MySqlClient;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Administrator_supermarket;

namespace Administrator_company.Preview__Test_
{
    public partial class TestFormTwo : Form
    {

        public TestFormTwo()
        {
            InitializeComponent();
        }

        Connection connection = new Connection();
        Settings settings = new Settings();
        Checking checking = new Checking();
        BindingManagerBase managerBase;


        private static string nameTable = "info",
                              id_field = "id_info";
        private static string[] nameFieldsAll = { "id_info", "full_name", "passport_id", "age", "address", "phone", "photo" }, //все поля
                                 variables = { "@id", "@name", "@passport", "@age", "@address", "@phone", "@photo" }, //для переменных
                                 nameFieldsAS = { "ИД", "ФИО", "Серия и номер паспорта", "Возраст", "Адрес", "Телефон", "Фото" }, //как будут отображаться
                                 numericFields = { "id_info", "age" }; //для корректного поиска по числовым столбцам                                                    
        private static MySqlDbType[] mySqlDbTypes = { MySqlDbType.UInt32, MySqlDbType.VarChar, MySqlDbType.VarChar, MySqlDbType.UInt32,
                                           MySqlDbType.MediumText, MySqlDbType.VarChar, MySqlDbType.LongBlob };//для типов


        //При загрузке отобразить таблицу
        private void TestFormTwo_Load(object sender, EventArgs e)
        {
            FillDataGridView("");           
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
            DataTable table = connection.FillDataGridView(dataGridView1, 100, new[] {6}, query);
            managerBase = this.BindingContext[table];
        }

        //для выбора изображения 
        private void BTN_CHOOSE_IMAGE_Click(object sender, EventArgs e)
        {
            settings.GetChooseImage(pictureBox1);
        }


        //Когда пользователь будет кликать мышью по нужной строке, то в textBox-ах и pictuerBox будут отображаться данные текущей строки
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            //отображаем текущую картинку из DataGridView в pictureBox
            settings.CurrentRowCellsIMG(6, pictureBox1, dataGridView1);

            //Массив куда будут (в textBox-ы) отображаться значения текущей строки из DataGridView
            TextBox[] textBoxs = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6 };
            //отображаем все текстовые записи из DataGridView в textBox-ы
            settings.CurrentRowCellsTEXT(textBoxs, dataGridView1);
        }

        //Когда пользователь будет нажимать стрелками клавиш "вверх" и "вниз" то будут в textBox-ах и pictuerBox отображаться данные текущей строки
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //отображаем текущую картинку из DataGridView в pictureBox
            settings.CurrentRowCellsIMG(6, pictureBox1, dataGridView1);

            //Массив куда будут (в textBox-ы) отображаться значения текущей строки из DataGridView
            TextBox[] textBoxs = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6 };
            //отображаем все текстовые записи из DataGridView в textBox-ы
            settings.CurrentRowCellsTEXT(textBoxs, dataGridView1);
        }

        private void Insert_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                //возвращаем результаты проверок всех полей
                bool resultSecurity = checking.SecurityAll(textBox1, textBox2, textBox3, textBox5, textBox6),
                    resultVoid = checking.VoidAll(textBox1, textBox2, textBox3); //Проверяем только обязательные для ввода поля
                                                                                 //если результаты вернулись положительные, тогда можно добавить данные, иначе вывести ошибку
                if (resultSecurity == true && resultVoid == true)
                {
                    //1-й этап
                    byte[] img = settings.SaveImagesToBytes(pictureBox1);

                    string query = connection.GetQueryInsert(nameTable, nameFieldsAll, variables);

                    //3-й этап
                    connection.command = new MySqlCommand(query, connection.connection);

                    //для объектов
                    object[] objects = {textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, img};
                    connection.AddParameters(connection.command, variables, mySqlDbTypes, objects);

                    connection.ExecuteQuery(connection.command);
                    FillDataGridView("");
                }
                else
                {
                    checking.ErrorMessage(this);
                }
            }
            else
            {
                MessageBox.Show("Выберите изображение!");
            }
        }

        private void Update_Click(object sender, EventArgs e)
        {
            //возвращаем результаты проверок всех полей
            bool resultSecurity = checking.SecurityAll(textBox1, textBox2, textBox3, textBox5, textBox6),
                resultVoid = checking.VoidAll(textBox1, textBox2, textBox3); //Проверяем только обязательные для ввода поля
                                                                                                           //если результаты вернулись положительные, тогда можно добавить данные, иначе вывести ошибку
            if (resultSecurity == true && resultVoid == true)
            {
                byte[] img = settings.SaveImagesToBytes(pictureBox1);

                string query = connection.GetQueryUpdate(nameTable, nameFieldsAll, variables, id_field);

                connection.command = new MySqlCommand(query, connection.connection);

                //для объектов
                object[] objects = {textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, img};
                connection.AddParameters(connection.command, variables, mySqlDbTypes, objects);

                connection.ExecuteQuery(connection.command);
                FillDataGridView("");
            }
            else
            {
                checking.ErrorMessage(this);
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            bool resultSecurity = checking.SecurityAll(textBox1),
                resultVoid = checking.VoidAll(textBox1);
            if (resultSecurity == true && resultVoid == true)
            {
                string query = connection.GetQueryDelete(nameTable, id_field);

                connection.command = new MySqlCommand(query, connection.connection);

                //для объектов
                object[] objects = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6 };

                connection.AddParameters(connection.command, variables[0], mySqlDbTypes[0], objects[0]);

                connection.ExecuteQuery(connection.command);
                FillDataGridView("");

                TextBox[] textBoxs = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6 };
                PictureBox[] pictureBoxs = { pictureBox1 };
                settings.ClearFields(textBoxs, pictureBoxs: pictureBoxs);
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
                    string[] nameFieldsAS = { "ИД", "ФИО", "'Серия и номер паспорта'", "Возраст", "Адрес", "Телефон", "Фото" };

                string query = connection.GetQueryFindSelect(nameTable, nameFieldsAll, nameFieldsAS, id_field);

                connection.command = new MySqlCommand(query, connection.connection); //Создаём запрос для поиска
                //для объектов
                object[] objects = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6 };

                connection.AddParameters(connection.command, variables[0], mySqlDbTypes[0], objects[0]);

                TextBox[] textBoxs = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6 };
                PictureBox[] pictureBoxs = { pictureBox1 };
                int[] rowsTextForTextBox = { 0, 1, 2, 3, 4, 5 };
                DataTable table = connection.Find(connection.command, textBoxs: textBoxs, rowsTextForTextBox: rowsTextForTextBox,
                                                    pictureBoxs: pictureBoxs, rowsPictureForPictureBox: new[] { 6 });
                managerBase = this.BindingContext[table];
            }
            else
            {
                checking.ErrorMessage(this);
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            FillDataGridView(textBoxSearch.Text);
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            TextBox[] textBoxs = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6 };
            PictureBox[] pictureBoxs = { pictureBox1 };
            settings.ClearFields(textBoxs, pictureBoxs: pictureBoxs);
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
