using System;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Administrator_company.LogicProgram
{
    class DocumentPDF
    {
        #region Создание документа
        /// <summary>
        /// Создание документа
        /// </summary>
        /// <param name="saveFileDialog">Путь и имя файла для хранения</param>
        /// <returns>Созданный документ</returns>
        public static iTextSharp.text.Document CreateDocument(SaveFileDialog saveFileDialog)
        {
            //Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 45, 35);
            Document doc = new Document(iTextSharp.text.PageSize.A4, 15, 15, 10, 15);
            PdfWriter pdfWriter = null;
            saveFileDialog.Filter = "pdf file (*.pdf)|*.pdf";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string name = saveFileDialog.FileName;
                pdfWriter = PdfWriter.GetInstance(doc, new FileStream(name, FileMode.Create));
            }
            //doc.Open();
            return doc;
        }
        #endregion

        #region Установка шрифта с кодировкой Unicode
        public BaseFont SetBaseFont()
        {
        //задаём шрифт UNICODE
        string sylfaenpath = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\sylfaen.ttf";
        BaseFont sylfaen = BaseFont.CreateFont(sylfaenpath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
        //Font font = new Font(sylfaen, 12f, Font.NORMAL, BaseColor.BLACK);
        return sylfaen;
        }
        #endregion

        #region Работа с текстом

        #region Добавление строки
        /// <summary>
        /// Добавление строки в документ
        /// </summary>
        /// <param name="doc">Текущий документ</param>
        /// <param name="text">Текст строки</param>
        /// <param name="font">Шрифт</param>
        /// <returns>Изменённый документ</returns>
        public iTextSharp.text.Document InsertChunk(iTextSharp.text.Document doc, string text, Font font = null)
        {
            //Создать строку
            Chunk chunk = new Chunk(text);
            if (font != null)
                chunk.Font = font;
            //Добавить строку в документ
            doc.Add(chunk);
            return doc;
        }
        #endregion

        #region Добавление фразы
        /// <summary>
        /// Добавление фразы и выравнивание по центру
        /// </summary>
        /// <param name="doc">Текущий документ</param>
        /// <param name="text">Текст фразы</param>
        /// <param name="font">Шрифт</param>
        /// <returns>Изменённый документ</returns>
        public iTextSharp.text.Document InsertPhrase(iTextSharp.text.Document doc, string text, Font font = null)
        {
            //Создать фразу
            Phrase phrase = new Phrase(text);
            if(font != null)
                phrase.Font = font;
            //Добавить фразу в документ
            doc.Add(phrase);
            return doc;
        }
        #endregion

        #region Добавление параграфа

        /// <summary>
        /// Добавление параграфа и выравнивание по центру
        /// </summary>
        /// <param name="doc">Текущий документ</param>
        /// <param name="text">Текст параграфа</param>
        /// <param name="font">Информация о шрифте</param>
        /// <returns>Изменённый документ</returns>
        public iTextSharp.text.Document InsertParagraph(iTextSharp.text.Document doc, string text=null, Font font=null)
        {
            //Font verdanaFont = FontFactory.GetFont("Verdana", 7f, Font.BOLD);
            //Создать параграф
            Paragraph paragraph = null;
            if (text != null)
               paragraph = new Paragraph(text, font) {Alignment = Element.ALIGN_CENTER};
            else 
             paragraph  = new Paragraph("\n") { Alignment = Element.ALIGN_CENTER };
            if(font!=null)
                paragraph.Font = font;
            //Добавить параграф в документ
            doc.Add(paragraph);
            return doc;
        }
        #endregion

        #endregion

        #region Работа с изображениями
        /// <summary>
        /// Вставка изображения в текст
        /// </summary>
        /// <param name="doc">Текущий документ</param>
        /// <param name="image">Путь к изображению</param>
        /// <param name="sizeX">Размер изображение по Х</param>
        /// <param name="sizeY">Размер изображение по У</param>
        /// <param name="scalePercent">Установить размер изображения в % от первоначального</param>
        /// <returns>Изменённый документ</returns>
        public iTextSharp.text.Document InsertImage(iTextSharp.text.Document doc, string image, float sizeX = 1, float sizeY = 1, float scalePercent = 1)
        {
            //создание изображения
            iTextSharp.text.Image jpgImage = iTextSharp.text.Image.GetInstance(image);
            if(sizeX > 1 && sizeY >1)
                jpgImage.ScaleToFit(sizeX, sizeY);
            else
                jpgImage.ScaleToFit(100f, 100f);
            if(scalePercent > 1)
                jpgImage.ScalePercent(scalePercent);
            //Изменение пропорции
            //Также можно записывать дробное значение как 10.3f
            //jpgImage.ScalePercent(10f);
            //Разместить картинку в указанных координатах  X, Y
            //jpgImage.SetAbsolutePosition(doc.PageSize.Width - 300f, doc.PageSize.Height - 150f);
            //Устнавоть рамки для изображения
            //jpgImage.Border = iTextSharp.text.Rectangle.BOX;
            //jpgImage.BorderColor = iTextSharp.text.BaseColor.YELLOW;
            //jpgImage.BorderWidth = 20f;
            //Добавление изображения
            doc.Add(jpgImage);
            return doc;
        }
        #endregion

        #region Создание таблицы. Получение данных с DataGridView
        /// <summary>
        /// Создание таблицы. Получение данных с DataGridView
        /// </summary>
        /// <param name="doc">Текущий документ</param>
        /// <param name="dataGridView">DataGridView формы с которого нужно вытащить данные</param>
        /// <param name="font">Шрифт</param>
        /// <returns>Изменённый документ</returns>
        public iTextSharp.text.Document InsertTable(iTextSharp.text.Document doc, DataGridView dataGridView, Font font = null)
        {
            PdfPTable table = new PdfPTable(dataGridView.Columns.Count);
            //Создание шапки таблицы
            for (int i = 0; i < dataGridView.ColumnCount; i++)
                table.AddCell(new Phrase(dataGridView.Columns[i].HeaderText, font));
            //Флаг первая строка как шапка
            table.HeaderRows = 1;
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView.Columns.Count; j++)
                {
                    if (dataGridView[j, i].Value != null)
                    {
                        table.AddCell(new Phrase(dataGridView[j, i].Value.ToString(), font));
                    }
                }
            }
            doc.Add(table);
            return doc;
        }

        #endregion
    }
}
