namespace Administrator_supermarket
{
    public class Queries
    {

        #region Все функции не рабочие
        /*
        #region Часть запроса Select
        /// <summary>
        /// Создаёт часть запроса SELECT
        /// </summary>
        /// <param name="nameTables">Имя таблиц с откуда нужно выбрать данные</param>
        /// <param name="nameFields">Имя полей таблицы, которые нужно выбрать</param>
        /// <returns>Вернуть часть запроса</returns>
        public string CreateSelect(string[] nameTables, string[] nameFields)
        {
            string select = "select"; //создаём часть запроса select
            //просматриваем все имена таблиц
            foreach (var table in nameTables)
            {
                //просматриваем все имена полей
                foreach (var field in nameFields)
                {
                    select += " " + table + "." + field + ", ";
                }
            }
            
            //удаляем пробел и запятую перед From, чтобы был корректный запрос.
            select = select.Remove(select.Length - 2) + " ";
            return select;
        }
        #endregion

        #region Часть запроса From
        /// <summary>
        /// Создаёт часть запроса from
        /// </summary>
        /// <param name="nameDatabase">Имя базы данных</param>
        /// <param name="nameTables">Имя таблиц с откуда нужно выбрать данные</param>
        /// <returns>вернуть часть запроса</returns>
        public string CreateFrom(string nameDatabase, string[] nameTables)
        {
            string from = "from"; //Создаём часть запроса from
            foreach (var table in nameTables)
            {
                from += " " + nameDatabase + "." + table + ", ";
            }
            //удаляем пробел и запятую перед Where, чтобы был корректный запрос.
            from = from.Remove(from.Length - 2) + " ";
            return from;
        }
        #endregion

        #region Часть запроса Where
        /// <summary>
        /// Создаём часть запроса Where
        /// </summary>
        /// <param name="nameDatabase">Название базы данных</param>
        /// <param name="nameTables">Назавание таблиц в которых содержаться id</param>
        /// <param name="AllIdForWhere">Все Id для WHERE чтобы данные не дублировались</param>
        /// <returns>Вернуть часть запроса</returns>
        public string CreateWhere(string nameDatabase, string[] nameTables, string[] AllIdForWhere)
        {
            string where = "where";//создаём часть запроса Where
            for (int table=0; table < nameTables.Length; table++)
            {
                for (int id=0; id < AllIdForWhere.Length; id++)
                {
                    where += " " + nameDatabase + "." + nameTables[table] + "." + AllIdForWhere[id] + " = ";
                    where += " " + nameDatabase + "." + nameTables[table++] + "." + AllIdForWhere[id++] + ", ";
                    if (id == AllIdForWhere.Length - 1) break;
                }
            }
            where = where.Remove(where.Length - 2) + " ;";
            return where;
        }
        #endregion

        #region CreateQueryChooseFields Запрос чтобы отобразить выбранные нами поля в таблице
        /// <summary>
        /// Запрос для отображения выбранных нами полей в таблице
        /// </summary>
        /// <param name="select">Часть запроса Select</param>
        /// <param name="from">Часть запроса from</param>
        /// <param name="where">Часть запроса where</param>
        /// <returns>Вернуть запрос</returns>
        public string CreateQueryChooseFields(string select, string from, string where)
        { 
            return " " + select + " "+ from + " " + where + " ";
        }
        #endregion

        #region CreateQueryChooseFields overload
        /// <summary>
        /// Запрос для отображения выбранных нами полей в таблице
        /// </summary>
        /// <param name="nameDatabase">Имя базы данных</param>
        /// <param name="nameTables">Имя таблиц с откуда нужно выбрать данные</param>
        /// <param name="nameFields">Имя полей таблицы, которые нужно выбрать</param>
        /// <param name="idForWhere">Id для WHERE чтобы данные не дублировались</param>
        /// <returns>Вернуть запрос</returns>
        public string CreateQueryChooseFields(string nameDatabase , string[] nameTables , string[] nameFields , string idForWhere )
        {
            string select = "select"; //создаём часть запроса select
            //просматриваем все имена таблиц
            foreach (var table in nameTables)
            {
                //просматриваем все имена полей
                foreach (var field in nameFields)
                {
                    select += " " + table + "." + field + ", ";
                }
            }
            //удаляем пробел и запятую перед From, чтобы был корректный запрос.
            select = select.Remove(select.Length - 2) + " ";

            string from = "from"; //Создаём часть запроса from
            foreach (var table in nameTables)
            {
                from += " " + nameDatabase + "." + table + ", ";
            }
            //удаляем пробел и запятую перед Where, чтобы был корректный запрос.
            from = from.Remove(from.Length - 2) + " ";

            string where = "where";//создаём часть запроса Where
            where += " " + nameDatabase + "." + nameTables[0] + "." + idForWhere + " = ";
            where += " " + nameDatabase + "." + nameTables[1] + "." + idForWhere + " ; ";

            return " " + select + " " + from + " " + where + " ";
        }
        #endregion
    */
        #endregion


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

