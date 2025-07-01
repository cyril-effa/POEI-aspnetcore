using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System.IO;

namespace ex10bis.Infrastructure.Services
{
    public class PDFService
    {
        public static void GeneratePDF(string filepath, string[] content)
        {
            // Create a new PDF document
            var document = new PdfDocument();

            // Create an empty page
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);

            // Set font
            var font = new XFont("Verdana", 14, XFontStyle.Bold);

            // Draw the content
            for (int i = 0; i < content.Length; i++)
            {
                gfx.DrawString(content[i], font, XBrushes.Black, new XRect(0, i * 16, page.Width, page.Height), XStringFormats.TopLeft);
            }

            document.Save(filepath);
        }
    }
}
