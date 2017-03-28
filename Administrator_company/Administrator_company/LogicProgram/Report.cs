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

        public iTextSharp.text.Document CreateReport(SaveFileDialog saveFileDialog) => DocumentPDF.CreateDocument(saveFileDialog);

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

        public iTextSharp.text.Document CreateHeader(iTextSharp.text.Document doc, string nameReport, iTextSharp.text.Font fontDoc = null)
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
            newDoc = document.InsertParagraph(doc, header, font);
            return newDoc;
        }

        public iTextSharp.text.Document CreateTable(iTextSharp.text.Document doc, DataGridView dataGridView,
            Font font = null) => document.InsertTable(doc, dataGridView, font);
        
        //Создать функцию, которая будет вытаскивать результат и вставлять в строку
        //То есть из множества столбцов в одну строку, даже если они зависимые.

    }
}
