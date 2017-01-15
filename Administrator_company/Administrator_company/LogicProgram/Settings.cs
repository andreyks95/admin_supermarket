using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Administrator_supermarket
{
    public class Settings
    {
        public void GetSettingDisplayTable(DataGridView dataGridView, int height)
        {
            dataGridView.RowTemplate.Height = height; //высота строк
            dataGridView.AllowUserToAddRows = false; //нельзя пользователю добавлять самому строки
            //Как будет отображаться таблица
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //Растягивать таблицу (колонки) под окно dataGridView
        }

        public void GetViewImageInCellTable(DataGridView dataGridView, int numberColumn)
        {
            //Для отображения картинки в DataGridView
            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            imgCol = (DataGridViewImageColumn) dataGridView.Columns[numberColumn];
            //номер ячейки, где будет отоброжаться изображение
            imgCol.ImageLayout = DataGridViewImageCellLayout.Stretch; //делает картинку пропорционально ячейке 
        }

        public void GetAllViewImagesInCellTable(DataGridView dataGridView, int[] numberColumns)
        {
            foreach (var i in numberColumns)
            {
                //Для отображения картинки в DataGridView
                DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
                imgCol = (DataGridViewImageColumn) dataGridView.Columns[i];
                //номер ячейки, где будет отоброжаться изображение
                imgCol.ImageLayout = DataGridViewImageCellLayout.Stretch; //делает картинку пропорционально ячейке 
            }
        }

        //для выбора изображения 
        public void GetChooseImage(PictureBox pictureBox )
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Choose Image(*.JPG; *.PNG, *.GIF) | *.jpg; *.png; *.gif";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                pictureBox.Image = Image.FromFile(openFile.FileName);
                // если пользователь выбрал картинку то вставляем в pictureBox

            }
        }


        /// <summary>
        /// Отображает из текущей ячейки строки DataGridView картинку в PicureBox
        /// </summary>
        /// <param name="number">номер столбца (ячейки) DataGridView в котором содержиться картинка</param>
        /// <param name="pictureBox">pictureBox куда нужно вставить картинку</param>
        /// <param name="dataGridView">текущий dataGridView</param>
        public void CurrentRowCellsIMG(int number, PictureBox pictureBox, DataGridView dataGridView)
        {
            //Если ячейка пустая, то в pictureBox ничего не отображать
            if (dataGridView.CurrentRow.Cells[number].Value == DBNull.Value)
            {
                pictureBox.Image = null;
            }
            else
            {
                try
                {
                    Byte[] img = (Byte[])dataGridView.CurrentRow.Cells[number].Value; //Получаем изображание
                    MemoryStream ms = new MemoryStream(img);
                    pictureBox.Image = Image.FromStream(ms); // вставляем в pictureBox это изображение
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        /// <summary>
        /// Помещает в textBox значение с ячейки текущей строки DataGridView
        /// </summary>
        /// <param name="number">Номер столбца (ячейки) DataGridView в которой содержиться текстовое значение</param>
        /// <param name="textBox">textBox куда нужно вставить значение</param>
        /// <param name="dataGridView">текущий dataGridView</param>
        public void CurrentRowCellsTEXT(int number, TextBox textBox, DataGridView dataGridView)
        {
            textBox.Text = dataGridView.CurrentRow.Cells[number].Value.ToString();
        }

        /// <summary>
        /// Помещает во ВСЕ textBox-ы значения из текущей строки DataGridView
        /// </summary>
        /// <param name="textBoxs">Массив textBox-ов куда нужно вставить значения</param>
        /// <param name="dataGridView">текущий dataGridView</param>
        public void CurrentRowCellsTEXT(TextBox[] textBoxs, DataGridView dataGridView)
        {

            for (var i = 0; i < textBoxs.Length; i++)
            {
                //передаёт номер ячейки и сам textBox куда нужно вставить значения из строки DataGridView
                CurrentRowCellsTEXT(i, textBoxs[i], dataGridView);
            }
        }


    }
}