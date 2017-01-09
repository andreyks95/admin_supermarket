using System.Windows.Forms;

namespace Administrator_supermarket
{
    public class ViewProgram
    {
        public void GetSettingDisplayTable(DataGridView dataGridView, int height)
        {
            dataGridView.RowTemplate.Height = height; //высота строк
            dataGridView.AllowUserToAddRows = false; //нельзя пользователю добавлять самому строки
            //Как будет отображаться таблица
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; //Растягивать таблицу (колонки) под окно dataGridView
        }

        public void GetViewImagesInCellTable(DataGridView dataGridView, int numberColumn)
        {
            //Для отображения картинки в DataGridView
            DataGridViewImageColumn imgCol = new DataGridViewImageColumn();
            imgCol = (DataGridViewImageColumn)dataGridView.Columns[numberColumn]; //номер ячейки, где будет отоброжаться изображение
            imgCol.ImageLayout = DataGridViewImageCellLayout.Stretch; //делает картинку пропорционально ячейке 
        }
    }
}