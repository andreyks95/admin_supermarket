using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Administrator_company
{
    public class TestFunction
    {

        public void CreatePDFDocument()
        {
            //Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 45, 35);
            Document doc = new Document(iTextSharp.text.PageSize.A4, 15, 15, 10, 15);
            PdfWriter pdfWriter = PdfWriter.GetInstance(doc, new FileStream("Report.pdf", FileMode.Create));
            doc.Open();
            //Создать параграф
            Paragraph paragraph = new Paragraph("My first pdf document\n");
            //Добавить параграф в документ
            doc.Add(paragraph);
            doc.Close();
        }
        
    }
}