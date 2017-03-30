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
        Connection connect = new Connection();

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
        public iTextSharp.text.Font SetFont(float size=0f, /*iTextSharp.text.Font fontStyle = null,*/ int style = 0, BaseColor color=null )
        {
            BaseFont sylfaen = document.SetBaseFont();
            Font font = null;
            if(size != 0 || color != null)
                font = new Font(sylfaen, size, Font.NORMAL, color);
            else if((size != 0 || color != null) && style != 0 )//fontStyle != null)
                                                    //font = new Font(sylfaen, size, fontStyle.Style, color);
                font = new Font(sylfaen, size, style, color);
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
        /// <param name="align">Выравнивание текста</param>
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

        #region Создание футера отчёта

        #region Создание футера отчёта
        /// <summary>
        /// Создание футера отчёта
        /// </summary>
        /// <param name="doc">Ссылка на pdf-документ</param>
        /// <param name="textValues">Все значения для вставки в абзац</param>
        /// <param name="text">Текст для отчёта</param>
        /// <param name="fontDoc">Шрифт</param>
        /// <param name="align">Выравнивание текста</param>
        /// <returns>Изменённый документ</returns>
        public iTextSharp.text.Document CreateFooter(iTextSharp.text.Document doc, string[] textValues = null, string text = null, 
            iTextSharp.text.Font fontDoc = null, int align = 1)
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
            string footer = "\nИтоги";
            
            newDoc = document.InsertParagraph(doc, footer, font, 1);
            if (text != null)
                newDoc = document.InsertParagraph(newDoc, text + "\n", font, align);
            else
            {
                foreach (var str in textValues)
                {
                    newDoc = document.InsertParagraph(newDoc, str + "\n", font, align);
                }
            }
            
            return newDoc;
        }
        #endregion

        #region Создание футера отчёта
        /// <summary>
        /// Создание футера отчёта
        /// </summary>
        /// <param name="doc">Ссылка на pdf-документ</param>
        /// <param name="text">Текст для отчёта</param>
        /// <param name="fontDoc">Шрифт</param>
        /// <param name="align">Выравнивание текста</param>
        /// <param name="side">Отступ 0 - слева, 2 - справа</param>
        /// <param name="indent">Еденицы отступа</param>
        /// <returns>Изменённый документ</returns>
        public iTextSharp.text.Document CreateFooter(iTextSharp.text.Document doc, string[] textValues = null, string text = null,
            iTextSharp.text.Font fontDoc = null, int align = 1, int side = 0, float indent = 10f)
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
            string footer = "\nИтоги";

            newDoc = document.InsertParagraph(doc, footer, font, 1);
            if (text != null)
                newDoc = document.InsertParagraph(newDoc, text + "\n", font, align, side, indent);
            else
            {
                foreach (var str in textValues)
                {
                    newDoc = document.InsertParagraph(newDoc, str + "\n", font, align, side, indent);
                }
            }
            return newDoc;
        }
        #endregion

        #endregion

        //Создание параграфа
        public iTextSharp.text.Document CreateParagraph(iTextSharp.text.Document doc, string text = null,
            Font font = null, int align = 1) => document.InsertParagraph(doc, text, font, align);

        //Создание таблицы
        public iTextSharp.text.Document CreateTable(iTextSharp.text.Document doc, DataGridView dataGridView,
            Font font = null) => document.InsertTable(doc, dataGridView, font);

        
    }
}
