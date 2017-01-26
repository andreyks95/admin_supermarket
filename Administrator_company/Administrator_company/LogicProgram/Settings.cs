using System;
using System.CodeDom;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Administrator_supermarket
{
    public class Settings
    {

        #region GetSettingDisplayTable
        /// <summary>
        /// Настройки отображения DataGridView (таблицы)
        /// </summary>
        /// <param name="dataGridView">текущая на форме таблица (DataGridView) таблица</param>
        /// <param name="height">Высота строк</param>
        public void GetSettingDisplayTable(DataGridView dataGridView, int height)
        {
            dataGridView.RowTemplate.Height = height; //высота строк
            dataGridView.AllowUserToAddRows = false; //нельзя пользователю добавлять самому строки
            //Как будет отображаться таблица
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //Растягивать таблицу (колонки) под окно dataGridView
        }
        #endregion

        #region GetViewImageInCellTable

        #region GetViewImageInCellTable. Вставляет в ячейку (столбец) изображение из БД
        /// <summary>
        /// Вставляет в ячейку (столбец) с указанным номером изображение из БД
        /// </summary>
        /// <param name="dataGridView">DataGridView (таблица) куда нужно вставить</param>
        /// <param name="numberColumn">Номер ячейки (столбца)</param>
        public void GetViewImageInCellTable(DataGridView dataGridView, int numberColumn)
        {
            //Для отображения картинки в DataGridView
            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            imgCol = (DataGridViewImageColumn) dataGridView.Columns[numberColumn];
            imgCol.ImageLayout = DataGridViewImageCellLayout.Stretch;
            
            //номер ячейки, где будет отоброжаться изображение    

        }
        #endregion

        #region GetViewImageInCellTable overload. Вставляет в ячейки (столбцы) таблицы изображения из БД
        /// <summary>
        /// Вставляет в ячейки (столбцов) с указанными номерами изображения из БД
        /// </summary>
        /// <param name="dataGridView">DataGridView таблицы</param>
        /// <param name="numberColumns">Массив номеров ячеек (столбцов)</param>
        public void GetViewImageInCellTable(DataGridView dataGridView, int[] numberColumns)
        {
            for(int i =0; i < numberColumns.Length; i++)
                GetViewImageInCellTable(dataGridView, numberColumns[i]);
        }
        #endregion

        #endregion

        #region GetChooseImage

        #region GetChooseImage. Выбрать изображение для PictureBox
        /// <summary>
        /// Для выбора изображения из файла и вставки в pictureBox на форме
        /// </summary>
        /// <param name="pictureBox">Текущий PictureBox куда нужно вставить изображение</param>
        public void GetChooseImage(PictureBox pictureBox )
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Choose Image(*.JPG; *.PNG, *.GIF) | *.jpg; *.png; *.gif";

            // если пользователь выбрал картинку то вставляем в pictureBox
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                pictureBox.Image = Image.FromFile(openFile.FileName);
            }

        }
        #endregion

        #region GetChooseImage overload. Выбрать изображения для всех PictureBox-ов
        /// <summary>
        /// Для выбора изображений из файлов и вставки во все pictureBox которые есть на форме
        /// </summary>
        /// <param name="pictureBox">Массив pictureBox где нужно отобразить изображение</param>
        public void GetChooseImage(PictureBox[] pictureBox)
        {
            //выбрать изображения для pictureBox
            for(int i=0; i < pictureBox.Length; i++)
                GetChooseImage(pictureBox[i]);
        }
        #endregion

        #endregion

        #region CurrentColumnCellsIMG

        #region CurrentColumnCellsIMG. Вставляет в PictureBox изображение из ячейки
        /// <summary>
        /// Отображает из текущей ячейки строки DataGridView картинку в PicureBox
        /// </summary>
        /// <param name="number">номер столбца (ячейки) DataGridView в котором содержиться картинка</param>
        /// <param name="pictureBox">pictureBox куда нужно вставить картинку</param>
        /// <param name="dataGridView">текущий dataGridView</param>
        public void CurrentColumnCellsIMG(int number, PictureBox pictureBox, DataGridView dataGridView)
        {
            try
            {
                //Если ячейка пустая, то в pictureBox ничего не отображать
                if (dataGridView.CurrentRow.Cells[number].Value == DBNull.Value)
                {
                    pictureBox.Image = null;
                }
                else
                {
                    Byte[] img = (Byte[]) dataGridView.CurrentRow.Cells[number].Value; //Получаем изображание
                    MemoryStream ms = new MemoryStream(img);
                    pictureBox.Image = Image.FromStream(ms); // вставляем в pictureBox это изображение
                }
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message);
            }

        }
        #endregion

        #region CurrentColumnCellsIMG overload. Вставляет в pictureBox-ы изображения из ячеек
        /// <summary>
        /// Отображает все картинки в pictureBox-ы из ячеек таблицы в которых есть изображения 
        /// </summary>
        /// <param name="pictureBoxs">Все PictureBoxs на форме куда нужно вставить картинки</param>
        /// <param name="dataGridView">Текущий DataGridView где содержаться картинки</param>
        public void CurrentColumnCellsIMG(PictureBox[] pictureBoxs, DataGridView dataGridView)
        {
            //Вставляем во все PictureBox-ы, которые есть изображения из ячеек таблицы DataGridView
            for(int i =0; i < pictureBoxs.Length; i++)
                CurrentColumnCellsIMG(i, pictureBoxs[i], dataGridView);
        }
        #endregion

        #region CurrentColumnCellsIMG overload. Вставляет в pictureBox-ы изображения из ячеек
        /// <summary>
        /// Отображает все картинки в pictureBox-ы из ячеек таблицы в которых есть изображения
        /// Если ячейки имеют разный порядковый номер в таблице 
        /// </summary>
        /// <param name="numbers">Массив номеров ячеек, которые содержат изображение</param>
        /// <param name="pictureBoxs">Все PictureBoxs на форме куда нужно вставить картинки</param>
        /// <param name="dataGridView">Текущий DataGridView где содержаться картинки</param>
        public void CurrentColumnCellsIMG(int[] numbers, PictureBox[] pictureBoxs, DataGridView dataGridView)
        {
            //Вставляем во все PictureBox-ы, которые есть изображения из ячеек таблицы DataGridView
            for (int i = 0; i < pictureBoxs.Length; i++)
                CurrentColumnCellsIMG(numbers[i], pictureBoxs[i], dataGridView);
        }
        #endregion

        #endregion

        #region CurrentColumnCellsText

        #region CurrentColumnCellsText. Вставка текста в textBox из ячейки таблицы 
        /// <summary>
        /// Помещает в textBox значение с ячейки текущей строки DataGridView
        /// </summary>
        /// <param name="number">Номер столбца (ячейки) DataGridView в которой содержиться текстовое значение</param>
        /// <param name="textBox">textBox куда нужно вставить значение</param>
        /// <param name="dataGridView">текущий dataGridView</param>
        public void CurrentColumnCellsTEXT(int number, TextBox textBox, DataGridView dataGridView)
        {
            try
            {
                if (number != 0 || number != null)
                    textBox.Text = dataGridView.CurrentRow.Cells[number].Value.ToString();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

        }
        #endregion

        #region CurrentColumnCellsText overload. Вставка текста в textBox-ы из ячеек таблицы 
        /// <summary>
        /// Помещает во ВСЕ textBox-ы значения из текущей строки DataGridView
        /// </summary>
        /// <param name="textBoxs">Массив textBox-ов куда нужно вставить значения</param>
        /// <param name="dataGridView">текущий dataGridView</param>
        public void CurrentColumnCellsTEXT(TextBox[] textBoxs, DataGridView dataGridView)
        {

            for (var i = 0; i < textBoxs.Length; i++)
            {
                //передаёт номер ячейки и сам textBox куда нужно вставить значения из строки DataGridView
                CurrentColumnCellsTEXT(i, textBoxs[i], dataGridView);
            }
        }
        #endregion

        #region CurrentColumnCellsText overload. Вставка текста в textBox-ы из ячеек таблицы в которых нумерация не попорядку
        /// <summary>
        /// Помещает во ВСЕ textBox-ы значения из текущей строки DataGridView
        /// Для случая, если текстовые ячейки имеют разные номера (не попорядку)
        /// </summary>
        /// <param name="numbers">массив содержащий номера ячеек с текстовым содержанием</param>
        /// <param name="textBoxs">Массив textBox-ов куда нужно вставить значения</param>
        /// <param name="dataGridView">текущий dataGridView</param>
        public void CurrentColumnCellsTEXT(int[] numbers, TextBox[] textBoxs, DataGridView dataGridView)
        {

            for (var i = 0; i < textBoxs.Length; i++)
            {
                //передаёт номер ячейки и сам textBox куда нужно вставить значения из строки DataGridView
                CurrentColumnCellsTEXT(numbers[i], textBoxs[i], dataGridView);
            }
        }
        #endregion

        #endregion

        #region SaveImagesToBytes. Сохранить изображение в byte[] из pictureBox 
        /// <summary>
        /// Сохраняет Изображение которое есть в pictureBox в массив byte
        /// </summary>
        /// <param name="pictureBox">PictureBox с которого нужно выбрать изображение</param>
        /// <returns>массив byte[]</returns>
        public byte[] SaveImagesToBytes(PictureBox pictureBox)
        {
            MemoryStream ms = new MemoryStream();
            NullReferenceException nullReference = new NullReferenceException();
            byte[] img;
            if (pictureBox.Image != null)
            {
                pictureBox.Image.Save(ms, pictureBox.Image.RawFormat);
                img = ms.ToArray();
                return img;
            }
            else
            {
                return img = null;
            }
        }
        #endregion

        #region InsertTextInTextBoxFromTable

        #region InsertTextInTextBoxFromTable. Вставить текст из таблицы в TextBox
        /// <summary>
        /// Вставляет текущий найденный текст из таблицы в TextBox
        /// </summary>
        /// <param name="table">Текущая таблица</param>
        /// <param name="Column">Номер строки где находятся текст</param>
        /// <param name="textBox">TextBox формы куда нужно вставить текст из таблицы</param>
        public void InsertTextInTextBoxFromTable(DataTable table, int Column, TextBox textBox)
        {
            textBox.Text = table.Rows[0][Column].ToString();
        }
        #endregion

        #region InsertTextInTextBoxFromTable overload. Вставить текст из таблицы в TextBox-ы
        /// <summary>
        /// Вставляет текущий найденный текст из таблицы в TextBox-ы
        /// </summary>
        /// <param name="table">Текущая таблица</param>
        /// <param name="Columns">Номера строк где находятся текст</param>
        /// <param name="textBoxs">Все TextBox формы куда нужно вставить текст из таблицы</param>
        public void InsertTextInTextBoxFromTable(DataTable table, int[] Columns, params TextBox[] textBoxs)
        {
            if (textBoxs != null)
                for (int i = 0; i < textBoxs.Length; i++)
                    InsertTextInTextBoxFromTable(table, Columns[i], textBoxs[i]);
        }
        #endregion

        #endregion

        #region InsertTextInComboBoxFromTable

        #region InsertTextInComboBoxFromTable. Вставить текст из таблицы в ComboBox
        /// <summary>
        /// Вставляет текущий найденный текст из таблицы в ComboBox
        /// </summary>
        /// <param name="table">Текущая таблица</param>
        /// <param name="Column">Номер строки где находятся текст</param>
        /// <param name="comboBox">ComboBox формы куда нужно вставить текст из таблицы</param>
        public void InsertTextInComboBoxFromTable(DataTable table, int Column, ComboBox comboBox)
        {
                comboBox.SelectedItem = table.Rows[0][Column].ToString();
        }
        #endregion

        #region InsertTextInComboBoxFromTable overload. Вставить текст из таблицы в ComboBox-ы
        /// <summary>
        /// Вставляет текущий найденный текст из таблицы в ComboBox-ы
        /// </summary>
        /// <param name="table">Текущая таблица</param>
        /// <param name="Columns">Номера строк где находятся текст</param>
        /// <param name="comboBoxs">Все ComboBox формы куда нужно вставить текст из таблицы</param>
        public void InsertTextInComboBoxFromTable(DataTable table, int[] Columns, params ComboBox[] comboBoxs)
        {
            if(comboBoxs !=  null)
                for (int i = 0; i < comboBoxs.Length; i++)
                    InsertTextInComboBoxFromTable(table, Columns[i], comboBoxs[i]);
        }
        #endregion

        #endregion

        #region InsertImageInPictureBoxFromTable

        #region InsertImageInPictureBoxFromTable overload. Вставить картинку из таблицы в PictureBox
        /// <summary>
        /// Вставляет текущее найденное изображение из таблицы в PictureBox
        /// </summary>
        /// <param name="table">Текущая таблица</param>
        /// <param name="Column">Номер строки где находяться изображения</param>
        /// <param name="pictureBox">PictureBox формы куда нужно вставить изображение из таблицы</param>
        public void InsertImageInPictureBoxFromTable(DataTable table, int Column, PictureBox pictureBox)
        {
            if (!DBNull.Value.Equals(table.Rows[0][Column]))
            {
                byte[] img = (byte[]) table.Rows[0][Column];
                MemoryStream ms = new MemoryStream(img);
                pictureBox.Image = Image.FromStream(ms);
            }
            else
                pictureBox.Image = null;

        }
        #endregion

        #region InsertImageInPictureBoxFromTable overload. Вставить картинки из таблицы в PictureBox-ы
        /// <summary>
        /// Вставляет текущие найденные изображения из таблицы в PictureBox-ы
        /// </summary>
        /// <param name="table">Текущая таблица</param>
        /// <param name="Columns">Номера строк где находяться изображения</param>
        /// <param name="pictureBoxs">Все PictureBox формы куда нужно вставить изображения из таблицы</param>
        public void InsertImageInPictureBoxFromTable(DataTable table, int[] Columns, params PictureBox[] pictureBoxs)
        {
            if (pictureBoxs != null)
                for (int i = 0; i < pictureBoxs.Length; i++)
                InsertImageInPictureBoxFromTable(table, Columns[i], pictureBoxs[i]);
        }
        #endregion

        #endregion

        #region ClearFields. Очистка TextBox, ComboBox, PictureBox таблицы
        /// <summary>
        /// Очищает все TextBox-ы, ComboBox-ы, PictureBox-ы текущей формы
        /// </summary>
        /// <param name="textBoxs">Все TextBox-ы на форме</param>
        /// <param name="comboBoxs">Все ComboBox-ы на форме</param>
        /// <param name="pictureBoxs">Все PictureBox-ы на форме</param>
        public void ClearFields(TextBox[] textBoxs=null, ComboBox[] comboBoxs=null, PictureBox[] pictureBoxs=null)
        {
            //для textBox-ов
            if(textBoxs != null)
                foreach (var i in textBoxs)
                    i.Text = "";

            //для comboBox-ов
            if(comboBoxs != null)
                foreach (var i in comboBoxs)
                    i.SelectedItem = "";
                    
            //для pictureBox-ов
            if(pictureBoxs != null)
                foreach (var i in pictureBoxs)
                    i.Image = null;
        }
        #endregion
    }
}