            int count = 0; //для nameTable_AS, для нормального запроса. Буквально: Назвать таблицу как
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
                for (int col = 0; col < nameFields.GetLength(1); col++)
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
                            set += nameTables_AS[i + j] + "." + nameFields[i, j] + " " + mathOperation + " ";
                        //иначе просто завершить добавлением поля таблицы и завершить строку.
                        else
                            set += nameTables_AS[i + j] + "." + nameFields[i, j] + " ";
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
        /// <returns>Вернуть значение выполненной фукнции в сроке, для вставки в запрос или другого использования</returns>
        public string GetValueFromFieldTable(string nameDatabase, string table, string field, string func)
        {
            Connection connect = new Connection();
            string result = default(string),
                //  Support functions 
                //  AVG()  BIT_AND()  BIT_OR()   BIT_XOR()   COUNT() COUNT(DISTINCT) GROUP_CONCAT()  MAX()  MIN()  STD()  
                //  STDDEV()    STDDEV_POP()  STDDEV_SAMP()  SUM()  VAR_POP()   VAR_SAMP()  VARIANCE() 

                query = "SELECT " + func + "(" + field + ")" + " FROM " + nameDatabase + "." + table + ";";

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
        public string GetSelectQuery(string nameDatabase = "", string table = "", string field = "", string idField = "", string id = "")
        {

            return " SELECT " + field +
                   " FROM " + nameDatabase + "." + table +
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
        /// <param name="nameDatabase">Название базы данных</param>
        /// <param name="tables">Массив таблиц с которых нужно получить значения</param>
        /// <param name="fields">Двумерный массив полей таблиц в котороых содержаться необходимые значения</param>
        /// <param name="idFields">Массив названия id полей таблиц</param>
        /// <param name="ids">Номера id записей в таблице</param>
        /// <returns>Получить float List список всех числовых значений необходимых нам ячеек из таблиц</returns>
        public List<float> GetAllSelectValues(string nameDatabase, string[] tables, string[][] fields, string[] idFields, string[] ids)
        {
            string query = default(string);
            float value = default(float);
            List<float> values = new List<float>();

            for (int currentTable = 0; currentTable < tables.Length; currentTable++)
                for (int currentField = 0; currentField < fields[currentTable].Length; currentField++)
                {
                    query = GetSelectQuery(nameDatabase, tables[currentTable], fields[currentTable][currentField],
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
        /// <param name="values">Списко всех числовых значений</param>
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
                            if (i == 0) { return result; }
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
        /// <param name="nameDatabase">Название БД</param>
        /// <param name="table">Название таблицы, где нужно применить запрос</param>
        /// <param name="field">Поле таблицы, к которому относиться запрос</param>
        /// <param name="idField">Название id поля табилцы</param>
        /// <param name="id">Необходимая ячейка куда нужно вставить новое значение</param>
        /// <param name="result">Числовое значение, результата вычисления</param>
        /// <returns>Update запрос для вставки нового значения в ячейку</returns>
        public string GetUpdateQuery(string nameDatabase, string table, string field, string idField, string id,
            float result)
        {
            string updateQuery = " UPDATE " + nameDatabase + "." + table + " AS T1 " +
                              " SET T1." + field + " = " + result.ToString() + " " +
                              " WHERE T1." + idField + " = " + id + "; ";
            return updateQuery;
        }
        #endregion

        #region Подключение к БД
        //Подсоединение к БД MySQL
        public MySqlConnection connection = new MySqlConnection("datasource=localhost; port=3306; username = root; password = andrey_1a6c2b");

        //подключение к серверу где находится бд.
        //public MySqlConnection connection = new MySqlConnection("Server =sql7.freemysqlhosting.net; Port=3306; Database=sql7150982; Uid=sql7150982; Pwd=1VAsQp6rY1;");

        //для выполнения комманд в дальнейшем
        public MySqlCommand command;

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
        public void ExecuteQuery(String query, Boolean showMessageBox = true)
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
                    if (showMessageBox == true)
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
        public void ShowTable(System.Windows.Forms.DataGridView DataGridView, string query)//, string nameTable)
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
        /// <param name="query">запрос, который содержит select с параметром поиска значения по столбцам (числовое или строковое)</param>
        public void FillDataGridView(DataGridView dataGridView, string query = "")
        {
            try
            {
                command = new MySqlCommand(query, connection); //Создаём запрос для поиска
                MySqlDataAdapter adapter = new MySqlDataAdapter(command); //Выполняем команду
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
        public string GetQueryShowSearch(string nameTable, string[] nameFieldsAll, string[] newNameFieldsAS, string[] nameNumericFields = null, string valueToSearh = "")
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
            for (int i = 0; i < nameFields.GetLength(0); i++)
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

    }
}