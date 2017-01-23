using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Administrator_supermarket
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

        public const string NAME_DATABASE = "supermarket";

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
                 //Для отображения в таблице
                 DataTable table = new DataTable(); //Создаём таблицу
                 adapter.Fill(table); //Вставляем данные при выполнении команды в таблицу
                 dataGridView.DataSource = table; //подключаем заполненную таблицу и отображаем
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message);
             }
         }
        #endregion

        #region FillDataGridView overload. Отобразить таблицу с учётом поиска значения
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
                DataTable table = new DataTable(); //Создаём таблицу
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

        #region GetText. Получить текущий текст из TextBox, ComboBox
        /// <summary>
        /// возвращает текущий текст из comboBox или textBox
        /// можно передать просто textBox или comboBox, а дальше из свойства объекта он вернёт текущий текст 
        /// </summary>
        /// <param name="obj">Объект который передаётся для выбора текста из его свойства</param>
        /// <returns>Текущий текст объекта</returns>
        public string GetText(object obj)
        {
            string text = null;
            Type currentType; //создаём  тип
            PropertyInfo property; //создаём свойство

            if (obj is TextBox)
            {
                currentType = obj.GetType(); //получаем тип
                property = currentType.GetProperty("Text");//Присваиваем ему свойство Text, если это textBox. Получить свойство text из этого типа                                            
                text = property.GetValue(obj).ToString(); //в свойстве получить значение объекта
            }
            else if (obj is ComboBox)
            {
                currentType = obj.GetType(); //получаем тип
                property = currentType.GetProperty("SelectedItem");//Присваиваем ему свойство SelectedItem, если это ComboBox. Получить свойство SelectedItem из этого типа                                            
                text = property.GetValue(obj).ToString(); //в свойстве получить значение объекта
            }
            else
                text = "";

            return text;
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
                //если объект является ComboBox или TextBox
                if (objects is ComboBox || objects is TextBox)
                    command.Parameters.Add(variables, mySqlDbTypes).Value = GetText(objects); //GetText если есть текст в объектах
                else
                    command.Parameters.Add(variables, mySqlDbTypes).Value = objects; //Для других объектов                
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
                //если объект является ComboBox или TextBox
                if (objects[i] is ComboBox || objects[i] is TextBox)
                    AddParameters(command, variables[i], mySqlDbTypes[i], objects[i]);
                    //command.Parameters.Add(variables[i], mySqlDbTypes[i]).Value = GetText(objects[i]); //GetText если есть текст в объектах
                else
                    AddParameters(command, variables[i], mySqlDbTypes[i], objects[i]);
                    //command.Parameters.Add(variables[i], mySqlDbTypes[i]).Value = objects[i]; //Для других объектов           
            }
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
                select += " " + nameFields[i] + " AS " + newNameFieldsAS[i] + ", ";
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
    }
}