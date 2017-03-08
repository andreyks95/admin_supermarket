using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Administrator_supermarket
{

    public class Сalculations
    {

        Connection connect = new Connection();

        //Старая версия. Методы для просчёта значения в ячейке с использованием "впайки" Запросов 

        #region Данные методы создают SELECT запрос(ы) только для значения в одной ТАБЛИЦЕ

        #region GetSelectFromOneFieldTable 
        /// <summary>
        /// Это функция создаёт Одиночный SELECT SQL запрос для одной таблицы. 
        /// Передавать туда, только таблицы и поля которые содержать ЧИСЛОВЫЕ ЗНАЧЕНИЯ. 
        /// </summary>
        /// <param name="nameDatabase">Название БД. Пример: "Администратор продуктового супермаркета"</param>
        /// <param name="nameTable">Название таблицы. "Сотрудники"</param>
        /// <param name="nameField">Имя поля, с которого нужно получить ЧИСЛОВЫЕ данные . "Стаж"</param>
        /// <param name="nameIdFieldTable">Название ID поля таблицы. "ID_Сотрудники"</param>
        /// <param name="id">id конкретной строки. "ID_Сотрудники = 3"</param>
        /// <returns>ОДИН SELECT SQL Запроса</returns>
        public string GetSelectFromOneFieldTable(string nameDatabase, string nameTable, string nameField,
            string nameIdFieldTable, uint id)
        {

            string select = " SELECT " + nameField + " FROM " + nameDatabase + "." + nameTable + " WHERE " +
                            nameIdFieldTable + " = " + id.ToString() + " ";
            //Example: SELECT Price_for_ne FROM Admin.Products WHERE id_product = 5 
            return select;
        }
        #endregion

        #region GetSelectAllFieldTables
        /// <summary>
        /// Это функция создаёт Много SELECT SQL запросов для одной таблицы. 
        /// Передавать туда, только таблицы и поля которые содержать ЧИСЛОВЫЕ ЗНАЧЕНИЯ. 
        /// </summary>
        ///<param name="nameDatabase">Название БД. Пример: "Администратор продуктового супермаркета"</param>
        /// <param name="nameTable">Название таблицы. "Сотрудники"</param>
        /// <param name="nameFields">Имя ПОЛЕЙ, с которого нужно получить ЧИСЛОВЫЕ данные . "Стаж", "Возраст"</param>
        /// <param name="nameIdFieldTable">Название ID поля таблицы. "ID_Сотрудники" для "Стажа" и "Возраста" должно быть одинаково</param>
        /// <param name="id">id конкретной строки. "ID_Сотрудники = 1 " для "Стажа" и "Возраста" должно быть одинаково</param>
        /// <param name="nameTable_AS">Как будут именоваться выбранные таблицы. ".. AS Table1 и т.д." </param>
        /// <returns>Одну строку которая содержит МНОГО SELECT SQL Запросов всех полей для одной таблицы</returns>
        public string GetSelectAllFieldTables(string nameDatabase, string[] nameTable, string[] nameFields,
            string[] nameIdFieldTable, uint[] id, string[] nameTable_AS)
        {
            var countFieldsTable = nameFields.Length;
            string select = default(string),
                allSelect = default(string);
            for (int i = 0; i < countFieldsTable; i++)
            {
                select = GetSelectFromOneFieldTable(nameDatabase, nameTable[i], nameFields[i], nameIdFieldTable[i], id[i]);
                allSelect += " (" + select + ")" + " AS " + nameTable_AS[i] + ", ";
            }
            //удаляем пробел и запятую перед в конце запроса и заменяем на " " чтобы был корректный запрос.
            allSelect = allSelect.Remove(allSelect.Length - 2) + " ";
            //" (SELECT Price_for_ne FROM Admin.Products WHERE id_product = 5 ) AS Table1, 
            // (SELECT Count_products FROM Admin.Stock WHERE id_stock = 3 ) AS Table2"
            return allSelect;
        }
        #endregion

        #region GetSelectAllFieldTables overload method
        /// <summary>
        /// Перегруженный параметр string[,] nameFields
        /// </summary>
        public string GetSelectAllFieldTables(string nameDatabase, string[] nameTable, string[,] nameFields, string[] nameIdFieldTable, uint[] id, string[] nameTable_AS)
        {

            int   count = 0; //для nameTable_AS, для нормального запроса. Буквально: Назвать таблицу как
            //string[,] nameFields ={
            //    {"price_for_one" }, строка 0, стоблец 0 => [row,col] = [0,0] поля для одной таблицы 
            //    {"quantity" } строка 1, столбец 0 => [row,col] = [1,0] поля для другой таблицы таблиц 
            // };
            string select = default(string),
                allSelect = default(string);
            //for для строк, количество полей для каждой таблицы
            for (int row = 0; row < nameFields.GetLength(0); row++)

                //for для стоблцов, потому что их может быть много в одной строке, 
                //а значит выполнять до тех пор, пока не закончяться все столбцы. 
                for (int col = 0; col <  nameFields.GetLength(1); col++)
                {
                    //выбрать один select
                    select = GetSelectFromOneFieldTable(nameDatabase, nameTable[row], nameFields[row, col], nameIdFieldTable[row], id[row]);
                    //добавить много selectов и назвать их "как" (подписать) как "Table1, Table2 и т.д."
                    allSelect += " (" + select + ")" + " AS " + nameTable_AS[count] + ", ";
                    count++;
                }
            //удаляем пробел и запятую перед в конце запроса и заменяем на " " чтобы был корректный запрос.
            allSelect = allSelect.Remove(allSelect.Length - 2) + " ";
            //" (SELECT Price_for_ne FROM Admin.Products WHERE id_product = 5 ) AS Table1, 
            // (SELECT Count_products FROM Admin.Stock WHERE id_stock = 3 ) AS Table2"
            return allSelect;
        }
        #endregion

        #endregion

        #region Данные методы создают SET запрос(ы) только для одной ТАБЛИЦЫ

        #region GetSet

        /// <summary>
        /// Количество nameTables_AS = количество nameFields
        /// </summary>
        /// <param name="nameTableResult_AS">Название таблицы (результирующая) в которой будет выполняться вычисление</param>
        /// <param name="nameFieldResult">Название поля (результирующего) в котором будет выполняться вычисление</param>
        /// <param name="nameTables_AS">Название таблиц "как". Как они подписаны в SELECT, когда выбирали данные</param>
        /// <param name="nameFields">Название полей, которые непосредственно принимают учавствие в мат. операции(ях) для получения результата</param>
        /// <param name="mathOperation">Математическая операция. Удаление, деление, умножение и т.д.</param>
        /// <returns>Вернуть ОДНУ строку SET части запроса</returns>
        public string GetSet(string nameTableResult_AS, string nameFieldResult, string[] nameTables_AS, string[] nameFields, string mathOperation)
        {
            string set = " SET " + nameTableResult_AS + "." + nameFieldResult + " = "; //Example: SET T1.price

            //если количество полей которые должны принимать учавствие в расчёте меньше или равно одному, 
            //то SET принимает результат первого поля.
            if (nameTables_AS.Length <= 1)
                set += nameFields[0] + " ";
            //иначе создавать запрос (постепенно добавлять записи в строку)
            else
            {
                for (int i = 0; i < nameTables_AS.Length; i++)
                {
                    //если ещё не конец названий таблиц "как" для вычисления 
                    //то добавить в запись название таблицы "как", его поле и мат. операцию и это всё объединить 
                    if (i < nameTables_AS.Length - 1)
                        set += nameTables_AS[i] + "." + nameFields[i] + " " + mathOperation + " ";
                    else
                        //иначе просто завершить добавлением поля таблицы и завершить строку.
                        set += nameTables_AS[i] + "." + nameFields[i] + " ";
                }
            }
            //Example: " SET T1.price = T2.price_for_one * T3.quantity "
            return set;
        }

        #endregion

        #region GetSet overloaded method

        //Количество nameTables_AS = количество nameFields
        //GetSet overloaded method
        public string GetSet(string nameTableResult_AS, string nameFieldResult, string[] nameTables_AS, string[,] nameFields, string mathOperation)
        {
            //string[,] nameFields ={
            //    {"price_for_one" }, строка 0, стоблец 0 => [row,col] = [0,0] поля для одной таблицы 
            //    {"quantity" } строка 1, столбец 0 => [row,col] = [1,0] поля для другой таблицы таблиц 
            // };
            int countRows = nameFields.GetLength(0); //количество строк  
            int countCol = nameFields.GetLength(1); //количество столбцов 
            string set = " SET " + nameTableResult_AS + "." + nameFieldResult + " = "; //Example: SET T1.price
            //если количество полей которые должны принимать участие в расчёте меньше или равно одному, 
            //то SET принимает результат первого поля.
            if (nameTables_AS.Length <= 1) 
                set += nameTableResult_AS + "." + nameFields[0, 0] + " ";
            else
            {
                for (int i = 0; i < countRows; i++) //количество строк 
                {
                    for (int j = 0; j < countCol; j++) //количество столбцов
                    {
                        //если ещё не конец названий таблиц "как" для вычисления 
                        //то добавить в запись название таблицы "как", его поле и мат. операцию и это всё объединить 
                        if (i < nameTables_AS.Length - 1)
                            set += nameTables_AS[i+j] + "." + nameFields[i, j] + " " + mathOperation + " ";
                        //иначе просто завершить добавлением поля таблицы и завершить строку.
                        else
                            set += nameTables_AS[i+j] + "." + nameFields[i, j] + " ";
                    }
                }
            }
            //Example: " SET T1.price = T2.price_for_one * T3.quantity "
            return set;
        }
        #endregion
        
        #endregion

        #region Метод - GetWhere который создаёт WHERE подзапрос для одной ТАБЛИЦЫ
        /// <summary>
        /// Получаем одну строку запроса WHERE
        /// </summary>
        /// <param name="nameTableResult_AS">Название результируещей таблицы "как". Example: T1</param>
        /// <param name="nameIdFieldResult">Название рельльтируещего поля таблицы. Example: id_stock</param>
        /// <param name="id">Номер id куда нужно вставить данные</param>
        /// <returns>Вернуть одну строку WHERE для запроса</returns>
        public string GetWhere(string nameTableResult_AS, string nameIdFieldResult, uint id)
        {
            return " WHERE " + nameTableResult_AS + "." + nameIdFieldResult + " = " + id.ToString() + "; ";
            //Exempl: WHERE T1.id_stock = 1; 
        }
        #endregion

        #region Метод - GetUpdate который создаёт Update подзапрос для одной ТАБЛИЦЫ
        /// <summary>
        /// Получаем начало запроса UPDATE, где нужно обновить данные (посчитать значение для ячейки таблицы) 
        /// </summary>
        /// <param name="nameDatabase">Название БД</param>
        /// <param name="nameTableResult">Название таблицы куда нужно вставить значение (посчитать в ячейке)</param>
        /// <param name="nameTableResult_AS">Подписать в дальнейшем эту таблицу "как". К примеру: "T1"</param>
        /// <returns>Вернуть одну строку запроса для Update</returns>
        public string GetUpdate(string nameDatabase, string nameTableResult, string nameTableResult_AS)
        {
            return "UPDATE " + nameDatabase + "." + nameTableResult + " AS " + nameTableResult_AS + ", ";
            //Example: UPDATE grocery_supermarket_manager.stock AS T1, 
        }
        #endregion

        #region Метод - GetUpdateQuery создаёт полностью UPDATE запрос для обновления данных в ячейке таблицы (Расчёт в этой ячейке) 

        #region GetUpdateQuery
        /// <summary>
        /// Получаем полностью запрос для обновления (расчёта значиений в ячейке таблицы)
        /// </summary>
        /// <param name="nameDatabase">Название БД</param>
        /// <param name="nameTableResult">Название таблицы куда нужно вставить значение (посчитать в ячейке)</param>
        /// <param name="nameTableResult_AS">Подписать в дальнейшем эту таблицу "как". К примеру: "T1"</param>
        /// <param name="nameIdFieldResult">Для запроса WHERE. Навзание ID поля таблицы, где нужно выполнить вычисление ячейки</param>
        /// <param name="resultId">Для подзапроса WHERE. Номер ID поля таблицы, где нужно выполнить вычисление ячейки</param>
        /// <param name="SELECT">Чать запроса. Запросы SELECT</param>
        /// <param name="SET">Часть запроса SET</param>
        /// <returns>Возвращает полностью строку запроса</returns>
        public string GetUpdateQuery(string nameDatabase, string nameTableResult, string nameTableResult_AS,
            string nameIdFieldResult, uint resultId, string SELECT, string SET)
        {
            string update = "UPDATE " + nameDatabase + "." + nameTableResult + " AS " + nameTableResult_AS + ", ";
            update += SELECT + SET;
            update += " WHERE " + nameTableResult_AS + "." + nameIdFieldResult + " = " + resultId.ToString() + " ; ";
            return update;
            //Example:
            // "UPDATE grocery_supermarket_manager.stock AS T1, " +
            //         " (SELECT price_for_one  FROM grocery_supermarket_manager.products WHERE id_products = 1) AS T2, " +
            //         " (SELECT quantity FROM grocery_supermarket_manager.stock WHERE id_stock = 1) AS T3 " +
            // " SET T1.price = T2.price_for_one * T3.quantity " +
            // " WHERE T1.id_stock = 1; ";
        }
        #endregion

        #region GetUpdateQuery overload method
        /// <summary>
        /// Перегруженный метод. Простой в использовании. 
        /// </summary>
        /// <param name="UPDATE">Часть запроса Update</param>
        /// <param name="SELECT">Чать запроса. Запросы SELECT</param>
        /// <param name="SET">Часть запроса SET</param>
        /// <param name="WHERE">Часть запроса WHERE</param>
        /// <returns>Возвращает полностью строку запроса</returns>
        public string GetUpdateQuery(string UPDATE, string SELECT, string SET, string WHERE)
        {
            return UPDATE + SELECT + SET + WHERE;
            //Example:
            // "UPDATE grocery_supermarket_manager.stock AS T1, " +
            //         " (SELECT price_for_one  FROM grocery_supermarket_manager.products WHERE id_products = 1) AS T2, " +
            //         " (SELECT quantity FROM grocery_supermarket_manager.stock WHERE id_stock = 1) AS T3 " +
            // " SET T1.price = T2.price_for_one * T3.quantity " +
            // " WHERE T1.id_stock = 1; ";
        }
        #endregion

        #endregion

        #region Метод - GetValueFromFieldTalbe позволяет получить нужное значение функции (min, max и т.д.) с помощью запроса
        /// <summary>
        /// Получаем нужное значение функции (min, max и т.д.) с помощью запроса 
        /// для данного (одного) поля таблицы
        /// </summary>
        /// <param name="nameDatabase">Название базы данных</param>
        /// <param name="table">Название таблицы с которого нужно получить значение функции</param>
        /// <param name="field">Название поля таблицы с которого нужно получить значение функции</param>
        /// <param name="func">Название самой функции</param>
        /// <returns>Вернуть значение выполненной фукнции в строке, для вставки в запрос или другого использования</returns>
        public string GetValueFromFieldTable(string table, string field, string func)
        {
            Connection connect = new Connection();
            string result = default(string),
              //  Support functions 
              //  AVG()  BIT_AND()  BIT_OR()   BIT_XOR()   COUNT() COUNT(DISTINCT) GROUP_CONCAT()  MAX()  MIN()  STD()  
              //  STDDEV()    STDDEV_POP()  STDDEV_SAMP()  SUM()  VAR_POP()   VAR_SAMP()  VARIANCE() 
            
                query = "SELECT " + func + "(" + field + ")" + " FROM " + connect.NAME_DATABASE + "." + table + ";";

            //передаём наш запрос и пытаемся выполнить команду.
            connect.command = new MySqlCommand(query, connect.connection);
            //Открываем соединение
            connect.OpenConnection();
            //System.NullReferenceException occurs when their is no data/result
            //Пытаемся получить нужное нам значение с запроса
            var getValue = connect.command.ExecuteScalar();
            //если получаем значение
            if (getValue != null)
            {
                //конвертируем результат в строку 
                result = getValue.ToString();
            }
            connect.CloseConnection(); //закрываем соединение
            //Example: query = 15; max id field id_stock table stock database admin_company
            return result;
        }
        #endregion


        //Новая версия. Новые методы для получения значения из ячейки с использование ExecuteScalar

        #region GetSelectQuery. Запрос для получения значения ячейки
        /// <summary>
        /// Получить запрос для выбора числового значения (и не только) из ячейки поля таблицы
        /// </summary>
        /// <param name="nameDatabase">Название базы данных</param>
        /// <param name="table">Название таблицы</param>
        /// <param name="field">Название поля таблицы</param>
        /// <param name="idField">Название id поля таблицы, по котором будет осуществляться выборка</param>
        /// <param name="id">id поля с которого нужно получить значение</param>
        /// <returns>Запрос SELECT</returns>
        public string GetSelectQuery(string table =  "", string field = "", string idField = "", string id = "" )
        {

            return " SELECT " + field +
                   " FROM " + connect.NAME_DATABASE + "." + table +
                   " WHERE " + idField + " = " + id + "; ";
        }
        #endregion

        #region GetSelectValue. Получить число с ячейки
        /// <summary>
        /// Получить числовое значение с ячейки  (с плавающей точкой) из запроса
        /// </summary>
        /// <param name="query">Запрос SELECT для получения значения из ячейки</param>
        /// <returns>Вернуть значение ячейки (число с плавающей точкой)</returns>
        public float GetSelectValue(string query)
        {
            connect.OpenConnection();
            connect.command = new MySqlCommand(query, connect.connection);
            var v = connect.command.ExecuteScalar();
            connect.CloseConnection();
            try
            {
                float value = Convert.ToSingle(v);
                return value;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не получено значение с ячейки: \n" + ex.Message);
                return 0.0f;
            }

        }
        #endregion

        #region GetAllSelectValues. Получить список List<float>  всех значений из нужных ячеек таблиц
        /// <summary>
        /// Получить числовове значение из всех выбранных ячеек полей таблиц 
        /// </summary>
        /// <param name="tables">Массив таблиц с которых нужно получить значения</param>
        /// <param name="fields">Двумерный массив столбцов таблиц в которых содержаться необходимые значения</param>
        /// <param name="idFields">Массив названия id полей таблиц</param>
        /// <param name="ids">Номера id записей в таблице</param>
        /// <returns>Получить float List список всех числовых значений необходимых нам ячеек из таблиц</returns>
        public List<float> GetAllSelectValues(string[] tables, string[][] fields, string[] idFields, string[] ids)
        {
            string query = default(string);
            float value = default(float);
            List<float> values = new List<float>();

            for(int currentTable =0; currentTable < tables.Length; currentTable++)
                for (int currentField = 0; currentField < fields[currentTable].Length; currentField++)
                {
                    query = GetSelectQuery(tables[currentTable], fields[currentTable][currentField],
                                        idFields[currentTable], ids[currentTable]);
                    value = GetSelectValue(query);
                    values.Add(value);
                }
            return values;
        }
        #endregion

        #region GetCalc Получить результат вычисления значений
        /// <summary>
        ///Получение результата вычисления из списка числовых значений
        /// </summary>
        /// <param name="values">Список всех числовых значений</param>
        /// <param name="mathOperation">Мат. операция</param>
        /// <returns>Результат вычисления</returns>
        public float GetCalc(List<float> values, string mathOperation = "+")
        {
            float result = values.First();
            switch (mathOperation)
            {
                case "+":
                {
                    for (int i = 1; i < values.Count; i++)
                        result += values[i];
                       break;
                 }
                case "-":
                    {
                        for (int i = 1; i < values.Count; i++)
                            result -= values[i];
                        break;
                    }
                case "*":
                    {
                        for (int i = 1; i < values.Count; i++)
                            result *= values[i];
                        break;
                    }
                case "/":
                    {
                        for (int i = 1; i < values.Count; i++)
                        { 
                            if(i == 0) { return result; }
                            result /= values[i];
                        }
                        break;
                    }
                default: return 0.0f;      
            }
            return result;
        }
        #endregion

        #region GetUpdateQuery. Получить update запрос для просчета значения в ячейке
        /// <summary>
        /// Update запрос для обновления данных (нового значения) в указанной ячейке таблицы
        /// </summary>
        /// <param name="table">Название таблицы, где нужно применить запрос</param>
        /// <param name="field">Поле таблицы, к которому относиться запрос</param>
        /// <param name="idField">Название id поля табилцы</param>
        /// <param name="id">Необходимая ячейка куда нужно вставить новое значение</param>
        /// <param name="result">Числовое значение, результата вычисления</param>
        /// <returns>Update запрос для вставки нового значения в ячейку</returns>
        public string GetUpdateQuery(string table, string field, string idField, string id, float result)
        {
            string updateQuery = " UPDATE " + connect.NAME_DATABASE + "." + table + " AS T1 " +
                                 " SET T1." + field + " = " + result.ToString().Replace(',','.') + " " +
                              " WHERE T1." + idField + " = " + id + "; ";
            return updateQuery;
        }
        #endregion

        public string GetUpdateQuery(Tuple<
                                    Tuple<string[], string[][], string[], string[]>,
                                    Tuple<string>,
                                    Tuple<string, string, string, string>
                            > dataCalculations)
        {
            //Получаем все числовые значения
            List<float> values = GetAllSelectValues(dataCalculations.Item1.Item1, dataCalculations.Item1.Item2, dataCalculations.Item1.Item3, dataCalculations.Item1.Item4);
            //Получаем расчёт всех значений
            float result = GetCalc(values, dataCalculations.Item2.Item1);
            //Получаем Полностью готовый Update запрос
            string updateQuery = GetUpdateQuery(dataCalculations.Item3.Item1, dataCalculations.Item3.Item2, dataCalculations.Item3.Item3, dataCalculations.Item3.Item4, result);

            return updateQuery;
        }
    }

}