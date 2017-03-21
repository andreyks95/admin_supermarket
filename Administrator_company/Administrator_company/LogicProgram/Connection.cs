using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Administrator_company.LogicProgram
{
    //В этот класс будет скидываться все общие переменные (константы) и методы, которые будут использоваться для работы с таблицами
    public class Connection
    {
        #region Подключение к БД
        //Подсоединение к БД MySQL
        public MySqlConnection connection = new MySqlConnection("datasource=localhost; port=3306; username = root; password = andrey_1a6c2b");

        //подключение к серверу где находится бд.
        //public MySqlConnection connection = new MySqlConnection("Server =sql7.freemysqlhosting.net; Port=3306; Database=sql7150982; Uid=sql7150982; Pwd=1VAsQp6rY1;");
        
        //для выполнения комманд в дальнейшем
        public MySqlCommand command;

        public MySqlDataAdapter adapter;

        public string NAME_DATABASE = "supermarket";

        DataTable table = new DataTable();  

        //Открыть соединение для работы с БД
        public void OpenConnection()
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();
        }

        //Закрыть соединение для работы с БД
        public void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
        }
        #endregion

        #region Попытка выполнить запрос
        /// <summary>
        /// Попытаться добавить (выполнить) необходимый нам запрос
        /// </summary>
        /// <param name="query">Запрос</param>
        /// <param name="showMessageBox">Показать диалоговое сообщения или нет</param>
        public void ExecuteQuery(String query, Boolean showMessageBox=true)
        {
           //попытаться выполнить
            try
            {
                OpenConnection();
                command = new MySqlCommand(query, connection);
                if (command.ExecuteNonQuery() == 1)
                {
                    if (showMessageBox == true)
                        MessageBox.Show("Запрос успешно выполнен");
                }
                else
                {
                    if(showMessageBox == true)
                        MessageBox.Show("Запрос не выполнен");
                }
            }
            //вызвать исключения с сообщением об ошибке 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //в других случаях закрыть соединение 
            finally
            {
                CloseConnection();
            }
        }
        #endregion

        #region Методы для отображения таблицы

        #region Отобразить выбранные поля в таблице
        /// <summary>
        /// Перегруженный методы, чтобы отобразить необходимые нам поля в таблице по запросу
        /// </summary>
        /// <param name="DataGridView">Где будем отображать таблицу</param>
        /// <param name="query">Запрос в котором содержится что вывести</param>
        /// <param name="nameTable">Название таблицы, которую будем выводить</param>
        public void ShowTable( System.Windows.Forms.DataGridView DataGridView, string query)//, string nameTable)
        {
            //System.Windows.Forms.DataGridView DataGridView 
            //Передаём в качестве параметра DataGridView из любых форм
            //и можем использовать его методы
            //например DataGridView.DataSource 
            try
            {
                //выбрать все поля с таблицы БД
                adapter = new MySqlDataAdapter(query, connection);
                connection.Open(); //открыть соединение
                DataSet ds = new DataSet(); //создать новый DataSet
                adapter.Fill(ds, "Table");//nameTable); //заполнить 
                //Подключение таблицы
                DataGridView.DataSource = ds.Tables["Table"];
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Отобразить таблицу 
        //Метод, который содержит запрос для отображения таблицы
        public void ShowTable(string nameDatabase, string nameTable, System.Windows.Forms.DataGridView DataGridView)
        {
            //System.Windows.Forms.DataGridView DataGridView 
            //Передаём в качестве параметра DataGridView из любых форм
            //и можем использовать его методы
            //например DataGridView.DataSource 
            try
            {
                //выбрать все поля с таблицы БД
                adapter = new MySqlDataAdapter("SELECT * FROM " + nameDatabase + "." + nameTable, connection);
                connection.Open(); //открыть соединение
                DataSet ds = new DataSet(); //создать новый DataSet
                adapter.Fill(ds, nameTable); //заполнить 
                //Подключение таблицы
                DataGridView.DataSource = ds.Tables[nameTable];
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region для Telerik WinForm
        //Перегруженный метод, для Telerik WinForm
        /*public void ShowTable(string nameDatabase, string nameTable, Telerik.WinControls.UI.RadGridView DataGridView)
        {
            //System.Windows.Forms.DataGridView DataGridView 
            //Передаём в качестве параметра DataGridView из любых форм
            //и можем использовать его методы
            //например DataGridView.DataSource 
            try
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM " + nameDatabase + "." + nameTable, connection);
                connection.Open();
                DataSet ds = new DataSet();
                adapter.Fill(ds, nameTable);
                //Подключение таблицы
                DataGridView.DataSource = ds.Tables[nameTable];
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }*/
        #endregion

        #endregion

        #region Добавление (вставка) данных в таблицу 
        /// <summary>
        /// Метод, который содержит запрос для ДОБАВЛЕНИЯ данных в таблицу
        /// </summary>
        /// <param name="nameDatabase">Название БД</param>
        /// <param name="nameTable">Название таблицы для вставки</param>
        /// <param name="arr">Поля таблицы, куда необходимо вставить данные</param>
        /// <param name="textBox">Значение вводимых данных в TextBox-ов соответствующих полей таблицы</param>
        public void InsertDataTable(string nameDatabase, string nameTable, string[] arr, params TextBox[] textBox)
        {
            //вытаскиваем все поля таблицы и записуем в переменную fieldsTable для вставки в запрос 
            //Example: (name, age)
            string fieldsTable = "(";
            for (int i = 0; i < arr.Length; i++)
            {
                //если не последний элемент, то между ними ставим ", " 
                if (i < arr.Length - 1)
                    fieldsTable += arr[i] + ", ";
                else
                    fieldsTable += arr[i] + ") ";
            }

            //вытаскиваем значения всех textBox и записуем в переменную values для вставки в запрос
            //Example: ('Taras','41')
            string values = "('";
            for (int i = 0; i < textBox.Length; i++)
            {
                //если не последний  элемент, то между ними ставим "', '" 
                if (i < textBox.Length - 1)
                    values += textBox[i].Text + "','";
                else
                    values += textBox[i].Text + "')";
            }

            //Собственно сам запрос
            //Example: INSERT INTO Employees.Man (name, age) Values ('Taras','41');
            string insertQuery = "INSERT INTO " + nameDatabase + "." + nameTable +
                                 fieldsTable + "VALUES" + values;
            //попытаться выполнить запрос, сообщение (MessageBox) о успешности показывать
            ExecuteQuery(insertQuery);
        }
        #endregion

        #region Обновление данных (строки) в таблице 
        /// <summary>
        /// Метод, который содержит запрос для ОБНОВЛЕНИЯ поля таблицы по выбранному ID
        /// </summary>
        /// <param name="nameDatabase">название БД</param>
        /// <param name="nameTable">Название таблицы для обновления</param>
        /// <param name="fieldsTable">Названия полей которые нужно обновить</param>
        /// <param name="textBox">Значение вводимых данных в TextBox-ов соответствующих полей таблицы</param>
        public void UpdateDataTable(string nameDatabase, string nameTable, string[] fieldsTable, params TextBox[] textBox)
        {
            //вытаскиваем все значение со всех textBox и присваиваем их соответстующим полям таблицы
            //Example: name = 'Ivan', age = '25', id (которое нужно обновить запись) = '4'
            string values = null;
            for (int i = 0; i < fieldsTable.Length; i++)
            {
                if (i < fieldsTable.Length - 1)
                    values += fieldsTable[i] + " = " + "'" + textBox[i].Text + "', ";
                else
                {
                    //удаляем пробел и запятую перед WHERE, чтобы был корректный запрос.
                    values = values.Remove(values.Length - 2);
                    values += " WHERE " + fieldsTable[i] + " = " + textBox[i].Text;
                    //Было: "name = 'Ivan', age = '25', id (которое нужно обновить запись) = '4', "
                    //Стало: "name = 'Ivan', age = '25', id (которое нужно обновить запись) = '4'"
                }
            }
            //Собственно сам запрос на обновление (изменение данных)
            //Example: Update Employees.Man SET name = 'Ivan', age = '25', id (которое нужно обновить запись) = '4';
            string updateQuery = "UPDATE " + nameDatabase + "." + nameTable + " SET " + values;
            //попытаться выполнить запрос, сообщение (MessageBox) о успешности показывать
            ExecuteQuery(updateQuery);
        }
        #endregion

        #region Удаление данных (строки) в таблице 
        /// <summary>
        /// Метод, который содержит запрос для УДАЛЕНИЯ поля таблицы по выбранному ID
        /// </summary>
        /// <param name="nameDatabase">название БД</param>
        /// <param name="nameTable">Название таблицы для удаление строки</param>
        /// <param name="fieldsTable">Названия поля (Id) которое нужно удалить</param>
        /// <param name="textBox">Значение id</param>
        public void DeleteDataTable(string nameDatabase, string nameTable, string[] fieldsTable, params TextBox[] textBox)
        {
            //Example: DELETE FROM Employees.Man WHERE id_man = 5;
            string deleteQuery = "DELETE FROM " + nameDatabase + "." + nameTable + " WHERE " + fieldsTable[0] + " = " + textBox[0].Text;
            //попытаться выполнить запрос, сообщение (MessageBox) о успешности показывать
            ExecuteQuery(deleteQuery);
        }
        #endregion

        #region Посчитать значение в ячеки таблицы
        /// <summary>
        /// Метод который содержит обновления одного поля таблицы, то есть посчитать значения в ячейке (динамический).
        /// </summary>
        /// <param name="updateQuery">Запрос для обновления поля таблицы, которое нужно посчитать. Выполнить мат. действия с ним.</param>
        public void FieldDateTableCalculation(string updateQuery)
        {
            ExecuteQuery(updateQuery, false);
        }
        #endregion

        /*  //Метод который содержит обновления одного поля таблицы, то есть посчитать значения в ячейке. (статический использовался в качестве примера)
          public void FieldDateTableCalculation()
          {

              string updateQuery = "UPDATE grocery_supermarket_manager.stock AS T1, " +
                                       " (SELECT price_for_one " +
                                       " FROM grocery_supermarket_manager.products " +
                                       " WHERE id_products = 1) AS T2, " +

                                       " (SELECT quantity " +
                                       " FROM grocery_supermarket_manager.stock " +
                                       " WHERE id_stock = 1) AS T3 " +

                                   " SET T1.price = T2.price_for_one * T3.quantity " +
                                   " WHERE T1.id_stock = 1; ";
              ExecuteQuery(updateQuery, false);
          }
          */


        //Новые методы 

        #region FillDataGridView. Отобразить таблицу с учётом поиска значения
        /// <summary>
        /// Отображает таблицу с учётом поиска значения (числового или строкового)
        /// </summary>
        /// <param name="dataGridView">текущий DataGridView для таблицы</param>
        /// <param name="query">запрос, который содержит select с параметром поиска значения по столбцам (числовое или строковое)
        /// Если нету, то просто отображаем таблицу</param>
        public void FillDataGridView(DataGridView dataGridView, string query="")
         {
             try
             {
                 command = new MySqlCommand(query, connection); //Создаём запрос для поиска
                 adapter = new MySqlDataAdapter(command); //Выполняем команду
                 adapter.SelectCommand = command;
                 //Для отображения в таблице
                 table.Clear();
                 adapter.Fill(table); //Вставляем данные при выполнении команды в таблицу
                 dataGridView.DataSource = table; //подключаем заполненную таблицу и отображаем
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message);
             }
         }
        #endregion

        /* #region FillDataGridView overload. Отобразить таблицу с учётом поиска значения
          /// <summary>
          /// Отображает таблицу с учётом поиска значения (числового или строкового)
          /// </summary>
          /// <param name="dataGridView">текущий DataGridView для таблицы</param>
          /// <param name="height">Высота ячеек в таблице</param>
          /// <param name="cellsImages">Номера строк содержащих картинки</param>
          /// <param name="query">запрос, который содержит select с параметром поиска значения по столбцам (числовое или строковое)
          /// Если нету, то просто отображаем таблицу</param>
          public void FillDataGridView(DataGridView dataGridView,  int height, int[] cellsImages, string query = "")
          {
              try
              {
                  command = new MySqlCommand(query, connection); //Создаём запрос для поиска
                  adapter = new MySqlDataAdapter(command); //Выполняем команду
                  //Для отображения в таблице
                  adapter.SelectCommand = command;
                  table.Clear();
                  adapter.Fill(table); //Вставляем данные при выполнении команды в таблицу

                  Settings settings = new Settings();
                  //настраиваем отображение таблицы
                  settings.GetSettingDisplayTable(dataGridView, height);
                  dataGridView.DataSource = table; //подключаем заполненную таблицу и отображаем
                  //Для отображения картинки в DataGridView
                  settings.GetViewImageInCellTable(dataGridView, cellsImages);
              }
              catch (Exception ex)
              {
                  MessageBox.Show(ex.Message);
              }
          }
          #endregion*/

        #region FillDataGridView overload. Отобразить таблицу с учётом поиска значения
        /// <summary>
        /// Отображает таблицу с учётом поиска значения (числового или строкового)
        /// </summary>
        /// <param name="dataGridView">текущий DataGridView для таблицы</param>
        /// <param name="height">Высота ячеек в таблице</param>
        /// <param name="cellsImages">Номера строк содержащих картинки</param>
        /// <param name="query">запрос, который содержит select с параметром поиска значения по столбцам (числовое или строковое)
        /// Если нету, то просто отображаем таблицу</param>
        public DataTable FillDataGridView(DataGridView dataGridView, int height, int[] cellsImages=null, string query = "")
        {
            try
            {
                command = new MySqlCommand(query, connection); //Создаём запрос для поиска
                adapter = new MySqlDataAdapter(command); //Выполняем команду
                                                         //Для отображения в таблице
                //Создаём таблицу
                adapter.SelectCommand = command;
                table.Clear();
                adapter.Fill(table); //Вставляем данные при выполнении команды в таблицу
               /* dataView = new DataView(table);
                dataView.Sort = value;*/
                
                Settings settings = new Settings();
                //настраиваем отображение таблицы
                settings.GetSettingDisplayTable(dataGridView, height);
                dataGridView.DataSource = table; //подключаем заполненную таблицу и отображаем
                if(cellsImages != null)
                //Для отображения картинки в DataGridView
                settings.GetViewImageInCellTable(dataGridView, cellsImages);
               
                return table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                DataTable table = new DataTable();
                return table;
            }
        }
        #endregion

        #region  GetQueryShowSearch. Получить запрос исходя из условия (числовых или строковых столбцов)
        /// <summary>
        /// Составляет запрос select в зависимости от условия, на которое влияет значения столбцов (с числовыми данными или строковы)
        /// </summary>
        /// <param name="nameTable">Название таблицы</param>
        /// <param name="nameFieldsAll">Название столбцов для запроса</param>
        /// <param name="newNameFieldsAS">Назвать столбцы как. Как они будут отображаться в таблице</param>
        /// <param name="nameNumericFields">Название столбцов, которые содержат числовые значения</param>
        /// <param name="valueToSearh">Значения для поиска</param>
        /// <returns>Запрос SELECT</returns>
        public string GetQueryShowSearch(string nameTable, string[] nameFieldsAll,  string[] newNameFieldsAS, string[] nameNumericFields=null, string valueToSearh = "")
        {
            string query = default(string), //для запроса
                value = valueToSearh; //значение для поиска
            uint valueNumber = 0;

                //если строковое значение поиска не пустое и числовое (можно попытаться с успехом превратить в число)
                if (value != "" && uint.TryParse(value, out valueNumber) == true)
                {
                    //если искомое число больше нуля и есть числовые столбцы, где нужно отыскать это число
                    if (valueNumber > 0 && nameNumericFields != null)
                        query = GetQuerySearch(nameTable, nameFieldsAll, newNameFieldsAS, nameNumericFields, valueNumber.ToString()); //возвращаем запрос с учётом числовых полей
                }
                else
                    query = GetQuerySearch(nameTable, nameFieldsAll, newNameFieldsAS, valueToSearh: value); //возвращаем запрос со всеми строковыми полями

                return query;
        }
        #endregion

        #region GetQuerySearch. Получить Select запрос
        /// <summary>
        /// Составляет запрос select в зависимости от столбцов (с числовыми данными или без)
        /// </summary>
        /// <param name="nameTable">Название таблицы</param>
        /// <param name="nameFields">Название столбцов для запроса</param>
        /// <param name="newNameFieldsAS">Назвать столбцов как. Как они будут отображаться в таблице</param>
        /// <param name="nameNumericFields">Название столбцов, которые содержат числовые значения</param>
        /// <param name="valueToSearh">Значения для поиска</param>
        /// <returns>Запрос SELECT</returns>
        public string GetQuerySearch(string nameTable, string[] nameFields, string[] newNameFieldsAS, string[] nameNumericFields = null, string valueToSearh = "")
        {
            string select = " SELECT ",
                from = " FROM ",
                where = " WHERE CONCAT (",
                like = " LIKE ",
                query = "";


            //сформировать часть запроса select со всех столбцов
            for (int i =0; i < nameFields.GetLength(0); i++)
                select += " " + nameFields[i] + " AS " + "'" + newNameFieldsAS[i] + "'" + ", "; //добавить "  price AS 'цена', "
           select = select.Remove(select.Length - 2) + " "; //удалить перед from ", " 

            //сформировать часть запроса from
            from += NAME_DATABASE + "." + nameTable; //добавить " supermarket.stock"

            //если есть столбцы в которых имеются числовые значения
            if (nameNumericFields != null)
            {
                //сформировать часть запроса where с столбцами в которых есть числовые значения
                for (int i = 0; i < nameNumericFields.GetLength(0); i++)
                    where += " " + nameNumericFields[i] + ", "; //добавить " id, price, "
                where = where.Remove(where.Length - 2) + ") "; //удалить перед like ", " 
            }
            else
            {
                //сформировать часть запроса where со всеми столбцами
                for (int i = 0; i < nameFields.GetLength(0); i++)
                    where += " " + nameFields[i] + ", ";  //добавить " name, address "
                where = where.Remove(where.Length - 2) + ") ";  //удалить перед like ", " 
            }

            //сформировать часть запроса
            like += "'%" + valueToSearh + "%'"; //конец части запроса "'%краматорск%'"

            //составляем полностью запрос из частей
            query += select + from + where + like;

            return query;

            //Примеры запроса:
            //для полей которые содержат числовые значения
                //   " SELECT id_info AS 'id', full_name AS 'Имя', passport_id AS 'Серия и номер паспорта', age, address, phone, photo " +
                //   " FROM supermarket.info " +
                //   " WHERE CONCAT(id_info, age) " +
                //   " LIKE '%" + valueNumber + "%'";

            //для всех остальных полей
                // " SELECT id_info AS 'id', full_name AS 'Имя', passport_id AS 'Серия и номер паспорта', age, address, phone, photo " +
                // " FROM supermarket.info " +
                // " WHERE CONCAT(id_info, full_name, passport_id, age, address, phone, photo) " +
                // " LIKE '%" + value + "%'";
        }
        #endregion

        #region ExecuteQuery overload - Перегруженный. Попытка выполнить запрос
        /// <summary>
        /// Попытаться добавить (выполнить) необходимый нам запрос
        /// </summary>
        
        /// <param name="dataGridView">Текущая таблица на форме (DataGridView)</param>
        /// <param name="commandSql">комманды которые нужно выполнить</param>
        /// <param name = "query" > Запрос</param >
       /// <param name = "showMessageBox" > Показать диалоговое сообщения или нет</param>
        public void ExecuteQuery(/*Action<DataGridView, string> FillDataGridView,*/ DataGridView dataGridView, 
            MySqlCommand commandSql,  string query = "", Boolean showMessageBox = true)
        {
            try
            {
                connection.Open();
                if (commandSql.ExecuteNonQuery() == 1)
                {
                    if (showMessageBox == true)
                        MessageBox.Show("Запрос успешно выполнен");
                }
                else
                {
                    if (showMessageBox == true)
                        MessageBox.Show("Запрос не выполнен");
                }
                //как выполнилась функция сразу обновить dataGridView
                FillDataGridView(dataGridView, query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            
            
        }
        #endregion

        #region ExecuteQuery overload - Перегруженный. Попытка выполнить запрос
        /// <summary>
        /// Попытаться добавить (выполнить) необходимый нам запрос
        /// </summary>
        /// <param name="commandSql">комманды которые нужно выполнить</param>
        /// <param name = "query" > Запрос</param >
        /// <param name = "showMessageBox" > Показать диалоговое сообщения или нет</param>
        public void ExecuteQuery(MySqlCommand commandSql, string query = "", Boolean showMessageBox = true)
        {
            try
            {
                connection.Open();
                //commandSql.ExecuteScalar();
                if (commandSql.ExecuteNonQuery() == 1)
                {
                    if (showMessageBox == true)
                        MessageBox.Show("Запрос успешно выполнен");
                }
                else
                {
                    if (showMessageBox == true)
                        MessageBox.Show("Запрос не выполнен");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }


        }
        #endregion

        #region AddParameters. Выполняем добавление команды (записи) Parameters.Add в MySqlCommand
        /// <summary>
        /// Выполняем добавление команды (записи) Parameters.Add в MySqlCommand
        /// </summary>
        /// <param name="command">текущая MySqlCommand готова к выполнению</param>
        /// <param name="variables">Переменные для добавление записи в таблицу</param>
        /// <param name="mySqlDbTypes">Массив MySqlDbType MediumText, LongBlob, UInt32, VarChar</param>
        /// <param name="objects">Объекты TextBox, ComboBox, byte[]</param>
        public void AddParameters(MySqlCommand command, string variables, MySqlDbType mySqlDbTypes, object objects)
        {
                GetTextObjectsForm getText = new GetTextObjectsForm();
                //если объект является ComboBox или TextBox
                if (objects is ComboBox || objects is TextBox || objects is DateTimePicker)
                    command.Parameters.Add(variables, mySqlDbTypes).Value = getText.GetText(objects); //GetText если есть текст в объектах
                else
                    command.Parameters.Add(variables, mySqlDbTypes).Value = objects; //Для других объектов                
        }
        #endregion

        #region AddParametersString overload. Выполняем добавление команды (записи) Parameters.Add в MySqlCommand
        /// <summary>
        /// Выполняем добавление команды (записи) Parameters.Add в MySqlCommand
        /// </summary>
        /// <param name="command">текущая MySqlCommand готова к выполнению</param>
        /// <param name="variables">Переменные для добавление записи в таблицу</param>
        /// <param name="mySqlDbTypes">Массив MySqlDbType MediumText, LongBlob, UInt32, VarChar</param>
        /// <param name="values">Значения, необходимые для добавления</param>
        public void AddParametersString(MySqlCommand command, string[] variables, MySqlDbType[] mySqlDbTypes, string[] values)
        {
            int  i = 0, j=0;
            foreach (string val in values)
                command.Parameters.Add(variables[i++], mySqlDbTypes[j++]).Value = val;
            }

        #endregion

        #region AddParameters overload. Выполняем добавление команды (записи) Parameters.Add в MySqlCommand
        /// <summary>
        /// Выполняем добавление команды (записи) Parameters.Add в MySqlCommand для многих полей
        /// </summary>
        /// <param name="command">текущая MySqlCommand готова к выполнению</param>
        /// <param name="variables">Переменные для добавление записи в таблицу</param>
        /// <param name="mySqlDbTypes">Массив MySqlDbType MediumText, LongBlob, UInt32, VarChar</param>
        /// <param name="objects">Объекты TextBox, ComboBox, byte[]</param>
        public void AddParameters(MySqlCommand command, string[] variables, MySqlDbType[] mySqlDbTypes,
            object[] objects)
        {
            //для всех переменных
            for (int i = 0; i < variables.Length; i++)
            {
                AddParameters(command, variables[i], mySqlDbTypes[i], objects[i]);
                ////если объект является ComboBox или TextBox
                //if (objects[i] is ComboBox || objects[i] is TextBox)
                //    AddParameters(command, variables[i], mySqlDbTypes[i], objects[i]);
                //    //command.Parameters.Add(variables[i], mySqlDbTypes[i]).Value = GetText(objects[i]); //GetText если есть текст в объектах
                //else
                //    AddParameters(command, variables[i], mySqlDbTypes[i], objects[i]);
                //    //command.Parameters.Add(variables[i], mySqlDbTypes[i]).Value = objects[i]; //Для других объектов        
            }
        }
        #endregion

        #region AddParameters overload. Выполняем добавление команды (записи) Parameters.Add в MySqlCommand
        /// <summary>
        /// Выполняем добавление команды (записи) Parameters.Add в MySqlCommand для многих полей
        /// </summary>
        /// <param name="command">текущая MySqlCommand готова к выполнению</param>
        /// <param name="variables">Переменные для добавление записи в таблицу</param>
        /// <param name="mySqlDbTypes">Массив MySqlDbType MediumText, LongBlob, UInt32, VarChar</param>
        /// <param name="objects">Объекты TextBox, ComboBox, byte[]</param>
        /// <param name="textBoxs">TextBox-ы на форме</param>
        /// <param name="comboBoxs">ComboBox-ы на форме</param>
        /// <param name="dateTimePickers">DateTimePikcer-ы на форме</param>
        /// <param name="pictureBoxs">PictureBox-ы на форме</param>
        public void AddParameters(MySqlCommand command, string[] variables, MySqlDbType[] mySqlDbTypes,
            object[] objects=null, TextBox[] textBoxs = null, ComboBox[] comboBoxs = null,
            DateTimePicker[] dateTimePickers = null, PictureBox[] pictureBoxs = null)
        {
            GetTextObjectsForm getText = new GetTextObjectsForm();
            //Получить все строковые значения 
            string[] allValues = getText.GetText(objects, textBoxs, comboBoxs, dateTimePickers, pictureBoxs);
            //добавить
            AddParameters(command, variables, mySqlDbTypes, allValues);      
            
        }
        #endregion

        #region GetQueryInsert. Получить Insert запрос
        /// <summary>
        /// Получить запрос Insert
        /// </summary>
        /// <param name="nameTable">Название таблицы</param>
        /// <param name="fields">Массив полей таблицы</param>
        /// <param name="variables">Переменные для вставки значений</param>
        /// <returns>Query Insert</returns>
        public string GetQueryInsert(string nameTable, string[] fields, string[] variables)
        {
            string query;

            //Example: INSERT INTO supermarket.info
            query = "INSERT INTO " + NAME_DATABASE + "." + nameTable + " (";

            //Example: ( id_info, full_name, passport_id, age, address, phone, photo) 
            for (int i = 0; i < fields.Length; i++)
            {
                query += " " + fields[i] + ", ";
            }
            query = query.Remove(query.Length - 2) + ") "; //удалить перед VALUES ", " 

            //Example: VALUES(@id, @name, @passport, @age, @address, @phone, @photo)
            query += " VALUES (";
            
            for (int i = 0; i < variables.Length; i++)
            {
                query += " " + variables[i] + ", ";
            }
            query = query.Remove(query.Length - 2) + ") ";

            return query;
        }
        #endregion

        #region GetQueryUpdate. Получить Update запрос
        /// <summary>
        /// Получить запрос Update
        /// </summary>
        /// <param name="nameTable">Название таблицы</param>
        /// <param name="fields">Массив полей таблицы</param>
        /// <param name="variables">Переменные для вставки значений</param>
        /// <param name="id_field">id поле таблицы</param>
        /// <returns>Query Update</returns>
        public string GetQueryUpdate(string nameTable, string[] fields, string[] variables, string id_field)
        {
            string query;
            //Example: "UPDATE supermarket.info " 
            query = "UPDATE " + NAME_DATABASE + "." + nameTable + " ";
            query += " SET ";
            //Exmample:   "SET full_name = @name, passport_id = @passport, age = @age, address = @address, phone = @phone, photo = @photo"
            for (int i = 0; i < fields.Length; i++)
            {
                query += " " + fields[i] + " = " + variables[i] + ", ";
            }
            query = query.Remove(query.Length - 2) + " "; //удалить перед WHERE ", "
            //Example: " WHERE id_info = @id"
            query += " WHERE ";
            query += " " + id_field + " = @id";
            return query;
        }
        #endregion

        #region GetQueryDelete. Получить Delete запрос
        /// <summary>
        /// Получить Delete запрос
        /// </summary>
        /// <param name="nameTable">Название таблицы</param>
        /// <param name="id_field">id поле таблицы</param>
        /// <returns>Query Delete</returns>
        public string GetQueryDelete(string nameTable, string id_field)
        {
            return "DELETE FROM " + NAME_DATABASE + "." + nameTable + " WHERE " + id_field + " = @id";  
        }
        #endregion

        #region GetQueryFindSelect. Получить Select запрос поиска по ид поля таблицы
        /// <summary>
        /// Получить Select запрос поиска по ид поля таблицы
        /// </summary>
        /// <param name="nameTable"></param>
        /// <param name="nameFields"></param>
        /// <param name="newNameFieldsAS"></param>
        /// <param name="id_field"></param>
        /// <returns></returns>
        public string GetQueryFindSelect(string nameTable, string[] nameFields, string[] newNameFieldsAS, string id_field)
        {
            string query, select, from, where;
            select = " SELECT ";
            for (int i = 0; i < nameFields.Length; i++)
            {
                select += " " + nameFields[i] + " AS " + "'" + newNameFieldsAS[i] + "'" + ", ";
            }
            select = select.Remove(select.Length - 2) + " "; //удалить перед From ", "

            from = " FROM " + NAME_DATABASE + "." + nameTable + " ";

            where = " WHERE " + id_field + " = @id";

            query = select + from + where;
            return query;
           // return " SELECT * FROM supermarket.info " +
            // " WHERE id_info = @id ";
        }
        #endregion
        
       /* #region Find. Найти данные по id поля
        /// <summary>
        /// Найти данные по id поля
        /// </summary>
        /// <param name="commandLocal">MySqlCommand с содержанием select запроса</param>
        /// <param name="textBoxs">Если найденные данные нужно отобразить в textBoх-ах</param>
        /// <param name="comboBoxs">Если найденные данные нужно отобразить в comboBox-ах</param>
        /// <param name="pictureBoxs">Если найденные данные нужно отобразить в pictureBox-ах</param>
        /// <param name="ColumnsTextForTextBox">Строки dataGridView которые содержать текстовые данные для TextBox-ов</param>
        /// <param name="ColumnsTextForComboBox">Строки dataGridView которые содержать текстовые данные для comboBox-ов</param>
        /// <param name="ColumnsPictureForPictureBox">Строки dataGridView которые содержать изображения для pictureBox-ов</param>
        public void Find(MySqlCommand commandLocal, TextBox[] textBoxs=null, ComboBox[] comboBoxs = null, PictureBox[] pictureBoxs= null,
                        int[] ColumnsTextForTextBox=null, int[] ColumnsTextForComboBox = null, int[] ColumnsPictureForPictureBox = null)
        {
            ///Для отображения в таблице
            DataTable table = new DataTable(); //Создаём таблицу
            adapter.SelectCommand = commandLocal;
            adapter.Fill(table); //Вставляем данные при выполнении команды в таблицу
            Settings settings = new Settings();
            if (table.Columns.Count <= 0)
            {
                MessageBox.Show("Указанная запись: " + textBoxs[0].Text + " не найдена!");//textBoxs[0] должен содержать id 
                settings.ClearFields(textBoxs, comboBoxs, pictureBoxs);
            }
            else
            {
                MessageBox.Show("Указанная запись " + textBoxs[0].Text + " найдена!");
                settings.InsertTextInTextBoxFromTable(table, ColumnsTextForTextBox, textBoxs);
                settings.InsertTextInComboBoxFromTable(table, ColumnsTextForComboBox, comboBoxs);
                settings.InsertImageInPictureBoxFromTable(table, ColumnsPictureForPictureBox, pictureBoxs);   
            }
        }
        #endregion*/

        #region Find. Найти данные по id поля
        /// <summary>
        /// Найти данные по id поля
        /// </summary>
        /// <param name="commandLocal">MySqlCommand с содержанием select запроса</param>
        /// <param name="textBoxs">Если найденные данные нужно отобразить в textBoх-ах</param>
        /// <param name="comboBoxs">Если найденные данные нужно отобразить в comboBox-ах</param>
        /// <param name="pictureBoxs">Если найденные данные нужно отобразить в pictureBox-ах</param>
        /// <param name="ColumnsTextForTextBox">Строки dataGridView которые содержать текстовые данные для TextBox-ов</param>
        /// <param name="ColumnsTextForComboBox">Строки dataGridView которые содержать текстовые данные для comboBox-ов</param>
        /// <param name="ColumnsPictureForPictureBox">Строки dataGridView которые содержать изображения для pictureBox-ов</param>
        public DataTable Find(MySqlCommand commandLocal, DataGridView dataGrid = null, 
                              TextBox[] textBoxs = null, ComboBox[] comboBoxs = null, PictureBox[] pictureBoxs = null, DateTimePicker[] dateTimePickers =null,
                              int[] ColumnsTextForTextBox = null, int[] ColumnsTextForComboBox = null, int[] ColumnsPictureForPictureBox = null, int[] ColumnsDateForDateDateTimePicker = null)
        {
            //Для отображения в таблице
            adapter = new MySqlDataAdapter(commandLocal); //Выполняем команду                
            adapter.SelectCommand = commandLocal; //выполнить выборку. Select нужно новый создавать
            DataTable table = new DataTable();
            //table.Clear();
            adapter.Fill(table); //Вставляем данные при выполнении команды в таблицу
            Settings settings = new Settings();
            //dataGrid.DataSource = table; //подключаем заполненную таблицу и отображаем
            if (table.Columns.Count <= 0)
            {
                MessageBox.Show("Указанная запись: " + textBoxs[0].Text + " не найдена!");//textBoxs[0] должен содержать id 
                settings.ClearFields(textBoxs, comboBoxs, pictureBoxs);
            }
            else
            {
                MessageBox.Show("Указанная запись " + textBoxs[0].Text + " найдена!");
                settings.InsertTextInTextBoxFromTable(table, ColumnsTextForTextBox, textBoxs); //вставляем все значения из таблицы в text-Box так же для остальных
                settings.InsertTextInComboBoxFromTable(table, ColumnsTextForComboBox, comboBoxs);
                settings.InsertImageInPictureBoxFromTable(table, ColumnsPictureForPictureBox, pictureBoxs);
                settings.InsertDateInDateTimePickerFromTable(table, ColumnsDateForDateDateTimePicker, dateTimePickers);
            }
            return table;
        }
        #endregion

        //------- тестим новые функции ------//
        
        #region  GetQueryShowSearch. Получить запрос исходя из условия (числовых или строковых столбцов)
        /// <summary>
        /// Составляет запрос select в зависимости от условия, на которое влияет значения столбцов (с числовыми данными или строковы)
        /// </summary>
        /// <param name="nameTable">Название таблицы</param>
        /// <param name="nameFieldsAll">Название столбцов для запроса</param>
        /// <param name="newNameFieldsAS">Назвать столбцы как. Как они будут отображаться в таблице</param>
        /// <param name="nameNumericFields">Название столбцов, которые содержат числовые значения</param>
        /// <param name="valueToSearh">Значения для поиска</param>
        /// <returns>Запрос SELECT</returns>
        public string GetQueryShowSearch(string[] nameTables, string[] nameFieldsAll, string[] newNameFieldsAS,
             string[] primaryTables, string secondaryTables, string[] primaryIdField, string[] secondaryIdField,
            string[] nameNumericFields = null, string valueToSearh = "")
        {
            string query = default(string), //для запроса
                value = valueToSearh; //значение для поиска
            uint valueNumber = 0;

            //если строковое значение поиска не пустое и числовое (можно попытаться с успехом превратить в число)
            if (value != "" && uint.TryParse(value, out valueNumber) == true)
            {
                //если искомое число больше нуля и есть числовые столбцы, где нужно отыскать это число
                if (valueNumber > 0 && nameNumericFields != null)
                    query = GetQuerySearch(nameTables, nameFieldsAll, newNameFieldsAS,
                        primaryTables, secondaryTables, primaryIdField, secondaryIdField,
                        nameNumericFields, valueNumber.ToString()); //возвращаем запрос с учётом числовых полей
            }
            else
                query = GetQuerySearch(nameTables, nameFieldsAll, newNameFieldsAS,
                    primaryTables, secondaryTables, primaryIdField, secondaryIdField,
                    valueToSearh: value); //возвращаем запрос со всеми строковыми полями

            return query;
        }
        #endregion

        #region GetQuerySearch. Получить Select запрос
        /// <summary>
        /// Составляет запрос select в зависимости от столбцов (с числовыми данными или без)
        /// </summary>
        /// <param name="nameTable">Название таблицы</param>
        /// <param name="nameFields">Название столбцов для запроса</param>
        /// <param name="newNameFieldsAS">Назвать столбцов как. Как они будут отображаться в таблице</param>
        /// <param name="nameNumericFields">Название столбцов, которые содержат числовые значения</param>
        /// <param name="valueToSearh">Значения для поиска</param>
        /// <returns>Запрос SELECT</returns>
        public string GetQuerySearch(string[] nameTables, string[] nameFields, string[] newNameFieldsAS,
            string[] primaryTables, string secondaryTables, string[] primaryIdField, string[] secondaryIdField,
            string[] nameNumericFields = null, string valueToSearh = "")
        {
            string select = " SELECT ",
                from = " FROM ",
                where = " WHERE ",
                concat = " (CONCAT( ",
                like = " LIKE ",
                query = "";


            //сформировать часть запроса select со всех столбцов
            for (int i = 0; i < nameFields.GetLength(0); i++)
                select += " " + nameFields[i] + " AS " + "'" + newNameFieldsAS[i] + "'" + ", "; //добавить "  price AS 'цена', "
            select = select.Remove(select.Length - 2) + " "; //удалить перед from ", " 

            //сформировать часть запроса from
            for(int i= 0; i < nameTables.GetLength(0); i++)
                from += " " + NAME_DATABASE + "." + nameTables[i] + ", "; //добавить " supermarket.stock"
            from = from.Remove(from.Length - 2) + " "; //удалить перед where ", " 

            //вытаскиваем primary.idfields = secondary.ifields
            where += GetWherePrimarySecondary(primaryTables, secondaryTables, primaryIdField, secondaryIdField) + "AND";

            //тестим здесь
            //если есть столбцы в которых имеются числовые значения
            if (nameNumericFields != null)
            {
                //сформировать часть запроса where с столбцами в которых есть числовые значения
                for (int i = 0; i < nameNumericFields.GetLength(0); i++)
                    concat += " " + nameNumericFields[i] + ", "; //добавить " id, price, "
                concat = concat.Remove(concat.Length - 2) + ") "; //удалить перед like ", " 
            }
            else
            {
                //сформировать часть запроса where со всеми столбцами
                for (int i = 0; i < nameFields.GetLength(0); i++)
                    concat += " " + nameFields[i] + ", ";  //добавить " name, address "
                concat = concat.Remove(concat.Length - 2) + ") ";  //удалить перед like ", " 
            }

            //сформировать часть запроса
            like += "'%" + valueToSearh + "%'" + ")"; //конец части запроса "'%краматорск%'"

            //составляем полностью запрос из частей
            query += select + from + where + concat + like;

            return query;

            //SELECT employees.id_employee AS 'ИД', info.full_name AS 'ФИО', position.position AS 'Должность', 
            //employees.department AS 'Отдел', employees.experience AS 'Опыт работы', employees.salary AS 'Зарплата', 
            //employees.started_work AS 'Принят', employees.fired AS 'Уволен'
            //FROM employees, position, info
            //WHERE employees.id_position = position.id_position
            //AND employees.id_info = info.id_info
            //AND
            //(concat(employees.id_employee, info.full_name, position.position,
            //    employees.department, employees.experience, employees.salary, employees.started_work, employees.fired)
            //like '%повар%');
        }
        #endregion

        #region GetWherePrimarySecondary
        /// <summary>
        /// Получить часть запроса для Where отношение между id
        /// Главной и зависимой таблицы
        /// </summary>
        /// <param name="primaryTables">Главные табицы</param>
        /// <param name="secondaryTables">Зависимая таблица</param>
        /// <param name="primaryIdField">Главные ID поля таблиц</param>
        /// <param name="secondaryIdField">Зависимые ID поля таблицы</param>
        /// <returns></returns>
        public string GetWherePrimarySecondary(string[] primaryTables, string secondaryTables, string[] primaryIdField,  string[] secondaryIdField)
        {
            string partQuery = default(string);
            for (int i = 0; i < primaryTables.GetLength(0); i++)
            {
                partQuery += " " + secondaryTables + "." + secondaryIdField[i] + " = " +
                             primaryTables[i] + "." + primaryIdField[i] + " " + "AND";
            }
            partQuery = partQuery.Remove(partQuery.Length - 3) + " "; 
            //WHERE employees.id_position = position.id_position
            //AND employees.id_info = info.id_info
            return partQuery;
        }
        #endregion

        #region GetValuesColumn. Получить все значения столбца таблицы

        #region GetValuesColumn. Получить все значения столбца таблицы
        /// <summary>
        /// Получить все значения столбца таблицы с запроса
        /// </summary>
        /// <param name="query">Запрос, для получения значений</param>
        /// <returns>Список значений </returns>
        public List<string> GetValuesColumn(string query)
        {
            OpenConnection();
            command = new MySqlCommand(query, connection);
            string value; //= command.ExecuteScalar().ToString(); //вид перечислений будет таким 'text','text2'
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = command.ExecuteReader();
            List<string> valuesEnum = new List<string>();
            while (dataReader.Read())
            {
                valuesEnum.Add(dataReader[0].ToString());
            }
            CloseConnection();
            return valuesEnum;
        }
        #endregion

        #region GetValuesColumn overload. Получить все значения столбца таблицы
        /// <summary>
        /// Получить все значения столбца таблицы
        /// </summary>
        /// <param name="nameTables">Название таблиц для concat</param>
        /// <param name="fields">Название полей для concat</param>
        /// <returns>Список значений</returns>
        public List<string> GetValuesColumn(string[] nameTables, string[] fields)
        {
            OpenConnection();
            string query = GetQueryConcat(nameTables, fields);
            command = new MySqlCommand(query, connection);
            string value; //= command.ExecuteScalar().ToString(); //вид перечислений будет таким 'text','text2'
            //Create a data reader and Execute the command
            MySqlDataReader dataReader = command.ExecuteReader();
            List<string> valuesEnum = new List<string>();
            while (dataReader.Read())
            {
                valuesEnum.Add(dataReader[0].ToString());
            }
            CloseConnection();
            return valuesEnum;
        }
        #endregion

        #endregion

        #region GetEnum

        #region GetEnum. Получить все значения enum столбца таблицы
        /// <summary>
        /// Получить все значения enum, которое может принимать значения столбца таблицы
        /// </summary>
        /// <param name="query">Запрос для получения значений enum</param>
        /// <returns>Значения enum</returns>
        public List<string> GetEnum(string query)
        {
            OpenConnection();
            command = new MySqlCommand(query, connection);
            string enumCategory = command.ExecuteScalar().ToString(); //вид перечислений будет таким 'text','text2'
            char delimiter = '\'';
            string[] substrings = enumCategory.Split(delimiter); //массив строк будет таким "text" "," "text2"
            enumCategory = "";
            foreach (var str in substrings)
                enumCategory += str; //объединяем массив в строку чтобы потом удалить , будет таким  "text,text2"
            delimiter = ',';
            substrings = enumCategory.Split(delimiter); //массив строк будет таки "text" "text2"
            List<string> valuesEnum = new List<string>();
            foreach (var x in substrings)
                valuesEnum.Add(x); //Получаем все значения products.name  
            CloseConnection();
            return valuesEnum;
        }
        #endregion

        #region GetEnum overload. Получить все значения enum столбца таблицы
        /// <summary>
        /// Получить все значения enum, которое может принимать значения столбца таблицы
        /// </summary>
        /// <param name="tableName">Название таблицы</param>
        /// <param name="columnName">Название поля (столбца) таблицы</param>
        /// <returns>Значения enum</returns>
        public List<string> GetEnum(string tableName, string columnName)
        {
            OpenConnection();
            string query = GetQueryConcat(tableName, columnName);
            command = new MySqlCommand(query, connection);
            string enumCategory = command.ExecuteScalar().ToString(); //вид перечислений будет таким 'text','text2'
            char delimiter = '\'';
            string[] substrings = enumCategory.Split(delimiter); //массив строк будет таким "text" "," "text2"
            enumCategory = "";
            foreach (var str in substrings)
                enumCategory += str; //объединяем массив в строку чтобы потом удалить , будет таким  "text,text2"
            delimiter = ',';
            substrings = enumCategory.Split(delimiter); //массив строк будет таки "text" "text2"
            List<string> valuesEnum = new List<string>();
            foreach (var x in substrings)
                valuesEnum.Add(x); //Получаем все значения products.name  
            CloseConnection();
            return valuesEnum;
        }
        #endregion

        #endregion

        #region GetQueryConcat

        #region GetQueryConcat
        /// <summary>
        /// Данный метод подойдёт для получения запроса объединенного в один столбец concat
        ///  значений с разных столбцов разных таблиц
        /// </summary>
        /// <param name="nameTables">Название таблиц</param>
        /// <param name="fields">Поля (столбцы) таблицы</param>
        /// <returns>Запрос concat с содержанием значений в одном столбце</returns>
        public string GetQueryConcat(string[] nameTables, string[] fields)
        {
            string query = "SELECT ",
                concat = " CONCAT( ",
                from = " FROM ",
                orderBy = " ORDER BY " + fields[0];

            for (int i = 0; i < fields.GetLength(0); i++)
            {
                concat += " " + fields[i] + ", " + "'   ', ";
            }
            concat = concat.Remove(concat.Length - 9) + " ) ";

            //сформировать часть запроса from
            for (int i = 0; i < nameTables.GetLength(0); i++)
                from += " " + NAME_DATABASE + "." + nameTables[i] + ", "; //добавить " supermarket.stock"
            from = from.Remove(from.Length - 2) + " "; 

            query += concat + " AS 'Field' " + from + orderBy;

            return query;
            //Select concat(info.id_info, '   ', info.full_name) AS 'Field'
            //from info
        }
        #endregion

        #region GetQueryConcat
        /// <summary>
        /// Данный метод подойдёт для получения значений enum
        ///  которое может принимать запись столбца таблицы
        /// </summary>
        /// <param name="tableName">Название таблицы</param>
        /// <param name="columnName">Название столбца, содержащий enum значения</param>
        /// <returns>Запрос concat с содержанием enum значений в одном столбце</returns>
        public string GetQueryConcat(string tableName, string columnName)
        {
           return "select trim(trailing ')'                       " +
                          "    from trim(leading '('                            " +
                          "    from trim(leading 'enum'                         " +
                          "    from column_type))) column_type                  " +
                          "    from information_schema.COLUMNS                  " +
                          "    where TABLE_SCHEMA = " + "'" + NAME_DATABASE + "'" +
                          "    AND TABLE_NAME = " + "'" + tableName + "'        " +
                          "    AND COLUMN_NAME = " + "'" + columnName + "';      ";           
        }
        #endregion

        #endregion

    }
}