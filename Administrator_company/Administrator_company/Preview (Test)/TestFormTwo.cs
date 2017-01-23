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
            //dataGridView1.Columns["Фото"].DefaultCellStyle.NullValue = null;
        }


        /* public MySqlConnection connection = new MySqlConnection("datasource=localhost; port=3306; username = root; password = andrey_1a6c2b");

         //При загрузке отобразить таблицу
         private void TestFormTwo_Load(object sender, EventArgs e)
         {
             FillDataGridView("");
         }

         //Заполняем DataGridView и корректируем её
         public void FillDataGridView(string valueToSearch)
         {
             //MySqlCommand command = new MySqlCommand("SELECT * FROM supermarket.info", connection); //Создаём запрос для выполнения команды
             //string query = " SELECT * FROM supermarket.info " +
            //                " WHERE CONCAT(id_info, full_name, passport_id, age, address, phone, photo) " +
               //             " LIKE '%"+valueToSearch+"%'";
             string query = default(string);
             uint valueNumber = 0;

             if (valueToSearch != "" && uint.TryParse(valueToSearch, out valueNumber) == true)
             {
                 if (valueNumber > 0)
                     query = " SELECT * FROM supermarket.info " +
                             " WHERE CONCAT(id_info, age) " +
                             " LIKE '%" + valueNumber + "%'";
             }
             else
             {
                query = " SELECT id_info AS 'id', full_name AS 'Имя', passport_id AS 'Серия и номер паспорта', age, address, phone, photo FROM supermarket.info " +
                         " WHERE CONCAT(id_info, full_name, passport_id, age, address, phone, photo) " +
                         " LIKE '%" + valueToSearch + "%'";
             }

             MySqlCommand command = new MySqlCommand(query, connection); //Создаём запрос для поиска
             MySqlDataAdapter adapter = new MySqlDataAdapter(command); //Выполняем команду



             //Для отображения в таблице
             DataTable table = new DataTable(); //Создаём таблицу
             adapter.Fill(table); //Вставляем данные при выполнении команды в таблицу

             dataGridView1.RowTemplate.Height = 60; //высота строк
             dataGridView1.AllowUserToAddRows = false; //нельзя пользователю добавлять самому строки


             dataGridView1.DataSource = table; //подключаем заполненную таблицу и отображаем

             //Для отображения картинки в DataGridView
             DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
             imgCol = (DataGridViewImageColumn)dataGridView1.Columns[6];
             imgCol.ImageLayout = DataGridViewImageCellLayout.Stretch;

             //Как будет отображаться таблица
             dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; //Растягивать таблицу (колонки) под окно dataGridView


         }

         //для выбора изображения 
         private void BTN_CHOOSE_IMAGE_Click(object sender, EventArgs e)
         {
             OpenFileDialog openFile = new OpenFileDialog();
             openFile.Filter = "Choose Image(*.JPG; *.PNG, *.GIF) | *.jpg; *.png; *.gif";

             if (openFile.ShowDialog() == DialogResult.OK)
             {
                 pictureBox1.Image = Image.FromFile(openFile.FileName); // если пользователь выбрал картинку то вставляем в pictureBox
             }
         }

         //Когда пользователь будет кликать мышью по нужной строке, то в textBox-ах и pictuerBox будут отображаться данные текущей строки
         private void dataGridView1_Click(object sender, EventArgs e)
         {
             //отображаем текущую картинку из DataGridView в pictureBox
             CurrentRowCellsIMG(6, pictureBox1, dataGridView1);

             //Массив куда будут (в textBox-ы) отображаться значения текущей строки из DataGridView
             TextBox[] textBoxs = {textBox1, textBox2, textBox3, textBox4, textBox5, textBox6};
             //отображаем все текстовые записи из DataGridView в textBox-ы
             CurrentRowCellsTEXT(textBoxs, dataGridView1);

         }

         //Когда пользователь будет нажимать стрелками клавиш "вверх" и "вниз" то будут в textBox-ах и pictuerBox отображаться данные текущей строки
         private void dataGridView1_SelectionChanged(object sender, EventArgs e)
         {
             //отображаем текущую картинку из DataGridView в pictureBox
              CurrentRowCellsIMG(6, pictureBox1, dataGridView1);

             //Массив куда будут (в textBox-ы) отображаться значения текущей строки из DataGridView
             TextBox[] textBoxs = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6 };
             //отображаем все текстовые записи из DataGridView в textBox-ы
             CurrentRowCellsTEXT(textBoxs, dataGridView1);
         }

         /// <summary>
         /// Отображает из текущей ячейки строки DataGridView картинку в PicureBox
         /// </summary>
         /// <param name="number">номер столбца (ячейки) DataGridView в котором содержиться картинка</param>
         /// <param name="pictureBox">pictureBox куда нужно вставить картинку</param>
         /// <param name="dataGridView">текущий dataGridView</param>
         public void CurrentRowCellsIMG(int number, PictureBox pictureBox, DataGridView dataGridView)
         {
             //Если ячейка пустая, то в pictureBox ничего не отображать
             if (dataGridView.CurrentRow.Cells[number].Value  == DBNull.Value)
             {
                 pictureBox.Image = null;
             }
             else
             {
                 try
                 {
                     Byte[] img = (Byte[]) dataGridView.CurrentRow.Cells[number].Value; //Получаем изображание
                     MemoryStream ms = new MemoryStream(img);
                     pictureBox.Image = Image.FromStream(ms); // вставляем в pictureBox это изображение
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show(ex.Message);
                 }

             }
         }

         /// <summary>
         /// Помещает в textBox значение с ячейки текущей строки DataGridView
         /// </summary>
         /// <param name="number">Номер столбца (ячейки) DataGridView в которой содержиться текстовое значение</param>
         /// <param name="textBox">textBox куда нужно вставить значение</param>
         /// <param name="dataGridView">текущий dataGridView</param>
         public void CurrentRowCellsTEXT(int number, TextBox textBox, DataGridView dataGridView)
         {
             textBox.Text = dataGridView.CurrentRow.Cells[number].Value.ToString();
         }

         /// <summary>
         /// Помещает во ВСЕ textBox-ы значения из текущей строки DataGridView
         /// </summary>
         /// <param name="textBoxs">Массив textBox-ов куда нужно вставить значения</param>
         /// <param name="dataGridView">текущий dataGridView</param>
         public void CurrentRowCellsTEXT(TextBox[] textBoxs, DataGridView dataGridView)
         {

             for (var i=0; i< textBoxs.Length; i++)
             {
                 //передаёт номер ячейки и сам textBox куда нужно вставить значения из строки DataGridView
                 CurrentRowCellsTEXT(i, textBoxs[i], dataGridView); 
             }
         }


         private void Insert_Click(object sender, EventArgs e)
         {
             //1-й этап
             MemoryStream ms = new MemoryStream();
             pictureBox1.Image.Save(ms,pictureBox1.Image.RawFormat);
             byte[] img = ms.ToArray();

             //2-й этап
             string query = "INSERT INTO supermarket.info (id_info, full_name, passport_id, age, address, phone, photo) " +
                            " VALUES(@id, @name, @passport, @age, @address, @phone, @photo)";

             //3-й этап
             MySqlCommand command = new MySqlCommand(query,connection);

             command.Parameters.Add("@id",MySqlDbType.UInt32).Value = textBox1.Text;
             command.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox2.Text;
             command.Parameters.Add("@passport", MySqlDbType.VarChar).Value = textBox3.Text;
             command.Parameters.Add("@age", MySqlDbType.UInt32).Value = textBox4.Text;
             command.Parameters.Add("@address", MySqlDbType.MediumText).Value = textBox5.Text;
             command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = textBox6.Text;
             command.Parameters.Add("@photo", MySqlDbType.LongBlob).Value = img;

             //для будущей функции
             //для типов
             MySqlDbType[] mySqlDbTypes = { MySqlDbType.UInt32, MySqlDbType.VarChar, MySqlDbType.MediumText, MySqlDbType.LongBlob };
             //для переменных
             string[] value = {"@id", "@name"};
             //для объектов
             object values = new {textBox1, textBox2, textBox3, textBox4, img};

             ExecuteQuery(command, "Данные успешно добавлены!");
         }

         public void ExecuteQuery(MySqlCommand command, string message)
         {
             connection.Open();
             if (command.ExecuteNonQuery() == 1)
             {
                 MessageBox.Show(message);
             }
             else
             {
                 MessageBox.Show("Ошибка при выполнении запроса!");
             }
             connection.Close();
             //как выполнилась функция сразу обновить dataGridView
             FillDataGridView("");
         }

         private void Update_Click(object sender, EventArgs e)
         {
             MemoryStream ms = new MemoryStream();
             pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
             byte[] img = ms.ToArray();
             //сделать только это
             string query = "UPDATE supermarket.info " +
                            "SET full_name = @name, passport_id = @passport, age = @age, address = @address, phone = @phone, photo = @photo" +
                            " WHERE id_info = @id";
             MySqlCommand command = new MySqlCommand(query, connection);
             command.Parameters.Add("@id", MySqlDbType.UInt32).Value = textBox1.Text;
             command.Parameters.Add("@name", MySqlDbType.VarChar).Value = textBox2.Text;
             command.Parameters.Add("@passport", MySqlDbType.VarChar).Value = textBox3.Text;
             command.Parameters.Add("@age", MySqlDbType.UInt32).Value = textBox4.Text;
             command.Parameters.Add("@address", MySqlDbType.MediumText).Value = textBox5.Text;
             command.Parameters.Add("@phone", MySqlDbType.VarChar).Value = textBox6.Text;
             command.Parameters.Add("@photo", MySqlDbType.LongBlob).Value = img;

             ExecuteQuery(command, "Данные успешно обновлены!");
         }


         private void Delete_Click(object sender, EventArgs e)
         {
             string query = "DELETE FROM supermarket.info WHERE id_info = @id";
             MySqlCommand command = new MySqlCommand(query, connection);
             command.Parameters.Add("@id", MySqlDbType.UInt32).Value = textBox1.Text;

             ExecuteQuery(command, "Данные успешно удалены!");

             ClearFields();

         }

         private void textBoxSearch_TextChanged(object sender, EventArgs e)
         {
             FillDataGridView(textBoxSearch.Text);
         }
         
         private void Find_Click(object sender, EventArgs e)
         {
             string query = " SELECT * FROM supermarket.info " +
                            " WHERE id_info = @id ";
             MySqlCommand command = new MySqlCommand(query, connection.connection); //Создаём запрос для поиска
             command.Parameters.Add("@id", MySqlDbType.UInt32).Value = textBox1.Text;

             MySqlDataAdapter adapter = new MySqlDataAdapter(command); //Выполняем команду

             //Для отображения в таблице
             DataTable table = new DataTable(); //Создаём таблицу
             adapter.Fill(table); //Вставляем данные при выполнении команды в таблицу
             if (table.Rows.Count <= 0)
             {
                 MessageBox.Show("Не найдено!");
                 ClearFields();
             }
             else
             {
                
                 textBox1.Text = table.Rows[0][0].ToString();
                 textBox2.Text = table.Rows[0][1].ToString();
                 textBox3.Text = table.Rows[0][2].ToString();
                 textBox4.Text = table.Rows[0][3].ToString();
                 textBox5.Text = table.Rows[0][4].ToString();
                 textBox6.Text = table.Rows[0][5].ToString();

                 byte[] img = (byte[])table.Rows[0][6];
                 MemoryStream ms = new MemoryStream(img);
                 pictureBox1.Image = Image.FromStream(ms);
             }
         }


         //написать общую
         public void ClearFields()
         {

             textBox1.Text = "";
             textBox2.Text = "";
             textBox3.Text = "";
             textBox4.Text = "";
             textBox5.Text = "";
             textBox6.Text = "";
             pictureBox1.Image = null;
         }*/

        Connection connection = new Connection();
        Settings settings = new Settings();

        //При загрузке отобразить таблицу
        private void TestFormTwo_Load(object sender, EventArgs e)
        {
            FillDataGridView("");
        }

        //Заполняем DataGridView и корректируем её
        public void FillDataGridView(string valueToSearch)
        {
            //получаем запрос на отображение данных с поиском
            string nameTable = "info";
            string[] nameFieldsAll = {"id_info", "full_name", "passport_id", "age", "address", "phone", "photo"};
            string[] nameFieldsAS = {"ИД","ФИО","Серия и номер паспорта","Возраст","Адрес","Телефон","Фото"};
            string[] numericFields = {"id_info","age"};
            string query = connection.GetQueryShowSearch(nameTable, nameFieldsAll, nameFieldsAS, numericFields, valueToSearch);
            //заполняем данные таблицы на основе запроса
           //connection.FillDataGridView(dataGridView1, query);
            //настраиваем отображение таблицы
            //settings.GetSettingDisplayTable(dataGridView1, 100);
            //Для отображения картинки в DataGridView
            //settings.GetViewImageInCellTable(dataGridView1, 6);
            connection.FillDataGridView(dataGridView1, 100, new[] {6}, query);

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
            //1-й этап
            byte[] img = settings.SaveImagesToBytes(pictureBox1);

            //2-й этап
            string nameTable = "info";
            string[] fields = {"id_info", "full_name", "passport_id", "age", "address", "phone", "photo"},
                variables = {"@id", "@name", "@passport", "@age", "@address", "@phone", "@photo"}; //для переменных
            string query = connection.GetQueryInsert(nameTable, fields, variables);

            //3-й этап
             connection.command = new MySqlCommand(query, connection.connection);

            //для типов
            MySqlDbType[] mySqlDbTypes = { MySqlDbType.UInt32, MySqlDbType.VarChar, MySqlDbType.VarChar, MySqlDbType.UInt32,
                                           MySqlDbType.MediumText, MySqlDbType.VarChar, MySqlDbType.LongBlob };
            //для объектов
            object[] objects =  { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, img };
            connection.AddParameters(connection.command,variables, mySqlDbTypes, objects);

            connection.ExecuteQuery(connection.command);
            FillDataGridView("");
        }

        private void Update_Click(object sender, EventArgs e)
        {
            byte[] img = settings.SaveImagesToBytes(pictureBox1);

            string nameTable = "info";
            string[] fields = { "id_info", "full_name", "passport_id", "age", "address", "phone", "photo" },
                variables = { "@id", "@name", "@passport", "@age", "@address", "@phone", "@photo" }; //для переменных
            string id_field = "id_info";
            string query = connection.GetQueryUpdate(nameTable, fields, variables, id_field);

            connection.command = new MySqlCommand(query, connection.connection);

            //для типов
            MySqlDbType[] mySqlDbTypes = { MySqlDbType.UInt32, MySqlDbType.VarChar, MySqlDbType.VarChar, MySqlDbType.UInt32,
                                           MySqlDbType.MediumText, MySqlDbType.VarChar, MySqlDbType.LongBlob };
            //для объектов
            object[] objects = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6, img };
            connection.AddParameters(connection.command, variables, mySqlDbTypes, objects);

            connection.ExecuteQuery(connection.command);
            FillDataGridView("");
        }


        private void Delete_Click(object sender, EventArgs e)
        {
            string nameTable = "info";
            string id_field = "id_info";
            string query = connection.GetQueryDelete(nameTable, id_field);
            
            connection.command = new MySqlCommand(query, connection.connection);

            string[] variables = { "@id", "@name", "@passport", "@age", "@address", "@phone", "@photo" }; //для переменных
                                                                                                          //для типов
            MySqlDbType[] mySqlDbTypes = { MySqlDbType.UInt32, MySqlDbType.VarChar, MySqlDbType.VarChar, MySqlDbType.UInt32,
                                           MySqlDbType.MediumText, MySqlDbType.VarChar, MySqlDbType.LongBlob };
            //для объектов
            object[] objects = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6};

            connection.AddParameters(connection.command, variables[0], mySqlDbTypes[0], objects[0]);

            connection.ExecuteQuery(connection.command);
            FillDataGridView("");

            TextBox[] textBoxs = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6 };
            PictureBox[] pictureBoxs = {pictureBox1};
            settings.ClearFields(textBoxs, pictureBoxs:pictureBoxs);

        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            FillDataGridView(textBoxSearch.Text);
        }
        
        private void Find_Click(object sender, EventArgs e)
        {
            string nameTable = "info";
            string[] fields = { "id_info", "full_name", "passport_id", "age", "address", "phone", "photo" },
                variables = { "@id", "@name", "@passport", "@age", "@address", "@phone", "@photo" }, //для переменных
                nameFieldsAS = { "ИД", "ФИО", "'Серия и номер паспорта'", "Возраст", "Адрес", "Телефон", "Фото" };
            string id_field = "id_info";

            string query = connection.GetQueryFindSelect(nameTable, fields, nameFieldsAS, id_field);

             connection.command = new MySqlCommand(query, connection.connection); //Создаём запрос для поиска

            
            MySqlDbType[] mySqlDbTypes = { MySqlDbType.UInt32, MySqlDbType.VarChar, MySqlDbType.VarChar, MySqlDbType.UInt32,
                                           MySqlDbType.MediumText, MySqlDbType.VarChar, MySqlDbType.LongBlob };
            //для объектов
            object[] objects = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6 };

            connection.AddParameters(connection.command, variables[0], mySqlDbTypes[0], objects[0]);

            //connection.adapter = new MySqlDataAdapter(connection.command); //Выполняем команду
            
            ///Для отображения в таблице
            DataTable table = new DataTable(); //Создаём таблицу
            connection.adapter.SelectCommand = connection.command;
            connection.adapter.Fill(table); //Вставляем данные при выполнении команды в таблицу

            if (table.Rows.Count <= 0)
            {
                MessageBox.Show("Указанная запись: " + textBox1.Text + " не найдена!");

                TextBox[] textBoxs = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6 };
                PictureBox[] pictureBoxs = { pictureBox1 };
                settings.ClearFields(textBoxs, pictureBoxs: pictureBoxs);
            }
            else
            {
                MessageBox.Show("Указанная запись " + textBox1.Text + " найдена!");
                TextBox[] textBoxs = {textBox1, textBox2, textBox3, textBox4, textBox5, textBox6};
                int[] rows = {0, 1, 2, 3, 4, 5};
                settings.InsertTextInTextBoxFromTable(table, rows, textBoxs);
                settings.InsertImageInPictureBoxFromTable(table, 6, pictureBox1);
            }


        }
        
        private void Clear_Click(object sender, EventArgs e)
        {

            TextBox[] textBoxs = { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6 };
            PictureBox[] pictureBoxs = { pictureBox1 };
            settings.ClearFields(textBoxs, pictureBoxs: pictureBoxs);
        }


    }
}
