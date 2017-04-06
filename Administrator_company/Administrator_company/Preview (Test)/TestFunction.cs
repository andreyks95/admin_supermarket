using System;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Administrator_company
{
    public class TestFunction
    {
        
        public void CreatePDFDocument(DataGridView dataGridView, SaveFileDialog saveFileDialog)
        {
            #region  Создание документа
            //Создание документа
            //Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 45, 35);
            Document doc = new Document(iTextSharp.text.PageSize.A4, 15, 15, 10, 15);
            PdfWriter pdfWriter = null;
            saveFileDialog.Filter = "pdf file (*.pdf)|*.pdf";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string name = saveFileDialog.FileName;
                pdfWriter = PdfWriter.GetInstance(doc, new FileStream(name, FileMode.Create));
            }
            //задаём шрифт UNICODE
            string sylfaenpath = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\sylfaen.ttf";
            BaseFont sylfaen = BaseFont.CreateFont(sylfaenpath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font font = new Font(sylfaen, 10f, Font.NORMAL, BaseColor.BLACK);

            doc.Open();
            #endregion

            #region Добавление изображения в документ
            //создание изображения
            iTextSharp.text.Image jpgImage = iTextSharp.text.Image.GetInstance("ImageTest.jpg");
            //Изменение пропорции
            //Также можно записывать дробное значение как 10.3f
            //jpgImage.ScalePercent(10f);
            //Разместить картинку в указанных координатах  X, Y
            //jpgImage.SetAbsolutePosition(doc.PageSize.Width - 300f, doc.PageSize.Height - 150f);
            jpgImage.ScaleToFit(100f,100f);
            //Устнавоть рамки для изображения
            jpgImage.Border = iTextSharp.text.Rectangle.BOX;
            jpgImage.BorderColor = iTextSharp.text.BaseColor.YELLOW;
            jpgImage.BorderWidth = 20f;
            //Добавление изображения
            doc.Add(jpgImage);
            #endregion

            #region Добавление параграфа
            //Создать параграф
            Paragraph paragraph = new Paragraph("My first pdf document\n");
            //Добавить параграф в документ
            doc.Add(paragraph);
            #endregion
            
            #region  Добавление списка в документ 
            //Создание списка в документе
            //Используеться класс iTextSharp.text

            //Список №1
            iTextSharp.text.List list = new List(List.UNORDERED);
            //Задать отступы параграфа с левой стороны
            list.IndentationLeft = 35f;
            list.Add(new ListItem("One"));
            list.Add("Two");
            list.Add("Three");
            //Добавить список в документ
            doc.Add(list);
            
            //Список №2
            RomanList romanList = new RomanList(true, 20);
            romanList.Add(new ListItem("One"));
            romanList.Add("Two");
            romanList.Add("Three");
            //Добавить список в документ
            //doc.Add(romanList);

            //Список №3
            list = new List(List.ORDERED, 20f);
            list.SetListSymbol("\u2022");
            list.IndentationLeft = 20f;
            list.Add(new ListItem("One"));
            list.Add("Two");
            //Вложенный список
            list.Add(romanList);
            list.Add("Three");
            //Добавить список в документ
            doc.Add(list);

            #endregion

            #region  Создание таблицы
            /*//3 - это количество столбцов 
            PdfPTable table = new PdfPTable(3);

            //Вставка ячейки, создание шапки
            PdfPCell cell = new PdfPCell(
                new Phrase("Header spanning 3 columns", new Font(Font.FontFamily.HELVETICA, 8f, Font.NORMAL, BaseColor.MAGENTA)));
            cell.BackgroundColor = new BaseColor(0,150,0);
            cell.Colspan = 3; 
            cell.HorizontalAlignment = 1; //0 - left; 1 - center; 2 - right;
            table.AddCell(cell); 

            table.AddCell("Col 1 Row 1");
            table.AddCell("Col 2 Row 1");
            table.AddCell("Col 3 Row 1");
            table.AddCell("Col 1 Row 2");
            table.AddCell("Col 2 Row 2");
            table.AddCell("Col 3 Row 2");
            table.AddCell("Col 1 Row 3");
            table.AddCell("Col 2 Row 3");
            table.AddCell("Col 3 Row 3");
            doc.Add(table);*/
            #endregion

            #region Получение данных с DataGridView
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
                    if (dataGridView[j,i].Value != null)
                    {
                        table.AddCell(new Phrase(dataGridView[j,i].Value.ToString(), font));
                    }
                }
            }
            doc.Add(table);
            #endregion


            doc.Close();
           // System.Diagnostics.Process.Start("Report.pdf"); //Открыть документ 
        }

        public void AddListPDFDocument()
        {
            
        }

        
    }
}