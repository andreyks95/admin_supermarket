using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Administrator_company.LogicProgram
{
    public class GetTextObjectsForm
    {
        #region GetTextDateTimePicker

        #region GetTextDateTimePicker. Получить значения текущего DateTimePicker
        /// <summary>
        /// Получить значения текущего DateTimePicker
        /// </summary>
        /// <param name="dateTimePicker">Текущий DateTimePicker формы</param>
        /// <returns>значение DateTimePicker</returns>
        public string GetTextDateTimePicker(DateTimePicker dateTimePicker)
        {
            string text  = dateTimePicker.Value.ToString();
            string inputFormat = "dd'.'MM'.'yyyy' 'H':'mm':'ss", //Текущий формат ввода DateTimePicker //"dd'.'MM'.'yyyy' 'HH':'mm':'ss"
                   outputFormat = "yyyy'-'MM'-'dd"; //для конвертирования даты в формат Date MySQL 
            DateTime dateTime = DateTime.ParseExact(text, inputFormat, CultureInfo.InvariantCulture);//null); //Превращаем текст в дату  
            text = dateTime.ToString(outputFormat, null); //Конвертируем дату в текст с нужным форматом данных
            return text;
        }
        #endregion

        #region GetTextDateTimePicker. Получить массив всех значений DateTimePicker-ов на форме
        /// <summary>
        /// Получить массив всех значений DateTimePicker-ов на форме
        /// </summary>
        /// <param name="dateTimePickers">Все DateTimePicker-ы формы</param>
        /// <returns>Массив значений</returns>
        public string[] GetTextDateTimePicker(DateTimePicker[] dateTimePickers)
        {
            string[] values = new string[dateTimePickers.Length];
            for (int i = 0; i < dateTimePickers.Length; i++)
            {
                values[i] = GetTextDateTimePicker(dateTimePickers[i]);
            }
            return values;
        }
        #endregion

        #endregion

        #region GetTextTextBox

        #region GetTextTextBox. Получить значения текущего TextBox
        /// <summary>
        /// Получить значения текущего TextBox
        /// </summary>
        /// <param name="textBox">Текущий TextBox формы</param>
        /// <returns>значение</returns>
        public string GetTextTextBox(TextBox textBox)
        {
            return textBox.Text;
        }
        #endregion

        #region GetTextTextBox для массива TextBox. Получить массив всех значений TextBox-ов на форме
        /// <summary>
        /// Получить массив всех значений TextBox-ов на форме
        /// </summary>
        /// <param name="textBoxs">Все TextBox-ы формы</param>
        /// <returns>Массив значений</returns>
        public string[] GetTextTextBox(TextBox[] textBoxs)
        {
            string[] values = new string[textBoxs.Length];
            for (int i = 0; i < textBoxs.Length; i++)
            {
                values[i] = GetTextTextBox(textBoxs[i]);
            }
            return values;
        }
        #endregion

        #endregion

        #region GetTextComboBox

        #region GetTextComboBox для ComboBox
        /// <summary>
        /// GetTextComboBox для одного ComboBox
        /// </summary>
        /// <param name="comboBox">Текущий ComboBox формы</param>
        /// <returns>значение текущего ComboBoх</returns>
        public string GetTextComboBox(ComboBox comboBox)
        {
            return comboBox.Text;
        }
        #endregion

        #region GetTextComboBox для массивов ComboBox
        /// <summary>
        /// GetTextComboBox для массивов ComboBox
        /// </summary>
        /// <param name="comboBoxs">Все ComboBox-ы формы</param>
        /// <returns>Массив значений ComboBoxs</returns>
        public string[] GetTextComboBox(ComboBox[] comboBoxs)
        {
            string[] values = new string[comboBoxs.Length];
            for (int i = 0; i < comboBoxs.Length; i++)
            {
                values[i] = GetTextComboBox(comboBoxs[i]);
            }
            return values;
        }
        #endregion

        #endregion

        #region GetTextPicture

        #region GetTextPictureBox. Получить значение с PictureBox и конвертировать в текст.
        /// <summary>
        /// Получить изображение с PictureBox в виде строки
        /// </summary>
        /// <param name="pictureBox"></param>
        /// <returns>Вернуть изображение в виде строки</returns>
        public string GetTextPictureBox(PictureBox pictureBox)
        {
            Settings settings = new Settings();
            //Сохранить изображение из pictureBox в массив byte[]
            byte[] img = settings.SaveImagesToBytes(pictureBox);
            //string text = default(string);
            //foreach (var i in img)
            //    text += Convert.ToString(i);
            //return text;
            return img.Aggregate(default(string), (current, i) => current + Convert.ToString(i));
        }
        #endregion

        #region GetTextPictureBox overload. Получить все значения с PictureBox-оф формы
        /// <summary>
        /// Получить все значения с PictureBox-оф формы
        /// </summary>
        /// <param name="pictureBoxs">все PictureBox-ы формы</param>
        /// <returns>Массив значений</returns>
        public string[] GetTextPictureBox(PictureBox[] pictureBoxs)
        {
            string[] values = new string[pictureBoxs.Length];
            for (int i = 0; i < pictureBoxs.Length; i++)
            {
                values[i] = GetTextPictureBox(pictureBoxs[i]);
            }
            return values;
        }
        #endregion

        #endregion

        //Методы, которые распознают объекты формы (TextBox, ComboBox, DateTimePicker)

        #region GetTextDate - Получить дату в виде текста для DateTimePicker
        /// <summary>
        /// Получить дату в виде текста для DateTimePicker
        /// </summary>
        /// <param name="obj">Объект DateTimePicker</param>
        /// <returns>Дата в виде текста</returns>
        public string GetTextDate(object obj)
        {
            string text;
            Type currentType = obj.GetType();
            PropertyInfo property = currentType.GetProperty("Value");
            text = property.GetValue(obj).ToString();//в свойстве получить значение объекта

            string inputFormat = "dd'.'MM'.'yyyy' 'H':'mm':'ss", //Текущий формат ввода DateTimePicker //"dd'.'MM'.'yyyy' 'HH':'mm':'ss"
                   outputFormat = "yyyy'-'MM'-'dd"; //для конвертирования даты в формат Date MySQL 
            DateTime dateTime = DateTime.ParseExact(text, inputFormat, CultureInfo.InvariantCulture);//null); //Превращаем текст в дату  
            text = dateTime.ToString(outputFormat, null); //Конвертируем дату в текст с нужным форматом данных
            return text;
        }
        #endregion

        #region GetText overload - Перегруженный. Получить текст с свойства объекта WinForm (TextBox, ComboBox ...)
        /// <summary>
        /// Получить текст с свойства объекта WinForm (TextBox, ComboBox ...)
        /// </summary>
        /// <param name="obj">Объект WinForm (TextBox, ComboBox ...)</param>
        /// <param name="nameProperty">Название свойства объекта</param>
        /// <returns>Текст с свойства объекта</returns>
        public string GetText(object obj, string nameProperty)
        {
            string text = null;
            Type currentType = obj.GetType();//получаем тип
            PropertyInfo property = currentType.GetProperty(nameProperty);//Присваиваем ему свойство, c определённым именем. Получить свойство из этого типа
                                                                          // text = property.GetValue(obj).ToString(); //в свойстве получить значение объекта

            //Если выбран ComboBox
            if (nameProperty == "SelectedItem")
            {
                //если SelectedItem == null, то есть не выбран
                //Берём значение того, что введено в ComboBox
                if (currentType.GetProperty(nameProperty).GetValue(obj) == null)
                //or if (string.IsNullOrEmpty(comboBox1.Text)) or if (comboBox1.SelectedIndex == -1)
                {
                    //text = currentType.GetProperty("Text").GetValue(obj).ToString();
                    property = currentType.GetProperty("Text");
                    text = property.GetValue(obj).ToString();//
                }
                else
                    text = property.GetValue(obj).ToString();
            }
            else
            {
                text = property.GetValue(obj).ToString(); //в свойстве получить значение объекта             
            }
            return text;
        }
        #endregion

        #region GetText. Получить текущий текст из TextBox, ComboBox, DateTimePicker
        /// <summary>
        /// возвращает текущий текст из comboBox, textBox, DateTimePicker
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
                text = GetText(obj, "Text");
                //currentType = obj.GetType(); //получаем тип
                //property = currentType.GetProperty("Text");//Присваиваем ему свойство Text, если это textBox. Получить свойство text из этого типа                                            
                //text = property.GetValue(obj).ToString(); //в свойстве получить значение объекта
            }

            //здесь нужно сделать если не selected item!
            else if (obj is ComboBox)
            {
                //Выберает то свойство, в котором содержиться текст
                //text = GetText(obj, "SelectedItem") ?? GetText(obj, "Text");
                //text = GetText(obj, "Text");
                text = GetText(obj, "SelectedItem");
                //currentType = obj.GetType(); //получаем тип
                //property = currentType.GetProperty("SelectedItem");//Присваиваем ему свойство SelectedItem, если это ComboBox. Получить свойство SelectedItem из этого типа                                            
                //text = property.GetValue(obj).ToString(); //в свойстве получить значение объекта
            }
            else if (obj is DateTimePicker)
            {
                text = GetTextDate(obj);
                //text = GetText(obj, "Value");
                //currentType = obj.GetType(); //получаем тип
                //property = currentType.GetProperty("Value");//Присваиваем ему свойство SelectedItem, если это ComboBox. Получить свойство SelectedItem из этого типа                                            
                //text = property.GetValue(obj).ToString(); //в свойстве получить значение объекта
            }
            else
                text = "";

            return text;
        }
        #endregion

        #region GetText overload. Получить текст из всех объектов формы
        /// <summary>
        /// возвращает текущий текст из comboBox, textBox, DateTimePicker
        /// можно передать просто textBox и comboBox или DateTimePicker, а дальше из свойства объекта он вернёт текущий текст 
        /// </summary>
        /// <param name="objects">Объект которые передаются для выбора текста из его свойства</param>
        /// <returns>Все значения объектов</returns>
        public string[] GetText(object[] objects)
        {
            Settings settings = new Settings();
            string value;
            string[] valuesObjects = new string[objects.Length];
            for (int i = 0; i < objects.Length; i++)
            {
                value = GetText(objects[i]);
                valuesObjects[i] = value;
            }
            return valuesObjects;
        }
        #endregion

        #region GetText overload. Получить текст из всех объектов формы
        /// <summary>
        /// возвращает текущий текст из ComboBox, TextBox, DateTimePicker, PictureBox
        /// можно передать просто textBox или comboBox, а дальше из свойства объекта он вернёт текущий текст 
        /// </summary>
        /// <param name="textBoxs">Все TextBox-ы формы</param>
        /// <param name="comboBoxs">Все ComboBox-ы формы</param>
        /// <param name="dateTimePickers">Все DateTimePicker-ы формы</param>
        ///  <param name="pictureBoxs">Все PictureBox-ы формы</param>
        /// <returns>Все значения объектов</returns>
        public string[] GetText(TextBox[] textBoxs = null, ComboBox[] comboBoxs = null,
            DateTimePicker[] dateTimePickers = null, PictureBox[] pictureBoxs = null)
        {
            string[] currentValue = null,
                  allValues = null;
            int sizeArray = 0,
                currentIndex = 0;
            if (textBoxs != null)
            {
                currentValue = GetTextTextBox(textBoxs);
                GetNewArray(currentValue, ref allValues, ref sizeArray, ref currentIndex);
            }
            if (comboBoxs != null)
            {
                currentValue = GetTextComboBox(comboBoxs);
                GetNewArray(currentValue, ref allValues, ref sizeArray, ref currentIndex);
            }
            if (dateTimePickers != null)
            {
                currentValue = GetTextDateTimePicker(dateTimePickers);
                GetNewArray(currentValue, ref allValues, ref sizeArray, ref currentIndex);

            }
            if (pictureBoxs != null)
            {
                currentValue = GetTextPictureBox(pictureBoxs);
                GetNewArray(currentValue, ref allValues, ref sizeArray, ref currentIndex);

            }
            return allValues;
        }
        #endregion

        #region GetText overload. Получить текст из всех объектов формы
        /// <summary>
        /// возвращает массив значений из ComboBox, TextBox, DateTimePicker, PictureBox
        /// </summary>
        /// <param name="objects">Объекты которые передаются для выбора текста из их свойств</param>
        /// <param name="textBoxs">Все TextBox-ы формы</param>
        /// <param name="comboBoxs">Все ComboBox-ы формы</param>
        /// <param name="dateTimePickers">Все DateTimePicker-ы формы</param>
        ///  <param name="pictureBoxs">Все PictureBox-ы формы</param>
        /// <returns>Все значения объектов</returns>
        public string[] GetText(object[] objects = null, TextBox[] textBoxs = null, ComboBox[] comboBoxs = null,
            DateTimePicker[] dateTimePickers = null, PictureBox[] pictureBoxs = null)
        {
            string[] currentValue = null,
                  allValues = null;
            int sizeArray = 0,
                currentIndex = 0;

            if (objects != null)
            {
                currentValue = GetText(objects);
                GetNewArray(currentValue, ref allValues, ref sizeArray, ref currentIndex);
            }
            if (textBoxs != null || comboBoxs != null || dateTimePickers != null || pictureBoxs != null)
            {
                currentValue = GetText(textBoxs, comboBoxs, dateTimePickers, pictureBoxs);
                GetNewArray(currentValue, ref allValues, ref sizeArray, ref currentIndex);
            }
            return allValues;
        }
        #endregion

        #region GetNewArray. Создание нового массива (изминение размера) и присвоение ему значений.
        /// <summary>
        /// Создание нового массива (изминение размера) и присвоение ему значений.
        /// </summary>
        /// <param name="currentArray">Текущий массив значений</param>
        /// <param name="allValues">Массив, который нужно изменить</param>
        /// <param name="sizeArray">Размер нового массива</param>
        /// <param name="currentIndex">Текущий индекс</param>
        /// <returns>Новый массив</returns>
        public string[] GetNewArray(string[] currentArray, ref string[] allValues, ref int sizeArray, ref int currentIndex)
        {
            sizeArray += currentArray.Length;
            Array.Resize(ref allValues, sizeArray);
            foreach (string i in currentArray)
                allValues[currentIndex++] = i;
            return allValues;
        }
        #endregion

    }
}