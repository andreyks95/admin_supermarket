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
    
    }
}