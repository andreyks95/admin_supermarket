using System;
using System.Collections.Generic;
using System.Data;
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

        #region Отобразить выбранные нами поля в таблице

        public string CreateQueryChooseFields()
        {
            return "";
        }
        #endregion

        #region Отобразить выбранные поля в таблице
        public void ShowTable( System.Windows.Forms.DataGridView DataGridView, string query, string nameTable)
        {
            //System.Windows.Forms.DataGridView DataGridView 
            //Передаём в качестве параметра DataGridView из любых форм
            //и можем использовать его методы
            //например DataGridView.DataSource 
            try
            {
                //выбрать все поля с таблицы БД
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
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
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM " + nameDatabase + "." + nameTable, connection);
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
    }
    #endregion

}