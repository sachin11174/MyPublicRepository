using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace WindowsPDFSplitter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string pdfFilePath = @"C:\Users\aks75987\Desktop\BGC Docs\HSC\SchoolCertificate.pdf";
            string outputPath = @"C:\Users\aks75987\Desktop\BGC Docs\HSC\SplittedFiles";
            int interval = 1;
            int pageNameSuffix = 0;

            // Intialize a new PdfReader instance with the contents of the source Pdf file:  
            PdfReader reader = new PdfReader(pdfFilePath);

            FileInfo file = new FileInfo(pdfFilePath);
            string pdfFileName = file.Name.Substring(0, file.Name.LastIndexOf(".")) + "-";

            for (int pageNumber = 1; pageNumber <= reader.NumberOfPages; pageNumber += interval)
            {
                pageNameSuffix++;
                string newPdfFileName = string.Format(pdfFileName + "{0}", pageNameSuffix);
                SplitAndSaveInterval(pdfFilePath, outputPath, pageNumber, interval, newPdfFileName);
            }
        }


        private void SplitAndSaveInterval(string pdfFilePath, string outputPath, int startPage, int interval, string pdfFileName)
        {
            using (PdfReader reader = new PdfReader(pdfFilePath))
            {
                Document document = new Document();
                PdfCopy copy = new PdfCopy(document, new FileStream(outputPath + "\\" + pdfFileName + ".pdf", FileMode.Create));
                document.Open();

                for (int pagenumber = startPage; pagenumber < (startPage + interval); pagenumber++)
                {
                    if (reader.NumberOfPages >= pagenumber)
                    {
                        copy.AddPage(copy.GetImportedPage(reader, pagenumber));
                    }
                    else
                    {
                        break;
                    }
                }
                document.Close();
            }
        }
    }
}
