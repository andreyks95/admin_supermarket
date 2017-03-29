using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Administrator_company.LogicProgram
{
    class Report
    {
        DocumentPDF document = new DocumentPDF();


        //Создание отчётов
        public iTextSharp.text.Document CreateReport(SaveFileDialog saveFileDialog) => DocumentPDF.CreateDocument(saveFileDialog);

        #region Настройка шрифта
        /// <summary>
        /// Настройка шрифта
        /// </summary>
        /// <param name="size">Размер шрифта</param>
        /// <param name="fontStyle">Стиль шрифта</param>
        /// <param name="color">Цвет шрифта</param>
        /// <returns>Шрифт</returns>
        public iTextSharp.text.Font SetFont(float size=0f, iTextSharp.text.Font fontStyle = null,  BaseColor color=null )
        {
            BaseFont sylfaen = document.SetBaseFont();
            Font font = null;
            if(size != 0 || color != null)
                font = new Font(sylfaen, size, Font.NORMAL, color);
            else if((size != 0 || color != null) && fontStyle != null)
                font = new Font(sylfaen, size, fontStyle.Style, color);
            else
                font = new Font(sylfaen, 12f, Font.NORMAL, BaseColor.BLACK);
            
            return font;
        }
        #endregion

        #region Создание шапки отчёта
        /// <summary>
        /// Создание шапки отчёта
        /// </summary>
        /// <param name="doc">Ссылка на pdf-документ</param>
        /// <param name="nameReport">Текст для отчёта</param>
        /// <param name="fontDoc">Шрифт</param>
        /// <returns>Изменённый документ</returns>
        public iTextSharp.text.Document CreateHeader(iTextSharp.text.Document doc, string nameReport, iTextSharp.text.Font fontDoc = null, int align = 1)
        {
            iTextSharp.text.Document newDoc = null;
            iTextSharp.text.Font font = null;
            if (font != null)
                font = fontDoc;
            else
            {
                BaseFont sylfaen = document.SetBaseFont();
                font = new Font(sylfaen, 12f, Font.NORMAL, BaseColor.BLACK);
            }
            string header = "Отчёт\n" + "\"" + nameReport + "\"" + "\n" + "Дата: " + Settings.GetDateTimeNow() + "\n";
            newDoc = document.InsertParagraph(doc, header, font, align);
            return newDoc;
        }
        #endregion
        
        //Создание параграфа
        public iTextSharp.text.Document CreateParagraph(iTextSharp.text.Document doc, string text = null,
            Font font = null, int align = 1) => document.InsertParagraph(doc, text, font, align);

        //Создание таблицы
        public iTextSharp.text.Document CreateTable(iTextSharp.text.Document doc, DataGridView dataGridView,
            Font font = null) => document.InsertTable(doc, dataGridView, font);
        
        //Создать функцию, которая будет вытаскивать результат и вставлять в строку
        //То есть из множества столбцов в одну строку, даже если они зависимые.

    }
}
