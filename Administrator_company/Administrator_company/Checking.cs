using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Administrator_supermarket
{
    /// <summary>
    /// Этот класс содержит методы для:
    /// проверки данных на пустоту
    /// безопасности ввода, чтобы пользователь не вводил свои запросы
    /// </summary>
    public class Checking
    {

        #region Данные методы проверяют поле(я) (textBox) на ввод вредных запросов

        #region Security 
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

        #region SecurityAll
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

        #endregion

        #region  Данные методы проверяют поле(я) (textBox) на  ввод пустых данных

        #region Void 
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

        #region VoidString overload
        public bool VoidString(string data)
        {
            if (data == null || data == "" || data == " " || data == "0")
                return false;
            else
                return true;
        }
        #endregion

        #region VoidAll 
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

            string message = "Ошибка при добавление записей в БД! \nПричины: пустое обязательное для записи поле или попытка ввода sql-инъекции.";
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
