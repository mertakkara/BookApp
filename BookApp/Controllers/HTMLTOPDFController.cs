using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BookApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HTMLTOPDFController : ControllerBase
    {
        private readonly IConverter _converter;
        public HTMLTOPDFController(IConverter converter)
        {
            _converter = converter;
        }
        [HttpPost("create-pdf")]
        public IActionResult CreatePdf()
        {
            var templateContent = @"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Offer Letter</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 20px;
            background-color: #f4f4f4;
        }
        .container {
            background-color: #fff;
            padding: 20px;
            margin: 0 auto;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            max-width: 800px;
        }
        .header, .footer {
            text-align: center;
        }
        .header h1, .footer p {
            margin: 0;
        }
        .content {
            margin: 20px 0;
        }
        .content p {
            line-height: 1.6;
        }
        .details {
            margin: 20px 0;
        }
        .details td {
            padding: 5px 0;
        }
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""header"">
            <h1>Offer Letter</h1>
        </div>
        <div class=""content"">
            <p>Dear Mert,</p>
            <p>We are pleased to offer you the position of Software Developer at X Corp.</p>
            <div class=""details"">
                <table>
                    <tr>
                        <td><strong>Start Date:</strong></td>
                        <td>August 1, 2024</td>
                    </tr>
                    <tr>
                        <td><strong>Benefits:</strong></td>
                        <td>Health, Dental, 401(k)</td>
                    </tr>
                </table>
            </div>
            <p>We look forward to working with you and are confident that you will make significant contributions to our company.</p>
            <p>Please sign and return this letter by July 20, 2024, to confirm your acceptance of this offer.</p>
            <p>Sincerely,</p>
            <p>HR Manager<br>
               X Corp</p>
        </div>
        <div class=""footer"">
            <p>&copy; 2024 X Corp. All rights reserved.</p>
        </div>
    </div>
</body>
</html>
";

            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10, Bottom = 10, Left = 10, Right = 10 }
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = templateContent,
                WebSettings = { DefaultEncoding = "utf-8" },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = "Report Footer" }
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            byte[] pdfBytes = _converter.Convert(pdf);
            return File(pdfBytes, "application/pdf", "ConvertedDocument.pdf");
        }
    }
}
