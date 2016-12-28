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

        public bool Void(TextBox textBox)
        {
            string data = textBox.ToString();
            if (data == null || data == "" || data == " " || data == "0")
                return false;
            else
                return true;
        }

        public bool VoidString(string data)
        {
            if (data == null || data == "" || data == " " || data == "0")
                return false;
            else
                return true;
        }

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

        public void ErrorMessage(System.Windows.Forms.Form Form)
        {

            string message = "Ошибка при добавление записей в БД! \nПричины: пустое обязательное для записи поле или попытка ввода sql-инъекции.";
            string caption = "Неверный ввод!";
            MessageBoxButtons buttons = MessageBoxButtons.RetryCancel;
            DialogResult result = MessageBox.Show(message, caption, buttons);
            if (result == DialogResult.Cancel)
               Form.Close();
        }
    }
}
