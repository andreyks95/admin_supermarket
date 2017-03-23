using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Administrator_company.LogicProgram
{
    /// <summary>
    /// Этот класс содержит методы для:
    /// проверки данных на пустоту
    /// безопасности ввода, чтобы пользователь не вводил свои запросы
    /// </summary>
    public class Checking
    {

        #region Данные методы проверяют поле(я) (TextBox, ComboBox, DateTimePicker) на ввод вредных запросов

        #region Security TextBox
        /// <summary>
        /// Проверяет безопасность ввода. Если в textBox есть sql-инъекция, то прервать ввод. 
        /// </summary>
        /// <param name="textBox">TextBox который нужно проверить</param>
        /// <returns>Можно добавлять запись или нет</returns>
        public bool Security(TextBox textBox)
        {

            string data = textBox.Text.ToString();
            //регулярное выражение
            string regex = @"SELECT  {1}?  | INSERT  {1}? | UPDATE  {1}? | UNION  {1}? | AND  {1}? | OR  {1}? |  group_concat  {1}? |  \'{1}? | \/\*{1}? | (--){1}? | \+ {1}? | \( {1}? | \;{1}? | (@@){1}?";
            Regex reg = new Regex(regex, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
           // bool result = reg.IsMatch(data);
            Match match = reg.Match(data);
            bool result = match.Success;

            //если строка пройшла регулярное выражение и в ней содержиться вредный SQL запрос
            if (result == true)
                //тогда не давать разрешение на вставку запроса в БД 
                return false;
            else
                //дать разрешение на вставку запроса в БД
                return true;
        }
        #endregion

        #region Security ComboBox
        /// <summary>
        /// Проверяет безопасность ввода. Если в ComboBox есть sql-инъекция, то прервать ввод. 
        /// </summary>
        /// <param name="ComboBox">CombobBox который нужно проверить</param>
        /// <returns>Можно добавлять запись или нет</returns>
        public bool Security(ComboBox ComboBox)
        {
            string data;
            if (ComboBox.SelectedItem == null) //or if (string.IsNullOrEmpty(comboBox1.Text)) or if (comboBox1.SelectedIndex == -1)
                data = ComboBox.Text.ToString();       
            else
                data = ComboBox.SelectedItem.ToString();

            //регулярное выражение
            string regex = @"SELECT  {1}?  | INSERT  {1}? | UPDATE  {1}? | UNION  {1}? | AND  {1}? | OR  {1}? |  group_concat  {1}? |  \'{1}? | \/\*{1}? | (--){1}? | \+ {1}? | \( {1}? | \;{1}? | (@@){1}?";
            Regex reg = new Regex(regex, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
            // bool result = reg.IsMatch(data);
            Match match = reg.Match(data);
            bool result = match.Success;

            //если строка пройшла регулярное выражение и в ней содержиться вредный SQL запрос
            if (result == true)
                //тогда не давать разрешение на вставку запроса в БД 
                return false;
            else
                //дать разрешение на вставку запроса в БД
                return true;
        }
        #endregion 

        #region Security DateTimePicker
        /// <summary>
        /// Проверяет безопасность ввода. Если в DateTimePicker есть sql-инъекция, то прервать ввод. 
        /// </summary>
        /// <param name="DateTimePicker">DateTimePicker который нужно проверить</param>
        /// <returns>Можно добавлять запись или нет</returns>
        public bool Security(DateTimePicker DateTimePicker)
        {

            string data = DateTimePicker.Value.ToString();
            //регулярное выражение
            string regex = @"SELECT  {1}?  | INSERT  {1}? | UPDATE  {1}? | UNION  {1}? | AND  {1}? | OR  {1}? |  group_concat  {1}? |  \'{1}? | \/\*{1}? | (--){1}? | \+ {1}? | \( {1}? | \;{1}? | (@@){1}?";
            Regex reg = new Regex(regex, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
            // bool result = reg.IsMatch(data);
            Match match = reg.Match(data);
            bool result = match.Success;

            //если строка пройшла регулярное выражение и в ней содержиться вредный SQL запрос
            if (result == true)
                //тогда не давать разрешение на вставку запроса в БД 
                return false;
            else
                //дать разрешение на вставку запроса в БД
                return true;
        }
        #endregion 

        #region SecurityString overload 
        public bool SecurityString(string data)
        {
            string regex = @"SELECT  {1}?  | INSERT  {1}? | UPDATE  {1}? | UNION  {1}? | AND  {1}? | OR  {1}? |  group_concat  {1}? |  \'{1}? | \/\*{1}? | (--){1}? | \+ {1}? | \( {1}? | \;{1}? | (@@){1}?";
            Regex reg = new Regex(regex, RegexOptions.Compiled | RegexOptions.IgnoreCase  | RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline);
            // bool result = reg.IsMatch(data.ToString());
            Match match = reg.Match(data);
            bool result = match.Success;

            //если строка пройшла регулярное выражение и в ней содержиться вредный SQL запрос
            if (result == true)
                //тогда не давать разрешение на вставку запроса в БД 
                return false;
            else
                //дать разрешение на вставку запроса в БД
                return true;
        }
        #endregion

        #region Security
        /// <summary>
        /// Проверяет безопасность ввода. Если есть sql-инъекция хотя бы в одном из объектов, то прервать ввод. 
        /// </summary>
        /// <param name="textBox">TextBox который нужно проверить</param>
        /// <param name="comboBox">ComboBox который нужно проверить</param>
        /// <param name="dateTimePicker">DateTimePicker который нужно проверить</param>
        /// <returns>Можно добавлять запись или нет</returns>
        public bool Security(TextBox textBox = null, ComboBox comboBox = null, DateTimePicker dateTimePicker = null)
        {
            string data;
            if (textBox != null || comboBox != null || dateTimePicker != null)
            {
                if (textBox != null)
                    data = textBox.Text.ToString();
                else
                    data = "";
                if (comboBox != null)
                    data = comboBox.SelectedItem.ToString();
                else
                    data = "";
                if (dateTimePicker != null)
                    data = dateTimePicker.Value.ToString(CultureInfo.InvariantCulture);
                else
                    data = "";
            }
            else
                return false;

            //регулярное выражение
            string regex = @"SELECT  {1}?  | INSERT  {1}? | UPDATE  {1}? | UNION  {1}? | AND  {1}? | OR  {1}? |  group_concat  {1}? |  \'{1}? | \/\*{1}? | (--){1}? | \+ {1}? | \( {1}? | \;{1}? | (@@){1}?";
            Regex reg = new Regex(regex, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
            // bool result = reg.IsMatch(data);
            Match match = reg.Match(data);
            bool result = match.Success;

            //если строка пройшла регулярное выражение и в ней содержиться вредный SQL запрос
            if (result == true)
                //тогда не давать разрешение на вставку запроса в БД 
                return false;
            else
                //дать разрешение на вставку запроса в БД
                return true;
        }
        #endregion

        #region SecurityAll TextBox
        /// <summary>
        /// Проверяет каждый textBox таблицы на ввод sql-инъекции (вредный запрос)
        /// Если есть хотя бы одно поле, которое содержит sql-инъекцию (вредный запрос)
        /// тогда прервавть добавление данных в таблицу
        /// </summary>
        /// <param name="textBoxs">Массив textBox-ов таблицы</param>
        /// <returns>Можно добавить данные или нет</returns>
        public bool SecurityAll(params TextBox[] textBoxs)
        {
            bool result = default(bool);
            byte count = 0;

            foreach (var i in textBoxs)
            {
                result = Security(i);
                //если результат проверки вернул то, что нельзя добавлять данные увеличиваем счётчик
                if (result == false)
                    count++;
            }
            //Если вредных вводимых данных больше, чем одно тогда нельзя добавлять данные.
            if (count >= 1)
                return false;
            else
                return true;
        }
        #endregion

        #region SecurityAll ComboBox
        /// <summary>
        /// Проверяет каждый ComboBox таблицы на ввод sql-инъекции (вредный запрос)
        /// Если есть хотя бы одно поле, которое содержит sql-инъекцию (вредный запрос)
        /// тогда прервавть добавление данных в таблицу
        /// </summary>
        /// <param name="ComboBox">Массив ComboBox-ов таблицы</param>
        /// <returns>Можно добавить данные или нет</returns>
        public bool SecurityAll(params ComboBox[] ComboBox)
        {
            bool result = default(bool);
            byte count = 0;

            foreach (var i in ComboBox)
            {
                result = Security(i);
                //если результат проверки вернул то, что нельзя добавлять данные увеличиваем счётчик
                if (result == false)
                    count++;
            }
            //Если вредных вводимых данных больше, чем одно тогда нельзя добавлять данные.
            if (count >= 1)
                return false;
            else
                return true;
        }
        #endregion

        #region SecurityAll DateTimePicker
        /// <summary>
        /// Проверяет каждый DateTimePicker таблицы на ввод sql-инъекции (вредный запрос)
        /// Если есть хотя бы одно поле, которое содержит sql-инъекцию (вредный запрос)
        /// тогда прервавть добавление данных в таблицу
        /// </summary>
        /// <param name="DateTimePicker">Массив DateTimePicker-ов таблицы</param>
        /// <returns>Можно добавить данные или нет</returns>
        public bool SecurityAll(params DateTimePicker[] DateTimePicker)
        {
            bool result = default(bool);
            byte count = 0;

            foreach (var i in DateTimePicker)
            {
                result = Security(i);
                //если результат проверки вернул то, что нельзя добавлять данные увеличиваем счётчик
                if (result == false)
                    count++;
            }
            //Если вредных вводимых данных больше, чем одно тогда нельзя добавлять данные.
            if (count >= 1)
                return false;
            else
                return true;
        }
        #endregion

        #region SecurityAllString overload
        public bool SecurityAllString(string[] str)
        {
            bool result = default(bool);
            byte count = 0;

            foreach (var i in str)
            {
                result = SecurityString(i);
                //если результат проверки вернул то, что нельзя добавлять данные увеличиваем счётчик
                if (result == false)
                    count++;
            }
            //Если вредных вводимых данных больше, чем одно тогда нельзя добавлять данные.
            if (count >= 1)
                return false;
            else
                return true;
        }
        #endregion

        #region SecurityAll
        /// <summary>
        /// Проверяет каждые объекты формы таблицы на ввод sql-инъекции (вредный запрос)
        /// Если есть хотя бы одно поле, которое содержит sql-инъекцию (вредный запрос)
        /// тогда прервавть добавление данных в таблицу
        /// </summary>
        /// <param name="textBoxs">Массив textBox-ов таблицы</param>
        /// <param name="comboBoxs">Массив comboBoxs-ов таблицы</param>
        /// <param name="dateTimePickers">Массив dateTimePickers-ов таблицы</param>
        /// <returns>Можно добавить данные или нет</returns>
        public bool SecurityAll(TextBox[] textBoxs=null, ComboBox[] comboBoxs=null, DateTimePicker[] dateTimePickers=null)
        {
            bool result = default(bool);
            byte count = 0;
            if (textBoxs != null || comboBoxs != null || dateTimePickers != null)
            {
                if (textBoxs != null)
                    foreach (var i in textBoxs)
                    {
                        result = Security(i);
                        //если результат проверки вернул то, что нельзя добавлять данные увеличиваем счётчик
                        if (result == false)
                            count++;
                    }
                if (comboBoxs != null)
                    foreach (var i in comboBoxs)
                    {
                        result = Security(i);
                        //если результат проверки вернул то, что нельзя добавлять данные увеличиваем счётчик
                        if (result == false)
                            count++;
                    }
                if (dateTimePickers != null)
                    foreach (var i in dateTimePickers)
                    {
                        result = Security(i);
                        //если результат проверки вернул то, что нельзя добавлять данные увеличиваем счётчик
                        if (result == false)
                            count++;
                    }
            }
            else
                return false;
            //Если вредных вводимых данных больше, чем одно тогда нельзя добавлять данные.
            if (count >= 1)
                return false;
            else
                return true;
        }
        #endregion

        #endregion

        #region  Данные методы проверяют поле(я) (TextBox, ComboBox, DateTimePicker) на  ввод пустых данных

        #region Void TextBox
        /// <summary>
        /// Проверяет каждое поле (TextBox) на ввод пустых значений 
        /// Если пустое поле - то вернуть информацию о том, что это поле нельзя добавлять в БД
        /// </summary>
        /// <param name="textBox">TextBox - который передаётся</param>
        /// <returns>Можно это поле добавлять или нет</returns>
        public bool Void(TextBox textBox)
        {
            string data = textBox.ToString();
            if (data == null || data == "" || data == " " || data == "0")
                return false;
            else
                return true;
        }
        #endregion

        #region Void ComboBox
        /// <summary>
        /// Проверяет каждое поле (ComboBox) на ввод пустых значений 
        /// Если пустое поле - то вернуть информацию о том, что это поле нельзя добавлять в БД
        /// </summary>
        /// <param name="ComboBox">ComboBox - который передаётся</param>
        /// <returns>Можно это поле добавлять или нет</returns>
        public bool Void(ComboBox ComboBox)
        {
            string data;
            if (ComboBox.SelectedItem == null) //or if (string.IsNullOrEmpty(comboBox1.Text)) or if (comboBox1.SelectedIndex == -1)
                data = ComboBox.Text.ToString();
            else
                data = ComboBox.SelectedItem.ToString();

            if (data == null || data == "" || data == " " || data == "0")
                return false;
            else
                return true;
        }
        #endregion

        #region Void DateTimePicker
        /// <summary>
        /// Проверяет каждое поле (DateTimePicker) на ввод пустых значений 
        /// Если пустое поле - то вернуть информацию о том, что это поле нельзя добавлять в БД
        /// </summary>
        /// <param name="dateTimePicker">DateTimePicker - который передаётся</param>
        /// <returns>Можно это поле добавлять или нет</returns>
        public bool Void(DateTimePicker dateTimePicker)
        {
            string data = dateTimePicker.Value.ToString();
            if (data == null || data == "" || data == " " || data == "0")
                return false;
            else
                return true;
        }
        #endregion

        #region VoidString overload
        public bool VoidString(string data)
        {
            if (data == null || data == "" || data == " " || data == "0")
                return false;
            else
                return true;
        }
        #endregion

        #region Void
        /// <summary>
        /// Проверяет каждое поле на ввод пустых значений 
        /// Если пустое поле - то вернуть информацию о том, что это поле нельзя добавлять в БД
        /// </summary>
        /// <param name="textBox">TextBox - который передаётся</param>
        /// <returns>Можно это поле добавлять или нет</returns>
        public bool Void(TextBox textBox=null, ComboBox comboBox = null, DateTimePicker dateTimePicker = null)
        {
            string data;
            if (textBox != null || comboBox != null || dateTimePicker != null)
            {
                if (textBox != null)
                    data = textBox.Text.ToString();
                else
                    data = "";
                if (comboBox != null)
                    data = comboBox.SelectedItem.ToString();
                else
                    data = "";
                if (dateTimePicker != null)
                    data = dateTimePicker.Value.ToString(CultureInfo.InvariantCulture);
                else
                    data = "";
            }
            else
                return false;

            if (data == null || data == "" || data == " " || data == "0")
                return false;
            else
                return true;
        }
        #endregion

        #region VoidAll TextBox
        /// <summary>
        /// Проверяет все textBox-ы в таблице на ввод пустых значений
        /// если хотя бы есть ОДНО пустое поле, которое нужно ОБЯЗАТЕЛЬНО заполнить
        /// тогда возвращаем информацию о том, что поля нельзя добавлять в таблицу
        /// </summary>
        /// <param name="textBoxs">Массив TextBox-ов таблицы</param>
        /// <returns>Можно добавлять или нет</returns>
        public bool VoidAll(params TextBox[] textBoxs)
        {
            bool result = default(bool);
            byte count = 0;

            foreach (var i in textBoxs)
            {
                result = Void(i);
                //если результат проверки вернул то, что нельзя добавлять данные увеличиваем счётчик
                if (result == false)
                    count++;
            }
            //Если есть пустые данные, которые необходимо добавить больше, чем одно тогда нельзя добавлять данные.
            if (count >= 1)
                return false;
            else
                return true;
        }
        #endregion

        #region VoidAll ComboBox
        /// <summary>
        /// Проверяет все ComboBox-ы в таблице на ввод пустых значений
        /// если хотя бы есть ОДНО пустое поле, которое нужно ОБЯЗАТЕЛЬНО заполнить
        /// тогда возвращаем информацию о том, что поля нельзя добавлять в таблицу
        /// </summary>
        /// <param name="ComboBox">Массив ComboBox-ов таблицы</param>
        /// <returns>Можно добавлять или нет</returns>
        public bool VoidAll(params ComboBox[] ComboBoxs)
        {
            bool result = default(bool);
            byte count = 0;

            foreach (var i in ComboBoxs)
            {
                result = Void(i);
                //если результат проверки вернул то, что нельзя добавлять данные увеличиваем счётчик
                if (result == false)
                    count++;
            }
            //Если есть пустые данные, которые необходимо добавить больше, чем одно тогда нельзя добавлять данные.
            if (count >= 1)
                return false;
            else
                return true;
        }
        #endregion

        #region VoidAll DateTimePicker
        /// <summary>
        /// Проверяет все DateTimePicker-ы в таблице на ввод пустых значений
        /// если хотя бы есть ОДНО пустое поле, которое нужно ОБЯЗАТЕЛЬНО заполнить
        /// тогда возвращаем информацию о том, что поля нельзя добавлять в таблицу
        /// </summary>
        /// <param name="dateTimePicker">Массив DateTimePicker-ов таблицы</param>
        /// <returns>Можно добавлять или нет</returns>
        public bool VoidAll(params DateTimePicker[] dateTimePicker)
        {
            bool result = default(bool);
            byte count = 0;

            foreach (var i in dateTimePicker)
            {
                result = Void(i);
                //если результат проверки вернул то, что нельзя добавлять данные увеличиваем счётчик
                if (result == false)
                    count++;
            }
            //Если есть пустые данные, которые необходимо добавить больше, чем одно тогда нельзя добавлять данные.
            if (count >= 1)
                return false;
            else
                return true;
        }
        #endregion

        #region VoidAllString overload
        public bool VoidAllString(string[] str)
        {
            bool result = default(bool);
            byte count = 0;

            foreach (var i in str)
            {
                result = VoidString(i);
                //если результат проверки вернул то, что нельзя добавлять данные увеличиваем счётчик
                if (result == false)
                    count++;
            }
            //Если есть пустые данные, которые необходимо добавить больше, чем одно тогда нельзя добавлять данные.
            if (count >= 1)
                return false;
            else
                return true;
        }
        #endregion

        #region VoidAll
        /// <summary>
        /// Проверяет все объекты формы в таблице на ввод пустых значений
        /// если хотя бы есть ОДНО пустое поле, которое нужно ОБЯЗАТЕЛЬНО заполнить
        /// тогда возвращаем информацию о том, что поля нельзя добавлять в таблицу
        /// </summary>
        /// <param name="textBoxs">Массив TextBox-ов таблицы</param>
        /// <param name="comboBoxs">Массив ComboBox-ов таблицы</param>
        /// <param name="dateTimePickers">Массив DateTimePicker-ов таблицы</param>
        /// <returns>Можно добавлять или нет</returns>
        public bool VoidAll(TextBox[] textBoxs = null, ComboBox[] comboBoxs = null, DateTimePicker[] dateTimePickers = null)
        {
            //if (comboBoxs == null) throw new ArgumentNullException(nameof(comboBoxs));
            bool result = default(bool);
            byte count = 0;
            if (textBoxs != null || comboBoxs != null || dateTimePickers != null)
            {
                if (textBoxs != null)
                    foreach (var i in textBoxs)
                    {
                        result = Void(i);
                        //если результат проверки вернул то, что нельзя добавлять данные увеличиваем счётчик
                        if (result == false)
                            count++;
                    }
                if (comboBoxs != null)
                    foreach (var i in comboBoxs)
                    {
                        result = Void(i);
                        //если результат проверки вернул то, что нельзя добавлять данные увеличиваем счётчик
                        if (result == false)
                            count++;
                    }
                if (dateTimePickers != null)
                    foreach (var i in dateTimePickers)
                    {
                        result = Void(i);
                        //если результат проверки вернул то, что нельзя добавлять данные увеличиваем счётчик
                        if (result == false)
                            count++;
                    }
            }
            else
                return false;
            //Если есть пустые данные, которые необходимо добавить больше, чем одно тогда нельзя добавлять данные.
            if (count >= 1)
                return false;
            else
                return true;
        }
        #endregion

        #endregion

        #region Проверить корректность ввода дат

        #region CheckDate. Проверить корректность ввода дат
        /// <summary>
        /// CheckDate. Проверить корректность ввода дат
        /// </summary>
        /// <param name="startDateTimePicker">Начальная дата</param>
        /// <param name="endDateTimePicker">Конечная дата</param>
        /// <returns>Можно добавлять или нет</returns>
        public bool CheckDate(DateTimePicker startDateTimePicker, DateTimePicker endDateTimePicker)
        {
            try
            {
                DateTime startDate = startDateTimePicker.Value,
                    endDate = endDateTimePicker.Value;
                TimeSpan difference = endDate - startDate;
                string resultDif = difference.ToString();
                return resultDif[0] != '-';
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        #endregion

        #region CheckDate. Проверить корректность ввода дат
        /// <summary>
        /// CheckDate. Проверить корректность ввода дат
        /// </summary>
        /// <param name="dateTimePickers">Все dateTimpePicker-ы которые содержат даты</param>
        /// <returns>Можно добавлять или нет</returns>
        public bool CheckDate(DateTimePicker[] dateTimePickers)
        {
            return CheckDate(dateTimePickers[0], dateTimePickers[1]);
        }
        #endregion
        
        #endregion

        #region ErrorMessage
        /// <summary>
        ///Если имеются неверно вводимиые данные в таблицу, то закрываем окно в которой оторображается таблица
        /// если пользователь нажал "Отмена"
        /// и даём возможность вводить данные,если пользователь выбрал "Продолжить"
        /// </summary>
        /// <param name="Form">Передача Формы для вывыда сообщения на экран</param>
        public void ErrorMessage(System.Windows.Forms.Form Form)
        {

            string message = "Ошибка при добавление записей в БД! \nПричины: пустое обязательное для записи поле, попытка ввода sql-инъекции, конечная дата меньше чем началальная";
            string caption = "Неверный ввод!";
            //создаётся MessageBox с кнопками: "Продолжить", "Отмена"
            MessageBoxButtons buttons = MessageBoxButtons.RetryCancel;
            //Показываем MessageBox
            //Возвращаем результат "Отмена" или "Продолжить" 
            DialogResult result = MessageBox.Show(message, caption, buttons);
            //Если результат "Отмена" то закрыть форму таблицы
            if (result == DialogResult.Cancel)
               Form.Close();
        }
        #endregion

    }
}
